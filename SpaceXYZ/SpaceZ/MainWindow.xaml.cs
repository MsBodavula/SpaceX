﻿using MessageInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows;
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
        private string PayloadType;
        private string PayloadName;
        DispatcherTimer dt;

        public MainWindow(string SpacecraftName, string PayloadName, string SpacecraftOrbit, string PayloadType)
        {

            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();
            this.SpacecraftName = SpacecraftName;
            SpaceCraftNameTextBox.Text = this.SpacecraftName;
            this.PayloadName = PayloadName;
            TextPayload.Text = this.PayloadName;
            this.SpacecraftOrbit = SpacecraftOrbit;
            this.PayloadType= PayloadType;
            Console.WriteLine("Yes");
        }

        public int timer = 0;
        private void dtTicker(object sender, EventArgs e)
        {
            timer += 1;
            lblTimer.Content = timer.ToString();

            if (Convert.ToInt32(SpacecraftOrbit) == timer)
            {
                this.dt.Stop();

                lblTimer.Content = "ORBIT REACHED";

            }
            server.UpdateAltitude(SpacecraftName, timer);
        }



        public void TakeMessage(string message, string spacecraftname)
        {
            DisplayTextBox.Text += spacecraftname + " =: " + message + "\n";
            DisplayTextBox.ScrollToEnd();
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
            this.dt.Interval = TimeSpan.FromSeconds(2);
            this.dt.Tick += dtTicker;
            this.dt.Start();

            int returnValue = server.Login(SpaceCraftNameTextBox.Text, this.SpacecraftOrbit);
            if (returnValue == 1)
            {
                MessageBox.Show("You are already added to Message Queue. Try Again!!");
            }
            else if (returnValue == 0)
            {
                MessageBox.Show(SpaceCraftNameTextBox.Text + " is added to Queue");
                SpaceCraftNameTextBox.IsEnabled = false;
                LoginButton.IsEnabled = false;
                WelcomeLabel.Content = "[Welcome" + " " + SpaceCraftNameTextBox.Text + "]";

                //LOAD SPACECRAFT LIST
                LoadSpacecraftList(server.GetCurrentUsers());

            }
        }




        private void LoadSpacecraftList(List<string> spacecrafts)
        {
            foreach (var spacecraft in spacecrafts)
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
            if (spacecraftConnectedList.Items.Contains(spacecraftname))
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

        private void btnPayload_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("C:\\Users\\vbodavul\\Downloads\\Deep-Space-Network-Mission-Control\\Deep-Space-Network-Mission-Control-DupleX-Communication-System-For-Spacecrafts-master\\SpaceXYZ\\Payload\\bin\\Debug\\Payload.exe");
            p.StartInfo.Arguments = SpaceCraftNameTextBox.Text + " " + TextPayload.Text;
            p.Start();
            btnPayload.IsEnabled = false;

        }
    }
}
