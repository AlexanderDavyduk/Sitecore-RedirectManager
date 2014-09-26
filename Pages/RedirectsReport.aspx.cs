// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedirectsReport.aspx.cs" company="Sitecore A/S">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the RedirectsReport type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using Sitecore.Data;

namespace Sitecore.SharedSource.RedirectManager.Pages
{
  using System;
  using System.Web.UI.WebControls;
  using Sitecore.Configuration;
  using Sitecore.Data.Items;
  using Sitecore.SharedSource.RedirectManager.Templates;
  using Sitecore.SharedSource.RedirectManager.Utils;

  /// <summary>
  /// The RedirectsReport
  /// </summary>
  public partial class RedirectsReport : System.Web.UI.Page
  {
    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      this.GenerateReport();
    }

    /// <summary>
    /// Builds the name of the item to item node.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>item to item node name</returns>
    private static string BuildItemToItemNodeName(ItemToItem item)
    {
      if (item == null)
      {
        return string.Empty;
      }

      var targetUrl = "Empty";
      if (!string.IsNullOrEmpty(item.TargetItem.Url) || item.TargetItem.TargetItem != null)
      {
        targetUrl = item.TargetItem.IsInternal ? UrlNormalizer.GetItemUrl(item.TargetItem.TargetItem) : item.TargetItem.Url;
      }

      return string.Format(
           "<div class=\"block-name\"><div class=\"name\">{0}</div><div class=\"title\">Base Url: {2}, Target Url: {3}</div></div><div class=\"description\">Redirect Code: {4}, Multisites: {6}, Last Use: {5}, ID: {1}</div>",
           item.Name,
           item.ID,
           string.IsNullOrEmpty(item.BaseItem.Value) ? "Empty" : UrlNormalizer.EncodeUrl(UrlNormalizer.CheckPageExtension(UrlNormalizer.Normalize(item.BaseItem.Value))),
           UrlNormalizer.EncodeUrl(targetUrl),
           item.RedirectCode != 0 ? item.RedirectCode : Configuration.RedirectStatusCode,
           item.LastUse.DateTime.ToString("MM/dd/yy") != "01/01/01" ? item.LastUse.DateTime.ToString("MM/dd/yy") : "Never",
           UrlNormalizer.EncodeUrl(RedirectProcessor.ConvertMultisites(item.Multisites)));
    }

    /// <summary>
    /// Builds the section to item node.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>section to item node</returns>
    private static string BuildSectionToItemNode(SectionToItem item)
    {
      if (item == null)
      {
        return string.Empty;
      }

      var targetUrl = "Empty";
      if (!string.IsNullOrEmpty(item.TargetItem.Url) || item.TargetItem.TargetItem != null)
      {
        targetUrl = item.TargetItem.IsInternal ? UrlNormalizer.GetItemUrl(item.TargetItem.TargetItem) : item.TargetItem.Url;
      }

      return string.Format(
         "<div class=\"block-name\"><div class=\"name\">{0}</div><div class=\"title\">Base Section Url: {2}, Target Url: {3}</div></div><div class=\"description\">Redirect Code: {4}, Multisites: {6}, Last Use: {5}, ID: {1}</div>",
         item.Name,
         item.ID,
         string.IsNullOrEmpty(item.BaseSection.Value) ? "Empty" : UrlNormalizer.EncodeUrl(UrlNormalizer.CheckPageExtension(UrlNormalizer.Normalize(item.BaseSection.Value))),
         UrlNormalizer.EncodeUrl(targetUrl),
         item.RedirectCode != 0 ? item.RedirectCode : Configuration.RedirectStatusCode,
         item.LastUse.DateTime.ToString("MM/dd/yy") != "01/01/01" ? item.LastUse.DateTime.ToString("MM/dd/yy") : "Never",
         UrlNormalizer.EncodeUrl(RedirectProcessor.ConvertMultisites(item.Multisites)));
    }

    /// <summary>
    /// Builds the section to section node.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>section to section node</returns>
    private static string BuildSectionToSectionNode(SectionToSection item)
    {
      if (item == null)
      {
        return string.Empty;
      }

      return string.Format(
         "<div class=\"block-name\"><div class=\"name\">{0}</div><div class=\"title\">Base Section Url: {2}, Target Url: {3}</div></div><div class=\"description\">Redirect Code: {4}, Multisites: {6}, Last Use: {5}, ID: {1}</div>",
         item.Name,
         item.ID,
         string.IsNullOrEmpty(item.BaseSection.Value) ? "Empty" : UrlNormalizer.EncodeUrl(UrlNormalizer.CheckPageExtension(UrlNormalizer.Normalize(item.BaseSection.Value))),
         item.TargetSection.TargetItem != null ? UrlNormalizer.EncodeUrl(UrlNormalizer.GetItemUrl(item.TargetSection.TargetItem)) : "Empty",
         item.RedirectCode != 0 ? item.RedirectCode : Configuration.RedirectStatusCode,
         item.LastUse.DateTime.ToString("MM/dd/yy") != "01/01/01" ? item.LastUse.DateTime.ToString("MM/dd/yy") : "Never",
         UrlNormalizer.EncodeUrl(RedirectProcessor.ConvertMultisites(item.Multisites)));
    }

