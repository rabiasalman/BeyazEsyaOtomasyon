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
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void musterihareketleri()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec CustomerMovementss", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }



        void firmahareketleri()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec CompaniesMovements",bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;

        }
      


        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            firmahareketleri();
            musterihareketleri();
        }

    }
}
