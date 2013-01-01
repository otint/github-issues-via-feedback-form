using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace github
{
  public class IssueUser
  {
    public string login { get; set; }
    public uint id { get; set; }
    public string avatar_url { get; set; }
    public string gravatar_id { get; set; }
    public string url { get; set; }
  }
}