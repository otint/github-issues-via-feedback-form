using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace github
{
  public class IssueMilestone
  {
    public string url { get; set; }
    public uint number { get; set; }
    public string state { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public IssueUser creator { get; set; }
    public uint open_issues { get; set; }
    public uint closed_issues { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? due_on { get; set; }
  }
}