    /// <summary>
    /// Builds the name of the regex node.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>regex node</returns>
    private static string BuildRegExNodeName(RegExRedirect item)
    {
      if (item == null)
      {
        return string.Empty;
      }

      return string.Format(
            "<div class=\"block-name\"><div class=\"name\">{0}</div><div class=\"title\">Expression: {2}, Value: {3}</div></div><div class=\"description\">Redirect Code: {4}, Last Use: {5}, ID: {1}</div>",
            item.Name,
            item.ID,
            !string.IsNullOrEmpty(item.Expression.Value) ? item.Expression.Value : "Empty",
            !string.IsNullOrEmpty(item.Value.Value) ? item.Value.Value : "Empty",
            item.RedirectCode != 0 ? item.RedirectCode : Configuration.RedirectStatusCode,
            item.LastUse.DateTime.ToString("MM/dd/yy") != "01/01/01" ? item.LastUse.DateTime.ToString("MM/dd/yy") : "Never");
    }

    /// <summary>
    /// Gets the icon path.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>icon path</returns>
    private static string GetIconPath(Item item)
    {
      return item == null ? string.Empty : string.Format("/~/icon/{0}", item["__icon"]);
    }

    /// <summary>
    /// The build node name.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string BuildNodeName(Item item)
    {
      if (item == null)
      {
        return string.Empty;
      }

      var templateId = item.TemplateID.ToString();
      switch (templateId)
      {
        case ItemToItem.TemplateId:
          var itemToItem = new ItemToItem(item);
          return BuildItemToItemNodeName(itemToItem);

        case SectionToItem.TemplateId:
          var sectionToItem = new SectionToItem(item);
          return BuildSectionToItemNode(sectionToItem);

        case SectionToSection.TemplateId:
          var sectionToSection = new SectionToSection(item);
          return BuildSectionToSectionNode(sectionToSection);

        case RegExRedirect.TemplateId:
          var regExRedirect = new RegExRedirect(item);
          return BuildRegExNodeName(regExRedirect);
      }

      return string.Format("<p>{0}</p>", item.Name);
    }

    /// <summary>
    /// Checks the template.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>true if acceptable template; otherwise - false</returns>
    private static bool CheckTemplate(Item item)
    {
      var templateId = item.TemplateID.ToString();
      return templateId == ItemToItem.TemplateId || templateId == SectionToItem.TemplateId || templateId == SectionToSection.TemplateId || templateId == RegExRedirect.TemplateId || templateId == "{75A8F163-186F-459E-8AFF-3D161A94A9C7}";
    }

    /// <summary>
    /// Checks the template without folder.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    private static bool CheckTemplateWithoutFolder(Item item)
    {
      var templateId = item.TemplateID.ToString();
      return templateId == ItemToItem.TemplateId || templateId == SectionToItem.TemplateId || templateId == SectionToSection.TemplateId || templateId == RegExRedirect.TemplateId;
    }

    /// <summary>
    /// Generates the report.
    /// </summary>
    /// <param name="showOld">if set to <c>true</c> [show old].</param>
    /// <param name="buttonClick">if set to <c>true</c> [button click].</param>
    private void GenerateReport(bool showOld = false, bool buttonClick = false)
    {
      if (this.Page.IsPostBack && !buttonClick)
      {
        return;
      }

      this.Redirects.Nodes.Clear();
      var rootItem = Factory.GetDatabase(Configuration.Database).GetItem(RedirectManager.Items.ItemIDs.RedirectsFolderItem);

      if (rootItem == null)
      {
        return;
      }

      var rootNode = new TreeNode(BuildNodeName(rootItem), rootItem.ID.ToString(), GetIconPath(rootItem));
      if (showOld)
      {
        this.AddDescendants(rootNode, rootItem);
      }
      else
      {
        this.AddChildNodes(rootNode, rootItem);
      }

      this.Redirects.Nodes.Add(rootNode);
      this.Redirects.ExpandAll();
    }

    /// <summary>
    /// Adds the child nodes.
    /// </summary>
    /// <param name="rootNode">The root node.</param>
    /// <param name="rootItem">The root item.</param>
    private void AddChildNodes(TreeNode rootNode, Item rootItem)
    {
      foreach (var item in rootItem.Children.ToArray().Where(CheckTemplate))
      {
        var node = new TreeNode(BuildNodeName(item), item.ID.ToString(), GetIconPath(item));
        this.AddChildNodes(node, item);
        rootNode.ChildNodes.Add(node);
      }
    }

    /// <summary>
    /// Adds the descendants.
    /// </summary>
    /// <param name="rootNode">The root node.</param>
    /// <param name="rootItem">The root item.</param>
    private void AddDescendants(TreeNode rootNode, Item rootItem)
    {
      var children = rootItem.Axes.GetDescendants();

      if (!children.Any())
      {
        return;
      }

      var currentDate = DateTime.Now;
      foreach (var item in from item in children.Where(x => x.IsItemOfType(Templates.Settings.TemplateId))
        let settings = new Templates.Settings(item)
        where
          (currentDate - settings.LastUse.DateTime).Days >= Configuration.RemovalDate
        select item)
      {
        var node = new TreeNode(BuildNodeName(item), item.ID.ToString(), GetIconPath(item));
        rootNode.ChildNodes.Add(node);
      }
    }

    /// <summary>
    /// Handles the Click event of the ShowOldRedirects control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    public void ShowOldRedirects_Click(object sender, EventArgs e)
    {
      this.GenerateReport(true, true);
    }

    /// <summary>
    /// Handles the Click event of the ShowAllRedirects control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    public void ShowAllRedirects_Click(object sender, EventArgs e)
    {
      this.GenerateReport(false, true);
    }
  }
}