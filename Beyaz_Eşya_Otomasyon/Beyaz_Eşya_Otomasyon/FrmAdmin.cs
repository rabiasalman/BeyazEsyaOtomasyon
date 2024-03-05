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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

       
         SqlBaglantisi bgl=new  SqlBaglantisi();

        private void button1_MouseHover(object sender, EventArgs e)
        {
            btnGirisYap.BackColor = Color.Yellow;

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            btnGirisYap.BackColor = Color.Red;



        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("Select*From Admin where  UserName=@p1 and Password=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtKullanici.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if(dr.Read())
            {
                FrmAnaModul fr = new FrmAnaModul();
                fr.kullanici = txtKullanici.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
            bgl.baglanti().Close();
          

        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
