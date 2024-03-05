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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();


        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select*From Companies", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirlistele()
        {
            SqlCommand komut = new SqlCommand("Select City  From Provinces ", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();//SqlDataReader sınıfından dr isimli nesne oluşturup komut kümesiyle ilişkilendirdim

            //Okuma işlemi sürdüğü müddetçe
            while (dr.Read())
            {
                cmbİl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

       

        void temizle()
        {
            txtAd.Text = "";
            txtId.Text = "";
           
            txtSektor.Text = "";
            txtVergiD.Text = "";
            txtMail.Text = "";
            txtYetkili.Text = "";
            txtYGörev.Text = "";
            mskFax.Text = "";
            mskTelefon1.Text = "";
            mskTelefon2.Text = "";
            mskTelefon3.Text = "";
            mskYetkiliTC.Text = "";
            rchAdres.Text = "";



            txtAd.Focus();//imleci oraya odaklar
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();
            sehirlistele();
         
            temizle();
        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["Id"].ToString();
                txtAd.Text = dr["CompanyName"].ToString();
                txtYGörev.Text = dr["AuthorizedStatus"].ToString();
                txtYetkili.Text = dr["AuthorizedNameAndSurname"].ToString();
                mskYetkiliTC.Text = dr["AuthorityTc"].ToString();
                mskTelefon1.Text = dr["FirstPhoneNumber"].ToString();
                mskTelefon2.Text = dr["SecendPhoneNumber"].ToString();
                mskTelefon3.Text = dr["ThirdPhoneNumber"].ToString();
                txtMail.Text = dr["Email"].ToString();
                mskFax.Text = dr["FaxNumber"].ToString();
                cmbİl.Text = dr["Province"].ToString();
                cmbİlce.Text = dr["District"].ToString();
                txtVergiD.Text = dr["TaxAuthority"].ToString();
                rchAdres.Text = dr["Adress"].ToString();
              
                txtSektor.Text = dr["Sector"].ToString();

            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("insert into Companies(CompanyName,Sector,AuthorizedNameAndSurname,AuthorizedStatus,AuthorityTc,FirstPhoneNumber,SecendPhoneNumber,ThirdPhoneNumber,FaxNumber,Email,Province,District,TaxAuthority,Adress) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSektor.Text);
            komut.Parameters.AddWithValue("@p3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", txtYGörev.Text);
            komut.Parameters.AddWithValue("@p5", mskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@p6", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p7", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@p8", mskTelefon3.Text);
            komut.Parameters.AddWithValue("@p9", mskFax.Text);
            komut.Parameters.AddWithValue("@p10", txtMail.Text);
            komut.Parameters.AddWithValue("@p11", cmbİl.Text);
            komut.Parameters.AddWithValue("@p12", cmbİlce.Text);
            komut.Parameters.AddWithValue("@p13", txtVergiD.Text);
            komut.Parameters.AddWithValue("@p14", rchAdres.Text);
          
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();



        }

        private void cmbİl_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            // O ile ait ilçeleri getirme
            //cmbİl comboboxsımda her hangi bir değişiklik olduğunda şarta göre illeri getirecek
            //Comboboxsın secilen index değerinden alınacak ilçeler

            cmbİlce.Properties.Items.Clear();//Daha önce seçilmiş olan ilçeleri temizleyen kod

            SqlCommand komut = new SqlCommand("Select District From Districts where City=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbİl.SelectedIndex + 1);//@p1 nerden gelen değer cmbİl'in seçilen indexsinden gelen değer

            SqlDataReader dr = komut.ExecuteReader();

            //dr komutu okuma yaptığı müddetçe

            while (dr.Read())
            {
                cmbİlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From Companies where Id=@p1", bgl.baglanti());
            komut.Parameters.Add("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmalistesi();
            MessageBox.Show("Firma Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Companies Set CompanyName=@p1,Sector=@p2,AuthorizedNameAndSurname=@p3," +
              "AuthorizedStatus=@p4,AuthorityTc=@p5,FirstPhoneNumber=@p6,SecendPhoneNumber=@p7,ThirdPhoneNumber=@p8,FaxNumber=@p9," +
              "Email=@p10,Province=@p11,District=@p12,TaxAuthority=@p13,Adress=@p14,Where Id=@p15", bgl.baglanti());





            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSektor.Text);
            komut.Parameters.AddWithValue("@p3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", txtYGörev.Text);
            komut.Parameters.AddWithValue("@p5", mskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@p6", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p7", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@p8", mskTelefon3.Text);
            komut.Parameters.AddWithValue("@p9", mskFax.Text);
            komut.Parameters.AddWithValue("@p10", txtMail.Text);
            komut.Parameters.AddWithValue("@p11", cmbİl.Text);
            komut.Parameters.AddWithValue("@p12", cmbİlce.Text);
            komut.Parameters.AddWithValue("@p13", txtVergiD.Text);
            komut.Parameters.AddWithValue("@p14", rchAdres.Text);
           
            komut.Parameters.AddWithValue("@p15", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
