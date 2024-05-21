using MessageInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SpaceNetwork
{
    // Interaction logic for ViewSpacecrafts.xaml
    public partial class ViewSpacecrafts : Window
    {
        public static IMessagingService server;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;
        private string SelectedSpacecraft;
        private string SelectedSpacecraftForData;


        DispatcherTimer dt;


        public ViewSpacecrafts()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();
            Update();
        }

        public void Update()
        {
            List<String> listS = server.GetCurrentUsers();
            comboBoxSpacecrafts.Items.Clear();

            comboBoxSpacecrafts.Items.Add("Select");
            spacecraftConnectedList.Items.Clear();

            foreach (var craft in listS)
            {
                if (!spacecraftConnectedList.Items.Contains(craft))
                {
                    spacecraftConnectedList.Items.Add(craft);
                    comboBoxSpacecrafts.Items.Add(craft);
                }

            }

            foreach (var craft in MainWindow.AllSpacecrafts.Keys)
            {
                if (!comboForData.Items.Contains(craft))
                {

                    comboForData.Items.Add(craft);
                }

            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var viewMain = new MainWindow(); //create your new form.
            viewMain.Show(); //show the new form.
            this.Close();
        }


        private void ButtonStartTelemetry_Click(object sender, RoutedEventArgs e)
        {

            int epoch = 5 * 1000;
            StartTelemetryCycle(epoch);

            LabelTelemetry.Content = "Telemetry Start : " + SelectedSpacecraft;

        }
        private void StartTelemetryCycle(int epoch)
        {
            ButtonStopTelemetry.Visibility = System.Windows.Visibility.Visible;
            this.dt = new DispatcherTimer();
            TextTelemetry.Text = "";
            this.dt.Interval = TimeSpan.FromSeconds(2);
            this.dt.Tick += dtTicker;
            this.dt.Start();
        }

        private void ButtonStopTelemetry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dt.Stop();
                ButtonStopTelemetry.Visibility = System.Windows.Visibility.Hidden;
            }
            catch
            {
                MessageBox.Show("Select a spacecraft from dropdown");
            }
            
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {

        }


        private void dtTicker(object sender, EventArgs e)
        {
            string data = server.RequestTelemetry(SelectedSpacecraft, 1);
            if (data == "ORBIT REACHED")
            {
                TextTelemetry.Text += "\n=[ORBIT REACHED FOR : " + SelectedSpacecraft + "]=";
                this.dt.Stop();
            }
            else if(data == "Please select a spacecraft from Dropdown")
            {
                TextTelemetry.Text += "Please select a spacecraft from Dropdown";
                this.dt.Stop();
            }
            else
            {
                TextTelemetry.Text += SelectedSpacecraft + " :\n" + data;
                TextTelemetry.Text += "\n_______________\n";
            }
            TextTelemetry.ScrollToEnd();
        }

        private void btnDeorbit_Click(object sender, RoutedEventArgs e)
        {
            string spacecraft = SelectedSpacecraft;
            try 
            { 
                Process P = MainWindow.ActiveSpacecraftProcess[spacecraft];
                P.Kill();
                MessageBox.Show(spacecraft + " is DEORBITED");

                server.LogoutByServer(spacecraft);
                Window mainWindow = this;

                // Get all open windows in the current application
                foreach (Window window in Application.Current.Windows)
                {
                    // Check if the window is not the MainWindow
                    if (window != mainWindow)
                    {
                        // Close the window
                        window.Close();
                    }
                }
                var mainWin = new MainWindow();
                mainWin.Show();

                MainWindow.ActiveSpacecraftProcess.Remove(spacecraft);
            }
            catch
            {
                Window mainWindow = this;
                foreach (Window window in Application.Current.Windows)
                {
                    // Check if the window is not the MainWindow
                    if (window != mainWindow)
                    {
                        // Close the window
                        window.Close();
                    }
                }
                var mainWin = new MainWindow();
                mainWin.Show();
                if (spacecraft == "Select")
                {
                    MessageBox.Show("You haven't selected any from dropdown");
                }
                else
                {
                    server.LogoutByServer(spacecraft);
                    MessageBox.Show("It already got deorbited");
                    MainWindow.ActiveSpacecraftProcess.Remove(spacecraft);
                    this.Close();
                }

                // Get all open windows in the current application
                
                
            }
            
        }

        private void comboBoxSpacecrafts_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            SelectedSpacecraft = comboBoxSpacecrafts.SelectedItem.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Data = "Name : " + MainWindow.AllSpacecrafts[SelectedSpacecraftForData].Name + "\n" + "Orbit : " + MainWindow.AllSpacecrafts[SelectedSpacecraftForData].Orbit + "\n";
            Data += "Payload Name : " + MainWindow.AllSpacecrafts[SelectedSpacecraftForData].PayloadLink.Name + "\n" + "Payload Type : " + MainWindow.AllSpacecrafts[SelectedSpacecraftForData].PayloadLink.Type + "\n";
            txtSpacecraftData.Text = Data;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSpacecraftForData = comboForData.SelectedItem.ToString();
        }
    }
}
