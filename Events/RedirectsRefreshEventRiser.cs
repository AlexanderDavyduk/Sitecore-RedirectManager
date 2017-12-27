namespace Sitecore.SharedSource.RedirectManager.Events
{
  using System;
  using Sitecore.Foundation.RedirectManager.Events;
  using Sitecore.SharedSource.RedirectManager.Utils;

  public class RedirectsRefreshEventRiser
  {
    public static void RaiseEvent()
    {
      var @event = new RedirectsRefreshEvent();
      Sitecore.Events.Event.RaiseEvent("redirects:refresh");
      Eventing.EventManager.QueueEvent(@event, true, true);
      LogManager.WriteInfo($"Refresh redirects was triggered");
    }
  }
}