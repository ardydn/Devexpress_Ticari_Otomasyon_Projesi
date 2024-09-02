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
    public partial class frmUrunler : Form
    {
        public frmUrunler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl=new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void listele()
        {
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt=new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da = new SqlDataAdapter("select * from TblUrunler",bgl.baglanti()); //Ürünler tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık.
        }

        void temizle()
        {
            //Veri girdiğimiz alanı temizleme metodu.
            txtAd.Text = "";
            txtAlisfiyat.Text = "";
            txtId.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            txtSatisfiyat.Text = "";
            mskYil.Text = "";
            nudAdet.Value = 0;
            rchDetay.Text = "";
        }

        private void frmUrunler_Load(object sender, EventArgs e)
        {
            listele(); //Listele metodumuzu çağırdık.
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri kaydetme.
            SqlCommand komut=new SqlCommand("insert into TblUrunler(AD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", mskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisfiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchDetay.Text);
            komut.ExecuteNonQuery();//DML komutlarını gerçekleştir yani sorguyu çalıştır.
            bgl.baglanti().Close();//bağlantıyı kapattık.
            MessageBox.Show("Ürün sisteme eklendi.", "BİLGİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information); //Bilgi kutusu
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Ürünlerin gridden araçlara taşınması.
            //Formumuzun özelliklerinden events(olaylar) kısmından focusedrowchanged seçeneğine çift tıkladık.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);//veri satırı sınıfından dr ısmınde bir nesne türettik ve bu dr komutuna bir görev ataması yaptık(satırın verisini al).
            txtId.Text = dr["ID"].ToString();
            txtAd.Text = dr["AD"].ToString();
            txtMarka.Text = dr["MARKA"].ToString();
            txtModel.Text = dr["MODEL"].ToString();
            mskYil.Text = dr["YIL"].ToString();
            nudAdet.Value = decimal.Parse(dr["ADET"].ToString());
            txtAlisfiyat.Text = dr["ALISFIYAT"].ToString();
            txtSatisfiyat.Text = dr["SATISFIYAT"].ToString();
            rchDetay.Text = dr["DETAY"].ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Verileri silme.
            SqlCommand komut = new SqlCommand("delete from TblUrunler where ID=@p1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarını gerçekleştir yani sorguyu çalıştır.
            bgl.baglanti().Close(); //bağlantıyı kapattık.
            MessageBox.Show("Ürün sistemden silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); //Bilgi kutusu
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri güncelleme.
            SqlCommand komut =new SqlCommand("update TblUrunler set AD=@p1,MARKA=@p2,MODEL=@p3,YIL=@p4,ADET=@p5,ALISFIYAT=@p6,SATISFIYAT=@p7,DETAY=@p8 where ID=@p9",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", mskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisfiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchDetay.Text);
            komut.Parameters.AddWithValue("@P9",txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün sistemde güncellendi.", "BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }
    }
}
