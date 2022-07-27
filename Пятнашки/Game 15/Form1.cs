using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dll;

namespace Game_15
{
    public partial class Form1 : Form
    {
        Board Game;
        int[,] solved = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 },{ 13, 14, 15, 0 } };
        public Form1()
        {
            InitializeComponent();
            Game = new Board(solved);

        }

        private Button button(int pos)
        {
            switch (pos)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                default: return null;
            }
        }

        private void StartGame()
        {
            Game.Start();
            for (int j = 0; j < 100; j++)
                Game.ShiftRandom();
            path = null;
            Refresh();
        }

        private void Refresh()
        {
            if (path != null)
                Game = path[ind];
            for (int pos = 0; pos < 16; pos++)
            {
                int num = Game.GetNum(pos);
                button(pos).Text = num.ToString();
                button(pos).Visible = (num > 0);
            }
            if (path != null)
            {
                CurrentStep_Label.Text = $"Шаг {ind} из {path.Count - 1}";
                CurrentStep_Label.Visible = true;
                Back_Button.Visible = true;
                Forward_Button.Visible = true;
                
            }
            else
            {
                CurrentStep_Label.Visible = false;
                Back_Button.Visible = false;
                Forward_Button.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartGame();
        }
        
        int ind;
        List<Board> path=null;

        private void Back_Click(object sender, EventArgs e)
        {
            if (ind == 0)
                return;
            ind--;
            Refresh();
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            if (ind == path.Count-1)
                return;
            ind++;
            Refresh();
        }

        private void Form_Button_Click(object sender, EventArgs e)
        {
            Game = new Board(solved);
            path = null;
            Refresh();
        }

        private void Reform_button_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void Solve_Button_Click(object sender, EventArgs e)
        {
            Solver Game15 = new Solver(Game);
            path = Game15.solution();
            ind = 0;
            Refresh();
        }

        private void gridbutton_Click(object sender, EventArgs e)
        {
            int pos = Convert.ToInt16(((Button)sender).Tag);
            Game.Shift(pos);
            path = null;
            Refresh();
        }
    }
}
