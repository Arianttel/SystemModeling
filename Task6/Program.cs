using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<DataPoint> data = new List<DataPoint>();
            Random random = new Random();
            double lambda = random.NextDouble();

            for (double i = 0; i < 50; i+=0.1)
            {
                double point = lambda * Math.Pow(2.7, -(lambda * i));
                data.Add(new DataPoint(i, point));
            }

            var expectValue = 1 / lambda;
            var dispersion = 1 / Math.Pow(lambda, 2);
            var standardDeviation = Math.Sqrt(dispersion);

            Console.WriteLine($"Expect value: {expectValue}{Environment.NewLine}" +
                $"Standart deviation {standardDeviation}{Environment.NewLine}" +
                $"Dispersion {dispersion}");

            var model = new PlotModel { Title = "Graph", DefaultFont = "Arial" };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

            var areaSeries = new AreaSeries();
            areaSeries.Points.AddRange(data);

            model.Series.Add(areaSeries);

            var exporter = new PdfExporter { Width = 400, Height = 400 };
            using (var stream = File.Create("Graph.pdf"))
            {
                exporter.Export(model, stream);
            }
        }
    }
}
