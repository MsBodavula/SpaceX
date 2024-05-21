using MessageInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpaceZ
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SpacecraftCallBack : IClient
    {
        /*
         * GetMessage By Spacecraft
         */
        public void GetMessage(string message, string spacecraftname)
        {
            ((MainWindow)Application.Current.MainWindow).TakeMessage(message, spacecraftname);
        }


        public void GetUpdate(int value, string spacecraftname)
        {
           switch(value)
            {
                case 0:
                    {
                          ((MainWindow)Application.Current.MainWindow).AddSpacecraftToList(spacecraftname);

                        break;
                    }

                case 1:
                    {
                        ((MainWindow)Application.Current.MainWindow).RemoveSpacecraftFromList(spacecraftname);
                        break;
                    }
            }
        }

        public void PlaceHolder()
        {
            throw new NotImplementedException();
        }
    }
}
