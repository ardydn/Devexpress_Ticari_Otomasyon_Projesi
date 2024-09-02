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
    public partial class frmHareketler : Form
    {
        public frmHareketler()
        {
            InitializeComponent();
        }
        /*
         * SQL server kısmına prosedür oluşturuyoruz.
           create procedure FirmaHareketler
           as
           select HAREKETID, URUNID, TblFirmahareketler.ADET,(TblPersoneller.AD + ' ' + SOYAD) as 'AD SOYAD', TblFirmalar.AD as 'FIRMA AD',FIYAT,TOPLAM,FATURAID,TARIH,NOTLAR
           from TblFirmahareketler 
           inner join
           TblUrunler
           on
           TblFirmahareketler.URUNID=tblurunler.ID
           inner join
           TblPersoneller
           on
           TblFirmahareketler.PERSONEL=TblPersoneller.ID
           inner join
           TblFirmalar
           on
           TblFirmahareketler.FIRMA=TblFirmalar.ID
        --------------------------------------------
           SQL server kısmına 2. prosedür oluşturuyoruz.
           create procedure MusteriHareketler
           as
           select HAREKETID, TblUrunler.AD, TblMusterihareketler.ADET, (TblPersoneller.AD+ ' ' +TblPersoneller.SOYAD) as 'AD SOYAD', (TblMusteriler.AD+''+TblMusteriler.SOYAD) as 'MÜŞTERİ',
           FIYAT,TOPLAM,TblMusterihareketler.ADET,FATURAID,TARIH,NOTLAR 
           from
           TblMusterihareketler
           inner join
           TblUrunler
           on
           TblMusterihareketler.URUNID=TblUrunler.ID
           inner join TblPersoneller
           on 
           TblMusterihareketler.PERSONEL=TblPersoneller.ID
           inner join TblMusteriler
           on
           TblMusterihareketler.MUSTERI=TblMusteriler.ID
        */

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void listele()
        {
            //SQL veri tabanında oluşturduğumuz proseduru formda listeleme metodu.
            DataTable dt = new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da=new SqlDataAdapter("exec FirmaHareketler",bgl.baglanti()); //FirmaHareketler isminde oluşturduğumuz prosedürü içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl2.DataSource = dt; //Araca yazdırdık. Gridcontrol2 yapıyoruz çünkü firma hareketleri aracına yazdıracağız.
        }
        void listele2()
        {
            //SQL veri tabanında oluşturduğumuz proseduru formda listeleme metodu.
            DataTable dt = new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da = new SqlDataAdapter("exec MusteriHareketler", bgl.baglanti()); //MusteriHareketler isminde oluşturduğumuz prosedürü içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık. Gridcontrol1 yapıyoruz çünkü müşteri hareketleri aracına yazdıracağız.
        }
        private void frmHareketler_Load(object sender, EventArgs e)
        {
            listele(); //Listele metodumuzu çağırdık.
            listele2(); //Listele metodumuzu çağırdık.
        }
        
    }
}
