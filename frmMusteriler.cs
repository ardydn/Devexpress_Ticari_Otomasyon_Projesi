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
    public partial class frmMusteriler : Form
    {
        public frmMusteriler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void listele() 
        {
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt =new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da = new SqlDataAdapter("select * from TblMusteriler", bgl.baglanti()); //Müşteriler tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık.
        }  
        
        void temizle()
        {
            //Veri girdiğimiz alanı temizleme metodu.
            txtAd.Text = "";
            txtId.Text = "";
            txtMail.Text = "";
            txtSoyad.Text = "";
            txtVergidairesi.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            mskTc.Text = "";
            mskTelefon1.Text = "";
            mskTelefon2.Text = "";
            rchAdres.Text = "";
        }

        void sehirlistele()
        {
            //Şehirler tablomuzu comboboxa çagırma metodu.
            SqlCommand komut = new SqlCommand("select SEHIR from TblIller", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void frmMusteriler_Load(object sender, EventArgs e)
        {
            listele(); //Listele metodumuzu çağırdık.
            sehirlistele(); //Şehirler metodumuzu çağırdık.
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //İller aracına çift tıkladık.
            //İller aracımızda herhangi bir değişiklik olduğunda ilçeler aracımızda o ile ait ilçeler listelenecek.
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ILCE from TblIlceler where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex+1);
            SqlDataReader dr=komut.ExecuteReader();
            while(dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri kaydetme.
            SqlCommand komut = new SqlCommand("insert into TblMusteriler (AD,SOYAD,TELEFON1,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4",mskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5",mskTc.Text);  
            komut.Parameters.AddWithValue("@p6",txtMail.Text);
            komut.Parameters.AddWithValue("@p7",cmbIl.Text);
            komut.Parameters.AddWithValue("@p8",cmbIlce.Text);
            komut.Parameters.AddWithValue("@p9",rchAdres.Text);
            komut.Parameters.AddWithValue("@p10",txtVergidairesi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Müşterilerin gridden araçlara taşınması.
            //Formumuzun özelliklerinden events(olaylar) kısmından focusedrowchanged seçeneğine çift tıkladık.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle); //Veri satırı sınıfından dr ısmınde bir nesne türettik ve bu dr komutuna bir görev ataması yaptık(satırın verisini al).
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                mskTelefon1.Text = dr["TELEFON1"].ToString();
                mskTelefon2.Text = dr["TELEFON2"].ToString();
                mskTc.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtVergidairesi.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri silme.
            SqlCommand komut = new SqlCommand("delete from TblMusteriler where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarını gerçekleştir yani sorguyu çalıştır.
            bgl.baglanti().Close(); //bağlantıyı kapattık.
            MessageBox.Show("Müşteri sistemden silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri güncelleme.
            SqlCommand komut = new SqlCommand("update TblMusteriler set AD=@p1,SOYAD=@p2,TELEFON1=@p3,TELEFON2=@p4,TC=@p5,MAIL=@p6,IL=@P7,ILCE=@p8,ADRES=@p9,VERGIDAIRE=@p10 where ID=@p11", bgl.baglanti());
            //Bilgi; update dml komutlarinda where yazmazsak geri donusu olmayan bir sekilde tablonun hepsi guncellenir. 
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", mskTc.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", cmbIl.Text);
            komut.Parameters.AddWithValue("@p8", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p9", rchAdres.Text);
            komut.Parameters.AddWithValue("@p10", txtVergidairesi.Text);
            komut.Parameters.AddWithValue("@p11", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sistemde güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

    }
}
