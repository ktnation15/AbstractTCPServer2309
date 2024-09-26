using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundedBuffer
{
    public class Item
    {
        private static int _idCounter = 0;
        public int Id { get; private set; }
        public int Value { get; set; }

        public Item(int value)
        {
            Id = ++_idCounter;
            Value = value;
        }

        public override string ToString()
        {
            return $"Item Id: {Id}, Value: {Value}";
        }
    }
}
