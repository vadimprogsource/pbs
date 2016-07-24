using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Map
{
    public interface IModelMapper
    {
        void Map(ModelBuilder context);
    }
}
