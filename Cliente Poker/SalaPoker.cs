using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente_Poker
{
    public partial class SalaPoker : Form
    {
        public SalaPoker()
        {
            InitializeComponent();
        }

        private void SalaPoker_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(1280, 720);
        }
    }
}
