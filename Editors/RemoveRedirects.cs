// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveRedirects.cs" company="Sitecore A/S">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the RemoveRedirects type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Sitecore.Data.Events;
using Sitecore.SharedSource.RedirectManager.Pipelines;

namespace Sitecore.SharedSource.RedirectManager.Editors
{
  using System;
  using System.Globalization;
  using System.Linq;
  using System.Web.UI.WebControls;

  using Sitecore.Configuration;
  using Sitecore.Globalization;
  using Sitecore.SharedSource.RedirectManager.Utils;
  using Sitecore.Shell.Framework.Commands;
  using Sitecore.Web.UI.Sheer;

  /// <summary>
  /// The remove old redirects type
  /// </summary>
  public class RemoveRedirects : Command
  {
    /// <summary>
    /// Executes the command in the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Execute([NotNull]CommandContext context)
    {
      var clientArgs = new ClientPipelineArgs();
      Context.ClientPage.Start(this, "DialogProcessor", clientArgs);
    }

    /// <summary>
    /// Dialogs the processor.
    /// </summary>
    /// <param name="args">The arguments.</param>
    protected void DialogProcessor(ClientPipelineArgs args)
    {
      if (args.IsPostBack)
      {
        if (args.Result == "yes")
        {
          var rootItem = Factory.GetDatabase(Configuration.Database).GetItem(Items.ItemIDs.RedirectsFolderItem);

          if (rootItem == null)
          {
            return;
          }

          var children = rootItem.Axes.GetDescendants();

          if (!children.Any())
          {
            return;
          }

          using (new EventDisabler())
          {
            var currentDate = DateTime.Now;
            foreach (var item in from item in children.Where(x => x.IsItemOfType(Templates.Settings.TemplateId))
              let settings = new Templates.Settings(item)
              where (currentDate - settings.LastUse.DateTime).Days >= Configuration.RemovalDate
              select item)
            {
              item.Delete();
            }
          }

          RedirectProcessor.CreateListOfRedirects();
          var load = string.Concat(new object[]
          {
            "item:load(id=", rootItem.ID, ",language=", rootItem.Language, ",version=", rootItem.Version, ")"
          });
          Context.ClientPage.SendMessage(this, load);

          var refresh = string.Format("item:refreshchildren(id={0})", rootItem.Parent.ID);
          Context.ClientPage.ClientResponse.Timer(refresh, 2);
        }
      }
      else
      {
        Context.ClientPage.ClientResponse.Confirm(Translate.Text("Redirect_Remove Old Redirects").Replace("[0]", Configuration.RemovalDate.ToString(CultureInfo.InvariantCulture)));
        args.WaitForPostBack();
      }
    }
  }
}