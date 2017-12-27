namespace Sitecore.SharedSource.RedirectManager.Repository
{
  using System;
  using System.Collections.Generic;
  using Sitecore.SharedSource.RedirectManager.Models;

  public interface IRedirectsRepository
  {
    List<RedirectModel> GetRedirects();
    void UpdateRedirect(Guid id, DateTime lastUse);
    void RemoveRedirect(Guid id);
  }
}