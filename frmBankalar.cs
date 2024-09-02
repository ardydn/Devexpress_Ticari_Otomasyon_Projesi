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
using System.Reflection;

namespace TicariOtomasyon
{
    public partial class frmBankalar : Form
    {
        public frmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi(); //Bağlantı adresimizi çagırıyoruz.

        /* Formumuzdaki firmaid kismi sayi yerine firma ismi yazsin istiyorsak inner join kullanacagiz
        -- bu kismi sql tarafinda yaziyoruz
        -- sutunlar tablo1 inner join tablo2 on tablo1.deger==tablo2.deger

        create procedure BANKABILGILERI
        as
        select TblBankalar.ID, BANKAADI,TblBankalar.IL,TblBankalar.ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,AD
        from TblBankalar inner join TblFirmalar
        on
        TblBankalar.FIRMAID=TblFirmalar.ID

        execute BANKABILGILERI */

        void listele()
        {
            //SQL veri tabanında oluşturduğumuz tablomuzu formda listeleme metodu.
            DataTable dt = new DataTable(); //Datatable sınıfından dt isminde bir nesne türettik.
            SqlDataAdapter da = new SqlDataAdapter("execute BANKABILGILERI", bgl.baglanti()); //Bankalar tablosu içindeki bütün veriyi çekip türettiğimiz da'ya atadık.
            da.Fill(dt); //Dataadapterın içini datatable ile dolduruyoruz.
            gridControl1.DataSource = dt; //Araca yazdırdık.
        }

        void firmalistesi()
        {
            //LOOKUP EDIT
            //Devexpressde combobox yerine lookup aracını kullanıyoruz.
            DataTable dt=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from TblFirmalar", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        void temizle()
        {
            //Veri girdiğimiz alanı temizleme metodu.
            txtBankaadi.Text = "";
            lookUpEdit1.Text = "";
            txtHesapno.Text = "";
            txtHesapturu.Text = "";
            txtIban.Text = "";
            txtId.Text = "";
            txtSube.Text = "";
            txtYetkili.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            mskTarih.Text = "";
            mskTelefon.Text = "";
            
        }

        void sehirlistele()
        {
            //Şehirler tablomuzu comboboxa çagırma metodu.
            SqlCommand komut = new SqlCommand("select SEHIR from TblIller", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void frmBankalar_Load(object sender, EventArgs e)
        {
            listele(); //Listele metodumuzu çağırdık.
            sehirlistele(); //sehirlistele metodumuzu çağırdık.
            firmalistesi(); //firmalistesi metodumuzu çağırdık.
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri kaydetme.
            SqlCommand komut =new SqlCommand("insert into TblBankalar(BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtBankaadi.Text);
            komut.Parameters.AddWithValue("@p2", cmbIl.Text);
            komut.Parameters.AddWithValue("@p3", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtIban.Text);
            komut.Parameters.AddWithValue("@p6",  txtHesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", mskTarih.Text);
            komut.Parameters.AddWithValue("@p10", txtHesapturu.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);//
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka sisteme kaydedildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Bankaların gridden araçlara taşınması.
            //Formumuzun özelliklerinden events(olaylar) kısmından focusedrowchanged seçeneğine çift tıkladık.
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle); //Veri satırı sınıfından dr ısmınde bir nesne türettik ve bu dr komutuna bir görev ataması yaptık(satırın verisini al).
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtBankaadi.Text = dr["BANKAADI"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtSube.Text = dr["SUBE"].ToString();
                txtIban.Text = dr["IBAN"].ToString();
                txtHesapno.Text = dr["HESAPNO"].ToString();
                txtYetkili.Text = dr["YETKILI"].ToString();
                mskTelefon.Text = dr["TELEFON"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                txtHesapturu.Text = dr["HESAPTURU"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri silme.
            SqlCommand komut = new SqlCommand("delete from TblBankalar where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarını gerçekleştir yani sorguyu çalıştır.
            bgl.baglanti().Close(); //Bağlantıyı kapattık.
            MessageBox.Show("Banka sistemden silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Girdiğimiz yeni verileri güncelleme.
            SqlCommand komut = new SqlCommand("update TblBankalar set BANKAADI=@p1, IL=@p2, ILCE=@p3, SUBE=@p4, IBAN=@p5, HESAPNO=@p6, YETKILI=@p7, TELEFON=@p8, TARIH=@p9, HESAPTURU=@p10, FIRMAID=@p11 where ID=@p12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtBankaadi.Text);
            komut.Parameters.AddWithValue("@p2", cmbIl.Text);
            komut.Parameters.AddWithValue("@p3", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtIban.Text);
            komut.Parameters.AddWithValue("@p6", txtHesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", mskTarih.Text);
            komut.Parameters.AddWithValue("@p10", txtHesapturu.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);//
            komut.Parameters.AddWithValue("@p12", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka sistemde güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); //Listele metodumuzu çağırdık.
            temizle(); //Temizle metodumuzu çağırdık.
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {

            //İller aracına çift tıkladık.
            //İller aracımızda herhangi bir değişiklik olduğunda ilçeler aracımızda o ile ait ilçeler listelenecek.
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ILCE from TblIlceler where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
    }
}
