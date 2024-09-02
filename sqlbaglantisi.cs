using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Drawing;

namespace TicariOtomasyon
{
    internal class sqlbaglantisi
    {
        //Sql bağlantımızı sağlayabilmemiz için proje>yeni veri kaynağı ekle üzerinden sunucu adresimizi kopyalıyoruz.
        //Sunucudan bağlantı adresi alabilmemiz için açtığımız bölümde sunucu sertifikası tikinin seçili olduğuna dikkat et.
        //Sunucu baglanti adresindeki tire(-) sagindaki ve solundaki bosluklari silmeye dikkat et. 

        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source = DESKTOP-QTDSAOT\SQLEXPRESS; Initial Catalog = DboTicariOtomasyon; Integrated Security = True;"); //baglan komutumuza adresimizi ilistirdik.
            baglan.Open();
            return baglan;
        }

    }
}
