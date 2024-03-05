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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();


        void listele()
        {
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*From Products", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;       

        }

        void temizle()
        {
            txtİd.Text = "";
            txtAd.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            mskYil.Text = "";
            txtAlisFiyat.Text = "";
            txtSatisFiyat.Text = "";
            rchDetay.Text = "";
            nudAdet.ResetText();
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            //Verileri Kaydetme


            SqlCommand komut = new SqlCommand("insert into Products(ProductName,Brand,Model,Year,Piece,BuyingPrice,SellingPrice,Detail) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", mskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString())); //Veritabanında türü neyse o türe donüştürülmesi gerekiyor 
            komut.Parameters.AddWithValue("@p6",decimal.Parse(txtAlisFiyat.Text).ToString());
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisFiyat.Text).ToString());
            komut.Parameters.AddWithValue("@p8", rchDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //ID değerine göre silinecek

            SqlCommand komutsil = new SqlCommand("delete From Products where Id=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtİd.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // ÜRÜNLERİN  GRİDDEN ARAÇLARA  TAŞINMASI
             DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtİd.Text = dr["Id"].ToString();
            txtAd.Text = dr["ProductName"].ToString();
            txtMarka.Text = dr["Brand"].ToString();
            txtModel.Text = dr["Model"].ToString();
            mskYil.Text = dr["Year"].ToString();
            nudAdet.Value = decimal.Parse(dr["Piece"].ToString());
            txtAlisFiyat.Text = dr["BuyingPrice"].ToString();
            txtSatisFiyat.Text = dr["SellingPrice"].ToString();
            rchDetay.Text = dr["Detail"].ToString();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            //ID gelen degere göre güncelleme


            SqlCommand komut = new SqlCommand("update Products set ProductName=@p1,Brand=@p2,Model=@p3,Year=@p4,Piece=@p5,BuyingPrice=@p6,SellingPrice=@p7,Detail=@p8 where Id=@p9", bgl.baglanti());//ID göre güncellicek

            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", mskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchDetay.Text);
            komut.Parameters.Add("@p9", txtİd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Parametresi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
