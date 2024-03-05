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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();


        void listele()
        {

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("Select*From Admin",bgl.baglanti());
            da.Fill(dt);

            gridControl1.DataSource = dt;






        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {

            listele();
            txtKullaniciAd.Text = "";
            txtSifre.Text = "";

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            //Güncelleme ve Kaydetme Aynı Buton içerisinde oldu

            if (btnİslem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into Admin values(@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtKullaniciAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

            }
            if(btnİslem.Text=="Güncelle")

            {
                SqlCommand komut1 = new SqlCommand("update Admin set Password=@p2 where UserName=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1",txtKullaniciAd.Text);
                komut1.Parameters.AddWithValue("@p2",txtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi ","",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                listele();
            }
            

            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtKullaniciAd.Text = dr["UserName"].ToString();
                txtSifre.Text = dr["Password"].ToString();
            }
        }

        private void txtKullanici_EditValueChanged(object sender, EventArgs e)
        {
            if(txtKullaniciAd.Text!="")
            {
                btnİslem.Text = "Güncelle";
                btnİslem.BackColor = Color.GreenYellow;
                    

            }
            else
            {
                btnİslem.Text = "Kaydet";
                btnİslem.BackColor = Color.Orange;

            }
           
        }
    }
}
