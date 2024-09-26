using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FillingMachine
{
    private readonly BoundedBuffer<Bottle> _inputBuffer;
    private readonly BoundedBuffer<Bottle> _outputBuffer;

    public FillingMachine(BoundedBuffer<Bottle> inputBuffer, BoundedBuffer<Bottle> outputBuffer)
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
                Console.WriteLine($"Filling Bottle {bottle.Id}");
                Thread.Sleep(200); // Simulate filling time
                _outputBuffer.Add(bottle);
                Console.WriteLine($"Filled Bottle {bottle.Id}");
            }
        }).Start();
    }
}
