using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            double serviceableProbability = 0.9;
            int detailsCount = 4;
            int testCount = 1000;

            double brokenProbability = 1 - serviceableProbability;

            double oneBrokenDetailProbability = brokenProbability;
            double twoBrokenDetailProbability = Math.Pow(brokenProbability, 2);
            double threeBrokenDetailProbability = Math.Pow(brokenProbability, 3);
            double fourBrokenDetailProbability = Math.Pow(brokenProbability, 4);

            double detailWorkingProbability = Math.Pow(serviceableProbability, 4);
            double detailNeedRepairProbability = brokenProbability;
            double detailNotRepairableProbability = twoBrokenDetailProbability + threeBrokenDetailProbability + fourBrokenDetailProbability;

            Console.WriteLine($"Probability one broken detail: {oneBrokenDetailProbability}");
            Console.WriteLine($"Probability two broken detail: {twoBrokenDetailProbability}");
            Console.WriteLine($"Probability three broken detail: {threeBrokenDetailProbability}");
            Console.WriteLine($"Probability four broken detail: {fourBrokenDetailProbability}");

            Console.WriteLine($"Probability detail working: {detailWorkingProbability}");
            Console.WriteLine($"Probability detail need repair: {detailNeedRepairProbability}");
            Console.WriteLine($"Probability detail not working: {detailNotRepairableProbability}");

            IList<DataPoint> data = new List<DataPoint>();
            Random random = new Random();

            var model = new PlotModel { Title = "Graph", DefaultFont = "Arial" };
            model.Axes.Add(new LinearAxis { Title = "Test Count", Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Title = "Detail status (0 - working, 1 - need repair, 2 - broken)", Position = AxisPosition.Left });

            for (int i = 0; i < testCount; i++)
            {
                DetailStatus status;
                if (random.NextDouble() <= detailNotRepairableProbability)
                {
                    status = DetailStatus.Broken;
                }
                else if (random.NextDouble() <= detailNeedRepairProbability)
                {
                    status = DetailStatus.NeedRepair;
                }
                else
                {
                    status = DetailStatus.Working;
                }

                data.Add(new DataPoint(i, (int)status));
            }

            var areaSeries = new AreaSeries();
            areaSeries.Points.AddRange(data);

            model.Series.Add(areaSeries);

            var exporter = new PdfExporter { Width = 400, Height = 400 };
            using (var stream = File.Create("Graph.pdf"))
            {
                exporter.Export(model, stream);
            }
        }

        enum DetailStatus
        {
            Working = 0,
            NeedRepair = 1,
            Broken = 2
        }
    }
}
