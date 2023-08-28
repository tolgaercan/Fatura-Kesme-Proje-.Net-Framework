using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FATURA_KESME_PROJE_GÜNCEL
{
    class sqlBaglantı
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection ("Data Source=DESKTOP-Q90HJ59;Initial Catalog=Fatura_Proje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }

}



