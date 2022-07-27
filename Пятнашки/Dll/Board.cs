using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll
{
    public class Board
    {
        private int[,] blocks;
        private int zeroX;
        private int zeroY;
        private int h;
        static Random rnd = new Random();

        public int dimension()
        {
            return blocks.GetLength(0);
        }

        public int GetH()
        {
            return h;
        }

        public bool isGoal()
        { 
            return h == 0;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;


            Board board = (Board)o;

            if (board.dimension() != dimension()) return false;
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    if (blocks[i, j] != board.blocks[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static int[,] deepCopy(int[,] original)
        {
            if (original == null)
            {
                return null;
            }

            int[,] result = new int[original.GetLength(0), original.GetLength(1)];
            for (int i = 0; i < original.GetLength(0); i++)
            {

                for (int j = 0; j < original.GetLength(1); j++)
                {
                    result[i, j] = original[i, j];
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(0); j++)
                {
                    s.Append(blocks[i, j]);
                    s.Append("\t");
                }
                s.Append("\n");

            }
            return s.ToString();
        }

        private int[,] getNewBlock()
        {
            return deepCopy(blocks);
        }

        private Board chng(int[,] blocks2, int x1, int y1, int x2, int y2)
        { 

            if (x2 > -1 && x2 < dimension() && y2 > -1 && y2 < dimension())
            {
                int t = blocks2[x2, y2];
                blocks2[x2, y2] = blocks2[x1, y1];
                blocks2[x1, y1] = t;
                return new Board(blocks2);
            }
            else
                return null;

        }

        void Prepare()
        {
            h = 0;
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    if (blocks[i, j] != (i * dimension() + j + 1) && blocks[i, j] != 0)
                    { 
                        h += 1;
                    }
                    if (blocks[i, j] == 0)
                    {
                        zeroX = (int)i;
                        zeroY = (int)j;
                    }
                }
            }

        }

        public Board(int[,] blocks)
        {
            int[,] blocks2 = deepCopy(blocks);
            this.blocks = blocks2;
            Prepare();
            
        }

        public List<Board> neighbors()
        {
            List<Board> boardList = new List<Board>();
            boardList.Add(chng(getNewBlock(), zeroX, zeroY, zeroX, zeroY + 1));
            boardList.Add(chng(getNewBlock(), zeroX, zeroY, zeroX, zeroY - 1));
            boardList.Add(chng(getNewBlock(), zeroX, zeroY, zeroX - 1, zeroY));
            boardList.Add(chng(getNewBlock(), zeroX, zeroY, zeroX + 1, zeroY));

            return boardList;
        }

        public void Start()
        {
            for (int x = 0; x < dimension(); x++)
                for (int y = 0; y < dimension(); y++)
                    blocks[x, y] = CoordsToPosition(x, y) + 1;
            zeroX = dimension()- 1;
            zeroY = dimension() - 1;
            blocks[zeroX, zeroY] = 0;
        }
        public int GetNum(int pos)
        {
            int x, y;
            PositionToCoords(pos, out x, out y);
            if (x < 0 || x >= dimension()) return 0;
            if (y < 0 || y >= dimension()) return 0;
            return blocks[x, y];
        }
        public bool CheckNum()
        {
            if (!(zeroX == dimension() - 1 && zeroY == dimension() - 1))
                return false;
            for (int x = 0; x < dimension(); x++)
                for (int y = 0; y < dimension(); y++)
                    if (!(x == dimension() - 1 && y == dimension() - 1))
                        if (blocks[x, y] != CoordsToPosition(x, y) + 1)
                            return false;
            return true;
        }
        public void Shift(int pos)
        {
            int x, y;
            PositionToCoords(pos, out x, out y);
            if (Math.Abs(zeroX - x) + Math.Abs(zeroY - y) != 1)
                return;
            blocks[zeroX, zeroY] = blocks[x, y];
            blocks[x, y] = 0;
            Prepare();
        }
        private int CoordsToPosition(int x, int y)
        {
            if (x < 0) x = 0;
            if (x > dimension() - 1) x = dimension() - 1;
            if (y < 0) y = 0;
            if (y > dimension() - 1) y = dimension() - 1;
            return x * dimension() + y;
        }
        private void PositionToCoords(int pos, out int x, out int y)
        {
            if (pos < 0) pos = 0;
            if (pos > dimension() * dimension() - 1) pos = dimension() * dimension() - 1;
            y = pos % dimension();
            x = pos / dimension();
        }
        public void ShiftRandom()
        {
            int a = rnd.Next(0, 4);
            int x = zeroX;
            int y = zeroY;
            switch (a)
            {
                case 0: x--; break;
                case 1: x++; break;
                case 2: y--; break;
                case 3: y++; break;

            }

            Shift(CoordsToPosition(x, y));
        }
    }
}
