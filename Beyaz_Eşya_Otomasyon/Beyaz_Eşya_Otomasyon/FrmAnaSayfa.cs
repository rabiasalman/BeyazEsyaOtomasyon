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
using System.Xml; //kÜTÜPHANE EKLENDİ


namespace Beyaz_Eşya_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void stoklar()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ProductName, Sum(Piece) as 'Piece' from Products group by ProductName having Sum(Piece) <= 20 order by Sum(Piece)",bgl.baglanti());

            da.Fill(dt);
            GridControlStoklar.DataSource = dt;


        }

        void ajanda()

        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select top  14 NoteDate, NoteHour, NoteTitle FROM Notes order by NoteId desc ",bgl.baglanti());
            da.Fill(dt);
            GridControlAjanda.DataSource = dt;




        }



        void firmahareketleri()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec CompaniesMovements2", bgl.baglanti());
            da.Fill(dt);
            GridControlHareket.DataSource = dt;

        }


        void  fihrist()
        {

            DataTable  dt= new DataTable();
            SqlDataAdapter da =  new SqlDataAdapter("Select  CompanyName,FirstPhoneNumber From  Companies",bgl.baglanti());
            da.Fill(dt);
            GridControlFihrist.DataSource = dt;

        }

        void haberler()
        {

            XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");

            while (xmloku.Read())
            {
            
               if(xmloku.Name=="title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            
            
            }

        }


        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            firmahareketleri();
            fihrist();
            haberler();

            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");


        }


    }
}
