using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid;
using static DevExpress.XtraBars.Docking2010.Views.BaseRegistrator;
using System.Xml;

namespace TicariOtomasyon
{
    public partial class frmAnasayfa : Form
    {
        public frmAnasayfa()
        {
            InitializeComponent();
        }

       sqlbaglantisi bgl=new sqlbaglantisi();

        void stoklar()
        {
            DataTable dt= new DataTable();  
            SqlDataAdapter da=new SqlDataAdapter("select AD, sum(ADET) as 'Adet' from TblUrunler group by ad having sum(ADET)<=20 order by sum(ADET)",bgl.baglanti());
            da.Fill(dt);
            gridControlstoklar.DataSource=dt;
        }

        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select top 8 TARIH, SAAT, BASLIK from TblNotlar order by ID desc", bgl.baglanti());
            da.Fill(dt);
            gridControlajanda.DataSource = dt;
        }
        
        void hareketler()
        {
            //Sql veri tabanına prosedür oluşturuyoruz.
            //            create procedure FirmaHareketler2
            //as
            //select TblUrunler.AD, TblFirmahareketler.ADET, TblFirmalar.AD, FIYAT, TOPLAM
            //from TblFirmahareketler
            //inner join
            //TblUrunler
            //on
            //TblFirmahareketler.URUNID = tblurunler.ID
            //inner join
            //TblFirmalar
            //on
            //TblFirmahareketler.FIRMA = TblFirmalar.ID

            //SQL veri tabanında oluşturduğumuz proseduru formda listeleme metodu.
            DataTable dt = new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da = new SqlDataAdapter("exec FirmaHareketler2", bgl.baglanti()); //FirmaHareketler2 isminde oluşturduğumuz prosedürü içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControlHareket.DataSource = dt; //Araca yazdırdık.
        }

        void fihrist()
        {
            DataTable dt=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AD, TELEFON1 from TblFirmalar", bgl.baglanti());
            da.Fill(dt);
            gridControlfihrist.DataSource = dt;   
        }

        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.sozcu.com.tr/feeds-rss-category-gundem");
            while (xmloku.Read())
            {
                if (xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }

        private void frmAnasayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            hareketler();
            fihrist();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
            haberler();
        }
    }
}
