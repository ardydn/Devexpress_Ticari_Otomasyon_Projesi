using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class frmNotdetay : Form
    {
        public frmNotdetay()
        {
            InitializeComponent();
        }

        public string metin; //Değişken oluşturduk.

        private void frmNotdetay_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = metin; //Araca değişkeni atadık.
        }
    }
}
