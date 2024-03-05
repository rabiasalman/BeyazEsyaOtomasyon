using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//SQL kütüphanesi eklendi
namespace Beyaz_Eşya_Otomasyon
{
    public class SqlBaglantisi
    {

        public SqlConnection baglanti()//Geriye değer döndüren bir metot tanımladım
        {

            SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-EU1TQ2HJ;Initial Catalog=CommercialAutomation;Integrated Security=True");//veri tabanı baglantısı için nesne türettik
            baglan.Open();//Bağlantıyı ac
            return baglan;//Geriye bağlan değerini döndür




        }



    }
}
