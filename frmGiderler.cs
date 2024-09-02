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
    public partial class frmGiderler : Form
    {
        public frmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl=new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        void listele()
        {
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt = new DataTable();//Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da= new SqlDataAdapter("select * from TblGiderler",bgl.baglanti()); //Giderler tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık.
        }
        void temizle()
        {
            //Veri girdiğimiz alanı temizleme metodu.
            txtDogalgaz.Text = "";
            txtEkstra.Text = "";
            txtElektrik.Text = "";
            txtId.Text = "";
            txtInternet.Text ="" ;
            txtMaas.Text = "";
            txtSu.Text = "";
            cmbAy.Text = "";
            cmbYil.Text = "";
            rchNotlar.Text = "";
        }

        private void frmGiderler_Load(object sender, EventArgs e)
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
            SqlCommand komut =new SqlCommand("insert into TblGiderler(AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAAS,EKSTRA,NOTLAR) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYil.Text);
            komut.Parameters.AddWithValue("@p3",decimal.Parse( txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text)); ;
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", rchNotlar.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Giderler sisteme kaydedildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Giderlerin gridden araçlara taşınması.
            //Formumuzun özelliklerinden events(olaylar) kısmından focusedrowchanged seçeneğine çift tıkladık.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle); //Veri satırı sınıfından dr ısmınde bir nesne türettik ve bu dr komutuna bir görev ataması yaptık(satırın verisini al).
            txtId.Text = dr["ID"].ToString();
            cmbAy.Text = dr["AY"].ToString();
            cmbYil.Text = dr["YIL"].ToString();
            txtElektrik.Text = dr["ELEKTRIK"].ToString();
            txtSu.Text = dr["SU"].ToString();
            txtDogalgaz.Text = dr["DOGALGAZ"].ToString();
            txtInternet.Text = dr["INTERNET"].ToString();
            txtMaas.Text = dr["MAAS"].ToString();
            txtEkstra.Text = dr["EKSTRA"].ToString();
            rchNotlar.Text = dr["NOTLAR"].ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri silme.
            SqlCommand komut = new SqlCommand("delete from TblGiderler where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Giderler sistemden silindi","BİLGİ",MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri güncelleme.
            SqlCommand komut = new SqlCommand("update TblGiderler set AY=@p1, YIL=@p2, ELEKTRIK=@p3, SU=@p4, DOGALGAZ=@p5, INTERNET=@p6, MAAS=@p7, EKSTRA=@p8, NOTLAR=@p9 where ID=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text)); ;
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", rchNotlar.Text);
            komut.Parameters.AddWithValue("@p10", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Giderler sistemde güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

    }
}
