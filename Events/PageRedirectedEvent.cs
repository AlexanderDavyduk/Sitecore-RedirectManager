using System;
using System.Runtime.Serialization;

namespace Sitecore.SharedSource.RedirectManager.Events
{
  [DataContract]
  public class PageRedirectedEvent
  {
    [DataMember]
    public string RedirectId { get; set; }

    [DataMember]
    public DateTime RedirectTime { get; set; }
  }
}