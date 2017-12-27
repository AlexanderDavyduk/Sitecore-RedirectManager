namespace Sitecore.SharedSource.RedirectManager.Events
{
  using System;
  using Sitecore.SharedSource.RedirectManager.Utils;

  public class PageRedirectedEventRiser
  {
    public static void RaiseEvent(string redirectId, DateTime redirectTime)
    {
      PageRedirectedEvent @event = new PageRedirectedEvent
      {
        RedirectId = redirectId,
        RedirectTime = redirectTime
      };

      PageRedirectedEventArgs arguments = new PageRedirectedEventArgs(@event);
      Sitecore.Events.Event.RaiseEvent("page:redirected", arguments);
      Eventing.EventManager.QueueEvent(@event, true, true);
      LogManager.WriteInfo($"Update last use for {redirectId} has been triggered"); 
    }
  }
}