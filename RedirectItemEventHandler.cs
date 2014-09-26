// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedirectItemEventHendler.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   ItemEventHandler class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager
{
  using System;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Events;
  using Sitecore.SharedSource.RedirectManager.Templates;

  /// <summary>
  /// ItemEventHandler class
  /// </summary>
  public class RedirectItemEventHandler
  {
    /// <summary>
    /// Called when the item has saved.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments.</param>
    public void OnItemSaved(object sender, EventArgs args)
    {
      if (!Configuration.Enabled || args == null)
      {
        return;
      }

      var item = Event.ExtractParameter(args, 0) as Item;
      Assert.IsNotNull(item, "No item in parameters");
      if (item.Database.Name != Configuration.Database)
      {
        return;
      }

      if (item.ID.ToString() == Items.ItemIDs.RedirectsFolderItem)
      {
        RedirectProcessor.CreateListOfRedirectsInThread();
      }

      if (item.TemplateID == Multisite.TemplateId)
      {
        RedirectProcessor.RebuildMultisites();
        RedirectProcessor.CreateListOfRedirectsInThread();
        return;
      }

      if (!CheckTemplate(item))
      {
        return;
      }

      var changes = Event.ExtractParameter(args, 1) as ItemChanges;
      if (changes != null)
      {
        if (changes.FieldChanges.Contains(Settings.LastUseFieldId))
        {
          return;
        }
      }

      //if (Configuration.RebuildRedirectsList)
      {
        RedirectProcessor.CreateListOfRedirectsInThread();
      }
      //else
      {
        //RedirectProcessor.UpdateRedirectInThread(item);
      }
    }

    /// <summary>
    /// Called when the item has saved.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments.</param>
    public void OnItemSavedRemote(object sender, EventArgs args)
    {
      if (!Configuration.Enabled || args == null)
      {
        return;
      }

      var eventArgs = args as Data.Events.ItemSavedRemoteEventArgs;
      Assert.IsNotNull(eventArgs, "ItemSavedRemoteEventArgs is null");

      var item = eventArgs.Item;
      Assert.IsNotNull(item, "No item in parameters");
      if (item.Database.Name != Configuration.Database)
      {
        return;
      }

      if (item.ID.ToString() == Items.ItemIDs.RedirectsFolderItem)
      {
        RedirectProcessor.CreateListOfRedirectsInThread();
      }

      if (item.TemplateID == Multisite.TemplateId)
      {
        RedirectProcessor.RebuildMultisites();
        RedirectProcessor.CreateListOfRedirectsInThread();
        return;
      }

      if (!CheckTemplate(item))
      {
        return;
      }

      var changes = eventArgs.Changes;
      if (changes != null)
      {
        if (changes.FieldChanges.Contains(Settings.LastUseFieldId))
        {
          return;
        }
      }

      //if (Configuration.RebuildRedirectsList)
      //{
        RedirectProcessor.CreateListOfRedirectsInThread();
      //}
      //else
      {
        //RedirectProcessor.UpdateRedirectInThread(item);
      }
    }

    /// <summary>
    /// Called when the item has deleted.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments.</param>
    public void OnItemDeleted(object sender, EventArgs args)
    {
      if (!Configuration.Enabled || args == null)
      {
        return;
      }

      var item = Event.ExtractParameter(args, 0) as Item;
      Assert.IsNotNull(item, "No item in parameters");
      if (item.Database.Name != Configuration.Database)
      {
        return;
      }

      if (item.TemplateID == Multisite.TemplateId)
      {
        RedirectProcessor.RebuildMultisites();
        RedirectProcessor.CreateListOfRedirectsInThread();
        return;
      }

      if (!CheckTemplate(item))
      {
        return;
      }

      RedirectProcessor.RemoveRedirectInThread(item);
    }

    /// <summary>
    /// Called when the item has deleted.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments.</param>
    public void OnItemDeletedRemote(object sender, EventArgs args)
    {
      if (!Configuration.Enabled || args == null)
      {
        return;
      }

      var eventArgs = args as Data.Events.ItemDeletedRemoteEventArgs;
      Assert.IsNotNull(eventArgs, "ItemSavedRemoteEventArgs is null");

      var item = eventArgs.Item;
      Assert.IsNotNull(item, "No item in parameters");

      if (item.Database.Name != Configuration.Database)
      {
        return;
      }

      if (item.TemplateID == Multisite.TemplateId)
      {
        RedirectProcessor.RebuildMultisites();
        RedirectProcessor.CreateListOfRedirectsInThread();
        return;
      }

      if (!CheckTemplate(item))
      {
        return;
      }

      RedirectProcessor.RemoveRedirectInThread(item);
    }

    /// <summary>
    /// Checks the admissible template.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// True if admissible; false - otherwise.
    /// </returns>
    private static bool CheckTemplate(Item item)
    {
      var templateId = item.TemplateID.ToString();
      return templateId == ItemToItem.TemplateId || templateId == SectionToItem.TemplateId || templateId == SectionToSection.TemplateId || templateId == RegExRedirect.TemplateId;
    }
  }
}