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
using DevExpress.Charts; //Kütühane ekledik.

namespace TicariOtomasyon
{
    public partial class frmKasa : Form
    {
        public frmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl=new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void MusteriHareket()
        {
            //SQL veri tabanında oluşturduğumuz prosedürü formda listeleme metodu.
            DataTable dt = new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da=new SqlDataAdapter("execute MusteriHareketler",bgl.baglanti()); //MusteriHareketler prosedür içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz
            gridControl1.DataSource = dt; //Araca yazdırdık.
        }

        void FirmaHareket()
        {
            //SQL veri tabanında oluşturduğumuz prosedürü formda listeleme metodu.
            DataTable dt2 = new DataTable(); //Datatable sınıfından dt2 isminde bir nesne türettik.
            SqlDataAdapter da2 = new SqlDataAdapter("execute FirmaHareketler", bgl.baglanti()); //FirmaHareketler prosedür içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da2.Fill(dt2); //Dataadapterın içini datatable ile dolduruyoruz
            gridControl3.DataSource = dt2; //Araca yazdırdık.
        }

        void Giderler()
        {
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt3 = new DataTable(); //Datatable sınıfından dt3 isminde bir nesne türettik.
            SqlDataAdapter da3 = new SqlDataAdapter("select * from TblGiderler", bgl.baglanti()); //Giderler tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da3.Fill(dt3); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl2.DataSource=dt3; //Araca yazdırdık.
        }

        public string ad; //!

