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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }


        SqlBaglantisi bgl = new SqlBaglantisi();


        void listele()
        {

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("Select *From Notes", bgl.baglanti());

            da.Fill(dt);

            gridControl1.DataSource = dt;



        }


        void temizle()
        {
            txtId.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtOlusturan.Text = "";
            txtBaslık.Text = "";
            txtHitap.Text = "";
            rchDetay.Text = "";


        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {

            listele();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Notes (NoteDate,NoteHour,NoteTitle,NoteConstituent,NoteHitap,NoteDetail) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtBaslık.Text);
            komut.Parameters.AddWithValue("@p4", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@p5", txtHitap.Text);
            komut.Parameters.AddWithValue("@p6", rchDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Not Bilgisi Sisteme Kaydedildi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["NoteId"].ToString();
                mskTarih.Text = dr["NoteDate"].ToString();
                mskSaat.Text = dr["NoteHour"].ToString();
                txtBaslık.Text = dr["NoteTitle"].ToString();
                txtOlusturan.Text = dr["NoteConstituent"].ToString();
                txtHitap.Text = dr["NoteHitap"].ToString();
                rchDetay.Text = dr["NoteDetail"].ToString();
               
            }
    }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From Notes Where NoteId=@p1 ",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Not Sistemden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Notes set NoteDate=@p1,NoteHour=@p2,NoteTitle=@p3,NoteConstituent=@p4,NoteHitap=@p5,NoteDetail=@p6 where NoteId=@p7 ",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtBaslık.Text);
            komut.Parameters.AddWithValue("@p4", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@p5", txtHitap.Text);
            komut.Parameters.AddWithValue("@p6", rchDetay.Text);
            komut.Parameters.AddWithValue("@p7", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Not Bilgisi Sistemde Güncellendi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            temizle();

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

            //Çift tıklandığında Not Deyat Formu Gelicek

            //Bunu KUllanbilmek için diğer formdan bir deişken oluşturmamız gerek

            FrmNotDetay fr = new FrmNotDetay();

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!=null)

            {

                fr.metin = dr["NoteDetail"].ToString();



            }
            fr.Show();



        }
    }
}
