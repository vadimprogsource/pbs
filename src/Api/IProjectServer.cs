using Api.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IProjectServer
    {
        IPageResult<IProject> GetProjects(IFilter filter);
        IProject GetProject(Guid id);
        IProject CreateProject(string projectCode);
        IProject UpdateProject(IProject project);
        bool RemoveProject(Guid id);
    }
}
