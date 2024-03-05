using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//Kütüphane eklendi

namespace Beyaz_Eşya_Otomasyon
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void personelliste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Employees",bgl.baglanti());
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
            mskTC.Text = "";
            txtMail.Text = "";
            rchAdres.Text = "";
            txtGörev.Text = "";
            cmbİl.Text = "";
            cmbİlce.Text = "";

        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelliste();
            temizle();
            sehirlistele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Employees(FirstName,LastName,PhoneNumber,İdentityNumber,Email,Province,District,Duty,Adress) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtAd.Text);//@p1 değer olarak ekle @p1 nerden gelen değer txtAddan gelen değer
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", cmbİl.Text);
            komut.Parameters.AddWithValue("@p7", cmbİlce.Text);
            komut.Parameters.AddWithValue("@p8", txtGörev.Text);
            komut.Parameters.AddWithValue("@p9", rchAdres.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Personel Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();
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

            //GRİDDEN ARAÇLARA VERİ TAŞIMA

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);//gridviewden alıcaz değeri gridview'in veri satırını al 
            //(gridView1.FocusedRowHandle)=fareyle tıklamıs olduğum alanın verisini alıcak


            if(dr!=null)
            //dr null bir değerse yada değilse 
            {
                txtId.Text = dr["Id"].ToString();
                txtAd.Text = dr["FirstName"].ToString();
                txtSoyad.Text = dr["LastName"].ToString();
                mskTelefon1.Text = dr["PhoneNumber"].ToString();
                mskTC.Text = dr["İdentityNumber"].ToString();
                txtMail.Text = dr["Email"].ToString();
                cmbİl.Text = dr["Province"].ToString();
                cmbİlce.Text = dr["District"].ToString();
                txtGörev.Text = dr["Duty"].ToString();
                rchAdres.Text = dr["Adress"].ToString();


            }





        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From Employees where Id=@p1", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            personelliste();
         
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Employees Set FirstName=@p1,LastName=@p2,PhoneNumber=@p3,İdentityNumber=@p4,Email=@p5,Province=@p6,District=@p7,Duty=@p8,Adress=@p9 where Id=@p10", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtAd.Text);//@p1 değer olarak ekle @p1 nerden gelen değer txtAddan gelen değer
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", cmbİl.Text);
            komut.Parameters.AddWithValue("@p7", cmbİlce.Text);
            komut.Parameters.AddWithValue("@p8", txtGörev.Text);
            komut.Parameters.AddWithValue("@p9", rchAdres.Text);
            komut.Parameters.AddWithValue("@p10", txtId.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personelliste();
            temizle();

        }
    }
}
