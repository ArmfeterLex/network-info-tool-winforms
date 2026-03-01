using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp17
{
    public partial class Form1 : Form
    {
        [DllImport("wininet.dll")]
        static extern bool InternetGetConnectedState(ref int lpdwFlags, int dwReserved);

        [DllImport("sensapi.dll")]
        private static extern bool IsNetworkAlive(ref int flags);

        private static int NETWORK_ALIVE_LAN = 0x00000001;
        private static int NETWORK_ALIVE_WAN = 0x00000002;

        public Form1()
        {
            InitializeComponent();
        }

        public static bool CheckUrl(string url)
        {
            bool rt = false;
            if (url.ToLower().StartsWith("www."))
            {
                url = "http://" + url;
            }

            HttpWebResponse myResponse = null;
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                rt = true;
            }
            catch (WebException)
            {
                rt = false;
            }
            finally
            {
                if (myResponse != null)
                {
                    myResponse.Close();
                }
            }
            return rt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UriBuilder ubuild = new UriBuilder(@"http:\\rusproject.narod.ru:80");

            MessageBox.Show(ubuild.Host);
            MessageBox.Show(ubuild.Port.ToString());
            MessageBox.Show(ubuild.Scheme);
            MessageBox.Show(ubuild.Uri.ToString());

            UriBuilder builder = new UriBuilder("http://rusproject.narod.ru/");
            builder.Path = "index.htm";
            builder.Fragment = "main";

            Uri muUri = builder.Uri;

            MessageBox.Show(builder.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Net.IPHostEntry host;
            host = System.Net.Dns.GetHostEntry("yandex.ru");
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                MessageBox.Show(ip.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Environment.MachineName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("rusproject.narod.ru");
            if (reply.Status == IPStatus.Success)
            {
                listBox1.Items.Add("Address: " + reply.Address.ToString());
                listBox1.Items.Add("RoundTrip time: " + reply.RoundtripTime);
                listBox1.Items.Add("Time to live: " + reply.Options.Ttl);
                listBox1.Items.Add("Don't fragment: " + reply.Options.DontFragment);
                listBox1.Items.Add("Buffer size: " + reply.Buffer.Length);
            }
            else
            {
                listBox1.Items.Add(reply.Status);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string strIP = "";
            System.Net.IPHostEntry host;
            host = System.Net.Dns.GetHostEntry(strIP);
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                MessageBox.Show(ip.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show(CheckUrl(textBox1.Text).ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int flags = 0;
            bool checkStatus = InternetGetConnectedState(ref flags, 0);
            MessageBox.Show("Подключение к интернету: " + checkStatus.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Environment.UserDomainName + @"\" + Environment.UserName);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool lanAlive = IsNetworkAlive(ref NETWORK_ALIVE_LAN);
            bool wanAlive = IsNetworkAlive(ref NETWORK_ALIVE_WAN);
            MessageBox.Show($"LAN alive: {lanAlive}\nWAN alive: {wanAlive}");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();
            MessageBox.Show(user.Name.ToString());
        }
    }
}