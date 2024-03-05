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
    public partial class FrmMusteriler : Form
    {
        //Farklılık il ve ilçe kodları
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*From Customers", bgl.baglanti());
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
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTelefon1.Text = "";
            mskTelefon2.Text = "";
            mskTelefon3.Text = "";
            mskTC.Text = "";
            txtMail.Text = "";
            txtVergiD.Text="";
            rchAdres.Text = "";
            cmbİl.Text = "";
            cmbİlce.Text = "";
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistele();
            temizle();
        }

        private void comboBoxEdit1_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            cmbİlce.Properties.Items.Clear();//

            SqlCommand komut = new SqlCommand("Select District From Districts where City=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbİl.SelectedIndex + 1);

            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                cmbİlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("insert into Customers(FirstName,LastName,PhoneNumber,SecendPhoneNumber,İdentityNumber,Email,Province,District,TaxAuthority,Adress) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());


            komut.Parameters.AddWithValue("@p1", txtAd.Text);//gelen değere göre ekleme yap
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", mskTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", cmbİl.Text);
            komut.Parameters.AddWithValue("@p8", cmbİlce.Text);
            komut.Parameters.AddWithValue("@p9", txtVergiD.Text);
            komut.Parameters.AddWithValue("@p10", rchAdres.Text);
            komut.ExecuteNonQuery();

            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //MÜŞTERİLERİN  DATAGRİDDEN ARAÇLARA  TAŞINMASI 
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle); //Fareyle imlecin seçtiği satır

            if (dr != null)//null değerden farklıysa
            {
                txtId.Text = dr["Id"].ToString();
                txtAd.Text = dr["FirstName"].ToString();
                txtSoyad.Text = dr["LastName"].ToString();
                mskTelefon1.Text = dr["PhoneNumber"].ToString();
                mskTelefon2.Text = dr["SecendPhoneNumber"].ToString();
                mskTC.Text = dr["İdentityNumber"].ToString();
                txtMail.Text = dr["Email"].ToString();
                cmbİl.Text = dr["Province"].ToString();
                cmbİlce.Text = dr["District"].ToString();
                txtVergiD.Text = dr["TaxAuthority"].ToString();
                rchAdres.Text = dr["Adress"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From Customers where  Id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();//insert update delete sorgusunda değişiklik olduğunda  çalışıyor
            bgl.baglanti().Close();
            DialogResult karar = new DialogResult();
            karar = MessageBox.Show("Müşteriyi Gerçekten  Silmek İstiyor Musunuz ? ", "Uyarı", MessageBoxButtons.YesNo);
            if (karar == DialogResult.Yes)
            {
                MessageBox.Show("Müşteri Silindi Sistemden");

            }
            else
            {
                this.Close();
            }
            listele();
            temizle();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("Update Customers Set FirstName=@p1,LastName=@p2,PhoneNumber=@p3,SecendPhoneNumber=@p4,İdentityNumber=@p5,Email=@p6,Province=@p7,District=@p8,TaxAuthority=@p9,Adress=@p10 where Id=@p11", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", mskTC.Text);//Merin kutusu olmaadığı için tostring dönüştürdüm
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", cmbİl.Text);
            komut.Parameters.AddWithValue("@p8", cmbİlce.Text);
            komut.Parameters.AddWithValue("@p9", txtVergiD.Text);
            komut.Parameters.AddWithValue("@p10", rchAdres.Text);
            komut.Parameters.AddWithValue("@p11", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müştere Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
