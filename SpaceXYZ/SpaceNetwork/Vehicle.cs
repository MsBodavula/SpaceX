namespace SpaceNetwork
{
    public class Vehicle
    {

        public string Name { get; set; }
        public int Orbit { get; set; }
        public Payload PayloadLink { get; set; }

        public Vehicle(string name, int orbit, Payload payload)
        {
            this.Name = name;
            this.Orbit = orbit;
            this.PayloadLink = payload;
        }

    }
}
