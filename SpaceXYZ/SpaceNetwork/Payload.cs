namespace SpaceNetwork
{
    public class Payload
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Payload(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

    }
}
