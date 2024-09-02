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
    public partial class frmStokdetay : Form
    {
        public frmStokdetay()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        public string ad; //Değişken oluşturduk.

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TblUrunler where AD='" + ad + "'", bgl.baglanti()); //sql veritabaninda sorgulama yaparken direkt sayisal bir ifade degilse o zaman tek tirnak icerisinde yazar.
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void frmStokdetay_Load(object sender, EventArgs e)
        {
            label1.Text = ad; //Araca değişkeni atadık.
            listele(); //Listele metodumuzu çağırdık.
        }
    }
}
