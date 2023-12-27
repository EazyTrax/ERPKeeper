

using KeeperCore.ERPNode.Models.Profiles;
using KeeperCore.ERPNode.Models.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace KeeperCore.ERPNode.DAL.Projects
{

    public class WorkTasks : ERPNodeDalRepository
    {
        public WorkTasks(Organization organization) : base(organization)
        {
        }

        public List<WorkTask> ListAll() => erpNodeDBContext.WorkTasks.ToList();
        public IQueryable<WorkTask> All() => erpNodeDBContext.WorkTasks;

        public WorkTask CreateDefault()
        {
            var newTask = new KeeperCore.ERPNode.Models.Projects.WorkTask()
            {
                Id = Guid.NewGuid(),
                Title = "NEW",
                CreatedDate = DateTime.Now
            };

            erpNodeDBContext.WorkTasks.Add(newTask);
            organization.SaveChanges();
            return newTask;
        }

        public WorkTask Find(Guid id) => erpNodeDBContext.WorkTasks.Find(id);


        public WorkTask CreateNew(NewTask newTaskModel)
        {
            var newTask = new KeeperCore.ERPNode.Models.Projects.WorkTask()
            {
                Id = Guid.NewGuid(),
                ResponsibleUserId = newTaskModel.ResponsibleUserId,
                Title = newTaskModel.Title,
                CreatedDate = newTaskModel.CreatedDate ?? DateTime.Now
            };

            erpNodeDBContext.WorkTasks.Add(newTask);
            organization.SaveChanges();
            return newTask;
        }

        public void Remove(WorkTask task)
        {
            erpNodeDBContext.WorkTasks.Remove(task);
            organization.SaveChanges();
        }

        public List<WorkTask> ListByResponsible(Guid id) => erpNodeDBContext.WorkTasks.Where(p => p.ResponsibleUserId == id).ToList();

        public List<WorkTask> GetOpenTasks() => erpNodeDBContext.WorkTasks
            .Where(t => t.Status == Models.Projects.Enums.WorkTaskStatus.Open).ToList();

    
    }
}