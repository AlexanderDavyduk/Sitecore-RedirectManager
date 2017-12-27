using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.SharedSource.RedirectManager.Models
{
  public class RedirectModel
  {
    public Guid RedirectId { get; set; }
    public DateTime LastUse { get; set; }
  }
}