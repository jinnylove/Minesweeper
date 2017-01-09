using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form_Rank : Form
    {
        public Form_Rank()
        {
            InitializeComponent();
        }

        private void Form_Rank_Load(object sender, EventArgs e)
        {
            Label_Beginner.Text = Properties.Settings.Default.Beginner.ToString();
            Label_Intermediate.Text = Properties.Settings.Default.Intermediate.ToString();
            Label_Expert.Text = Properties.Settings.Default.Expert.ToString();
        }

        private void Button_Reset_Click(object sender, EventArgs e)
        {
            Label_Beginner.Text = 0.ToString();
            Label_Intermediate.Text = 0.ToString();
            Label_Expert.Text = 0.ToString();
            Properties.Settings.Default.Beginner = 0;
            Properties.Settings.Default.Intermediate = 0;
            Properties.Settings.Default.Expert = 0;
            Properties.Settings.Default.Save();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
