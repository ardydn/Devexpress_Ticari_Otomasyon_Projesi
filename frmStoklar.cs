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
    public partial class frmStoklar : Form
    {
        public frmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        /*
         --count, avg,sum
         -- count örnek select count(*) from TblFirmalar where IL='ADANA'
         -- avg örnek select avg(SATISFIYAT) from TblUrunler
         -- sum örnek select sum(SATISFIYAT) from TblUrunler

         Select Ad,sum(ADET) from TblUrunler group by Ad
         
         */

        private void frmStoklar_Load(object sender, EventArgs e)
        {
            //chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 4);
            //chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 8);
            //chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 6);
            //chartControl1.Series["Series 1"].Points.AddPoint("Adana", 5);

            SqlDataAdapter da = new SqlDataAdapter("Select AD,sum(ADET) As 'Miktar' from TblUrunler group by AD", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //Charta stok miktarı listeleme
            SqlCommand komut = new SqlCommand("Select AD, sum(ADET) as 'Miktar' from TblUrunler group by AD", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }

            //Charta firma şehir sayısı çekme
            SqlCommand komut2 = new SqlCommand("select IL,count(*) from TblFirmalar Group By IL", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Stoklar formundan -> Stokdetay formuna geçiş.
            //Formlar arasi bilgi taşıma işlemi.
            //Bu formun datagridview - özellikler - olaylar - doubleclick kısmına çift tıklayıp kodlar kısmına geldik.
            frmStokdetay frm = new frmStokdetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                frm.ad = dr["AD"].ToString(); //Faturadetay formunda oluşturduğumuz değişkeninin değerini bu formdan gönderiyoruz.
                frm.Show();
            }
        }
    }
}
