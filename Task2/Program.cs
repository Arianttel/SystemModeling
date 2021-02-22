using System;
using System.Collections.Generic;
using System.IO;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Task2
{
    class Program
    {
        static void InputValidation(out double value, string message)
        {
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid input, try again");
                Console.Write(message);
            }
        }

        static void Main(string[] args)
        {
            double x = 0;
            double y = 0;
            double g = 9.8;
            
            double k;
            InputValidation(out k, "Enter k: ");
            double m;
            InputValidation(out m, "Enter m: ");
            double h;
            InputValidation(out h, "Enter h: ");

            var model = new PlotModel { Title = "Graph", DefaultFont = "Arial" };
            model.Axes.Add(new LinearAxis { Title = "x", Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Title = "y", Position = AxisPosition.Left });

            var maxEndPoint = 2 * m * k / g;
            while (k > 0)
            {
                var data = new List<DataPoint>();
                var areaSeries = new AreaSeries();

                var xTemp = x;
                var yTemp = y;

                var endPoint = 2 * m * k / g;
                var modificator = (maxEndPoint - endPoint) / 2;
                
                while (xTemp <= endPoint)
                {
                    yTemp = -(g * Math.Pow(xTemp, 2) / 2) + (m * k * xTemp);
                    xTemp += 0.1;

                    data.Add(new DataPoint(xTemp + modificator, yTemp));
                }
                areaSeries.Points.AddRange(data);
                k -= h;
                model.Series.Add(areaSeries);
            }

            var exporter = new PdfExporter { Width = 400, Height = 400 };
            using (var stream = File.Create("Graph.pdf"))
            {
                exporter.Export(model, stream);
            }
        }
    }
}
