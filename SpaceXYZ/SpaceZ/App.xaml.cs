using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SpaceZ
{
    // Interaction logic for App.xaml
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
     
          

           // MessageBox.Show("Arg =" + e.Args[0].ToString());
            string SpacecraftName = e.Args[0].ToString();
            string PayloadName = e.Args[1].ToString();
            string SpacecraftOrbit = e.Args[2].ToString();
            string PayloadType = e.Args[3].ToString();
            MainWindow window = new MainWindow(SpacecraftName, PayloadName, SpacecraftOrbit, PayloadType);
            this.MainWindow = window;



            window.Show();

        }
    }
}
