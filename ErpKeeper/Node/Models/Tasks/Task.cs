using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ERPKeeper.Node.Models.Security;
using ERPKeeper.Node.Models.Tasks.Enums;

namespace ERPKeeper.Node.Models.Tasks
{
    [Table("ERP_Tasks")]
    public class Task
    {

        [Key]
        public Guid Uid { get; set; }
        public int No { get; set; }
        public String Name => $"TSK-{No}";
        public Enums.TaskStatus Status { get; set; }
        public String Title { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CloseDate { get; set; }


        public Guid? ProjectUid { get; set; }
        [ForeignKey("ProjectUid")]
        public virtual Projects.Project Project { get; set; }


        public int Age
        {
            get
            {
                if (DueDate != null)
                    return (int)(((DateTime)DueDate).Date - DateTime.Today.Date).TotalDays;
                else
                    return 0;
            }
        }

        public String Detail { get; set; }
        public void SetStatus(TaskStatus status) => this.Status = status;

        public Guid? ResponsibleMemberUid { get; set; }
        [ForeignKey("ResponsibleMemberUid")]
        public virtual Member ResponsibleMember { get; set; }



        public void ChangeStatus(TaskStatus status)
        {
            this.Status = status;
        }

        public void SendLineNotification()
        {
            string apiUrl = "https://notify-api.line.me/api/notify";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            {
                string message = $"เรียน {this.ResponsibleMember?.Name} " + Environment.NewLine;
                message = message + $"มีงาน {this.Title} ค้างเป็นเวลา {Age} วัน" + Environment.NewLine;
                message = message + $" {this.Detail}" + Environment.NewLine;


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
