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
using DevExpress.XtraLayout.Resizing;

namespace TicariOtomasyon
{
    public partial class frmFaturalar : Form
    {
        public frmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void listele()
        {
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt = new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da = new SqlDataAdapter("select * from TblFaturabilgi", bgl.baglanti()); //Faturalar tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık.
        }

        void temizle()
        {
            //Veri girdiğimiz alanı temizleme metodu.
            txtAlici.Text = "";
            txtFirma.Text = "";
            txtFiyat.Text = "";
            txtId.Text = "";
            txtMiktar.Text = "";
            txtSeri.Text = "";
            txtSira.Text = "";
            txtTeslimalan.Text = "";
            txtTeslimeden.Text = "";
            txtTutar.Text = "";
            txtUrunadi.Text = "";
            txtUrunid.Text = "";
            txtVergi.Text = "";
            mskSaat.Text = "";
            mskTarih.Text = "";
        }

        private void frmFaturalar_Load(object sender, EventArgs e)
        {
            listele(); //Listele metodumuzu çağırdık.
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri kaydetme.
            if (txtFaturaid.Text == "") //Boş ise
            {
                SqlCommand komut=new SqlCommand("insert into TblFaturabilgi (SERINO,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtSeri.Text);
                komut.Parameters.AddWithValue("@p2", txtSira.Text);
                komut.Parameters.AddWithValue("@p3", mskTarih.Text);
                komut.Parameters.AddWithValue("@p4", mskSaat.Text);
                komut.Parameters.AddWithValue("@p5", txtVergi.Text);
                komut.Parameters.AddWithValue("@p6", txtAlici.Text);
                komut.Parameters.AddWithValue("@p7", txtTeslimeden.Text);
                komut.Parameters.AddWithValue("@p8", txtTeslimalan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura sisteme kaydedildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele(); //Listele metodumuzu çağırdık.
            }
            //Firma carisi
            if (txtFaturaid.Text != "" && comboBox1.Text == "Firma") //Boş değil ise
            {
                double miktar, tutar, fiyat;
                fiyat=Convert.ToDouble(txtFiyat.Text);
                miktar=Convert.ToDouble(txtMiktar.Text);
                tutar=miktar * fiyat;
                txtTutar.Text=tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TblFaturadetay(URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtUrunadi.Text);
                komut2.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", txtFaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TblFirmahareketler(URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", txtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", txtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", txtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", txtFirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", txtFaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", mskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();
            }

            //Müşteri carisi
            //Girdiğimiz yeni verileri kaydetme.
            
            if (txtFaturaid.Text != "") //Boş değil ise
            {
                double miktar, fiyat, tutar;
                miktar = Convert.ToDouble(txtMiktar.Text);
                fiyat = Convert.ToDouble(txtFiyat.Text);
                tutar = miktar * fiyat;
                txtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TblFaturadetay(URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtUrunadi.Text);
                komut2.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", txtFaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TblMusterihareketler(URUNID,ADET,PERSONEL,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", txtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", txtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", txtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", txtFirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", txtFaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", mskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Faturaların gridden araçlara taşınması.
            //Formumuzun özelliklerinden events(olaylar) kısmından focusedrowchanged seçeneğine çift tıkladık.
            DataRow dr =gridView1.GetDataRow(gridView1.FocusedRowHandle); //Veri satırı sınıfından dr ısmınde bir nesne türettik ve bu dr komutuna bir görev ataması yaptık(satırın verisini al).
            if (dr != null)
            {
                txtId.Text = dr["FATURABILGIID"].ToString();
                txtSeri.Text = dr["SERINO"].ToString();
                txtSira.Text = dr["SIRANO"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                mskSaat.Text = dr["SAAT"].ToString();
                txtVergi.Text = dr["VERGIDAIRE"].ToString();
                txtAlici.Text = dr["ALICI"].ToString();
                txtTeslimeden.Text = dr["TESLIMEDEN"].ToString();
                txtTeslimalan.Text = dr["TESLIMALAN"].ToString();
            }
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri silme.
            SqlCommand komut = new SqlCommand("delete from TblFaturabilgi where FATURABILGIID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarını gerçekleştir yani sorguyu çalıştır.
            bgl.baglanti().Close(); //Bağlantıyı kapattık.
            MessageBox.Show("Fatura sistemden silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri güncelleme.
            SqlCommand komut = new SqlCommand("update TblFaturabilgi set SERINO=@p1,SIRANO=@p2,TARIH=@p3,SAAT=@p4,VERGIDAIRE=@p5,ALICI=@p6,TESLIMEDEN=@p7,TESLIMALAN=@p8 where FATURABILGIID=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtSeri.Text);
            komut.Parameters.AddWithValue("@p2", txtSira.Text);
            komut.Parameters.AddWithValue("@p3", mskTarih.Text);
            komut.Parameters.AddWithValue("@p4", mskSaat.Text);
            komut.Parameters.AddWithValue("@p5", txtVergi.Text);
            komut.Parameters.AddWithValue("@p6", txtAlici.Text);
            komut.Parameters.AddWithValue("@p7", txtTeslimeden.Text);
            komut.Parameters.AddWithValue("@p8", txtTeslimalan.Text);
            komut.Parameters.AddWithValue("@p9", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura sistemde güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Faturlar formundan -> faturadetay formuna geçiş
            //Formlar arasi bilgi taşıma işlemi
            //Bu formun datagridview - özellikler - olaylar - doubleclick kısmına çift tıklayıp kodlar kısmına geldik.
            frmFaturaurundetay frm = new frmFaturaurundetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                frm.id = dr["FATURABILGIID"].ToString(); //Faturadetay formunda oluşturduğumuz değişkeninin değerini bu formdan gönderiyoruz.
                frm.Show();
            }
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut= new SqlCommand("select AD,SATISFIYAT from TblUrunler where ID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtUrunid.Text);
            SqlDataReader dr= komut.ExecuteReader();
            while (dr.Read())
            {
                txtUrunadi.Text = dr[0].ToString();
                txtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
