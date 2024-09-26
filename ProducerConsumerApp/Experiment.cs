using BoundedBuffer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BoundedBuffer
{
    public class Experiment
    {
        private readonly BoundedBuffer<Item> _buffer = new BoundedBuffer<Item>(10);
        private readonly Random _random = new Random();

        public void Producer()
        {
            while (true)
            {
                Thread.Sleep(_random.Next(20));
                int value = _random.Next(100); // Random value between 0 and 99
                Item item = new Item(value);
                _buffer.Insert(item);
                Console.WriteLine($"Produced: {item}");
            }
        }

        public void Consumer()
        {
            while (true)
            {
                Thread.Sleep(_random.Next(10));
                Item item = _buffer.Take();
                Console.WriteLine($"Consumed: {item}");
            }
        }

        public void Start(int producerCount, int consumerCount)
        {
            // Start producer threads
            for (int i = 0; i < producerCount; i++)
            {
                Task.Run(() => Producer());
            }

            // Start consumer threads
            for (int i = 0; i < consumerCount; i++)
            {
                Task.Run(() => Consumer());
            }
        }
    }
}

