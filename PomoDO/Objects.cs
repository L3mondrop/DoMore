using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoMore
{
    public class Objects
    {
        public class WorkItem
        {
            public int Id { get; set; }
            public string GUID { get; set; }
            public string Text { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string TotalTime
            {
            get
                {
                    if (StartTime != null && EndTime != null)
                    {
                        DateTime start = DateTime.Parse(StartTime);
                       
                        DateTime end = DateTime.Parse(EndTime);

                        TimeSpan time = end.Subtract(start);

                        return $"Total: {time.Minutes} minutes, {time.Seconds} seconds";
                    }
                    else
                    {
                        return null;
                    }


                    
                }
            set { TotalTime = value; }
            }
            
            public WorkItem(int Id, string GUID, string Text, string StartTime, string EndTime)
            {
                this.Id = Id;
                this.GUID = GUID;
                this.Text = Text;
                this.StartTime = StartTime;
                this.EndTime = EndTime;
            }
            public WorkItem()
            {
                this.Id = 0;
                this.GUID = "";
                this.Text = "";
                this.StartTime = "";
                this.EndTime = "";
            }
        }
    }
}
