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
  using Sitecore.Shell.Framework.Commands;

  /// <summary>
  /// The remove old redirects type
  /// </summary>
  public class SyncLastUse : Command
  {
    /// <summary>
    /// Executes the command in the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Execute([NotNull]CommandContext context)
    {
      var agent = new Agents.SyncLastUse();
      agent.SyncInThread();

      Context.ClientPage.ClientResponse.Alert("SyncLastUse job has been started");
    }

    
  }
}