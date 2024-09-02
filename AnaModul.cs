using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class AnaModul : Form
    {
        public AnaModul()
        {
            InitializeComponent();
        }
        //Hata olmaması için Form1 formunun özellikleri içindeki ismdicontainer seçeneği true olması gerekiyor. frmurunler formunun özellikleri içindeki ismdicontainer in false olması gerekiyor.

        frmUrunler frm;

        private void btnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan ürünler tablosu butonuna tıkladığımzda ürünler formunun açılmasını sağlıyoruz.
            if (frm==null || frm.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
            frm = new frmUrunler();
            frm.MdiParent = this;
            frm.Show();
            }
        }

        frmMusteriler frm2;

        private void btnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan müşteriler tablosu butonuna tıkladığımzda müşteriler formunun açılmasını sağlıyoruz.
            if (frm2== null || frm2.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm2 = new frmMusteriler();
                frm2.MdiParent = this;
                frm2.Show();
            }
        }

        frmFirmalar frm3;

        private void btnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan firmalar tablosu butonuna tıkladığımzda firmalar formunun açılmasını sağlıyoruz.
            if (frm3== null || frm3.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm3=new frmFirmalar();
                frm3.MdiParent= this;
                frm3.Show();
            }
        }

        frmPersoneller frm4;

        private void btnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan personeller tablosu butonuna tıkladığımzda personeller formunun açılmasını sağlıyoruz.
            if (frm4 == null || frm4.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm4=new frmPersoneller();
                frm4.MdiParent= this;
                frm4.Show();
            }
        }

        frmRehber frm5;

        private void btnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan rehber tablosu butonuna tıkladığımzda rehber formunun açılmasını sağlıyoruz.
            if (frm5 == null || frm5.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm5 = new frmRehber();
                frm5.MdiParent= this;
                frm5.Show();
            }
        }

        frmGiderler frm6;

        private void btnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan giderler tablosu butonuna tıkladığımzda giderler formunun açılmasını sağlıyoruz.
            if ( frm6 == null || frm6.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm6 = new frmGiderler();
                frm6.MdiParent= this;
                frm6.Show();
            }
        }

        frmBankalar frm7;
        
        private void btnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan bankalar tablosu butonuna tıkladığımzda bankalar formunun açılmasını sağlıyoruz.
            if (frm7 == null || frm7.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm7 = new frmBankalar();
                frm7.MdiParent= this;
                frm7.Show();
            }
        }

        frmFaturalar frm8;

        private void btnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan faturalar tablosu butonuna tıkladığımzda faturalar formunun açılmasını sağlıyoruz.
            if (frm8 == null || frm8.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm8 = new frmFaturalar();
                frm8.MdiParent= this;
                frm8.Show();
            }
        }

        frmNotlar frm9;

        private void btnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan notlar tablosu butonuna tıkladığımzda notlar formunun açılmasını sağlıyoruz.
            if (frm9 == null || frm9.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm9 = new frmNotlar();
                frm9.MdiParent= this;
                frm9.Show();
            }
        }

        frmHareketler frm10;

        private void btnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan hareketler tablosu butonuna tıkladığımzda hareketler formunun açılmasını sağlıyoruz.
            if (frm10 == null || frm10.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm10 = new frmHareketler();
                frm10.MdiParent = this;
                frm10.Show();
            }
        }

        frmRaporlar frm11;

        private void btnRaporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan raporlar tablosu butonuna tıkladığımzda raporlar formunun açılmasını sağlıyoruz.
            if (frm11 == null || frm11.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm11 = new frmRaporlar();
                frm11.MdiParent = this;
                frm11.Show();
            }
        }

        frmStoklar frm12;

        private void btnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan stoklar tablosu butonuna tıkladığımzda stoklar formunun açılmasını sağlıyoruz.
            if (frm12 == null || frm12.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm12 = new frmStoklar();
                frm12.MdiParent = this;
                frm12.Show();
            }
        }

        frmAyarlar frm13;

        private void btnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan ayarlar tablosu butonuna tıkladığımzda ayarlar formunun açılmasını sağlıyoruz.
            if (frm13== null || frm13.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm13 = new frmAyarlar();
                frm13.Show();
            }
        }

        frmKasa frm14;
        public string kullanici; //!
        private void btnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan kasa tablosu butonuna tıkladığımzda kasa formunun açılmasını sağlıyoruz.
            if (frm14== null || frm14.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm14 = new frmKasa();
                frm14.ad = kullanici; //!
                frm14.MdiParent = this;
                frm14.Show();
            }
        }

        frmAnasayfa frm15;

        private void btnAnasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ana formdan ana sayfa butonuna tıkladığımzda ana sayfa formunun açılmasını sağlıyoruz.
            if (frm15== null || frm15.IsDisposed) //frm.ısdisposed:formu kapattığımızda tekar açabilmemizi sağlıyor.
            {
                frm15 = new frmAnasayfa();
                frm15.MdiParent = this;
                frm15.Show();
            }
        }

        private void AnaModul_Load(object sender, EventArgs e)
        {
            //Form yüklendiğinde ana sayfa formunun açılmasını sağlıyoruz.
            if (frm15 == null)
            {
                frm15 = new frmAnasayfa();
                frm15.MdiParent = this;
                frm15.Show();
            }
        }
    }
}
