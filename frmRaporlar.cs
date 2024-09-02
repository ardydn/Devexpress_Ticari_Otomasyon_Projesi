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
    public partial class frmRaporlar : Form
    {
        public frmRaporlar()
        {
            InitializeComponent();
        }
        /*ReportViever gelmesi için;
        https://www.youtube.com/watch?v=KCkmRtnFx1c
        Designer yapmak için ;
        https://www.youtube.com/watch?v=qsLLsMEI1_Q
        https://www.youtube.com/watch?v=WgpHwDnHaxs */
        private void frmRaporlar_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'DboTicariOtomasyonDataSet.TblMusteriler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.TblMusterilerTableAdapter.Fill(this.DboTicariOtomasyonDataSet.TblMusteriler);
            // TODO: Bu kod satırı 'DboTicariOtomasyonDataSet.TblPersoneller' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.TblPersonellerTableAdapter.Fill(this.DboTicariOtomasyonDataSet.TblPersoneller);
            // TODO: Bu kod satırı 'DboTicariOtomasyonDataSet.TblGiderler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.TblGiderlerTableAdapter.Fill(this.DboTicariOtomasyonDataSet.TblGiderler);
            // TODO: Bu kod satırı 'DboTicariOtomasyonDataSet.TblFirmalar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.TblFirmalarTableAdapter.Fill(this.DboTicariOtomasyonDataSet.TblFirmalar);

            this.reportViewer1.RefreshReport();
        }
    }
}
