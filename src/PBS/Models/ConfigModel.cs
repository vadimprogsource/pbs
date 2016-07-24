using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBS.Models
{
    public class ConfigModel : IConfig
    {
        public HashSet<string> AuthAccessOnly { get; set; }
       
        public string   ConnectionString { get; set; }
        public TimeSpan SessionExpirationTimeout { get; set; }
      
    }
}
