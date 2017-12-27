namespace Sitecore.SharedSource.RedirectManager.Events
{
  using System;
  using Sitecore.Events;

  public class PageRedirectedEventArgs : EventArgs, IPassNativeEventArgs
  {
    public string RedirectId { get; set; }

    public DateTime RedirectTime { get; set; }

    public PageRedirectedEventArgs(PageRedirectedEvent @event)
    {
      RedirectId = @event.RedirectId;
      RedirectTime = @event.RedirectTime;
    }
  }
}