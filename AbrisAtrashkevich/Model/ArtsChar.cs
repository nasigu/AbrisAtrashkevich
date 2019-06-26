using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AbrisAtrashkevich.Model
{
    public class ArtsChar
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public ArtsChar(string id, string name)
        {
            ID = id;
            Name = name;
        }
    }


}
