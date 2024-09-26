using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ToppingMachine
{
    private readonly BoundedBuffer<Bottle> _inputBuffer;
    private readonly BoundedBuffer<Bottle> _outputBuffer;

    public ToppingMachine(BoundedBuffer<Bottle> inputBuffer, BoundedBuffer<Bottle> outputBuffer)
    {
        _inputBuffer = inputBuffer;
        _outputBuffer = outputBuffer;
    }

    public void Start()
    {
        new Thread(() =>
        {
            while (true)
            {
                var bottle = _inputBuffer.Remove();
                Console.WriteLine($"Topping Bottle {bottle.Id}");
                Thread.Sleep(200); // Simulate topping time
                _outputBuffer.Add(bottle);
                Console.WriteLine($"Topped Bottle {bottle.Id}");
            }
        }).Start();
    }
}

