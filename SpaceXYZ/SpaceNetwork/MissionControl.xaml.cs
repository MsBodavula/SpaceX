using MessageInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows;

namespace SpaceNetwork
{
    // Interaction logic for MissionControl.xaml
    public partial class MissionControl : Window
    {
        public static IMessagingService server;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;

        public MissionControl()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();
            UpdateData();
        }

        public void UpdateData()
        {
            List<String> listS = server.GetCurrentUsers();
            listNonActiveSpacecrafts.Items.Clear();
            comboNewSpacecrafts.Items.Clear();
            listActiveSpacecrafts.Items.Clear();
            comboNewSpacecrafts.Items.Add("Select");
            foreach (var craft in listS)
            {
                if (!MainWindow.ActiveSpacecrafts.Contains(craft))
                {
                    MainWindow.ActiveSpacecrafts.Add(craft);
                }
                if (MainWindow.NonActiveSpacecrafts.Contains(craft))
                {
                    MainWindow.NonActiveSpacecrafts.Remove(craft);
                }
            }

            foreach (var name in MainWindow.ActiveSpacecrafts)
            {
                if (!listActiveSpacecrafts.Items.Contains(name))
                {
                    listActiveSpacecrafts.Items.Add(name);

                }
            }
            foreach (var name in MainWindow.NonActiveSpacecrafts)
            {
                if (!listNonActiveSpacecrafts.Items.Contains(name))
                {
                    listNonActiveSpacecrafts.Items.Add(name);
                    comboNewSpacecrafts.Items.Add(name);
                }

            }

        }

        private void btnLaunchSpacecraft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string spacecraft = comboNewSpacecrafts.SelectedItem.ToString();
                if (spacecraft != "Select")
                {
                    if (!MainWindow.ActiveSpacecraftProcess.ContainsKey(spacecraft))
                    {
                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo("C:\\Users\\schemudu\\Desktop\\Software_project\\SpaceXYZ\\SpaceXYZ\\bin\\Debug\\SpaceZ.exe");
                        p.StartInfo.Arguments = spacecraft + " " + MainWindow.AllSpacecrafts[spacecraft].PayloadLink.Name + " " + MainWindow.AllSpacecrafts[spacecraft].Orbit.ToString();

                        p.Start();
                        this.Close();
                        MainWindow.ActiveSpacecraftProcess.Add(spacecraft, p);
                    }
                    else
                    {
                        MessageBox.Show("Already in Message Queue");
                    }
                    var mainWin = new MainWindow();
                    mainWin.Show();
                    this.Close();
                }

                UpdateData();
            }
            catch
            {
                MessageBox.Show("Please Select Something from dropdown");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void btnAddNewSpacecraft_Click(object sender, RoutedEventArgs e)
        {
            var newSpaceCraft = new NewSpacecraft();
            newSpaceCraft.Show();
            this.Close();
        }
    }
}
