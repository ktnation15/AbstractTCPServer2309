using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BoxingMachine
{
    private readonly BoundedBuffer<Bottle> _inputBuffer;

    public BoxingMachine(BoundedBuffer<Bottle> inputBuffer)
    {
        _inputBuffer = inputBuffer;
    }

    public void Start()
    {
        new Thread(() =>
        {
            while (true)
            {
                for (int i = 0; i < 24; i++)
                {
                    var bottle = _inputBuffer.Remove();
                    Console.WriteLine($"Boxing Bottle {bottle.Id}");
                }
                Console.WriteLine("Boxed 24 Bottles");
            }
        }).Start();
    }
}
