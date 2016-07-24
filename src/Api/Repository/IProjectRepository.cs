using Api.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IProjectRepository : IRepository<IProject>
    {
        bool TryFindProject(string projectCode, out IProject project);
    }
}
