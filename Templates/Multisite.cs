// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Multisite.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Settings class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Templates
{
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;

  /// <summary>
  /// ItemToItem class
  /// </summary>
  public sealed class Multisite : CustomItem
  {
    // Fields

    /// <summary>
    ///  ItemToItemTemplate template ID
    /// </summary>
    public static readonly ID TemplateId = new ID("{A77B0208-6FD5-4FF5-BEED-A922EDB35A90}");
   
    /// <summary>
    /// The multisite prefix
    /// </summary>
    private TextField multisitePrefix;

    // Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="Multisite"/> class.
    /// </summary>
    /// <param name="innerItem">Inner item.</param>
    public Multisite(Item innerItem)
      : base(innerItem)
    {
    }

    // Properties
    
    /// <summary>
    /// Gets the multisite prefix.
    /// </summary>
    /// <value>
    /// The multisite prefix.
    /// </value>
    public TextField MultisitePrefix
    {
      get
      {
        return this.multisitePrefix ?? (this.multisitePrefix = this.InnerItem.Fields["Multisite Prefix"]);
      }
    }
  }
}