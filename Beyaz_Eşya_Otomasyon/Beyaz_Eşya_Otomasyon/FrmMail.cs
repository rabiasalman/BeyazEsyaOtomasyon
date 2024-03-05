using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;//Kütüphane eklendi
using System.Net.Mail;//kütüphane eklendi



namespace Beyaz_Eşya_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }

        public string mail;

        private void FrmMail_Load(object sender, EventArgs e)
        {

            textEdit1.Text = mail;
       
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesajim = new MailMessage();

            SmtpClient istemci = new SmtpClient();
           

            istemci.Credentials = new System.Net.NetworkCredential("Mail","Şifre");

            istemci.Port = 587;
            istemci.Host = "smtp.gmail.com";
            istemci.EnableSsl = true;
            mesajim.To.Add(textEdit1.Text);
            mesajim.From = new MailAddress("ilhanesmanur72@gmail.com");//kendi mail adresimiz kimden gönderildiği yani

            mesajim.Subject = textEdit2.Text;
            mesajim.Body = richTextBox1.Text;
            istemci.Send (mesajim);


        }
    }
}
