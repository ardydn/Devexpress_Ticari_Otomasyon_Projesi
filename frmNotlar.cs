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
    public partial class frmNotlar : Form
    {
        public frmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl=new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void listele()
        {
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt =new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da=new SqlDataAdapter("select * from TblNotlar", bgl.baglanti()); //Notlar tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık.
        }

        void temizle()
        {
            //Veri girdiğimiz alanı temizleme metodu.
            txtBaslik.Text = "";
            txtHitap.Text = "";
            txtId.Text = "";
            txtOlusturan.Text = "";
            mskSaat.Text = "";
            mskTarih.Text = "";
            rchDetay.Text = "";
        }

        private void frmNotlar_Load(object sender, EventArgs e)
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
            SqlCommand komut =new SqlCommand("insert into TblNotlar(TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) values (@p1,@p2,@p3,@p4,@p5,@p6)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@p4", rchDetay.Text);
            komut.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@p6", txtHitap.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not sisteme kaydedildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Notların gridden araçlara taşınması.
            //Formumuzun özelliklerinden events(olaylar) kısmından focusedrowchanged seçeneğine çift tıkladık.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle); //Veri satırı sınıfından dr ısmınde bir nesne türettik ve bu dr komutuna bir görev ataması yaptık(satırın verisini al).
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                mskSaat.Text = dr["SAAT"].ToString();
                txtBaslik.Text = dr["BASLIK"].ToString();
                rchDetay.Text = dr["DETAY"].ToString();
                txtOlusturan.Text = dr["OLUSTURAN"].ToString();
                txtHitap.Text = dr["HITAP"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri silme.
            SqlCommand komut = new SqlCommand("delete from TblNotlar where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarını gerçekleştir yani sorguyu çalıştır.
            bgl.baglanti().Close(); //Bağlantıyı kapattık.
            MessageBox.Show("Not sistemden silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri güncelleme.
            SqlCommand komut = new SqlCommand("update TblNotlar set TARIH=@p1,SAAT=@p2,BASLIK=@p3,DETAY=@p4,OLUSTURAN=@p5,HITAP=@p6 where ID=@p7", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@p4", rchDetay.Text);
            komut.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@p6", txtHitap.Text);
            komut.Parameters.AddWithValue("@p7", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not sisteme güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Notlar formundan -> Notdetay formuna geçiş
            //Formlar arasi bilgi taşıma işlemi
            //Bu formun datagridview - özellikler - olaylar - doubleclick kısmına çift tıklayıp kodlar kısmına geldik.
            frmNotdetay frm=new frmNotdetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                frm.metin = dr["DETAY"].ToString(); //frmNotdetay formunda oluşturduğumuz değişkeninin değerini bu formdan gönderiyoruz.
            }
            frm.Show();
        }
    }
}
