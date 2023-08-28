using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FATURA_KESME_PROJE_GÜNCEL
{
    public partial class FrmÖnizleme : Form
    {
        public FrmÖnizleme()
        {
            InitializeComponent();
        }
        sqlBaglantı bgl=new sqlBaglantı();
        private void FrmÖnizleme_Load(object sender, EventArgs e)
        {
           

           
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_FaturaBilgileri", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1"; // Raporunuzun veri seti adını buraya yazmalısınız
            reportDataSource.Value = dt; // Direkt DataTable'ı kullanıyoruz

            // LocalReport nesnesinin DataSources koleksiyonuna ekleyin
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            // ReportViewer'ı güncelleyin
            reportViewer1.LocalReport.ReportPath = "C:\\Users\\user\\Desktop\\dosyalar\\YAZILIM\\FATURA KESME PROJE GÜNCEL\\Report1.rdlc"; // Rapor dosyasının yolunu buraya yazmalısınız
            reportViewer1.RefreshReport();
        }
    }
}
