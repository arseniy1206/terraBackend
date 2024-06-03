using Terra.Server.Models.enums;

namespace Terra.Server.Models
{
    public class Way
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Season Season { get; set; }
        public TransportType Transport { get; set; }
        public double Length { get; set; }
        public int? Time { get; set; }
        public string Color { get; set; }
        public string? Author { get; set; }
        public List<Cordinate> Cordinates { get; set; }
        public List<Checkpoint> Checkpoints { get; set; }
    }

    public class Cordinate
    {
        public int Id { get; set; }
        public double[] Cords { get; set; }
        public int WayId { get; set; }
    }

    public class Checkpoint
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public PointType? Type { get; set; }
        public int WayId { get; set; }
    }
}
