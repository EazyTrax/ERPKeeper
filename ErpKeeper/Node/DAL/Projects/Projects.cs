
using ERPKeeper.Node.Models.Profiles;
using ERPKeeper.Node.Models.Projects;
using ERPKeeper.Node.Models.Projects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPKeeper.Node.DAL.Projects
{

    public class Projects : ERPNodeDalRepository
    {
        public Projects(Organization organization) : base(organization)
        {

        }

        public IQueryable<Project> Query() => erpNodeDBContext.Projects;
        public List<Project> ListAll() => erpNodeDBContext.Projects.ToList();
        public Project Find(Guid id) => erpNodeDBContext.Projects.Find(id);
        public IList<Project> GetListByStatus(ProjectStatus status) => this.Query().Where(t => t.Status == status).ToList();

        public IList<Project> getByProfile(Guid? ProfileGuid, DateTime? date = null)
        {
            if (ProfileGuid != null)
                return erpNodeDBContext.Projects.Where(p => p.CustomerProfileGuid == ProfileGuid).ToList();
            else
                return
                    null;
        }


        //public Project CreateNew(Project newProject)
        //{
        //    newProject.Uid = Guid.NewGuid();
        //    erpNodeDBContext.Projects.Add(newProject);
        //    erpNodeDBContext.SaveChanges();
        //    return newProject;
        //}
        public Project Create(string name, string code)
        {
            var newProject = new Project()
            {
                Uid = Guid.NewGuid(),
                Name = name,
                Code = code,
                CreatedDate = DateTime.Now
            };

            erpNodeDBContext.Projects.Add(newProject);
            erpNodeDBContext.SaveChanges();
            return newProject;
        }

        public Project CreateNew(NewProjectModel newProjectModel)
        {
            var newProject = new Project()
            {
                Uid = Guid.NewGuid(),
                Name = newProjectModel.Name,
                Code = "N/A",
                CreatedDate = DateTime.Now
            };

            erpNodeDBContext.Projects.Add(newProject);
            erpNodeDBContext.SaveChanges();
            return newProject;
        }

        public List<Models.Transactions.Commercial> GetCommercials(Guid id)
        {
            var project = this.Find(id);
            return project.Commercials.ToList();
        }


    }
}