using System.Collections.Generic;

namespace IDL.MapsApi.Net.MapBox.Models
{
    public class Point : List<double>
    {
        public Point()
        {
            Capacity = 2;
        }

        public double X
        {
            get { return this[0]; }

            set { this[0] = value; }
        }

        public double Y
        {
            get { return this[1]; }

            set { this[1] = value; }
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", X, Y);
        }

        public static Point Parse(string queryParameter)
        {
            var split = queryParameter.Split(',');
            return new Point
            {
                X = double.Parse(split[0]),
                Y = double.Parse(split[1])
            };
        }
    }
}