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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmRehber_Load(object sender, EventArgs e)
        {

            //MÜŞTERİ BİLGİLERİ
            DataTable dt1 =new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select FirstName,LastName,PhoneNumber,SecendPhoneNumber,Email From Customers ", bgl.baglanti());
            da1.Fill(dt1);
            gridControl2.DataSource = dt1;


            //FİRMA BİLGİLERİ

            DataTable dt2 = new DataTable();

            SqlDataAdapter da2 = new SqlDataAdapter("Select CompanyName,AuthorizedNameAndSurname,FirstPhoneNumber,SecendPhoneNumber,ThirdPhoneNumber,FaxNumber,Email From Companies ", bgl.baglanti());
            da2.Fill(dt2);
            gridControl1.DataSource = dt2;





        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();

            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);//seçtiğim satırın değerini dr değişkenine atıcak

            if(dr !=null)
            {
                frm.mail = dr["Email"].ToString();
            }
            frm.Show();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();

            DataRow dr = gridView2.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                frm.mail = dr["Email"].ToString();
            }
            frm.Show();
        }
    }
}
