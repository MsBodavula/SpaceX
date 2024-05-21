using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace MessageInterface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMessagingService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IMessagingService
    {
        //Used while Logging into the service
        [OperationContract]
        int Login(string SpacecraftName, string SpacecraftOrbit);

        //Send Message to all Methods
        [OperationContract]
        void SendMessage(string message, string SpacecraftName);

        //Logout Contract
        [OperationContract]
        void Logout();


        //To get all current logged in Users
        [OperationContract]
        List<string> GetCurrentUsers();

        //To get all Payloads
        [OperationContract]
        List<string> GetCurrentPayloads();


        //To Get Current Logs
        [OperationContract]
        StringBuilder GetAllLogs();


        //To Request Telemetry
        [OperationContract]
        string RequestTelemetry(string spacecraft, int type);

        //To Request Payload Data
        [OperationContract]
        ConcurrentDictionary<string, string> RequestPayloadData(string payload);

        //To Request Spacecraft of Payload
        [OperationContract]
        string GetVechileofPayload(string payload);

        //Used while Logging out by Server
        [OperationContract]
        void LogoutByServer(string Spacecarft);

        //Used to check if the user got added
        [OperationContract]
        bool UsersAdded(string spacecraft, string orbit, string payload, string payloadType);


        //Update Altitude
        [OperationContract]
        void UpdateAltitude(string Spacecraft, int timer);
    }
}
