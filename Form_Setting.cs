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
            NumericUpDown_Width.Value = Convert.ToDecimal(mineArea.RowCount);
            NumericUpDown_Height.Value = Convert.ToDecimal(mineArea.ColumeCount);
            NumericUpDown_Mine.Value = Convert.ToDecimal(mineArea.MineCount);
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            mineArea.Set(Convert.ToInt32(NumericUpDown_Width.Value), Convert.ToInt32(NumericUpDown_Height.Value), Convert.ToInt32(NumericUpDown_Mine.Value));
            Close();
        }
    }
}
