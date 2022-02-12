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
using System.Drawing.Design;
using System.Linq;

namespace System.Windows.Forms
{
    [ToolboxItem(true)]
    public partial class ListViewEdit : BaseEdit, ISupportPaint, ISupportEditValue
    {
        public ListViewEdit()
        {
            Padding = new Padding(1);
            BorderStyle = Border3DSide.All;
            BackColor = ApplicationAppereance.Colors.Window;
        }

        protected ListView CurrentView;

        public ListView View
        {
            get
            {
                if (CurrentView.IsNull())
                {
                    Controls.Add(CurrentView = OnCreateView());
                }

                return CurrentView;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                if (string.IsNullOrEmpty(View.Text) && View.List.HasValue() && View.List.Properties.Count > 0)
                {
                    return View.List.Name;
                }

                return View.Text;
            }
            set
            {
                View.Text = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object EditValue
        {
            get
            {
                if (CheckBox)
                {
                    object[] values = CheckedRows.Select(row => row.Value).ToArray();

                    if (values.Length > 1)
                    {
                        return values;
                    }

                    return values.FirstOrDefault();
                }

                return View.EditValue;
            }
            set
            {
                if (CurrentView.HasValue())
                {
                    CurrentView.SelectedRow = Rows.IndexOfKey(value);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RowHeight
        {
            get { return View.RowHeight; }
            set { View.RowHeight = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CheckBox
        {
            get { return View.CheckBox; }
            set { View.CheckBox = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MultiSelect
        {
            get { return View.MultiSelect; }
            set { View.MultiSelect = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HitItemEnabled
        {
            get { return View.HitItemEnabled; }
            set { View.HitItemEnabled = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListViewHeaderVisibilty HeaderVisibilty
        {
            get { return View.HeaderVisibilty; }
            set { View.HeaderVisibilty = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListViewFilterMode FilterMode
        {
            get { return View.FilterMode; }
            set { View.FilterMode = value; }
        }

        [Browsable(false)]
        public IRowList Rows
        {
            get { return View.List ?? View.Rows; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual object DataSource
        {
            get { return View.DataSource; }
            set { View.DataSource = value; }
        }

        [Browsable(false)]
        public ColumnCollection Columns
        {
            get { return View.Columns; }
        }

        public Row SelectedRow
        {
            get
            {
                if (View.SelectedRow >= 0)
                {
                    return View.Items[View.SelectedRow];
                }

                return default(Row);
            }
        }

        public IEnumerable<Row> CheckedRows
        {
            get { return View.CheckedRows; }
        }

        public IEnumerable<Row> SelectedRows
        {
            get { return View.SelectedRows; }
        }

        public void RemoveSelectedRows()
        {
            View.Rows.RemoveSelectedRows();
        }

        public void Redraw()
        {
            View.Redraw();
        }

        protected virtual ListView OnCreateView()
        {
            return new ListView();
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            Row row = Rows.FindByKey(e.Value);

            if (row.HasValue())
            {
                CurrentView.PaintRow(e.Graphics, e.Bounds, row, row.Index, DrawItemState.ComboBoxEdit);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            View.SetBounds(Padding.Left, Padding.Top, Width - Padding.Horizontal, Height - Padding.Vertical, BoundsSpecified.All);
            base.OnSizeChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                CurrentView = null;
            }
        }
    }
}
