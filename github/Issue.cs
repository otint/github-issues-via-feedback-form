using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace github
{
  public class Issue
  {
    public string url { get; set; }
    public string html_url { get; set; }
    public uint number { get; set; }
    public string state { get; set; }
    public string title { get; set; }
    public string body { get; set; }
    public IssueUser user { get; set; }
    public List<IssueLabel> labels { get; set; }
    public IssueUser assignee { get; set; }
    public IssueMilestone milestone { get; set; }
    public uint comments { get; set; }
    public IssuePullRequest pull_request { get; set; }
    public DateTime? closed_at { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
  }
}