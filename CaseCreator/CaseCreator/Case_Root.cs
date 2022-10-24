using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCreator
{
    public class Item
    {
        public string name { get; set; }
        public string image { get; set; }
        public int value { get; set; }
        public int chance { get; set; }
    }

    public class Case_Root
    {
        public string name { get; set; }
        public int price { get; set; }
        public List<Item> items { get; set; }
    }


}
