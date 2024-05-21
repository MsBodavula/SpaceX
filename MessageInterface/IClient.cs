using System.ServiceModel;

namespace MessageInterface
{
    public interface IClient
    {
        [OperationContract]
        void PlaceHolder();


        [OperationContract]
        void GetMessage(string message, string spacecraftname);


        [OperationContract]
        // 0 is login and 1 is logoff
        void GetUpdate(int value, string spacecraftname);
    }
}