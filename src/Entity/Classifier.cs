using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public class Classifier :IClassifier 
    {
         public int Id { get; set; }
         public string Code { get; set; }
        public string Name { get; set; }
    }
}
