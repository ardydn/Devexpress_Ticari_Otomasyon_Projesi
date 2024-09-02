using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net; //Burayı ekledik.
using System.Net.Mail; //Burayı ekledik.

namespace TicariOtomasyon
{
    public partial class frmMail : Form
    {
        public frmMail()
        {
            InitializeComponent();
        }

        public string mail; //Değişken oluşturduk.

        private void frmMail_Load(object sender, EventArgs e)
        {
            txtMailadresi.Text = mail; //Araca değişkeni atadık.
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            //...
            MailMessage mesajim= new MailMessage();
            SmtpClient istemci= new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("MAIL", "SIFRE");
            istemci.Port = 587;
            istemci.Host = "smtp.live.com";
            istemci.EnableSsl = true;
            mesajim.To.Add(rchMesaj.Text);
            mesajim.From = new MailAddress("Mail");
            mesajim.Subject = txtKonu.Text;
            mesajim.Body = rchMesaj.Text;
            istemci.Send(mesajim);
        }
    }
}
