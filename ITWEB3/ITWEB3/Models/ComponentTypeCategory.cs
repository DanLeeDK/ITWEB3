using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITWEB3.Models
{
    public class ComponentTypeCategory
    {
        public int ID { get; set; }
        public int ComponentTypeID { get; set; }
        public ComponentType ComponentType { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
