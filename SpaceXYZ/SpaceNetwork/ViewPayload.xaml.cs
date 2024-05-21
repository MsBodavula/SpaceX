using MessageInterface;
using MessagingServer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SpaceNetwork
{
    // Interaction logic for ViewSpacecrafts.xaml
    public partial class ViewPayload : Window
    {
        public static IMessagingService server;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;
        private string SelectedSpacecraft;
        private string SelectedSpacecraftForData;


        DispatcherTimer dt;


        public ViewPayload()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new PayloadCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();
            Update();
        }

        public void Update()
        {
            List<String> listS = server.GetCurrentPayloads();
            comboBoxPayloads.Items.Clear();

            comboBoxPayloads.Items.Add("Select");
            payloadsConnectedList.Items.Clear();

            foreach (var craft in listS)
            {
                if (!payloadsConnectedList.Items.Contains(craft))
                {
                    payloadsConnectedList.Items.Add(craft);
                    comboBoxPayloads.Items.Add(craft);
                    comboForData.Items.Add(craft);
                }

            }

            /*foreach (var craft in MainWindow.AllSpacecrafts.Values)
            {
                if (!comboForData.Items.Contains(craft.PayloadLink.Name))
                {
                    comboForData.Items.Add(craft.PayloadLink.Name);
                }

            }*/

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



        private void StartSendingData()
        {
            StopData.Visibility = System.Windows.Visibility.Visible;
            this.dt = new DispatcherTimer();
            //txtPayloadData.Text = "";
            this.dt.Interval = TimeSpan.FromSeconds(5);
            this.dt.Tick += PayloadTicker;

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

        private void ButtonStopSendingData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dt.Stop();
                StopData.Visibility = System.Windows.Visibility.Hidden;
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
            string data = server.RequestTelemetry(SelectedSpacecraft, 2);
            if (data == "ORBIT REACHED")
            {
                TextTelemetry.Text += "\n=[ORBIT REACHED FOR : " + SelectedSpacecraft + "]=";
                this.dt.Stop();
            }
            else if (data == "Please select a spacecraft from Dropdown")
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


        private void PayloadTicker(object sender, EventArgs e)
        {
            List<PayloadData> payloads = new List<PayloadData>();
            string spacecraft = server.GetVechileofPayload(SelectedSpacecraftForData);
            ConcurrentDictionary<string, string> data = server.RequestPayloadData(spacecraft);
            foreach (PayloadData item in txtPayloadData.Items)
            {
                payloads.Add(item);
            }
            PayloadData Data = new PayloadData()
            {
                imageData = data.Keys.First(),
                message = data.Values.First()
            };
            var items = new ObservableCollection<PayloadData>();
            if (Data.message == "ORBIT REACHED")
            {
                Data.message = "\n=[ORBIT REACHED FOR : " + SelectedSpacecraftForData + "]=";
                this.dt.Stop();
            }

            else if (Data.message == "spy")
            {
                Data.message = "";
            }
            else
            {
                Data.message = Data.message + "\n_______________\n";
            }
            payloads.Add(Data);
            txtPayloadData.ItemsSource = payloads;
        }

        private void btnDeorbit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string spacecraft = server.GetVechileofPayload(SelectedSpacecraft);
                Process P = MainWindow.ActiveSpacecraftProcess[spacecraft];
                P.Kill();
                MessageBox.Show(spacecraft + " is DECOMMISIONED");

                server.LogoutByServer(spacecraft);
                //MainWindow.ActiveSpacecraftProcess.Remove();
                var mainWin = new MainWindow();
                mainWin.Show(); //show the new form.
                this.Close();
            }
            catch
            {
                MessageBox.Show("Spacecraft is DEORBITED");
                var mainWin = new MainWindow();
                mainWin.Show(); //show the new form.
                this.Close();
            }
            
        }

        private void comboBoxSpacecrafts_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            SelectedSpacecraft = comboBoxPayloads.SelectedItem.ToString();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            StartSendingData();

            LabelPayload.Content = "Payload Data Start : " + SelectedSpacecraftForData;
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSpacecraftForData = comboForData.SelectedItem.ToString();
            txtPayloadData.Items.Clear();
        }
    }
}
