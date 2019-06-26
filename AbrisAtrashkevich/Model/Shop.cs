using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrisAtrashkevich.Model
{
    public class Shop
    {
        public string ID { get; set; }

        public string Address { get; set; }

        public Shop(string id, string address)
        {
            ID = id;
            Address = address;
        }
    }
}
