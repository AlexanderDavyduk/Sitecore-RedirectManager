// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemToItem.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   ItemToItem class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Templates
{
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.SecurityModel;

  /// <summary>
  /// ItemToItem class
  /// </summary>
  public sealed class ItemToItem : CustomItem
  {
    // Fields

    /// <summary>
    ///  ItemToItemTemplate template ID
    /// </summary>
    public const string TemplateId = "{D0BFFA7B-CA51-400D-9037-809DECFB14D3}";

    /// <summary>
    ///  Base Item field
    /// </summary>
    private TextField baseItem;

    /// <summary>
    /// Target Item field
    /// </summary>
    private LinkField targetItem;

    /// <summary>
    /// The redirect code
    /// </summary>
    private int redirectCode;

    /// <summary>
    /// The date of last use
    /// </summary>
    private DateField lastUse;

    /// <summary>
    /// The multisites
    /// </summary>
    private MultilistField multisites;

    /// <summary>
    /// The use on default
    /// </summary>
    private CheckboxField useOnDefault;

    // Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemToItem"/> class.
    /// </summary>
    /// <param name="innerItem">
    /// The inner item.
    /// </param>
    public ItemToItem(Item innerItem)
      : base(innerItem)
    {
    }

    // Properties

    /// <summary>
    /// Gets the base item.
    /// </summary>
    public TextField BaseItem
    {
      get
      {
        return this.baseItem ?? (this.baseItem = this.InnerItem.Fields["Base Item"]);
      }
    }

    /// <summary>
    /// Gets the target item.
    /// </summary>
    public LinkField TargetItem
    {
      get
      {
        return this.targetItem ?? (this.targetItem = this.InnerItem.Fields["Target Item"]);
      }
    }

    /// <summary>
    /// Gets the redirect code.
    /// </summary>
    /// <value>
    /// The redirect code.
    /// </value>
    public int RedirectCode
    {
      get
      {
        return this.redirectCode == 0 ? (this.redirectCode = MainUtil.GetInt(this.InnerItem.Fields["Redirect Code"].Value, Configuration.RedirectStatusCode)) : this.redirectCode;
      }
    }

    /// <summary>
    /// Gets the multisite prefix.
    /// </summary>
    /// <value>
    /// The multisite prefix.
    /// </value>
    public MultilistField Multisites
    {
      get
      {
        return this.multisites ?? (this.multisites = this.InnerItem.Fields["Multisites"]);
      }
    }

    /// <summary>
    /// Gets the last use.
    /// </summary>
    /// <value>
    /// The last use.
    /// </value>
    public DateField LastUse
    {
      get
      {
        return this.lastUse ?? (this.lastUse = this.InnerItem.Fields["Last Use"]);
      }
    }

    /// <summary>
    /// Gets the use on default.
    /// </summary>
    /// <value>
    /// The use on default.
    /// </value>
    public CheckboxField UseOnDefault
    {
      get
      {
        return this.useOnDefault ?? (this.useOnDefault = this.InnerItem.Fields["Use on default site"]);
      }
    }
  }
}