using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrisAtrashkevich.Model
{
    public class Arts
    {
        public string ID { get; set; }
        public string ArtsChar_ID { get; set; }
        public string Shop_ID { get; set; }
        public string Price_ID { get; set; }
        public string Barcode_ID { get; set; }
        public string Name { get; set; }

        public Arts(string id, string artsChar, string shop, string price, string barcode, string name)
        {
            ID = id;
            ArtsChar_ID = artsChar;
            Shop_ID = shop;
            Price_ID = price;
            Barcode_ID = barcode;
            Name = name;
        }
    }
}
