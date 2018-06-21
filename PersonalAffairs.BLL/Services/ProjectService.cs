using AutoMapper;
using PersonalAffairs.BLL.DTO;
using PersonalAffairs.BLL.Interfaces;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalAffairs.BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork unitOfWork;
        public ProjectService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddProject(ProjectDTO projDTO)
        {
            if (projDTO == null)
                throw new ArgumentNullException();
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()).CreateMapper();

            Project project = mapper.Map<ProjectDTO, Project>(projDTO);

            unitOfWork.Projects.Create(project);
            unitOfWork.Save();
        }

        public bool DeleteProject(int projId)
        {
            Project proj = unitOfWork.Projects.Get(projId);

            if (proj != null)
                unitOfWork.Projects.Delete(proj);
            else
                return false;

            unitOfWork.Save();
            return true;
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            IMapper mapperProject = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Worker, WorkerDTO>()).CreateMapper();

            IEnumerable<Project> projects = unitOfWork.Projects.GetAll();
            IEnumerable<ProjectDTO> projDTOs = mapperProject.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(projects);

            return projDTOs;
        }


        public IEnumerable<ProjectDTO> GetAllProjectsByWorker(int id)
        {
            IMapper mapperProject = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Worker, WorkerDTO>()).CreateMapper();

            IEnumerable<Project> projects = unitOfWork.Projects.GetAllProjectsByWorker(id);
            IEnumerable<ProjectDTO> projDTOs = mapperProject.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(projects);

            return projDTOs;
        }

        public decimal GetSumOfProjects(int wId)
        {
            IMapper mapperProject = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Worker, WorkerDTO>()).CreateMapper();

            IEnumerable<Project> projects = unitOfWork.Projects.GetAllProjectsByWorker(wId);
            IEnumerable<ProjectDTO> projDTOs = mapperProject.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(projects);
            decimal sum = 0;
            foreach (ProjectDTO projDTO in projDTOs)
            {
                sum += projDTO.ProjectPrice;
            }
            
            return sum;
        }

        public ProjectDTO GetProjectById(int id)
        {
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();

            Project project = unitOfWork.Projects.Get(id);

            if (project == null)
                return null;

            ProjectDTO projectDTO = mapperWorker.Map<Project, ProjectDTO>(project);
            return projectDTO;
        }

        public void UpdateProject(ProjectDTO projectDTO)
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()).CreateMapper();
            Project project = unitOfWork.Projects.Get(projectDTO.Id);
            project = mapper.Map<ProjectDTO, Project>(projectDTO);
            unitOfWork.Projects.Update(project);
            unitOfWork.Save();
        }
    }
}
