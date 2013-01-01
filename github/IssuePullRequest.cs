using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace github
{
  public class IssuePullRequest
  {
    public string html_url { get; set; }
    public string diff_url { get; set; }
    public string patch_url { get; set; }
  }
}