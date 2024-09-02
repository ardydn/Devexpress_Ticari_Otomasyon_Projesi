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
    public partial class frmAyarlar : Form
    {
        public frmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void listele()
        {
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt = new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da = new SqlDataAdapter("select * from TblAdmin", bgl.baglanti()); //Admin tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık.
        }
        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            listele(); //Listele metodumuzu çağırdık.
            txtKullaniciad.Text = " "; //Veri girdiğimiz alanı temizleme.
            txtSifre.Text = " "; //Veri girdiğimiz alanı temizleme.
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri kaydetme.
            if (btnKaydet.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into TblAdmin values (@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtKullaniciad.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni admin sisteme kaydedildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele(); //Listele metodumuzu çağırdık.
            }
            if (btnKaydet.Text == "Güncelle")  //Girdiğimiz yeni verileri güncelleme.
            {
                SqlCommand komut1 = new SqlCommand("update TblAdmin set Sifre=@p2 where KullaniciAd=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", txtKullaniciad.Text);
                komut1.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt sistemde güncelledi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele(); //Listele metodumuzu çağırdık.
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Admin tablosunun gridden araçlara taşınması.
            //Formumuzun özelliklerinden events(olaylar) kısmından focusedrowchanged seçeneğine çift tıkladık.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle); //Veri satırı sınıfından dr isminde bir nesne türettik ve bu dr komutuna bir görev ataması yaptık(satırın verisini al).
            if (dr != null)
            {
                txtKullaniciad.Text =dr["KullaniciAd"].ToString();
                txtSifre.Text =dr["Sifre"].ToString();
            }
        }

        private void txtKullaniciad_TextChanged(object sender, EventArgs e)
        {
            //textbox'a çift tıkladık herhangi bir değişiklikte olacaklar.
            if(txtKullaniciad.Text != "")
            {
                btnKaydet.Text = "Güncelle";
            }
            else
            {
                btnKaydet.Text = "Kaydet";
            }
        }

    }
}
