﻿using System;
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
    public partial class FrmFaturaUrunDetay : Form
    {
        public FrmFaturaUrunDetay()
        {
            InitializeComponent();
        }

        public string id;

        SqlBaglantisi bgl = new SqlBaglantisi();

        void listele()
        {



            SqlDataAdapter da = new SqlDataAdapter("Select *From İnvoiceDetail where  İnvoiceId='" + id + "'", bgl.baglanti()); //SqlCommandları kullanmadığımız yerde tek tırnakları kullanıyoruz

            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }


        private void FrmFaturaUrunDetay_Load(object sender, EventArgs e)
        {



            listele();


        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme fr = new FrmFaturaUrunDuzenleme();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.urunid = dr["İnvoiceProductId"].ToString();



            }
            fr.Show();
            //this.Hide();
        }
    }
}
