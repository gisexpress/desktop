﻿//////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Copyright © GISExpress 2015-2022. All Rights Reserved.
//  
//  GISExpress .NET API and Component Library
//  
//  The entire contents of this file is protected by local and International Copyright Laws.
//  Unauthorized reproduction, reverse-engineering, and distribution of all or any portion of
//  the code contained in this file is strictly prohibited and may result in severe civil and 
//  criminal penalties and will be prosecuted to the maximum extent possible under the law.
//  
//  RESTRICTIONS
//  
//  THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES ARE CONFIDENTIAL AND PROPRIETARY TRADE SECRETS OF GISExpress
//  THE REGISTERED DEVELOPER IS LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET COMPONENTS AS PART OF AN EXECUTABLE PROGRAM ONLY.
//  
//  THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE
//  COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT
//  AND PERMISSION FROM GISExpress
//  
//  CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON ADDITIONAL RESTRICTIONS.
//  
//  Warning: This content was generated by GISExpress tools.
//  Changes to this content may cause incorrect behavior and will be lost if the content is regenerated.
//
///////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace System.Windows.Forms
{
    partial class PropertyGridEdit
    {
        public class GridItemCollection : ListViewEdit.RowCollection
        {
            public GridItemCollection(GridView view)
                : base(view)
            {
                View = view;
            }

            protected internal new GridView View;

            public GridCategoryItem FindCategory(string category)
            {
                return this.FirstOrDefault(item => item is GridCategoryItem && ((GridCategoryItem)item).Category.EqualsIgnoreCase(category)) as GridCategoryItem;
            }

            public void Add(PropertyDescriptorCollection properties)
            {
                Insert(-1, 0, null, properties);
            }

            public new GridItem this[int index]
            {
                get { return (GridItem)base[index]; }
            }

            public void Insert(int index, int level, GridItem parent, PropertyDescriptorCollection properties)
            {
                GridPropertyItem item;
                
                var descriptors = properties.OfType<IPropertyDescriptor>();
                var categories = new List<string>(descriptors.Select(e => e.Category).Distinct());

                foreach (PropertyDescriptor property in descriptors.OrderBy(e => categories.IndexOf(e.Category)))
                {
                    if (property.IsBrowsable)
                    {
                        if (level == 0)
                        {
                            GridCategoryItem category = FindCategory(property.Category);

                            if (category == null)
                            {
                                Add(index, category = new GridCategoryItem(this, property));
                            }
                        }

                        Add(index, item = new GridPropertyItem(this)
                        {
                            Parent = parent,
                            PropertyDescriptor = property,
                            Level = level
                        });

                        if (item.IsExpandable)
                        {
                            Insert(index < 0 ? -1 : index + 1, level + 1, item, item.ChildProperties);
                            item.Collapse();
                        }
                    }
                }
            }

            protected void Add(int index, GridItem item)
            {
                if (index < 0)
                {
                    Add(item);
                }
                else
                {
                    Insert(index, item);
                }
            }

            protected override ListViewEdit.Row OnCreateItem(object value, string text, Image image)
            {
                return new GridPropertyItem(this) { Value = value, Text = text, Image = image };
            }
        }
    }
}