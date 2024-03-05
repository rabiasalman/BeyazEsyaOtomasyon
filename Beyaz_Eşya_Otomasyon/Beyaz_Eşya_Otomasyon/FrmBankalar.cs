using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Beyaz_Eşya_Otomasyon
{
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute  Bankİnformation", bgl.baglanti());
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
        void firmalistesi()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Id,CompanyName  From Companies ", bgl.baglanti());

            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "Id";
            lookUpEdit1.Properties.DisplayMember = "CompanyName";
            lookUpEdit1.Properties.DataSource = dt;




        }
        void temizle()
        {
            txtİd.Text = "";
            txtBankaAdi.Text = "";
            cmbİl.Text = "";
            cmbİlce.Text = "";
            txtSube.Text = "";
            txtİban.Text = "";
            txtHesapNo.Text = "";
            txtYetkili.Text = "";
            mskTelefon.Text = "";
            mskTarih.Text = "";
            txtHesapTuru.Text = "";
           lookUpEdit1.EditValue = null;
        }


        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
           
            sehirlistele();

            firmalistesi();
            temizle();
            
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into  Banks(BankName,Province,Districts,Branch,Iban,AccountNumber,Authority,Phone,Date,AccountType,CompaniesId)values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtBankaAdi.Text);
            komut.Parameters.AddWithValue("@p2", cmbİl.Text);
            komut.Parameters.AddWithValue("@p3", cmbİlce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtİban.Text);
            komut.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", mskTarih.Text);
            komut.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
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

      
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr !=null)
            {

                txtİd.Text = dr["Id"].ToString();
                txtBankaAdi.Text = dr["BankName"].ToString();
                cmbİl.Text = dr["Province"].ToString();
                cmbİlce.Text = dr["Districts"].ToString();
                txtSube.Text = dr["Branch"].ToString();
                txtİban.Text = dr["Iban"].ToString();
                txtHesapNo.Text = dr["AccountNumber"].ToString();
                txtYetkili.Text = dr["Authority"].ToString();
                mskTelefon.Text = dr["Phone"].ToString();
                mskTarih.Text = dr["Date"].ToString();
                txtHesapTuru.Text = dr["AccountType"].ToString();
                lookUpEdit1.Text = dr["CompaniesId"].ToString();






            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From Banks where Id=@p1",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtİd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            MessageBox.Show("Banka Bilgisi Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }

        //Güncelle Butonu çalışmıyor

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Banks set BankName=@p1,Province=@p2,Districts=@p3,Branch=@p4,Iban=@p5,AccountNumber=@p6,Authority=@p7,Phone=@p8,Date=@p9,AccountType=@p10 CompaniesId=@p11 where Id=@p12", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtBankaAdi.Text);
            komut.Parameters.AddWithValue("@p2", cmbİl.Text);
            komut.Parameters.AddWithValue("@p3", cmbİlce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtİban.Text);
            komut.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", mskTarih.Text);
            komut.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
    
           komut.Parameters.AddWithValue("@p12", txtİd.Text);
            komut.ExecuteNonQuery();
            listele();

            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Sistemde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            temizle();

        }
    }
}
