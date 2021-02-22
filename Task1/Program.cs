using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
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
            IList<DataPoint> data = new List<DataPoint>();

            // Time start
            double timeStart;
            InputValidation(out timeStart, "Enter start time: ");

            // Growth
            double a;
            InputValidation(out a, "Enter growth: ");

            // Population number
            double n;
            InputValidation(out n, "Enter population number: ");

            // End time
            double timeEnd;
            InputValidation(out timeEnd, "Enter end time: ");

            // Step
            double h;
            InputValidation(out h, "Enter step: ");

            while (timeStart <= timeEnd)
            {
                var result = n * Math.Pow(Math.E, a * timeStart);
                data.Add(new DataPoint(timeStart, result));

                timeStart += h;
            }

            var model = new PlotModel { Title = "Graph", DefaultFont = "Arial" };
            model.Axes.Add(new LinearAxis { Title = "t", Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Title = "N", Position = AxisPosition.Left });

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
