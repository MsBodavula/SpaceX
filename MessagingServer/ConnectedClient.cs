using MessageInterface;

namespace MessagingServer
{
    // To Hold SpaceCraft Name and Data
    public class ConnectedClient
    {

        public IClient connection;
        public string SpaceCraftName { get; set; }
        public string PayloadName { get; set; }
        public string PayloadType { get; set; }

        public string Orbit { get; set; }

        public int longitude { get; set; }
        public int latitude { get; set; }
        public int altitude { get; set; }
        public int temperatureKelvin { get; set; }
        public int timeToOrbit { get; set; }
    }

    //To hold the Data of Payload of type Scientific
    public class ScientificPayload
    {
        public int rainfall { get; set; }
        public int humidity { get; set; }
        public int snow { get; set; }
        public ScientificPayload(int rainfall, int humidity, int snow)
        {
            this.rainfall = rainfall;
            this.humidity = humidity;
            this.snow = snow;
        }

    }


    //To hold the data of payload of type Communication

    public class CommunicationPayload
    {
        public int uplink { get; set; }
        public int downlink { get; set; }
        public int activeTransponders { get; set; }
        public CommunicationPayload(int uplink, int downlink, int activeTransponders)
        {
            this.uplink = uplink;
            this.downlink = downlink;
            this.activeTransponders= activeTransponders;
        }
    }

    //To hold the data of Payload of type Spy
    public class SpyPayload
    {
        public string imageData { get; set; }
        public byte[] image { get; set; }

        public SpyPayload(string imageData, byte[] image)
        {
            this.imageData = imageData;
            this.image = image;
        }
    }

    public class PayloadData
    {
        public string imageData { get; set; }
        public string message { get; set; }
    }
}
