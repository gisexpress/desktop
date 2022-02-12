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

namespace System.Windows.Forms
{
    partial class ListViewEdit
    {
        public interface IRowList : IRowCollection
        {
            event ListChangedEventHandler ListChanged;

            Row New(object value);
            Row New(object value, string text);
            Row New(object value, string text, Image image);

            Row Add(object value);
            Row Add(object value, string text);
            Row Add(object value, string text, Image image);

            bool Remove(object value);
        }

        public interface IRowCollection : IList<ListViewEdit.Row>
        {
            ListView View
            {
                get;
            }

            void SelectAll();
            void ClearSelection();

            Row FindByKey(object key);
            int IndexOfKey(object value);

            void ShowAll();
            void Show(Row row, int index);

            void HideAll();
            void Hide(Row row);

            bool IsVisible(Row row);
            bool IsSelected(Row row);

            bool IsChecked();
            bool IsChecked(Row row);

            void SetChecked(bool value);
            void SetChecked(Row row, bool value);
            void SetSelected(Row row, bool value);

            bool SupportsSorting { get; }
            bool SupportsFiltering { get; }

            void Sort();
            void Sort(IComparer<Row> comparison);
            
            void ApplyFilter(string filterString);

            void RemoveSelectedRows();

            IEnumerable<Row> GetCheckedRows();
            IEnumerable<Row> GetSelectedRows();
            IEnumerable<Row> GetAllRows();
        }
    }
}