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
    public partial class frmFaturaurundetay : Form
    {
        public frmFaturaurundetay()
        {
            InitializeComponent();
        }

        public string id; //Değişken oluşturduk.

        sqlbaglantisi bgl=new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void listele()
        {
            SqlDataAdapter da=new SqlDataAdapter("select * from TblFaturadetay where FATURAID='"+id+"'",bgl.baglanti()); //sql veritabaninda sorgulama yaparken direkt sayisal bir ifade degilse o zaman tek tirnak icerisinde yazar.
            DataTable dt=new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void frmFaturaurundetay_Load(object sender, EventArgs e)
        {
            label1.Text = id; //Araca değişkeni atadık.
            listele(); //Listele metodumuzu çağırdık.
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Faturalar -> Faturaurundetay-> Faturaduzenleme
            //Formlar arasI bilgi taşıma işlemi
            //Bu formun datagridview - özellikler - olaylar - doubleclick kısmına çift tıklayıp kodlar kısmına geldik.
            frmFaturaurunduzenleme frm =new frmFaturaurunduzenleme();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                frm.urundid = dr["FATURAURUNID"].ToString();
            }
            frm.Show();
        }
    }
}