        private void frmKasa_Load(object sender, EventArgs e)
        {
            lblAktifkullanici.Text = ad;//!

            MusteriHareket(); //MusteriHareket metodumuzu çağırdık.
            FirmaHareket(); //FirmaHareket metodumuzu çağırdık.
            Giderler(); //Giderler metodumuzu çağırdık.

            //Toplam tutarı hesaplama
            SqlCommand komut=new SqlCommand("select sum(tutar) from TblFaturadetay",bgl.baglanti());
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                lblToplamtutar.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Son ayın faturaları
            SqlCommand komut2 = new SqlCommand("select(ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TblGiderler order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString();
            }
            bgl.baglanti().Close();

            //Son ayın personler maaşları
            SqlCommand komut3 = new SqlCommand("select MAAS from TblGiderler", bgl.baglanti());
            SqlDataReader dr3=komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelmaas.Text = dr3[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam müşteri sayısı
            SqlCommand komut4 = new SqlCommand("select count(*) from TblGiderler", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusterisayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam firma sayısı
            SqlCommand komut5 = new SqlCommand("select count(*) from TblFirmalar", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmasayısı.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam firma şehir sayısı
            SqlCommand komut6 = new SqlCommand("select count(distinct(IL)) from TblFirmalar", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblFirmasehirsayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam müşteri şehir sayısı
            SqlCommand komut7 = new SqlCommand("select count(distinct(IL)) from TblMusteriler", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblMusterisehirsayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam personel sayısı
            SqlCommand komut8 = new SqlCommand("select count(*) from TblPersoneller", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblPersonelsayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam ürüm sayısı
            SqlCommand komut9 = new SqlCommand("select sum(ADET) from TblUrunler", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                lblStoksayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();
        }

        int sayac = 0; //Sayac isminde bir int değeri türettik ve sıfırı bu değere atadık.

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++; //Sayac int değerini timer1 butonuna atadık ve birer birer artmasını istedik.(artma hızı timer1 aracı özelliklerinden ayarladık.)

            //1. chart controle elektrik faturası son 4 ay listeleme
            if (sayac > 0 && sayac <= 5) //sayac bu değerler arasında ise;
            {
                groupControl10.Text = "ELEKTRİK"; //Aracın ismini değiştir.
                chartControl1.Series["AYLAR"].Points.Clear(); //Chartı temizle.
                SqlCommand komut10 = new SqlCommand("select top 4 AY,ELEKTRIK from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            //1. chart controle su faturası son 4 ay listeleme.
            if (sayac>5 && sayac <= 10) //Sayac bu değerler arasında ise;
            {
                groupControl10.Text = "SU"; //Aracın ismini değiştir.
                chartControl1.Series["AYLAR"].Points.Clear(); //Chartı temizle.
                SqlCommand komut11 = new SqlCommand("select top 4 AY,SU from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //1. chart controle doğalgaz faturası son 4 ay listeleme.
            if (sayac > 10 && sayac <= 15) //Sayac bu değerler arasında ise;
            {
                groupControl10.Text = "DOGALGAZ"; //Aracın ismini değiştir.
                chartControl1.Series["AYLAR"].Points.Clear(); //Chartı temizle.
                SqlCommand komut11 = new SqlCommand("select top 4 AY,DOGALGAZ from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //1. chart controle internet faturası son 4 ay listeleme.
            if (sayac > 15 && sayac <= 20) //Sayac bu değerler arasında ise;
            {
                groupControl10.Text = "INTERNET"; //Aracın ismini değiştir.
                chartControl1.Series["AYLAR"].Points.Clear(); //Chartı temizle.
                SqlCommand komut11 = new SqlCommand("select top 4 AY,INTERNET from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //1. chart controle ekstra faturası son 4 ay listeleme.
            if (sayac > 20 && sayac <= 25) //Sayac bu değerler arasında ise;
            {
                groupControl10.Text = "EKSTRA"; //Aracın ismini değiştir.
                chartControl1.Series["AYLAR"].Points.Clear(); //Chartı temizle.
                SqlCommand komut11 = new SqlCommand("select top 4 AY,EKSTRA from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac == 26) //Bu değere ulaştığında;
            {
                sayac = 0; //Sayacı sıfırla.
            }
        }
        int sayac2; //sayac2 isminde bir int değeri türettik ve sıfırı bu değere atadık.
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++; //sayac2 int değerini timer2 butonuna atadık ve birer birer artmasını istedik.(artma hızı timer2 aracı özelliklerinden ayarladık.)

            //2. chart controle elektrik faturası son 4 ay listeleme
            if (sayac2 > 0 && sayac2 <= 5) //Sayac2 bu değerler arasında ise;
            {
                groupControl11.Text = "ELEKTRİK"; //Aracın ismini değiştir.
                chartControl2.Series["AYLAR"].Points.Clear(); //2.Chartı temizle.
                SqlCommand komut10 = new SqlCommand("select top 4 AY,ELEKTRIK from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            //2. chart controle su faturası son 4 ay listeleme.
            if (sayac2 > 5 && sayac2 <= 10) //Sayac2 bu değerler arasında ise;
            {
                groupControl11.Text = "SU"; //Aracın ismini değiştir.
                chartControl2.Series["AYLAR"].Points.Clear(); //2.Chartı temizle.
                SqlCommand komut11 = new SqlCommand("select top 4 AY,SU from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //2. chart controle doğalgaz faturası son 4 ay listeleme.
            if (sayac2> 10 && sayac2 <= 15) //Sayac2 bu değerler arasında ise;
            {
                groupControl11.Text = "DOGALGAZ"; //Aracın ismini değiştir.
                chartControl2.Series["AYLAR"].Points.Clear(); //2.Chartı temizle.
                SqlCommand komut11 = new SqlCommand("select top 4 AY,DOGALGAZ from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //2. chart controle internet faturası son 4 ay listeleme.
            if (sayac2 > 15 && sayac2 <= 20) //Sayac2 bu değerler arasında ise;
            {
                groupControl11.Text = "INTERNET"; //Aracın ismini değiştir.
                chartControl2.Series["AYLAR"].Points.Clear(); //2.Chartı temizle.
                SqlCommand komut11 = new SqlCommand("select top 4 AY,INTERNET from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //2. chart controle ekstra faturası son 4 ay listeleme.
            if (sayac2 > 20 && sayac2 <= 25) //Sayac2bu değerler arasında ise;
            {
                groupControl11.Text = "EKSTRA"; //Aracın ismini değiştir.
                chartControl2.Series["AYLAR"].Points.Clear(); //2.Chartı temizle.
                SqlCommand komut11 = new SqlCommand("select top 4 AY,EKSTRA from TblGiderler order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26) //Bu değere ulaştığında;
            {
                sayac2 = 0; //Sayacı sıfırla.
            }
        }
    }
}
