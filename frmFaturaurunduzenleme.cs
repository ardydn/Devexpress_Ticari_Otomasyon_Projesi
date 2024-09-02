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
using DevExpress.XtraBars;

namespace TicariOtomasyon
{
    public partial class frmFaturaurunduzenleme : Form
    {
        public frmFaturaurunduzenleme()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        public string urundid; //Değişken oluşturduk.

        private void frmFaturaurunduzenleme_Load(object sender, EventArgs e)
        {
            txtUrunid.Text = urundid; //Araca değişkeni atadık.
            //Fatura detayları forma çağırma.
            SqlCommand komut = new SqlCommand("select * from TblFaturadetay where FATURAURUNID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtUrunid.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtFiyat.Text = dr[3].ToString();
                txtMiktar.Text = dr[2].ToString();
                txtTutar.Text = dr[4].ToString();
                txtUrunadi.Text = dr[1].ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri güncelleme.
            SqlCommand komut = new SqlCommand("update TblFaturadetay set URUNAD=@p1, MIKTAR=@p2,FIYAT=@p3,TUTAR=@p4 where FATURAURUNID=@p5", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtUrunadi.Text);
            komut.Parameters.AddWithValue("@p2", txtMiktar.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
            komut.Parameters.AddWithValue("@p5", txtUrunid.Text);
            komut.ExecuteNonQuery(); //DML komutlarını gerçekleştir yani sorguyu çalıştır.
            bgl.baglanti().Close(); //bağlantıyı kapattık. 
            MessageBox.Show("Fatura sistemde guncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information); //Bilgi kutusu
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Verileri silme.
            SqlCommand komut = new SqlCommand("delete from TblFaturadetay where FATURAURUNID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtUrunid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura sistemde silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
