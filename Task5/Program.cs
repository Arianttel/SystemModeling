using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task5
{
    class Program
    {
        static double GetValueTimeForNormalDistribution(double mi, double sigma)
        {
            Random random = new Random();
            double sum = 0;

            for (int i = 0; i < 12; i++)
            {
                sum += random.NextDouble() - 6 / 100;
            }
            return mi + sigma * sum;
        }

        static double GetPoint(double mi, double sigma, double value)
        {
            return (1 / (sigma * Math.Sqrt(Math.PI * 2)))
                * Math.Pow(2.71828, -((Math.Pow((value - mi), 2)) / (2 * Math.Pow(sigma, 2))));
        }

        static void Main(string[] args)
        {
            Random random = new Random();
            IList<DataPoint> data = new List<DataPoint>();

            int time = 1000;
            int limit = 100;

            double miFirst = random.Next(limit);
            double sigmaFirst = random.Next(limit);
            var firstDetailTime = (int)GetValueTimeForNormalDistribution(miFirst, sigmaFirst);
            double miSecond = random.Next(limit);
            double sigmaSecond = random.Next(limit);
            var secondDetailTime = (int)GetValueTimeForNormalDistribution(miSecond, sigmaSecond);
            double miThird = random.Next(limit);
            double sigmaThird = random.Next(limit);
            var thirdDetailTime = (int)GetValueTimeForNormalDistribution(miThird, sigmaThird);

            int countBrokenFirst = 0;
            int countBrokenSecond = 0;
            int countBrokenThird = 0;

            for (int i = 0; i < 1000; i++)
            {
                if (random.Next(time) > firstDetailTime)
                {
                    countBrokenFirst++;
                }
                if (random.Next(time) > secondDetailTime)
                {
                    countBrokenSecond++;
                }
                if (random.Next(time) > thirdDetailTime)
                {
                    countBrokenThird++;
                }
            }

            double probabilityFirst = (double)countBrokenFirst / 1000;
            double probabilitySecond = (double)countBrokenSecond / 1000;
            double probabilityThird = (double)countBrokenThird / 1000;

            double expectValue = 1 * probabilityFirst + 
                2 * probabilitySecond * probabilityFirst + 
                3 * probabilityThird * probabilitySecond * probabilityFirst;
            double dispersion = (Math.Pow(1, 2) * probabilityFirst + 
                Math.Pow(2, 2) * probabilitySecond * probabilityFirst + 
                Math.Pow(3, 2) * probabilityThird * probabilitySecond * probabilityFirst) -
                Math.Pow(expectValue, 2);
            double standardDeviation = Math.Sqrt(Math.Abs(dispersion));

            for (double i = -10; i < 10; i+=0.1)
            {
                data.Add(new DataPoint(i, GetPoint(expectValue, standardDeviation, i)));
            }

            Console.WriteLine($"Expect value: {expectValue}{Environment.NewLine}" +
                $"Standart deviation {standardDeviation}");

            var model = new PlotModel { Title = "Graph", DefaultFont = "Arial" };
            model.Axes.Add(new LinearAxis { Title = "Probability", Position = AxisPosition.Bottom });
            model.Axes.Add(item: new LinearAxis { Title = "Experiments", Position = AxisPosition.Left });

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
