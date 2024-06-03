namespace Terra.Server.Models
{
    public class Midpoint
    {
        public double Zoom;
        public double[] Center;
        public Midpoint(List<Cordinate> cordinates)
        {
            var minX = double.MaxValue;
            var maxX = double.MinValue;
            var minY = double.MaxValue;
            var maxY = double.MinValue;

            foreach (var cordinate in cordinates)
            {
                var x = cordinate.Cords[0];
                var y = cordinate.Cords[1];

                if (x < minX)
                    minX = x;
                if (x > maxX)
                    maxX = x;
                if (y < minY)
                    minY = y;
                if (y > maxY)
                    maxY = y;
            }
            var midpoint = new double[]
            {
                (minX + maxX) / 2,
                (minY + maxY) / 2
            };
            Center = midpoint;

            var dx = (maxX - minX);
            var dy = (maxY - minY);
            Zoom = 9.511 - 1.4419 * Math.Log(Math.Max(dx, dy));
        }
    }
}
