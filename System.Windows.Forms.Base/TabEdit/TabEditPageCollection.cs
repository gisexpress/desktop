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

using System.Collections.ObjectModel;
using System.Linq;

namespace System.Windows.Forms
{
    public class TabEditPageCollection : Collection<TabEditPage>
    {
        public TabEditPageCollection(TabEdit owner)
        {
            Owner = owner;
        }

        protected TabEdit Owner;

        public virtual TabEditPage this[string key]
        {
            get { return Items.FirstOrDefault(e => e.Name.EqualsIgnoreCase(key)); }
        }

        protected override void ClearItems()
        {
            Owner.Controls.Clear();
            base.ClearItems();
        }

        protected override void SetItem(int index, TabEditPage item)
        {
            throw new NotImplementedException();
        }

        protected override void InsertItem(int index, TabEditPage item)
        {
            Owner.Header.AddColumn();
            Owner.Header.Controls.Add(item.Caption, Owner.Header.ColumnCount - 1, 0);
            Owner.Controls.Add(item, 0, 1);

            item.Caption.UpdateLayout(true);

            base.InsertItem(index, item);

            if (Owner.SelectedIndex < 0)
            {
                item.Activate();
            }
        }

        protected override void RemoveItem(int index)
        {
            TabEditPage page = this[index];

            if (page.HasValue())
            {
                Owner.Header.RemoveColumn(page.Caption);
                Owner.Controls.Remove(this[index]);

                base.RemoveItem(index);

                if (index > 0 && Owner.PreviousTab.HasValue() && Owner.PreviousTab.TabPageIndex >= 0)
                {
                    Owner.OnDeselected(new TabEditEventArgs
                    {
                        TabPage = page,
                        TabPageIndex = index,
                        Action = TabControlAction.Deselected
                    });

                    Owner.PreviousTab.Activate();
                }
                else if (Count > 0)
                {
                    Items.First().Activate();
                }

                page.Dispose();
            }
        }
    }
}
