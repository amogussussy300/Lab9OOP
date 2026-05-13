using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab9OOP
{
    public partial class Form1 : Form
    {
        private const string HOST = "235.5.5.1";
        private const int PORT = 8001;
        private const int TTL = 20;

        private UdpClient? client;        // multicast
        private UdpClient? chatClient;    // unicast для ЛС
        private readonly IPAddress groupAddress = IPAddress.Parse(HOST);

        private bool alive;
        private bool isLoggedIn;
        private string myName = "";
        private int myChatPort;

        private record AdItem(string Name, IPAddress Ip, int ChatPort);
        private readonly List<AdItem> ads = new();   // параллельно adsListBox.Items

        public Form1()
        {
            InitializeComponent();
        }

        // --- Кнопки ---

        private void loginButton_Click(object sender, EventArgs e)
        {
            string name = userNameTextBox.Text.Trim();
            if (name == "") { MessageBox.Show("Введите имя."); return; }
            myName = name;

            try
            {
                Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                socket.Bind(new IPEndPoint(IPAddress.Any, PORT));
                client = new UdpClient { Client = socket };
                client.JoinMulticastGroup(groupAddress, TTL);

                chatClient = new UdpClient(0);
                myChatPort = ((IPEndPoint)chatClient.Client.LocalEndPoint!).Port;

                alive = true;
                isLoggedIn = true;

                loginButton.Enabled = false;
                logoutButton.Enabled = true;
                userNameTextBox.Enabled = false;
                publishButton.Enabled = true;

                Task.Run(ReceiveMulticast);
                Task.Run(ReceiveChat);

                Send($"SYS|{myName} вошёл(ла) на доску объявлений");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                alive = false;
                isLoggedIn = false;
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            if (isLoggedIn) DoLogout();
        }

        private void publishButton_Click(object sender, EventArgs e)
        {
            string title = titleTextBox.Text.Trim();
            string price = priceTextBox.Text.Trim();
            if (title == "" || price == "") { MessageBox.Show("Заполните поля."); return; }

            Send($"AD|{myName}|{title}|{price}|{myChatPort}");
            titleTextBox.Clear();
            priceTextBox.Clear();
        }

        private void adsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = adsListBox.SelectedIndex;
            writeButton.Enabled = isLoggedIn && i >= 0 && i < ads.Count
                                  && ads[i] != null && ads[i].Name != myName;
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            int i = adsListBox.SelectedIndex;
            if (i < 0 || i >= ads.Count || ads[i] == null) return;
            AdItem ad = ads[i];

            string msg = Microsoft.VisualBasic.Interaction.InputBox(
                $"Сообщение продавцу {ad.Name}:", "Написать", "");
            if (string.IsNullOrWhiteSpace(msg)) return;

            try
            {
                using UdpClient sender2 = new();
                byte[] data = Encoding.Unicode.GetBytes($"MSG|{myName}|{msg}");
                sender2.Send(data, data.Length, new IPEndPoint(ad.Ip, ad.ChatPort));
            }
            catch (Exception ex) { MessageBox.Show("Не удалось отправить: " + ex.Message); }
        }

        // --- Потоки приёма ---

        private void ReceiveMulticast()
        {
            try
            {
                while (alive)
                {
                    IPEndPoint? remote = null;
                    string[] parts = Encoding.Unicode.GetString(client!.Receive(ref remote!)).Split('|');
                    if (parts.Length < 2) continue;

                    if (parts[0] == "AD" && parts.Length >= 5
                        && int.TryParse(parts[4], out int port))
                    {
                        AddAd(parts[1], parts[2], parts[3], remote!.Address, port);
                    }
                    else if (parts[0] == "SYS")
                    {
                        Invoke(() => { ads.Insert(0, null!); adsListBox.Items.Insert(0, "→ " + parts[1]); });
                    }
                }
            }
            catch (ObjectDisposedException) { }
            catch (SocketException) { }
        }

        private void ReceiveChat()
        {
            try
            {
                while (alive)
                {
                    IPEndPoint? remote = null;
                    string[] parts = Encoding.Unicode.GetString(chatClient!.Receive(ref remote!)).Split('|');
                    if (parts.Length >= 3 && parts[0] == "MSG")
                    {
                        string from = parts[1], text = parts[2];
                        Invoke(() => MessageBox.Show(this, text, "ЛС от " + from));
                    }
                }
            }
            catch (ObjectDisposedException) { }
            catch (SocketException) { }
        }

        private void AddAd(string name, string title, string price, IPAddress ip, int port)
        {
            string line = $"[{DateTime.Now:HH:mm:ss}] {name}: {title} – {price} руб.";
            Invoke(() =>
            {
                ads.Insert(0, new AdItem(name, ip, port));
                adsListBox.Items.Insert(0, line);
            });
        }

        // --- Утилиты ---

        private void Send(string packed)
        {
            if (client == null) return;
            byte[] data = Encoding.Unicode.GetBytes(packed);
            client.Send(data, data.Length, HOST, PORT);
        }

        private void DoLogout()
        {
            try { Send($"SYS|{myName} покинул(а) доску объявлений"); } catch { }
            alive = false;

            try { client?.DropMulticastGroup(groupAddress); } catch { }
            client?.Close(); client = null;
            chatClient?.Close(); chatClient = null;

            ads.Clear();
            adsListBox.Items.Clear();
            isLoggedIn = false;

            loginButton.Enabled = true;
            logoutButton.Enabled = false;
            userNameTextBox.Enabled = true;
            publishButton.Enabled = false;
            writeButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isLoggedIn) DoLogout();
        }
    }
}
