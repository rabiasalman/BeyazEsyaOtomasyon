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

namespace Beyaz_Eşya_Otomasyon
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
           




            SqlDataAdapter da = new SqlDataAdapter("Select  ProductName,Sum(Piece) As 'Amount' From Products group by ProductName",bgl.baglanti());

            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;


            //Char Stok miktarı Listeleme

            SqlCommand komut = new SqlCommand("Select  ProductName,Sum(Piece) As 'Amount' From Products group by ProductName", bgl.baglanti());

            SqlDataReader dr = komut.ExecuteReader();

            while(dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString ( dr[0]),int.Parse (dr[1].ToString()));


            }
            bgl.baglanti().Close();

            //Charta Firma Şehir Sayısı Çekme

            SqlCommand komut1 = new SqlCommand("Select Province,Count(*) From Companies Group By  Province",bgl.baglanti());

            SqlDataReader dr1 = komut1.ExecuteReader();

            while(dr1.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr1[0]),int.Parse(dr1[1].ToString()));

            }
            bgl.baglanti();



        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
                     FrmStokDetay fr = new FrmStokDetay();
                     DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);


                      if (dr != null)
                     {
                          fr.ad = dr["ProductName"].ToString();

                      }
                    fr.Show();











        }
    }
}
