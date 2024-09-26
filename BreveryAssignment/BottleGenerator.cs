using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class BottleGenerator
{
    private readonly BoundedBuffer<Bottle> _buffer;
    private int _bottleId = 0;

    public BottleGenerator(BoundedBuffer<Bottle> buffer)
    {
        _buffer = buffer;
    }

    public void Start()
    {
        new Thread(() =>
        {
            Random random = new Random();
            while (true)
            {
                Thread.Sleep(random.Next(1000, 2000)); // Random delay
                var bottle = new Bottle(Interlocked.Increment(ref _bottleId));
                _buffer.Add(bottle);
                Console.WriteLine($"Generated Bottle {bottle.Id}");
            }
        }).Start();
    }
}

