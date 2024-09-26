using BoundedBuffer;
using System;

namespace BoundedBuffer
{
    class Program
    {
        static void Main(string[] args)
        {
            Experiment experiment = new Experiment();

            // Start with 4 producers and 2 consumers as in the original example
            experiment.Start(4, 2);

            Console.WriteLine("Press Enter to stop the experiment...");
            Console.ReadLine();

            // Uncomment the following lines to test with different numbers of producers and consumers

            // Start with 10 producers and 2 consumers
            // experiment.Start(10, 2);

            // Start with 4 producers and 10 consumers
            // experiment.Start(4, 10);

            // Start with 10 producers and 10 consumers
            // experiment.Start(10, 10);
        }
    }
}

