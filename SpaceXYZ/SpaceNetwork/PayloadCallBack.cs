using MessageInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SpaceNetwork
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PayloadCallBack : IClient
    {
        public void GetMessage(string message, string spacecraftname)
        {
            throw new NotImplementedException();
        }

        public void GetUpdate(int value, string spacecraftname)
        {
            throw new NotImplementedException();
        }

        public void PlaceHolder()
        {
            throw new NotImplementedException();
        }
    }
}
