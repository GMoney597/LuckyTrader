using LuckyTrader_CltForm.LTService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckyTrader_CltForm
{
    public partial class Form1 : Form
    {
        List<Stock> stockData = new List<Stock>();

        public Form1()
        {
            InitializeComponent();
        }

        private void cmdGetTable_Click(object sender, EventArgs e)
        {
            LuckyTraderClient ltc = new LuckyTraderClient();
            stockData = ltc.CheckStock().ToList<Stock>();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
