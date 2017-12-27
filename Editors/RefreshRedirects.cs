// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveRedirects.cs" company="Sitecore A/S">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the SyncLastUse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Editors
{
  using Sitecore.SharedSource.RedirectManager.Events;
  using Sitecore.Shell.Framework.Commands;

  /// <summary>
  /// The remove old redirects type
  /// </summary>
  public class RefreshRedirects : Command
  {
    /// <summary>
    /// Executes the command in the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Execute([NotNull]CommandContext context)
    {
      RedirectsRefreshEventRiser.RaiseEvent();
      Context.ClientPage.ClientResponse.Alert("RefreshRedirects job has been started");
    }
  }
}