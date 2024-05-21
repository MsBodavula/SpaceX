using MessageInterface;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows;
namespace SpaceNetwork
{
    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        public static IMessagingService server;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;

        public static Dictionary<string, Vehicle> AllSpacecrafts = new Dictionary<string, Vehicle>();
        public static Dictionary<string, Payload> AllPayloads = new Dictionary<string, Payload>();
        public static List<string> ActiveSpacecrafts = new List<string>();
        public static List<string> NonActiveSpacecrafts = new List<string>();
        public static Dictionary<string, Process> ActiveSpacecraftProcess = new Dictionary<string, Process>();
        public static Dictionary<string, Process> ActivePayloadProcess = new Dictionary<string, Process>();

        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            var missionControlSys = new MissionControl();
            missionControlSys.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewSpaceCraft = new ViewSpacecrafts();
            var payload = new ViewPayload();
            viewSpaceCraft.Show();
            payload.Show();
            this.Close();
        }

        private void Show_Logs_Click(object sender, RoutedEventArgs e)
        {
            DisplayLogs.Text = server.GetAllLogs().ToString();
            DisplayLogs.ScrollToEnd();
        }


    }
}
