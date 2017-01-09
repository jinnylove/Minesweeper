using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form_Setting : Form
    {
        public Form_Setting()
        {
            InitializeComponent();
        }

        private void Form_Setting_Load(object sender, EventArgs e)
        {
            NumericUpDown_Row.Value = Properties.Settings.Default.RowCount;
            NumericUpDown_Colume.Value = Properties.Settings.Default.ColumeCount;
            NumericUpDown_Mine.Value = Properties.Settings.Default.MineCount;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RowCount = Convert.ToInt32(NumericUpDown_Row.Value);
            Properties.Settings.Default.ColumeCount = Convert.ToInt32(NumericUpDown_Colume.Value);
            Properties.Settings.Default.MineCount = Convert.ToInt32(NumericUpDown_Mine.Value);
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
