using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beyaz_Eşya_Otomasyon
{
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();

        }




        FrmUrunler fr;//FrmUrunler sınıfından fr isimli  bir tane nesne türettim
        private void btnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr == null)
            {
                fr = new FrmUrunler(); //tanımlanan nesneye bu komutu yazdıysak yeniden acsın yoksa acmasın
                fr.MdiParent = this;
                fr.Show();
            }

        }

        FrmMusteriler fr1;
        private void btnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr1 == null)
            {
                fr1 = new FrmMusteriler(); 
                fr1.MdiParent = this;
                fr1.Show();
            }
        }
        FrmFirmalar fr2;
        private void btnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr2 == null)
            {
                fr2 = new FrmFirmalar(); 
                fr2.MdiParent = this;
                fr2.Show();
            }
        }


        FrmPersonel fr3;
        private void btnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr3 == null)
            {
                fr3 = new FrmPersonel();
                fr3.MdiParent = this;
                fr3.Show();
            }
        }

        FrmRehber fr4;

        private void btnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr4 == null)
            {
                fr4 = new FrmRehber();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }


        FrmGiderler fr5;
        private void btnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr5==null)
            {

                fr5 = new FrmGiderler();
                fr5.MdiParent = this;
                fr5.Show();

            }



        }
        FrmBankalar fr6;

        private void btnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr6==null)
            {
                fr6 = new FrmBankalar();
                fr6.MdiParent = this;
                fr6.Show();
            }

        }

        FrmFaturalar fr7;

        private void btnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr7==null)
            {
                fr7 = new FrmFaturalar();
                fr7.MdiParent = this;
                fr7.Show();



            }

        }

        FrmNotlar fr8;
        private void btnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (fr8 == null)
            {
                fr8 = new FrmNotlar();
                fr8.MdiParent = this;//Bu formda mdi olarak açılsın
                fr8.Show();//fr8 bu formda göster
            }


        }

        FrmHareketler fr9;
        private void btnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if(fr9==null)
            {

                fr9 = new FrmHareketler();
                fr9.MdiParent = this;
                fr9.Show();


            }


        }

        FrmStoklar fr11;
        private void btnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (fr11 == null)
            {
                fr11 = new FrmStoklar();
                fr11.MdiParent = this;
                fr11.Show();
            }


        }

        FrmAyarlar fr12;
        private void btnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr12==null)
            {

                fr12 = new FrmAyarlar();

                fr12.Show();

            }
        }




        FrmKasa fr13;
        private void btnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if(fr13==null)
            {
                fr13 = new FrmKasa();
                fr13.ad = kullanici;
                fr13.MdiParent = this;
                fr13.Show();
            }

        }


        public string kullanici;
        private void FrmAnaModul_Load(object sender, EventArgs e)
        {

            fr14 = new FrmAnaSayfa();

            fr14.MdiParent = this;
            fr14.Show();


        }


        FrmAnaSayfa fr14;
        private void btnAnaSayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr14==null)
            {

                fr14 = new FrmAnaSayfa();
              
                fr14.MdiParent = this;
                fr14.Show();




            }



        }
    }
}
