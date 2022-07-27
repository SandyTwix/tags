using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll
{
    public class Solver
    {
        private Board initial;
        private List<Board> result = new List<Board>();



        private static int measure(ITEM item)
        {
            ITEM item2 = item;
            int c = 0;
            int measure = item.getBoard().GetH();
            while (true)
            {
                c++;
                item2 = item2.prevBoard;
                if (item2 == null)
                {
                    return measure + c;
                }
            }
        }

        private void itemToList(ITEM item)
        {
            ITEM item2 = item;
            while (true)
            {
                item2 = item2.prevBoard;
                if (item2 == null)
                {
                    result.Reverse();
                    return;
                }
                result.Add(item2.board);
            }
        }

        private bool ContainsInPath(ITEM item, Board board)
        {
            ITEM item2 = item;
            while (true)
            {
                if (item2.board.Equals(board)) return true;
                item2 = item2.prevBoard;
                if (item2 == null) return false;
            }
        }
        public bool IsSolvable()
        {
            return true;
        }

        public int moves()
        {
            if (!IsSolvable()) return -1;
            return result.Count - 1;
        }

        
        public List<Board> solution()
        {
            return result;
        }
        public Solver(Board initial)
        {
            this.initial = initial;

            if (!IsSolvable()) return; 
            
            BinaryHeap<ITEM> priorityQueue = new BinaryHeap<ITEM>();
            
            ITEM item = new ITEM(null, initial);
            priorityQueue.Add(measure(item), item);

            while (true)
            {
                ITEM board = priorityQueue.GetMin();
                
                if (board.board.isGoal())
                {
                    itemToList(new ITEM(board, board.board));
                    return;
                }
                
                List<Board> iterator = board.board.neighbors(); 
                int i = 0;
                while (iterator.Count > i)
                {
                    Board board1 = iterator[i];
                    i++;
                    
                    if (board1 != null && !ContainsInPath(board, board1))
                    {
                        ITEM a = new ITEM(board, board1);
                        priorityQueue.Add(measure(a), a);
                    }
                }

            }
        }

    }
    public class ITEM
    {
        public ITEM prevBoard; 
        public Board board;  

        public ITEM(ITEM prevBoard, Board board)
        {
            this.prevBoard = prevBoard;
            this.board = board;
        }

        public Board getBoard()
        {
            return board;
        }


    }
}
