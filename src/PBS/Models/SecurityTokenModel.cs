using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBS.Models
{
    public class SecurityTokenModel
    {
        public bool IsValid { get; set; }
        public Guid Sid     { get; set; }
    }
}
