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
    public partial class frmRehber : Form
    {
        public frmRehber()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl=new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        private void frmRehber_Load(object sender, EventArgs e)
        {
            //Musteri bilgileri
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt =new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da = new SqlDataAdapter("Select AD,SOYAD,TELEFON1,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE from TblMusteriler", bgl.baglanti()); //Rehber tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık.

            //Firma bilgileri
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt2 =new DataTable(); //Datatable sınıfından dt2 isminde bir nesne türettik.
            SqlDataAdapter da2=new SqlDataAdapter("Select AD,YETKILIADSOYAD,TELEFON1,TELEFON2,TELEFON3,MAIl,FAX from TblFirmalar",bgl.baglanti()); //Rehber tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da2.Fill(dt2); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl2.DataSource = dt2; //Araca yazdırdık.
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Rehber formundan -> Mail formuna geçiş
            //Formlar arasi bilgi taşıma işlemi
            //Bu formun datagridview - özellikler - olaylar - doubleclick kısmına çift tıklayıp kodlar kısmına geldik.
            frmMail frm =new frmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
             if(dr != null )
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            //Rehber formundan -> Mail formuna geçiş
            //Formlar arasi bilgi taşıma işlemi
            //Bu formun datagridview - özellikler - olaylar - doubleclick kısmına çift tıklayıp kodlar kısmına geldik.
            frmMail frm = new frmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }
    }
}
