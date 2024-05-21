using MessageInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

namespace MessagingServer
{
    // Single : one instance of service and all clients gonna share that
    // Multiple : Multithreaded Server and having multiple client handling with separate thread

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class MessagingService : IMessagingService
    {
        //This DS Will Manage all spacecrafts connected to Messaging Service

        public static ConcurrentDictionary<string, ConnectedClient> _connectedSpacecrafts =
            new ConcurrentDictionary<string, ConnectedClient>();
        public static ConcurrentDictionary<string, ConnectedClient> _connectedSpacecraftsInactive =
            new ConcurrentDictionary<string, ConnectedClient>();
        public static ConcurrentDictionary<string, int> _scientificPayloads =
            new ConcurrentDictionary<string, int>();
        public static ConcurrentDictionary<string, int> _communicationPayloads =
            new ConcurrentDictionary<string, int>();
        public static ConcurrentDictionary<string, int> _spyPayloads =
            new ConcurrentDictionary<string, int>();

        public StringBuilder Logs = new StringBuilder();

        public int Login(string SpacecraftName, string SpacecraftOrbit)
        {
            try
            {
                Random r = new Random();
                int longitude = r.Next(-72, 60);
                int latitude = r.Next(-12, -6);
                int altitude = 0;
                int temperatureKelvin = 340;
                int timeToOrbit = 300;

                foreach (var spacecraft in _connectedSpacecrafts)
                {
                    if (spacecraft.Key.ToLower() == SpacecraftName.ToLower())
                    {
                        return 1; //Some spacecraft is already logged in with the same username
                    }
                }
                var establishedSpacecraftConnection = OperationContext.Current.GetCallbackChannel<IClient>();
                ConnectedClient newSpacecraft = new ConnectedClient();
                _connectedSpacecraftsInactive.TryGetValue(SpacecraftName, out newSpacecraft);
                newSpacecraft.connection = establishedSpacecraftConnection;
                newSpacecraft.SpaceCraftName = SpacecraftName;
                newSpacecraft.Orbit = SpacecraftOrbit;
                newSpacecraft.longitude = longitude;
                newSpacecraft.latitude = latitude;
                newSpacecraft.altitude = altitude;
                newSpacecraft.temperatureKelvin = temperatureKelvin;
                newSpacecraft.timeToOrbit = timeToOrbit;
                _connectedSpacecrafts.TryAdd(SpacecraftName, newSpacecraft);
                UpdateHelperForSpacecrafts(0, SpacecraftName);
                _connectedSpacecraftsInactive.TryRemove(SpacecraftName, out newSpacecraft);
                Console.ForegroundColor = ConsoleColor.Green;
                Logs.Append("Client login :" + newSpacecraft.SpaceCraftName.ToString() + " at" + System.DateTime.Now.ToString() + "\n");
                Console.WriteLine("Client login : {0} at {1}", newSpacecraft.SpaceCraftName, System.DateTime.Now);
                Console.WriteLine("Connected Spacecrafts {0}", _connectedSpacecrafts.Count);
                Console.ResetColor();
                return 0;
            }
            catch
            {
                return 1;
            }
            
        }

        //To establish communication between spacecrafts
        public void SendMessage(string message, string SpacecraftName)
        {
            foreach (var spacecraft in _connectedSpacecrafts)
            {
                if (spacecraft.Key.ToLower() != SpacecraftName.ToLower())
                {
                    spacecraft.Value.connection.GetMessage(message, SpacecraftName);
                }
            }
            Logs.Append(SpacecraftName + " : " + message + "\n");
        }

        public void Logout()
        {
            ConnectedClient spacecraft = GetMySpacecraft();
            if (spacecraft != null)
            {
                ConnectedClient removedSpaceCraft;
                _connectedSpacecrafts.TryRemove(spacecraft.SpaceCraftName, out removedSpaceCraft);

                UpdateHelperForSpacecrafts(1, removedSpaceCraft.SpaceCraftName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Client logoff : {0} at {1}", removedSpaceCraft.SpaceCraftName, System.DateTime.Now);
                Logs.Append("Client logout : " + removedSpaceCraft.SpaceCraftName + " at " + System.DateTime.Now + "\n");
                Console.ResetColor();
            }

        }

        public ConnectedClient GetMySpacecraft()
        {
            var establishedSpacecraftConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            foreach (var spacecraft in _connectedSpacecrafts)
            {
                if (spacecraft.Value.connection == establishedSpacecraftConnection)
                {
                    return spacecraft.Value;
                }
            }
            return null;
        }

        private void UpdateHelperForSpacecrafts(int value, string spacecraftname)
        {
            foreach (var spacecraft in _connectedSpacecrafts)
            {
                if (spacecraft.Value.SpaceCraftName.ToLower() != spacecraftname.ToLower())
                {
                    spacecraft.Value.connection.GetUpdate(value, spacecraftname);
                }
            }
        }

        public List<string> GetCurrentUsers()
        {
            List<string> listofspacecrafts = new List<string>();
            foreach (var spacecraft in _connectedSpacecrafts)
            {
                listofspacecrafts.Add(spacecraft.Value.SpaceCraftName);
            }
            return listofspacecrafts;
        }


        public bool UsersAdded(string spacecraft, string orbit, string payload, string payloadType)
        {
            ConnectedClient newSpacecraft = new ConnectedClient();
            newSpacecraft.Orbit = orbit;
            newSpacecraft.SpaceCraftName= spacecraft;
            newSpacecraft.PayloadName= payload;
            newSpacecraft.PayloadType= payloadType;
            return _connectedSpacecraftsInactive.TryAdd(spacecraft, newSpacecraft);
        }

        public List<string> GetCurrentPayloads()
        {
            List<string> listofspacecrafts = new List<string>();
            foreach (var spacecraft in _connectedSpacecrafts)
            {
                listofspacecrafts.Add(spacecraft.Value.PayloadName);
            }
            return listofspacecrafts;
        }

        public StringBuilder GetAllLogs()
        {
            return Logs;
        }

        public string GetVechileofPayload(string payload)
        {
            foreach (var craft in _connectedSpacecrafts)
            {
                if (craft.Value.PayloadName.ToLower() == payload.ToLower())
                {
                    return craft.Value.SpaceCraftName;
                }
            }
            return "";
        }

        public ConcurrentDictionary<string, string> ScientificPayload(string spacecraft)//Will send data for every 5 seconds
        {
            ConcurrentDictionary<string, string> data = new ConcurrentDictionary<string, string>();
            int i1 = 0;
            _scientificPayloads.TryGetValue(spacecraft, out i1);
            ScientificPayload sp = new ScientificPayload(0 + i1, 1 + i1, 2 + i1);
            Dictionary<string, string> RealTimeData = new Dictionary<string, string>();
            int m;
            _scientificPayloads.TryRemove(spacecraft, out m);
            i1 = i1 + 1;
            _scientificPayloads.TryAdd(spacecraft, i1);
            RealTimeData.Add("Snow : ", sp.snow.ToString() + "in");
            RealTimeData.Add("Humidity : ", sp.humidity.ToString() + " %");
            RealTimeData.Add("Rainfall : ", sp.rainfall.ToString() + " mm");
            var entries = RealTimeData.Select(d =>
                    string.Format("\"{0}\": {1}\n", d.Key, string.Join(",", d.Value)));
            data.TryAdd(i1.ToString(), "{\n" + string.Join(",", entries) + "\n}");
            return data;
               
        }

        public ConcurrentDictionary<string, string> CommunicationPayload(string spacecraft)
        {
            ConcurrentDictionary<string, string> data = new ConcurrentDictionary<string, string>();
            int i1 = 10;
            _communicationPayloads.TryGetValue(spacecraft, out i1);
            if(i1 > 10)
                Thread.Sleep(1000);
            CommunicationPayload sp = new CommunicationPayload(100 + i1, 60 + i1, 45 + i1);
            Dictionary<string, string> RealTimeData = new Dictionary<string, string>();
            int m;
            _communicationPayloads.TryRemove(spacecraft, out m);
            i1 = i1 + 10;
            _communicationPayloads.TryAdd(spacecraft, i1 + 10);
            RealTimeData.Add("Uplink : ", sp.uplink.ToString() + " MBps");
            RealTimeData.Add("Downlink : ", sp.downlink.ToString() + " MBps");
            RealTimeData.Add("ActiveTransponders : ", sp.activeTransponders.ToString());
            var entries = RealTimeData.Select(d =>
                    string.Format("\"{0}\": {1}\n", d.Key, string.Join(",", d.Value)));
            data.TryAdd((i1 / 10).ToString(), "{\n" + string.Join(",", entries) + "\n}");
            return data;
        }

        public ConcurrentDictionary<string, string> RequestPayloadData(string payload)
        {
            ConcurrentDictionary<string, string> data = new ConcurrentDictionary<string, string>() { };
            
            string type = "";
            string spacecraft = "";
            foreach (var craft in _connectedSpacecrafts)
            {
                if (craft.Value.SpaceCraftName.ToLower() == payload.ToLower())
                {
                    type = craft.Value.PayloadType;
                    spacecraft = craft.Value.SpaceCraftName;
                    if (Convert.ToInt32(craft.Value.Orbit) <= craft.Value.altitude)
                    {
                        data.TryAdd("", "ORBIT REACHED");
                        return data;
                    }
                    break;
                }
            }
            

            if (type == "Scientific")
            {
                int i1 = 0;
                if(! _scientificPayloads.TryGetValue(spacecraft, out i1))
                {
                    _scientificPayloads.TryAdd(spacecraft, i1);
                }
                return ScientificPayload(spacecraft);
            }
            if (type == "Communication")
            {
                int i1 = 0;
                if (!_communicationPayloads.TryGetValue(spacecraft, out i1))
                {
                    _communicationPayloads.TryAdd(spacecraft, i1);
                }
                return CommunicationPayload(spacecraft);
            }
            if(type == "Spy")
            {
                int i1 = 1;
                if (!_spyPayloads.TryGetValue(spacecraft, out i1))
                {
                    _spyPayloads.TryAdd(spacecraft, 1);
                }
                else
                {
                    if (i1 > 10)
                        Thread.Sleep(2000);
                    _spyPayloads.TryGetValue(spacecraft, out i1);
                    int m;
                    _spyPayloads.TryRemove(spacecraft, out m);
                    i1 = i1 + 1;
                    _spyPayloads.TryAdd(spacecraft, i1);
                }
                data.TryAdd("Images/img"+ i1.ToString() +".jpg", "Spy");
                return data;
            }

            return data;
        }

        public string RequestTelemetry(string spacecraft, int type)
        {
            try
            {
                if (type == 2)
                {
                    foreach (var craft in _connectedSpacecrafts)
                    {
                        if (craft.Value.PayloadName.ToLower() == spacecraft.ToLower())
                        {
                            spacecraft = craft.Value.SpaceCraftName;
                            break;
                        }
                    }
                }
                int orbit = 0;
                Random r = new Random();
                int longitude = r.Next(-90, 90);
                int latitude = r.Next(-180, 180);
                int altitude = 0;
                int temperatureKelvin = 0;
                int timeToOrbit = 0;


                foreach (var craft in _connectedSpacecrafts)
                {
                    if (craft.Value.SpaceCraftName.ToLower() == spacecraft.ToLower())
                    {
                        orbit = Convert.ToInt32(craft.Value.Orbit.ToLower());
                        craft.Value.longitude = r.Next(-90, 90); ;
                        craft.Value.latitude = r.Next(-180, 180);
                        craft.Value.altitude += 1;
                        craft.Value.temperatureKelvin -= 2;
                        craft.Value.timeToOrbit -= 1;
                        longitude = craft.Value.longitude;
                        latitude = craft.Value.latitude;
                        altitude = craft.Value.altitude;
                        temperatureKelvin = craft.Value.temperatureKelvin;
                        timeToOrbit = craft.Value.timeToOrbit;
                        break;
                    }
                }

                Dictionary<string, string> RealTimeData = new Dictionary<string, string>();


                RealTimeData.Add("altitude", altitude.ToString());
                RealTimeData.Add("longitude", longitude.ToString());
                RealTimeData.Add("latitude", latitude.ToString());
                RealTimeData.Add("temperature", temperatureKelvin.ToString());
                RealTimeData.Add("timeToOrbit", timeToOrbit.ToString());

                var entries = RealTimeData.Select(d =>
            string.Format("\"{0}\": {1}\n", d.Key, string.Join(",", d.Value)));

                if (orbit <= altitude)
                {
                    return "ORBIT REACHED";
                }
                return "{\n" + string.Join(",", entries) + "\n}";
            }
            catch
            {
                return "Please select a spacecraft from Dropdown";
            }
        }

        public void LogoutByServer(string Spacecarft)
        {

            if (Spacecarft != null)
            {

                ConnectedClient removedSpaceCraft;
                _connectedSpacecrafts.TryRemove(Spacecarft, out removedSpaceCraft);
                _connectedSpacecraftsInactive.TryAdd(Spacecarft, removedSpaceCraft);
                UpdateHelperForSpacecrafts(1, removedSpaceCraft.SpaceCraftName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Client logoff : {0} at {1}", removedSpaceCraft.SpaceCraftName, System.DateTime.Now);
                Logs.Append("Client logout : " + removedSpaceCraft.SpaceCraftName + " at " + System.DateTime.Now + "\n");
                Console.ResetColor();
            }

        }


        public void UpdateAltitude(string Spacecraft, int timer)
        {
            _connectedSpacecrafts[Spacecraft].altitude = timer;
        }

    }
}
