

using ERPKeeper.Node.Models.Profiles;
using ERPKeeper.Node.Models.Projects;
using ERPKeeper.Node.Models.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ERPKeeper.Node.DAL.Projects
{

    public class Tasks : ERPNodeDalRepository
    {
        public Tasks(Organization organization) : base(organization)
        {

        }

        public List<Task> ListAll() => erpNodeDBContext.Tasks.ToList();
        public IQueryable<Task> All() => erpNodeDBContext.Tasks;

        public Task CreateDefault()
        {
            var newTask = new ERPKeeper.Node.Models.Tasks.Task()
            {
                Uid = Guid.NewGuid(),
                Title = "NEW",
                CreatedDate = DateTime.Now
            };

            erpNodeDBContext.Tasks.Add(newTask);
            organization.SaveChanges();
            return newTask;
        }

        public Task Find(Guid id) => erpNodeDBContext.Tasks.Find(id);


        public Task CreateNew(NewTask newTaskModel)
        {
            var newTask = new ERPKeeper.Node.Models.Tasks.Task()
            {
                Uid = Guid.NewGuid(),
                ResponsibleMemberUid = newTaskModel.ResponsibleUid,
                Title = newTaskModel.Title,
                CreatedDate = newTaskModel.CreatedDate ?? DateTime.Now
            };

            erpNodeDBContext.Tasks.Add(newTask);
            organization.SaveChanges();
            return newTask;
        }

        public void Remove(Task task)
        {
            erpNodeDBContext.Tasks.Remove(task);
            organization.SaveChanges();
        }

        public List<Task> ListByResponsible(Guid id) => erpNodeDBContext.Tasks.Where(p => p.ResponsibleMemberUid == id).ToList();

        public List<Task> GetOpenTasks() => erpNodeDBContext.Tasks
            .Where(t => t.Status == Models.Tasks.Enums.TaskStatus.Open).ToList();

        public void SendOpenTasksLineNotification()
        {
            var taskGroups = erpNodeDBContext.Tasks
             .Where(t => t.Status == Models.Tasks.Enums.TaskStatus.Open)
                .Where(t => t.ResponsibleMemberUid != null)
             .GroupBy(t => t.ResponsibleMemberUid)

             .Select(group => new { key = group.Key, tasks = group.ToList() })
             .ToList();

            string apiUrl = "https://notify-api.line.me/api/notify";
            foreach (var group in taskGroups)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                {
                    Profile profile = organization.Profiles.Find((Guid)group.key);
                    string message = $"{profile.DisplayName}" + Environment.NewLine + $"เจ้ามี {group.tasks.Count} งานคงค้าง";

                    foreach (var task in group.tasks)
                        message = message + Environment.NewLine + $"[{task.Age}] {task.Title}";

                    var postData = string.Format("message={0}", message);
                    var data = Encoding.UTF8.GetBytes(postData);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    request.Headers.Add("Authorization", "Bearer aW9Xko3ajCj3SRpN7fvWsGamRLXKhEoB8F1gPxINrIc");

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        response.Close();
                    }


                }
            }
        }
    }
}