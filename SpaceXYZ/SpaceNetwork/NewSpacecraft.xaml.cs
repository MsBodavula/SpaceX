using System;
using System.ServiceModel;
using System.Windows;
using MessageInterface;

namespace SpaceNetwork
{
    // Interaction logic for NewSpacecraft.xaml
    public partial class NewSpacecraft : Window
    {
        public static IMessagingService server;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;
        public NewSpacecraft()
        {
            InitializeComponent();
            comboPayload.Items.Add("Select");
            comboPayload.Items.Add("Scientific");
            comboPayload.Items.Add("Communication");
            comboPayload.Items.Add("Spy");
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();
        }

        private void btnAddSpacecraft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MissionControl mControl = new MissionControl();

                if (txtSpacecraftName.Text != "" && txtOrbitRadius.Text != "" && txtPayloadName.Text != "" && comboPayload.SelectedItem.ToString() != "Select")
                {
                    string vehicleName = txtSpacecraftName.Text;
                    int orbitRadius = Convert.ToInt32(txtOrbitRadius.Text);
                    string payloadName = txtPayloadName.Text;
                    string typePayload = comboPayload.SelectedItem.ToString();
                    Payload newPayload = new Payload(payloadName, typePayload);
                    Vehicle newVehicle = new Vehicle(vehicleName, orbitRadius, newPayload);
                    MainWindow.AllSpacecrafts.Add(vehicleName, newVehicle);
                    mControl.comboNewSpacecrafts.Items.Add(txtSpacecraftName.Text);
                    mControl.listNonActiveSpacecrafts.Items.Add(txtSpacecraftName.Text);
                    MainWindow.NonActiveSpacecrafts.Add(txtSpacecraftName.Text);
                    server.UsersAdded(txtSpacecraftName.Text, txtOrbitRadius.Text, txtPayloadName.Text, comboPayload.SelectedItem.ToString());
                    txtSpacecraftName.Text = "";

                }
                else
                {
                    MessageBox.Show("All fields required !");
                }

                MessageBox.Show("Spacecraft and Payload Added to Queue");

                mControl.UpdateData();
                var missionWindow = new MissionControl();
                missionWindow.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please enter valid Inputs");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var missionWindow = new MissionControl();
            missionWindow.Show();
            this.Close();
        }
    }
}
