using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //sql kütüphanemizi açıyoruz.

namespace TicariOtomasyon
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        private void button1_MouseHover(object sender, EventArgs e)
        {
            btnGiris.BackColor = Color.White; //Buton üzerine geldiğinde renk değiştir.
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            btnGiris.BackColor= Color.AliceBlue; //Buton üzerinden çıktığında renk değiştir.
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut= new SqlCommand("select * from TblAdmin where KullaniciAd=@p1 and Sifre=@p2",bgl.baglanti()); //Tablodaki bütün değerleri oku ama KullaniciAd ve Sifre'ye eşit olanlar
            komut.Parameters.AddWithValue("@p1", txtKullaniciad.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader(); //dr isminde sqldatareader oluşturduk.
            if ( dr.Read()) //Eğer dr okunursa
            {
                AnaModul frm = new AnaModul(); //ana modulu frmye ata
                frm.kullanici = txtKullaniciad.Text; //!
                frm.Show(); //formu aç
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre.","HATA!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }
    }
}
