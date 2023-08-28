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
using Microsoft.Reporting.WinForms;
using System.IO;

namespace FATURA_KESME_PROJE_GÜNCEL
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        sqlBaglantı bgl = new sqlBaglantı();
        DataSet1TableAdapters.Tbl_FaturaBilgileriTableAdapter ds = new DataSet1TableAdapters.Tbl_FaturaBilgileriTableAdapter();
        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_FaturaBilgileri", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmÖnizleme fr = new FrmÖnizleme();
            fr.Show();
           
           
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtfaturaid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtisim_soyisim.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtadres_il_ilçe.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txteposta.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtvergidairesi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            msktxttckn.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtozellestirmeno.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            txtsenaryo.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            txtfaturano.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            msktxtfaturatarih.Text = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
            txtfaturatipi.Text = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
            txtgönderimsekli.Text = dataGridView1.Rows[secilen].Cells[11].Value.ToString();
            msktxtduzenlemetarihi.Text = dataGridView1.Rows[secilen].Cells[12].Value.ToString();
            msktxtduzenlemezamanı.Text = dataGridView1.Rows[secilen].Cells[13].Value.ToString();
            txtsırano.Text = dataGridView1.Rows[secilen].Cells[14].Value.ToString();
            txtmalhizmetkodu.Text = dataGridView1.Rows[secilen].Cells[15].Value.ToString();
            txtmalhizmetadı.Text = dataGridView1.Rows[secilen].Cells[16].Value.ToString();
            txtaçıklama.Text = dataGridView1.Rows[secilen].Cells[17].Value.ToString();
            txtmiktar.Text = dataGridView1.Rows[secilen].Cells[18].Value.ToString();
            txtbirimfiyat.Text = dataGridView1.Rows[secilen].Cells[19].Value.ToString();
            txtiskontooranı.Text = dataGridView1.Rows[secilen].Cells[20].Value.ToString();
            txtdigervergiler.Text = dataGridView1.Rows[secilen].Cells[23].Value.ToString();
            txtkdvoranı.Text = dataGridView1.Rows[secilen].Cells[25].Value.ToString();


        }
       

        private void BTNEKLE_Click(object sender, EventArgs e)
        {
            double kdvoranı = double.Parse(txtkdvoranı.Text) / 100;
            double miktar = double.Parse(txtmiktar.Text);
            double birimFiyat = double.Parse(txtbirimfiyat.Text);
            double iskontoOrani = double.Parse(txtiskontooranı.Text) / 100;
            double digervergileroran = double.Parse(txtdigervergiler.Text) / 100;
            double kdvmiktarı = (miktar * birimFiyat * kdvoranı);
            double toplamTutar = ((miktar * birimFiyat) - (miktar * birimFiyat * iskontoOrani) + kdvmiktarı + (miktar * birimFiyat * digervergileroran));
            string digervergilertutar = (digervergileroran * miktar * birimFiyat).ToString();
            string odenecektutar = toplamTutar.ToString();
            string vergilerdahiltutar = (toplamTutar + (miktar * birimFiyat * iskontoOrani)).ToString();
            string hesaplanankdv = kdvmiktarı.ToString();
            string malhizmettoplamtutar = (miktar * birimFiyat).ToString();
            string toplamiskonto = (miktar * birimFiyat * iskontoOrani).ToString();
            int toplamtutarint = int.Parse(odenecektutar);
            string yazıodenecektutar = SayiyiYaziyaCevir(toplamtutarint);
            SqlCommand komutkaydet = new SqlCommand("INSERT INTO [Tbl_FaturaBilgileri] ([isim_soyisim], [adres_il_ilçe], [eposta], [vergiDairesi], [TCKN], [özelleştirmeno], [senaryo], [faturano], [faturaTarihi], [faturaTipi], [gönderimŞekli], [düzenlemeTarihi], [düzenlemeZamanı], [sırano], [malhizmetKodu], [malHizmetAdı], [açıklama], [miktar], [birimFiyat], [iskontoOranı], [iskontoTutarı], [KDVtutarı], [diğerVergiler], [malHizmetTutarı], [KDVoranı], [malHizmetToplamTutarı], [toplamİskonto], [HesaplananKDV], [VergilerDahilToplam], [ÖdenecekTutar],[ÖdenecekTutarYazıyla]) VALUES (@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p2", txtisim_soyisim.Text);
            komutkaydet.Parameters.AddWithValue("@p3", txtadres_il_ilçe.Text);
            komutkaydet.Parameters.AddWithValue("@p4", txteposta.Text);
            komutkaydet.Parameters.AddWithValue("@p5", txtvergidairesi.Text);
            komutkaydet.Parameters.AddWithValue("@p6", msktxttckn.Text);
            komutkaydet.Parameters.AddWithValue("@p7", txtozellestirmeno.Text);
            komutkaydet.Parameters.AddWithValue("@p8", txtsenaryo.Text);
            komutkaydet.Parameters.AddWithValue("@p9", txtfaturano.Text);
            komutkaydet.Parameters.AddWithValue("@p10", msktxtfaturatarih.Text);
            komutkaydet.Parameters.AddWithValue("@p11", txtfaturatipi.Text);
            komutkaydet.Parameters.AddWithValue("@p12", txtgönderimsekli.Text);
            komutkaydet.Parameters.AddWithValue("@p13", msktxtduzenlemetarihi.Text);
            komutkaydet.Parameters.AddWithValue("@p14", msktxtduzenlemezamanı.Text);
            komutkaydet.Parameters.AddWithValue("@p15", txtsırano.Text);
            komutkaydet.Parameters.AddWithValue("@p16", txtmalhizmetkodu.Text);
            komutkaydet.Parameters.AddWithValue("@p17", txtmalhizmetadı.Text);
            komutkaydet.Parameters.AddWithValue("@p18", txtaçıklama.Text);
            komutkaydet.Parameters.AddWithValue("@p19", txtmiktar.Text);
            komutkaydet.Parameters.AddWithValue("@p20", txtbirimfiyat.Text);
            komutkaydet.Parameters.AddWithValue("@p21", txtiskontooranı.Text);
            komutkaydet.Parameters.AddWithValue("@p22", toplamiskonto);
            komutkaydet.Parameters.AddWithValue("@p23", hesaplanankdv);
            komutkaydet.Parameters.AddWithValue("@p24", digervergilertutar);
            komutkaydet.Parameters.AddWithValue("@p25", malhizmettoplamtutar);
            komutkaydet.Parameters.AddWithValue("@p26", txtkdvoranı.Text);
            komutkaydet.Parameters.AddWithValue("@p27", malhizmettoplamtutar);
            komutkaydet.Parameters.AddWithValue("@p28", toplamiskonto);
            komutkaydet.Parameters.AddWithValue("@p29", hesaplanankdv);
            komutkaydet.Parameters.AddWithValue("@p30", vergilerdahiltutar);
            komutkaydet.Parameters.AddWithValue("@p31", odenecektutar);
            komutkaydet.Parameters.AddWithValue("@p32", yazıodenecektutar);


            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Oluşturuldu.");

        }

        private void BTNGUNCELLE_Click(object sender, EventArgs e)
        {
            double kdvoranı = double.Parse(txtkdvoranı.Text) / 100;
            double miktar = double.Parse(txtmiktar.Text);
            double birimFiyat = double.Parse(txtbirimfiyat.Text);
            double iskontoOrani = double.Parse(txtiskontooranı.Text) / 100;
            double digervergileroran = double.Parse(txtdigervergiler.Text) / 100;
            double kdvmiktarı = (miktar * birimFiyat * kdvoranı);
            double toplamTutar = ((miktar * birimFiyat) - (miktar * birimFiyat * iskontoOrani) + kdvmiktarı+(miktar*birimFiyat*digervergileroran));
            string digervergilertutar = (digervergileroran * miktar * birimFiyat).ToString();
            string odenecektutar = toplamTutar.ToString();
            string vergilerdahiltutar = (toplamTutar + (miktar * birimFiyat * iskontoOrani)).ToString();
            string hesaplanankdv = kdvmiktarı.ToString();
            string malhizmettoplamtutar = (miktar * birimFiyat).ToString();
            string toplamiskonto = (miktar * birimFiyat * iskontoOrani).ToString();
            int toplamtutarint = int.Parse(odenecektutar);
            string yazıodenecektutar=SayiyiYaziyaCevir(toplamtutarint);
            
            SqlCommand komutkaydet = new SqlCommand("UPDATE [Tbl_FaturaBilgileri] SET [isim_soyisim] = @p2,[adres_il_ilçe] = @p3,[eposta] = @p4,[vergiDairesi] = @p5,[TCKN] = @p6,[özelleştirmeno] = @p7,[senaryo] = @p8,   [faturano] = @p9,[faturaTarihi] = @p10, [faturaTipi] = @p11,[gönderimŞekli] = @p12,[düzenlemeTarihi] = @p13,[düzenlemeZamanı] = @p14, [sırano] = @p15,[malhizmetKodu] = @p16, [malHizmetAdı] = @p17, [açıklama] = @p18,[miktar] = @p19, [birimFiyat] = @p20, [iskontoOranı] = @p21, [iskontoTutarı] = @p22, [KDVtutarı] = @p23, [diğerVergiler] = @p24, [malHizmetTutarı] = @p25,[KDVoranı] = @p26, [malHizmetToplamTutarı] = @p27,[toplamİskonto] = @p28, [HesaplananKDV] = @p29,[VergilerDahilToplam] = @p30,[ÖdenecekTutar] = @p31,[ÖdenecekTutarYazıyla]=@p32 WHERE [faturaid] = @p1", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", txtfaturaid.Text);
            komutkaydet.Parameters.AddWithValue("@p2", txtisim_soyisim.Text);
            komutkaydet.Parameters.AddWithValue("@p3", txtadres_il_ilçe.Text);
            komutkaydet.Parameters.AddWithValue("@p4", txteposta.Text);
            komutkaydet.Parameters.AddWithValue("@p5", txtvergidairesi.Text);
            komutkaydet.Parameters.AddWithValue("@p6", msktxttckn.Text);
            komutkaydet.Parameters.AddWithValue("@p7", txtozellestirmeno.Text);
            komutkaydet.Parameters.AddWithValue("@p8", txtsenaryo.Text);
            komutkaydet.Parameters.AddWithValue("@p9", txtfaturano.Text);
            komutkaydet.Parameters.AddWithValue("@p10", msktxtfaturatarih.Text);
            komutkaydet.Parameters.AddWithValue("@p11", txtfaturatipi.Text);
            komutkaydet.Parameters.AddWithValue("@p12", txtgönderimsekli.Text);
            komutkaydet.Parameters.AddWithValue("@p13", msktxtduzenlemetarihi.Text);
            komutkaydet.Parameters.AddWithValue("@p14", msktxtduzenlemezamanı.Text);
            komutkaydet.Parameters.AddWithValue("@p15", txtsırano.Text);
            komutkaydet.Parameters.AddWithValue("@p16", txtmalhizmetkodu.Text);
            komutkaydet.Parameters.AddWithValue("@p17", txtmalhizmetadı.Text);
            komutkaydet.Parameters.AddWithValue("@p18", txtaçıklama.Text);
            komutkaydet.Parameters.AddWithValue("@p19", txtmiktar.Text);
            komutkaydet.Parameters.AddWithValue("@p20", txtbirimfiyat.Text);
            komutkaydet.Parameters.AddWithValue("@p21", txtiskontooranı.Text);
            komutkaydet.Parameters.AddWithValue("@p22", toplamiskonto);
            komutkaydet.Parameters.AddWithValue("@p23", hesaplanankdv);
            komutkaydet.Parameters.AddWithValue("@p24", digervergilertutar);
            komutkaydet.Parameters.AddWithValue("@p25", malhizmettoplamtutar);
            komutkaydet.Parameters.AddWithValue("@p26", txtkdvoranı.Text);
            komutkaydet.Parameters.AddWithValue("@p27", malhizmettoplamtutar);
            komutkaydet.Parameters.AddWithValue("@p28", toplamiskonto);
            komutkaydet.Parameters.AddWithValue("@p29", hesaplanankdv);
            komutkaydet.Parameters.AddWithValue("@p30", vergilerdahiltutar);
            komutkaydet.Parameters.AddWithValue("@p31", odenecektutar);
            komutkaydet.Parameters.AddWithValue("@p32", yazıodenecektutar);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi.");

        }

        private void BTNSİL_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_FaturaBilgileri where faturaid=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtfaturaid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void BTNYENİLE_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_FaturaBilgileri", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        static string SayiyiYaziyaCevir(int sayi)
        {
            if (sayi == 0)
            {
                return "sıfır";
            }

            string[] birler = { "", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz" };
            string[] onlar = { "", "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };
            string[] binlerBasamak = { "", "bin", "milyon", "milyar" };

            string yaziliHali = "";
            int basamak = 0;

            while (sayi > 0)
            {
                int grup = sayi % 1000;
                if (grup > 0)
                {
                    string grupYazisi = "";
                    int birinciBasamak = grup % 10;
                    int onuncuBasamak = (grup / 10) % 10;
                    int yuzlerBasamak = (grup / 100) % 10;

                    if (yuzlerBasamak > 0)
                    {
                        grupYazisi += birler[yuzlerBasamak] + "yüz";
                    }

                    if (onuncuBasamak > 0)
                    {
                        grupYazisi += onlar[onuncuBasamak];
                    }

                    if (birinciBasamak > 0)
                    {
                        grupYazisi += birler[birinciBasamak];
                    }

                    grupYazisi += binlerBasamak[basamak];

                    if (!string.IsNullOrEmpty(grupYazisi))
                    {
                        if (!string.IsNullOrEmpty(yaziliHali))
                        {
                            yaziliHali = grupYazisi + " " + yaziliHali;
                        }
                        else
                        {
                            yaziliHali = grupYazisi;
                        }
                    }
                }

                sayi /= 1000;
                basamak++;
            }

            return yaziliHali.Trim();
        }



    }
}