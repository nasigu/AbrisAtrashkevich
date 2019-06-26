using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrisAtrashkevich.Model
{
    public class Price
    {
        public string ID { get; set; }

        public string Value { get; set; }

        public Price(string id, string value)
        {
            ID = id;
            Value = value;
        }
    }
}
