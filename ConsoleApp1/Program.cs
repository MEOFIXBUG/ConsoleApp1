using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        class Point
        {
            public float X { get; set; }
            public float Y { get; set; }

            public Point()
            {

            }
            public Point(float x, float y)
            {
                X = x;
                Y = y;
            }
            public string ToString()
            {
                return $"{X},{Y}";
            }
        }
        private static List<Point> MinizePoint(List<Point> A)
        {
            var MinY = A.Select(c => c.Y).Min() - 5;
            var MinX = A.Select(c => c.X).Min() - 5;
            return A.Select(c => new Point(c.X - MinX, c.Y - MinY)).ToList();
        }
        private static void ExportSVG(string filepath, List<Point> finalPoints)
        {
            // Initialize an SVG document from a string content
            try
            {
                var Mpoints = MinizePoint(finalPoints);
                var Content = System.IO.File.ReadAllText(@"SVGFormat.xml");
                var Height = Mpoints.Select(c => c.Y).Max();
                var Width = Mpoints.Select(c => c.X).Max();
                var SvgContent = string.Format(Content, Height, Width, string.Join(" ", Mpoints.Select(c => c.ToString())), @"fill:#044B94;fill-opacity:0.0;stroke:blue");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(SvgContent);
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                // Save the document to a file and auto-indent the output.
                XmlWriter writer = XmlWriter.Create(filepath, settings);
                doc.Save(writer);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        static void Main(string[] args)
        {
            List<Point> points = new List<Point>() { new Point(5, 125), new Point(119, 5), new Point(281, 25), new Point(237, 236) };
            Console.WriteLine("Enter Scale Value: ");

            // Converted string to int
            double scale = Convert.ToDouble(Console.ReadLine());
            var scaleValue = Convert.ToSingle(scale);
            var scalePoints = points.Select(c => new Point(c.X * scaleValue, c.Y * scaleValue)).ToList();
            ExportSVG("Draw.svg", points);
            ExportSVG("Scale.svg", scalePoints);
        }
    }
}
