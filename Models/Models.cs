using System.Text.Json.Serialization;

namespace WebApi.Models
{
public class Device
{
    public int Id { get; set; }
    public required string Model { get; set; }
    public int StoreId { get; set; }
    public int? ClientId { get; set; }
    
    public Store Store { get; set; } 
    public Client? Client { get; set; } 
}


    public class Store
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [JsonIgnore]
        public List<Device> Devices { get; set; } = new List<Device>();
    }

    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        [JsonIgnore]
        public List<Device> Devices { get; set; } = new List<Device>();
    }
}
