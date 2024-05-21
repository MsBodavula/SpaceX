using MessageInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpaceZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string Hex = "#ffcc80";
        public static IMessagingService server;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;
        private string SpacecraftName;
        private string SpacecraftOrbit;
        DispatcherTimer dt;

        public MainWindow(string SpacecraftName, string PayloadName, string SpacecraftOrbit)
        {

            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();
            this.SpacecraftName = SpacecraftName;
            SpaceCraftNameTextBox.Text = this.SpacecraftName;
            TextPayload.Text = PayloadName;
            this.SpacecraftOrbit = SpacecraftOrbit;

        
        }

        public int timer = 0;
        private void dtTicker(object sender, EventArgs e)
        {
            /*
            increament += 2;
            TimerLabel.Content = increament.ToString();
            */
            timer += 1;
            lblTimer.Content = timer.ToString();
            
            if(Convert.ToInt32(SpacecraftOrbit) == timer)
            {
                this.dt.Stop();

                lblTimer.Content = "ORBIT REACHED";
                
            }
          // server.UpdateAltitude(SpacecraftName, timer);
        }

  

        public void TakeMessage(string message, string spacecraftname)
        {
            DisplayTextBox.Text += spacecraftname + ":" + message + "\n";
            DisplayTextBox.ScrollToEnd();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            server.test("HELLO WORLD");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

            if (MessageTextBox.Text.Length == 0)
            {
                return;
            }
            server.SendMessage(MessageTextBox.Text, SpaceCraftNameTextBox.Text);
            TakeMessage(MessageTextBox.Text, "You");
            MessageTextBox.Text = "";
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.dt = new DispatcherTimer();
            this.dt.Interval = TimeSpan.FromSeconds(5);
            this.dt.Tick += dtTicker;
            this.dt.Start();

            int returnValue = server.Login(SpaceCraftNameTextBox.Text, this.SpacecraftOrbit);
            if (returnValue == 1)
            {
                MessageBox.Show("You are already added to Message Queue. Try Again!!");
            }
            else if(returnValue == 0)
            {
                MessageBox.Show(SpaceCraftNameTextBox.Text +" is added to Queue");
                SpaceCraftNameTextBox.IsEnabled = false;
                LoginButton.IsEnabled = false;
                WelcomeLabel.Content = "[Welcome" + " " + SpaceCraftNameTextBox.Text+"]";

                //LOAD SPACECRAFT LIST
                LoadSpacecraftList(server.GetCurrentUsers());
                
            }
        }


      

        private void LoadSpacecraftList(List<string> spacecrafts)
        {
            foreach(var spacecraft in spacecrafts)
            {
                AddSpacecraftToList(spacecraft);
            }
        }

       
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server.Logout();
        }
        

        public void AddSpacecraftToList(string spacecraftname)
        {
            if(spacecraftConnectedList.Items.Contains(spacecraftname))
            {
                return;
            }
            else
            {
                spacecraftConnectedList.Items.Add(spacecraftname);
                return;
            }
        }

        public void RemoveSpacecraftFromList(string spacecraftname)
        {
            if (spacecraftConnectedList.Items.Contains(spacecraftname))
            {
                spacecraftConnectedList.Items.Remove(spacecraftname);
            }
            else
            {
                return;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("C:\\Users\\dmali\\source\\repos\\SpaceZ\\SpaceZ\\bin\\Debug\\SpaceZ.exe");
            // p.StartInfo = new ProcessStartInfo("C:\\Program Files\\WordWeb\\wweb32.exe", this.Arrange(rectangledict));
            //p.WaitForInputIdle();
            p.Start();
        }

        private void btnPayload_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("C:\\Users\\dmali\\source\\repos\\SpaceZ\\Payload\\bin\\Debug\\Payload.exe");
            // p.StartInfo = new ProcessStartInfo("C:\\Program Files\\WordWeb\\wweb32.exe", this.Arrange(rectangledict));
            //p.WaitForInputIdle();
            p.StartInfo.Arguments = SpaceCraftNameTextBox.Text + " " + TextPayload.Text;
            p.Start();
            btnPayload.IsEnabled = false;
       
        }
    }
}
