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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }

        public string urunid;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtUrunİd.Text = urunid;

            SqlCommand komut = new SqlCommand("Select * From İnvoiceDetail  where İnvoiceProductId=@p1 ",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1",urunid);

            SqlDataReader dr = komut.ExecuteReader();

            while(dr.Read())
            {
                txtFiyat.Text = dr[3].ToString();
                txtMiktar.Text = dr[2].ToString();
                txtTutar.Text = dr[4].ToString();
                txtUrunAdi.Text = dr[1].ToString();
                bgl.baglanti().Close();

            }

           
          
            
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("update İnvoiceDetail set ProductName=@p1,Amount=@p2,Price=@p3,Total=@p4 where İnvoiceProductId=@p5  ",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1",txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@p2",txtMiktar.Text);
            komut.Parameters.AddWithValue("@p3",decimal.Parse(txtFiyat.Text));
            komut.Parameters.AddWithValue("@p4",decimal.Parse(txtTutar.Text));
            komut.Parameters.AddWithValue("@p5",txtUrunİd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Değişiklikler Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete From İnvoiceDetail  where İnvoiceProductId=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtUrunİd.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);


        }
    }
}
