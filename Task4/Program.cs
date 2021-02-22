using System;

namespace Task4
{
    class Program
    {
        static void InputValidation(out int value, string message)
        {
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid input, try again");
                Console.Write(message);
            }
        }

        static void Main(string[] args)
        {
            int T;
            InputValidation(out T, "Enter interval T: ");
            int t;
            InputValidation(out t, "Enter waiting time t: ");

            int countPointInInterval = 0;
            int countAllPoint = 10000;

            Random random = new Random();
            for (int i = 0; i < countAllPoint; i++)
            {
                double rndVal = random.NextDouble();
                var resultPointValue = rndVal * T;

                if (resultPointValue <= t)
                {
                    countPointInInterval++;
                }
            }
            var probability = (double)countPointInInterval / (double)countAllPoint;

            Console.WriteLine($"Probability: {probability * 100}%");
        }
    }
}
