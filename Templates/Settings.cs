// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Settings class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Templates
{
  using System;
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.SecurityModel;

  /// <summary>
  /// ItemToItem class
  /// </summary>
  public sealed class Settings : CustomItem
  {
    // Fields

    /// <summary>
    ///  ItemToItemTemplate template ID
    /// </summary>
    public static readonly ID TemplateId = new ID("{0E69AF35-6BF1-412C-8ECF-D9EA60BC3086}");

    /// <summary>
    /// The last use field identifier
    /// </summary>
    public static readonly ID LastUseFieldId = new ID("{64E091E5-91B4-485E-9F60-48607199197C}");
   
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

    // Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="Settings" /> class.
    /// </summary>
    /// <param name="innerItem">The inner item.</param>
    public Settings(Item innerItem)
      : base(innerItem)
    {
    }

    // Properties
    
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
        return this.redirectCode == 0 ? (this.redirectCode = MainUtil.GetInt(this.InnerItem.Fields["Redirect Code"], Configuration.RedirectStatusCode)) : this.redirectCode;
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
    /// Updates the last use.
    /// </summary>
    public void UpdateLastUseWithCurrentDate()
    {
      if (this.LastUse != null && this.LastUse.DateTime.Date == DateTime.Now.Date)
      {
        return;
      }

      using (new SecurityDisabler())
      {
        this.BeginEdit();
        this["Last Use"] = DateUtil.IsoNowDate;
        this.lastUse = this.InnerItem.Fields["Last Use"];
        this.EndEdit();
      }
    }
  }
}