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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void giderlistesi()
        {
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("Select *From  Expenses",bgl.baglanti());

            da.Fill(dt);

            gridControl1.DataSource = dt;

        }

        void temizle()
        {
            cmbAy.Text = "";
            cmbYıl.Text = "";
            txtElektrik.Text = "";
            txtSu.Text = "";
            txtDogalGaz.Text = "";
            txtİnternet.Text = "";
            txtMaas.Text = "";
            txtEkstra.Text = "";
            rchNotlar.Text = "";


        }
        
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistesi();
            temizle();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Expenses(Month,Year,Electricity,Water,NaturalGas,Internet,Salaries,Extra,Notes) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalGaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtİnternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", rchNotlar.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Gider Tabloya Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistesi();
            temizle();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {


            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr !=null)
            {
                txtId.Text = dr["Id"].ToString();
                cmbAy.Text = dr["Month"].ToString();
                cmbYıl.Text = dr["Year"].ToString();
                txtElektrik.Text = dr["Electricity"].ToString();
                txtSu.Text = dr["Water"].ToString();
                txtDogalGaz.Text = dr["NaturalGas"].ToString();
                txtİnternet.Text = dr["Internet"].ToString();
                txtMaas.Text = dr["Salaries"].ToString();
                txtEkstra.Text = dr["Extra"].ToString();
                rchNotlar.Text = dr["Notes"].ToString();


            }


        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From Expenses where Id=@p1",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1",txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            giderlistesi();
            MessageBox.Show("Gider Listeden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            temizle();
            



        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Expenses  set  Month=@p1,Year=@p2,Electricity=@p3,Water=@p4,NaturalGas=@p5,Internet=@p6,Salaries=@p7,Extra=@p8,Notes=@p9 where Id=@p10",bgl.baglanti());


            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalGaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtİnternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", rchNotlar.Text);
            komut.Parameters.AddWithValue("@p10", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Gider Bilgisi Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            giderlistesi();
            temizle();





        }
    }
}
