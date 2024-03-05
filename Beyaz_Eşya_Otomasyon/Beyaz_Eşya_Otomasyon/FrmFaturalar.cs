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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();


        void listele()
        { 
            SqlDataAdapter da = new SqlDataAdapter("Select*From  İnvoiceİnformation",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }

        void temizle()
        {
            txtId.Text = "";
            txtSeri.Text = "";
            txtSiraNo.Text = "";
            mskTarih .Text= "";
            mskSaat.Text = "";
            txtVergiD.Text = "";
            txtAlici.Text = "";
            txtTeslimAlan.Text = "";
            txtTeslimEden.Text = "";
            txtÜrünAdi.Text = "";
            txtMiktar.Text = "";
            txtTutar.Text = "";
            txtFiyat.Text = "";
            txtFirma.Text = "";

        }




        private void FrmFaturalar_Load(object sender, EventArgs e)
        {

            listele();
            temizle();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
           
            if(txtFaturaId.Text=="" )
            //Burası Boşsa
            {

                SqlCommand komut = new SqlCommand("insert into  İnvoiceİnformation(Serial,SequenceNumber,Date,Hour,TaxAuthority,Receiver, Submitter,DeliveryReceive)values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", txtSeri.Text);
                komut.Parameters.AddWithValue("@p2", txtSiraNo.Text);
                komut.Parameters.AddWithValue("@p3", mskTarih.Text);
                komut.Parameters.AddWithValue("@p4", mskSaat.Text);
                komut.Parameters.AddWithValue("@p5", txtVergiD.Text);
                komut.Parameters.AddWithValue("@p6", txtAlici.Text);
                komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme  Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();

               

            }
            //Firma  Carisi

            if (txtFaturaId.Text !=""   && comboBox1.Text == "Firma")//null değer değilse içerisinde bir değer varsa
            {
                double tutar, miktar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);

                miktar = Convert.ToDouble(txtMiktar.Text);

                tutar = miktar * fiyat;
                txtTutar.Text = tutar.ToString();

                SqlCommand komut = new SqlCommand("insert into İnvoiceDetail(ProductName,Amount,Price,Total,İnvoiceId ) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", txtÜrünAdi.Text);
                komut.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));//tutarı kendi hesaplıcak
                komut.Parameters.AddWithValue("@p5", txtFirma.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();




                //Hareket Tablosuna Veri Girişi

                SqlCommand komut1 = new SqlCommand("insert into  CompanieMovements(ProductId,Piece,Employee,Companies,Price,Total,InvoiceId,Date) values(@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut1.Parameters.AddWithValue("@h1", txtÜrünİd.Text);
                komut1.Parameters.AddWithValue("@h2", txtMiktar.Text);
                komut1.Parameters.AddWithValue("@h3", txtPersonel.Text);
                komut1.Parameters.AddWithValue("@h4", txtFirma.Text);
                komut1.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                komut1.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                komut1.Parameters.AddWithValue("@h7", txtFaturaId.Text);
                komut1.Parameters.AddWithValue("@h8", mskTarih.Text);
  
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();




                //STOK SAYISINI AZALTMA 

                SqlCommand komut2 = new SqlCommand("update Products set Piece=Piece-@s1 where Id=@s2 ",bgl.baglanti());
                komut2.Parameters.AddWithValue("@s1",txtMiktar.Text);
                komut2.Parameters.AddWithValue("@s2",txtÜrünİd.Text);
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya Ait Ürün  Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }

           


            //Müşteri Carisi

            if (txtFaturaId.Text != "" && comboBox1.Text== "Müşteri" )//null değer değilse içerisinde bir değer varsa
            {
                double tutar, miktar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);

                miktar = Convert.ToDouble(txtMiktar.Text);

                tutar = miktar * fiyat;
                txtTutar.Text = tutar.ToString();

                SqlCommand komut = new SqlCommand("insert into İnvoiceDetail(ProductName,Amount,Price,Total,İnvoiceId ) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtÜrünAdi.Text);
                komut.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));//tutarı kendi hesaplıcak
                komut.Parameters.AddWithValue("@p5", txtFirma.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();



                //Hareket Tablosuna Veri Girişi

                SqlCommand komut1 = new SqlCommand("insert into  CustomerMovements(ProductId,Piece,Employee,Customer,Price,Total,InvoiceId,Date) values(@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut1.Parameters.AddWithValue("@h1", txtÜrünİd.Text);
                komut1.Parameters.AddWithValue("@h2", txtMiktar.Text);
                komut1.Parameters.AddWithValue("@h3", txtPersonel.Text);
                komut1.Parameters.AddWithValue("@h4", txtFirma.Text);
                komut1.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                komut1.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                komut1.Parameters.AddWithValue("@h7", txtFaturaId.Text);
                komut1.Parameters.AddWithValue("@h8", mskTarih.Text);

                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();




                //STOK SAYISINI AZALTMA 

                SqlCommand komut2 = new SqlCommand("update Products set Piece=Piece-@s1 where Id=@s2 ", bgl.baglanti());
                komut2.Parameters.AddWithValue("@s1", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@s2", txtÜrünİd.Text);
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün  Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();


            }


        }

       

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["İnvoiceİnformationId"].ToString();
                txtSeri.Text = dr["Serial"].ToString();
                txtSiraNo.Text = dr["SequenceNumber"].ToString();
                mskTarih.Text = dr["Date"].ToString();
                mskSaat.Text = dr["Hour"].ToString();
                txtVergiD.Text = dr["TaxAuthority"].ToString();
                txtAlici.Text = dr["Receiver"].ToString();
                txtTeslimEden.Text = dr["Submitter"].ToString();
                txtTeslimAlan.Text = dr["DeliveryReceive"].ToString();



        }   }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From İnvoiceİnformation  where İnvoiceİnformationId=@p1", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
            listele();
            temizle();

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update İnvoiceİnformation set Serial=@p1,SequenceNumber=@p2,Date=@p3,Hour=@p4,TaxAuthority=@p5,Receiver=@p6,Submitter=@p7,DeliveryReceive=@p8 where İnvoiceİnformationId=@p9",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtSeri.Text);
            komut.Parameters.AddWithValue("@p2", txtSiraNo.Text);
            komut.Parameters.AddWithValue("@p3", mskTarih.Text);
            komut.Parameters.AddWithValue("@p4", mskSaat.Text);
            komut.Parameters.AddWithValue("@p5", txtVergiD.Text);
            komut.Parameters.AddWithValue("@p6", txtAlici.Text);
            komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            komut.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@p9",  txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();









        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();//fr fatura detaydaki özellikler ulaşmam için gerekli olan nesnem
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);


            if(dr !=null)
            {
                fr.id = dr["İnvoiceİnformationId"].ToString();

            }
            fr.Show();



        }

        private void btnBul_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("Select ProductName ,SellingPrice From Products where  Id=@p1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtÜrünİd.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {

                txtÜrünAdi.Text = dr[0].ToString();
                txtFiyat.Text = dr[1].ToString();


            }
            bgl.baglanti();









        }

     
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
