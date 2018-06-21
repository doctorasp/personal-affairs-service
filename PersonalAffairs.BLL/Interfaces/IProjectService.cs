using PersonalAffairs.BLL.DTO;
using System.Collections.Generic;

namespace PersonalAffairs.BLL.Interfaces
{
    public interface IProjectService
    {
        void AddProject(ProjectDTO projectDTO);
        bool DeleteProject(int projId);
        ProjectDTO GetProjectById(int id);
        IEnumerable<ProjectDTO> GetAllProjectsByWorker(int id);
        decimal GetSumOfProjects(int wId);
        IEnumerable<ProjectDTO> GetAllProjects();
        void UpdateProject(ProjectDTO projectDTO);
    }
}
