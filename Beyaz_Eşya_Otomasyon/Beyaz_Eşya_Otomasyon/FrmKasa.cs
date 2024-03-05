using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Charts;//Kütüphane eklendi

namespace Beyaz_Eşya_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void musterihareket()

        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute CustomerMovementss",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;


        }


        void firmahareket()

        {

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Execute CompaniesMovements", bgl.baglanti());
            da1.Fill(dt1);
            gridControl2.DataSource = dt1;


        }


        void giderlerhareket()
        {



            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select *From Expenses", bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;

        }


        //AKTİF KULLANICI SİSTEME KİM GİRİŞ YAPTI
        public string ad;



        private void FrmKasa_Load(object sender, EventArgs e)
        {

            lblAktifKullanici.Text = ad;
            musterihareket();
            firmahareket();
            giderlerhareket();

            //Toplam Tutarı Hesaplama

            SqlCommand komut1 = new SqlCommand("Select Sum(Total) From İnvoiceDetail",bgl.baglanti());

            SqlDataReader dr1 = komut1.ExecuteReader();

            while(dr1.Read())
            { 
                lblKasaToplam.Text = dr1[0].ToString() + "TL";
            }
            bgl.baglanti().Close();



            //SON AYIN FATURALARI
            //En son ayın giderini getiriyor
            SqlCommand komut2 = new SqlCommand("Select (Electricity+Water+NaturalGas+Internet+Extra)From Expenses order by Id asc", bgl.baglanti());

            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                lblÖdemeler.Text = dr2[0].ToString()+ "TL";


            }
            bgl.baglanti().Close();

            //Son Ayın Personel Maaşları

            SqlCommand komut3 = new SqlCommand("Select  Salaries From Expenses   order by Id asc", bgl.baglanti());

            SqlDataReader dr3 = komut3.ExecuteReader();

            while(dr3.Read())
            {
                lblPesonelMaaslar.Text = dr3[0].ToString()+ "TL";




            }
            bgl.baglanti().Close();


            //TOPLAM MÜŞTERİ SAYISI

            SqlCommand komut4 = new SqlCommand("Select  Count(*) From Customers", bgl.baglanti());

            SqlDataReader dr4 = komut4.ExecuteReader();

            while (dr4.Read())
            {
              lblMusteriSayisi.Text = dr4[0].ToString() ;




            }
            bgl.baglanti().Close();


            //TOPLAM FİRMA  SAYISI

            SqlCommand komut5= new SqlCommand("Select  Count(*) From Companies", bgl.baglanti());

            SqlDataReader dr5 = komut5.ExecuteReader();

            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();




            }
            bgl.baglanti().Close();


            //TOPLAM FİRMA ŞEHİR SAYISI

            SqlCommand komut6 = new SqlCommand("Select  Count(DISTINCT(Province)) From Companies", bgl.baglanti());

            SqlDataReader dr6 = komut6.ExecuteReader();

            while (dr6.Read())
            {
               lblSehirSayisi1.Text = dr6[0].ToString();




            }
            bgl.baglanti().Close();

            //TOPLAM MÜŞTERİ ŞEHİR SAYISI
            SqlCommand komut7 = new SqlCommand("Select  Count(DISTINCT(Province)) From Customers", bgl.baglanti());

            SqlDataReader dr7 = komut7.ExecuteReader();

            while (dr7.Read())
            {
                lblSehirSayisi2.Text = dr7[0].ToString();




            }
            bgl.baglanti().Close();

            //TOPLAM PERSONEL SAYISI
            SqlCommand komut8= new SqlCommand("Select  Count(*) From Employees", bgl.baglanti());

            SqlDataReader dr8 = komut8.ExecuteReader();

            while (dr8.Read())
            {
                lblPersonelSayisi.Text = dr8[0].ToString();




            }
            bgl.baglanti().Close();








            //TOPLAM ÜRÜN  SAYISI
            SqlCommand komut9 = new SqlCommand("Select  Sum(Piece) From Products", bgl.baglanti());

            SqlDataReader dr9 = komut9.ExecuteReader();

            while (dr9.Read())
            {
                lblStokSayisi.Text = dr9[0].ToString();




            }
            bgl.baglanti().Close();



        }


        int sayac = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;

            //ELEKTRİK
            if(sayac>0 && sayac <=5)
            {


          
                groupControl10.Text = "Elektrik";

                SqlCommand komut10 = new SqlCommand("Select top 5 Month ,Electricity from Expenses order by Id desc", bgl.baglanti());

                SqlDataReader dr10 = komut10.ExecuteReader();

                while (dr10.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));




                }
                bgl.baglanti().Close();


            }


            //SU

            if(sayac>5 && sayac<=10)

            { 

             
                groupControl10.Text = "Su";

                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 5 Month ,Water FROM Expenses ORDER BY Id DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();

                while (dr11.Read())
                {

                     chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));




                }
                bgl.baglanti().Close();
            }

            //DOĞALGAZ



            if (sayac > 10 && sayac <= 15)

            {


                groupControl10.Text = "DoğalGaz";

                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 5 Month ,NaturalGas FROM Expenses ORDER BY Id DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();

                while (dr11.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));




                }
                bgl.baglanti().Close();
            }

            //İNTERNET

            if (sayac > 15 && sayac <= 20)

            {


                groupControl10.Text = "İnternet";

                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 5 Month ,Internet FROM Expenses ORDER BY Id DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();

                while (dr11.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));




                }
                bgl.baglanti().Close();
            }


            //EKSTRA
            if (sayac > 20 && sayac <= 25)

            {


                groupControl10.Text = "Ekstra";

                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 5 Month ,Extra FROM Expenses ORDER BY Id DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();

                while (dr11.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));




                }
                bgl.baglanti().Close();
            }
            if(sayac==26)
            {
                sayac = 0;

            }

        }

        int sayac1;
        private void timer2_Tick(object sender, EventArgs e)
        {

            sayac1++;

            //ELEKTRİK
            if (sayac1 > 0 && sayac1 <= 5)
            {



                groupControl11.Text = "Elektrik";

                SqlCommand komut10 = new SqlCommand("Select top 5 Month ,Electricity from Expenses order by Id desc", bgl.baglanti());

                SqlDataReader dr10 = komut10.ExecuteReader();

                while (dr10.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));




                }
                bgl.baglanti().Close();


            }


            //SU

            if (sayac1 > 5 && sayac1 <= 10)

            {


                groupControl11.Text = "Su";

                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 5 Month ,Water FROM Expenses ORDER BY Id DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();

                while (dr11.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));




                }
                bgl.baglanti().Close();
            }

            //DOĞALGAZ



            if (sayac1 > 10 && sayac1 <= 15)

            {


                groupControl11.Text = "DoğalGaz";

                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 5 Month ,NaturalGas FROM Expenses ORDER BY Id DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();

                while (dr11.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));




                }
                bgl.baglanti().Close();
            }

            //İNTERNET

            if (sayac1 > 15 && sayac1 <= 20)

            {


                groupControl11.Text = "İnternet";

                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 5 Month ,Internet FROM Expenses ORDER BY Id DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();

                while (dr11.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));




                }
                bgl.baglanti().Close();
            }


            //EKSTRA
            if (sayac1 > 20 && sayac1 <= 25)

            {


                groupControl11.Text = "Ekstra";

                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 5 Month ,Extra FROM Expenses ORDER BY Id DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();

                while (dr11.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));




                }
                bgl.baglanti().Close();
            }
            if (sayac1 == 26)
            {
                sayac1 = 0;

            }

        }


    }
    }

