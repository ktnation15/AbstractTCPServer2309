using System;


class Program
{
    static void Main(string[] args)
    {
        var washingBuffer = new BoundedBuffer<Bottle>(10);
        var fillingBuffer = new BoundedBuffer<Bottle>(10);
        var toppingBuffer = new BoundedBuffer<Bottle>(10);
        var boxingBuffer = new BoundedBuffer<Bottle>(24);

        var generator = new BottleGenerator(washingBuffer);
        generator.Start();

        for (int i = 0; i < 3; i++)
        {
            var washer = new WashingMachine(washingBuffer, fillingBuffer);
            washer.Start();
        }

        for (int i = 0; i < 6; i++)
        {
            var filler = new FillingMachine(fillingBuffer, toppingBuffer);
            filler.Start();
        }

        for (int i = 0; i < 6; i++)
        {
            var topper = new ToppingMachine(toppingBuffer, boxingBuffer);
            topper.Start();
        }

        for (int i = 0; i < 2; i++)
        {
            var boxer = new BoxingMachine(boxingBuffer);
            boxer.Start();
        }

        // Prevent the application from exiting immediately
        Console.ReadLine();
    }
}
