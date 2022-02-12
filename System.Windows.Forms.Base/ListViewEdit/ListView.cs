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
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
    partial class ListViewEdit
    {
        public class ListView : BaseEdit, ISupportInitialize, ISupportDispatcher, IDispatcher
        {
            public ListView()
            {
                TabStop = true;
                Margin = new Padding(0);
                Padding = new Padding(1, 1, 0, 0);
                SetStyle(ControlStyles.Selectable, true);
                CellBorderStyle = Border3DSide.Bottom | Border3DSide.Right;
                IsListView = GetType().Equals(typeof(ListView));

                Header = new ListViewHeader();
                RowHeight = 24;
            }

            public event Action ColumnLayout;
            public event EventHandler ItemClick;
            public event EventHandler ItemDoubleClick;
            public event EventHandler SelectedIndexChanged;
            public event Action<ListViewEditCellPaintingEventArgs> PaintCell;

            protected int CurrentRow = -1;
            protected int CurrentHitRow = -1;
            protected int CurrentSelectedRow = -1;
            protected internal byte Init;

            protected bool IsListView;
            protected internal ListViewHeader Header;
            protected ImageColumn ImageColumn;
            protected CheckBoxColumn CheckColumn;
            protected IRowList RowItems;
            protected ColumnCollection ColumnItems;
            protected ListViewEditCellPaintingEventArgs PaintingArgs;

            public bool IsInitializing
            {
                get { return Init > 0; }
            }

            public void BeginInit()
            {
                Init++;
            }

            public void EndInit()
            {
                Init--;
            }

            public override object EditValue
            {
                get
                {
                    if (SelectedRow >= 0 && SelectedRow < Items.Count)
                    {
                        return Items[SelectedRow].Value;
                    }

                    return default(object);
                }
                set
                {
                    if (value.HasValue())
                    {
                        SelectedRow = Items.IndexOfKey(value);
                    }
                }
            }

            public int FirstVisibleRow
            {
                get;
                protected set;
            }

            public int LastVisibleRow
            {
                get;
                protected set;
            }

            public int HeaderHeight
            {
                get { return Header.HasValue() && Header.IsVisible ? RowHeight : 0; }
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int RowHeight
            {
                get { return (int)Properties["RowHeight"]; }
                set { Properties["RowHeight"] = Header.ItemHeight = value; }
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public virtual bool CheckBox
            {
                get { return (bool)(Properties["CheckBox"] ?? false); }
                set { Properties["CheckBox"] = value; }
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool MultiSelect
            {
                get;
                set;
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool HitItemEnabled
            {
                get;
                set;
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ListViewHeaderVisibilty HeaderVisibilty
            {
                get { return (ListViewHeaderVisibilty)Properties["HeaderVisibilty", ListViewHeaderVisibilty.Auto]; }
                set { Properties["HeaderVisibilty"] = value; }
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ListViewFilterMode FilterMode
            {
                get { return (ListViewFilterMode)Properties["FilterMode", ListViewFilterMode.Auto]; }
                set { Properties["FilterMode"] = value; }
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public virtual int SelectedRow
            {
                get
                {
                    return CurrentSelectedRow;
                }
                set
                {
                    if (CurrentSelectedRow != value)
                    {
                        if (value >= 0 && value < Items.Count)
                        {
                            Row row = Items[value];

                            if (row.HasValue())
                            {
                                CurrentRow = value;
                                CurrentSelectedRow = value;
                                Items.ClearSelection();
                                row.Selected = true;
                                SelectedIndexChanged.InvokeSafely(this, EventArgs.Empty);
                                EnsureVisible(value);
                                Redraw();
                            }
                        }
                        else
                        {
                            Items.ClearSelection();
                            CurrentSelectedRow = -1;
                            Redraw();
                        }
                    }
                }
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public virtual Color CellBorderColor
            {
                get;
                set;
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public virtual Border3DSide CellBorderStyle
            {
                get;
                set;
            }

            public ListComponent List
            {
                get { return Properties["ListComponent"] as ListComponent; }
            }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public object DataSource
            {
                get { return List.HasValue() ? List.Component : default(object); }
                set
                {
                    if (DeveloperEnvironment.RuntimeMode)
                    {
                        SelectedRow = -1;
                        List.DisposeSafely();
                        Properties["ListComponent"] = new ListComponent(this, value);
                        UpdateLayout(true);
                    }
                }
            }

            public IEnumerable<object> GetChildViews(int rowIndex)
            {
                Row row = Items[rowIndex];

                if (row.HasValue())
                {
                    foreach (PropertyColumn c in Columns.OfType<PropertyColumn>())
                    {
                        if (c.IsExpandable)
                        {
                            yield return c.Property.GetValue(row.Value);
                        }
                    }
                }
            }

            public IEnumerable<Row> CheckedRows
            {
                get { return Items.GetCheckedRows(); }
            }

            public IEnumerable<Row> SelectedRows
            {
                get { return Items.GetSelectedRows(); }
            }

            public int VisibleItemCount
            {
                get { return WorkingArea.Height / RowHeight; }
            }

            public IRowList Items
            {
                get { return List ?? Rows; }
            }

            public IRowList Rows
            {
                get { return RowItems ?? (RowItems = OnCreateRowCollection()); }
            }

            public ColumnCollection Columns
            {
                get { return ColumnItems ?? (ColumnItems = OnCreateColumnCollection()); }
            }

            public void Select(int row)
            {
                OnSelect(row);
            }

            public void EnsureVisible(int row)
            {
                var bounds = GetRowRectangle(row - TopRowIndex);

                UpdateLayout(false);

                if (ClientRectangle.Contains(Padding.Left, bounds.Top + bounds.Height / 2) == false)
                {
                    if (row <= FirstVisibleRow)
                    {
                        TopRowIndex = row;
                    }
                    else if (row >= LastVisibleRow)
                    {
                        TopRowIndex = row - VisibleItemCount + 1;
                    }
                }
            }

            public void Redraw()
            {
                if (IsHandleCreated && Header.HasValue())
                {
                    Invalidate(false);

                    if (Header.IsVisible)
                    {
                        Header.Invalidate(false);
                    }
                }
            }

            public int TopRowIndex
            {
                get
                {
                    return (VerticalScroll.Value - VerticalScroll.Value % RowHeight) / RowHeight;
                }
                protected set
                {
                    if (VerticalScroll.Visible)
                    {
                        var position = RowHeight * MakeValidRow(value);

                        if (position >= VerticalScroll.Minimum && position <= VerticalScroll.Maximum)
                        {
                            VerticalScroll.Value = position;
                        }
                    }

                    PerformLayout();
                    Redraw();
                }
            }

            public int RowWidth
            {
                get
                {
                    Column c = Columns.LastOrDefault();

                    if (c.HasValue())
                    {
                        return c.Right - Padding.Horizontal;
                    }

                    return Padding.Right;
                }
            }

            public virtual int RowMinWidth
            {
                get
                {
                    Column c = Columns.LastOrDefault(e => e.Style.HasValue());

                    if (c.HasValue())
                    {
                        return c.Right - Padding.Horizontal;
                    }

                    return Padding.Right;
                }
            }

            protected int MakeValidRow(int row)
            {
                return Math.Max(0, Math.Min(row, Items.Count - 1));
            }

            protected int GetRowIndex(int position)
            {
                return TopRowIndex + (position + VerticalScroll.Value % RowHeight - Padding.Top) / RowHeight;
            }

            protected internal Rectangle GetRowRectangle(int row)
            {
                return new Rectangle(Padding.Left, Padding.Top + row * RowHeight - VerticalScroll.Value % RowHeight, RowWidth, RowHeight);
            }

            protected virtual IRowList OnCreateRowCollection()
            {
                return List ?? new RowCollection(this) as IRowList;
            }

            protected virtual ColumnCollection OnCreateColumnCollection()
            {
                return new ColumnCollection(this);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                if (Items.HasValue())
                {
                    using (PaintingArgs = new ListViewEditCellPaintingEventArgs())
                    {
                        UpdateLayout(false);

                        int begin = TopRowIndex;
                        var rect = ClientRectangle;

                        FirstVisibleRow = -1;

                        for (int n = 0; n <= VisibleItemCount + 1; n++)
                        {
                            int index = begin + n;

                            if (index < Items.Count)
                            {
                                PaintingArgs.B = GetRowRectangle(n);

                                if (rect.Top <= PaintingArgs.B.Top && rect.Bottom >= PaintingArgs.B.Bottom)
                                {
                                    if (FirstVisibleRow < 0)
                                    {
                                        FirstVisibleRow = index;
                                    }
                                    else
                                    {
                                        LastVisibleRow = index;
                                    }
                                }

                                PaintRow(e.Graphics, PaintingArgs.B, Items[index], index, DrawItemState.None);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    if (Header.IsVisible)
                    {
                        e.Graphics.DrawBorder(Border3DSide.Top, 0, Padding.Top, Width, RowHeight);
                    }
                }
            }

            public void PaintRow(Graphics g, Rectangle bounds, Row row, int index, DrawItemState state)
            {
                if (row.HasValue())
                {
                    PaintingArgs = PaintingArgs ?? new ListViewEditCellPaintingEventArgs();
                    PaintingArgs.G = g;
                    PaintingArgs.R = row;
                    PaintingArgs.B = bounds;
                    PaintingArgs.Stroke = true;

                    if (state == DrawItemState.ComboBoxEdit)
                    {
                        if (row.Image.HasValue())
                        {
                            Rectangle r = bounds;

                            r.X = Padding.Left + bounds.Left;
                            r.Width = row.Image.Width + 6;
                            r.Width = OnPaintImage(g, row.Image, r, false);

                            PaintingArgs.B.Offset(r.Width + Padding.Left, 0);
                        }

                        PaintingArgs.C = null;
                        PaintingArgs.B.Width -= 34;
                        OnPaintCell(PaintingArgs);
                    }
                    else
                    {
                        PaintingArgs.IsHit = CurrentHitRow == index;

                        if (PaintingArgs.Row.Selected || PaintingArgs.IsHit)
                        {
                            OnPaintSelectedRow(PaintingArgs);
                        }

                        if (IsListView && CheckBox)
                        {
                            Rectangle r = bounds;

                            r.X = CheckColumn.Left + 3;
                            r.Y += (r.Height - 16) / 2;
                            r.Width = 16;
                            r.Height = 16;

                            g.DrawCheckBox(r, true, PaintingArgs.Row.Checked);

                            PaintingArgs.B.Offset(CheckColumn.Width, 0);
                            PaintingArgs.B.Width -= CheckColumn.Width * 2;
                        }

                        if (ImageColumn.HasValue())
                        {
                            Rectangle r = bounds;

                            r.X = AutoScrollPosition.X + ImageColumn.Left;
                            r.Width = ImageColumn.Width;

                            OnPaintImage(g, row.Image, r, true);
                        }

                        foreach (Column c in Columns.Where(c => c.Browsable && c.Visible))
                        {
                            if (c == CheckColumn) continue;
                            if (c == ImageColumn) continue;

                            PaintingArgs.C = c;
                            PaintingArgs.B.X = AutoScrollPosition.X + c.Left + Parent.Padding.Left;
                            PaintingArgs.B.Width = c.Width;

                            OnPaintCell(PaintingArgs);

                            PaintingArgs.B.Offset(PaintingArgs.B.Width, 0);
                        }
                    }
                }
            }

            protected virtual void OnPaintSelectedRow(ListViewEditCellPaintingEventArgs e)
            {
                PaintingArgs.B.Width += AutoScrollPosition.X;
                PaintingArgs.B.Height += 1;

                if (e.IsHitItem)
                {
                    e.Graphics.DrawToolbarButton(e.Bounds, ToolBarState.Hot, false);
                }
                else
                {
                    e.Graphics.DrawToolbarButton(e.Bounds, Focused ? ToolBarState.HotChecked : ToolBarState.Disabled, false);
                }

                PaintingArgs.B.Width -= AutoScrollPosition.X;
                PaintingArgs.B.Height -= 1;
            }

            protected virtual int OnPaintImage(Graphics g, Image image, Rectangle bounds, bool fixedWidth)
            {
                if (image.HasValue())
                {
                    try
                    {
                        float s = Math.Min(1F, bounds.Height / (float)image.Height);
                        float w = s * image.Width;
                        float h = s * image.Height;

                        if (fixedWidth)
                        {
                            g.DrawImage(image, bounds.Left + (bounds.Width - w) / 2, bounds.Top + (bounds.Height - h) / 2, w, h);
                        }
                        else
                        {
                            g.DrawImage(image, bounds.Left, bounds.Top + (bounds.Height - h) / 2, w, h);
                            return (int)w;
                        }
                    }
                    catch (Exception e)
                    {
                        e.Print();
                    }
                }

                return bounds.Width;
            }

            protected virtual void OnPaintCell(ListViewEditCellPaintingEventArgs e)
            {
                try
                {
                    PaintCell.InvokeSafely(e);

                    if (!e.Cancel)
                    {
                        if (e.Column.IsNull())
                        {
                            e.B.Offset(3, 0);
                            e.B.Width -= 6;

                            e.Graphics.DrawText(new DrawTextEventArgs
                            {
                                Text = e.Row.Text,
                                Font = Font,
                                Bounds = e.Bounds,
                                ForeColor = ForeColor,
                                FormatFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
                            });
                        }
                        else
                        {
                            e.Column.PaintValue(e.Graphics, e.Bounds, e.Row);
                        }
                    }

                    e.Cancel = default(bool);
                }
                catch (Exception error)
                {
                    error.Print();
                }
            }

            public IDispatcher Dispatcher
            {
                get { return this; }
                set { }
            }

            public void BeginUpdate()
            {
            }

            public void EndUpdate()
            {
                UpdateScrolls();
                Redraw();
            }

            protected override bool OnUpdateLayout(bool performLayout)
            {
                if ((Properties.HasChanges || performLayout) || Columns.Count == 0)
                {
                    if (Parent.HasValue() && Header.Parent.IsNull())
                    {
                        if (Items.Count > 0)
                        {
                            OnUpdateHeaderLayout(performLayout);
                        }
                    }

                    switch (HeaderVisibilty)
                    {
                        case ListViewHeaderVisibilty.Auto:
                            Header.IsVisible = Columns.Any(c => !string.IsNullOrEmpty(c.Text));
                            break;

                        case ListViewHeaderVisibilty.Hidden:
                            Header.IsVisible = false;
                            break;

                        case ListViewHeaderVisibilty.AlwaysVisible:
                            Header.IsVisible = true;
                            break;
                    }

                    OnColumnLayout();
                    UpdateScrolls();

                    return true;
                }

                return false;
            }

            protected virtual void OnColumnLayout()
            {
                ColumnLayout.InvokeSafely();
            }

            protected virtual void OnUpdateHeaderLayout(bool performLayout)
            {
                var imageWidth = default(int);

                Header.Clear();
                Header.AddRow(SizeType.Absolute, RowHeight);

                if (Columns.Count == 0)
                {
                    if (List.HasValue())
                    {
                        RowItems = null;

                        foreach (PropertyDescriptor property in List.Properties)
                        {
                            Columns.Add(property);
                        }
                    }

                    Columns.Add(DefaultColumn.Create());
                }

                if (Items.HasValue() && Items.Count > 0 && (imageWidth = Items.Take(0x100).Max(r => r.Image.HasValue() ? r.Image.Width : 0)) > 0)
                {
                    if (ImageColumn.IsNull())
                    {
                        Columns.Insert(0, ImageColumn = new ImageColumn(imageWidth + 6));
                    }
                }
                else if (ImageColumn.HasValue())
                {
                    if (Columns.Remove(ImageColumn))
                    {
                        ImageColumn.Dispose();
                        ImageColumn = null;
                    }
                }

                if (IsListView && CheckBox)
                {
                    if (CheckColumn.IsNull())
                    {
                        Columns.Insert(0, CheckColumn = new CheckBoxColumn());
                    }
                }
                else if (CheckColumn.HasValue())
                {
                    if (Columns.Remove(CheckColumn))
                    {
                        CheckColumn.Dispose();
                        CheckColumn = null;
                    }
                }

                foreach (Column c in Columns.Where(c => c.Browsable))
                {
                    Header.Controls.Add(c, Header.AddColumn(c.SizeType, c.SizeValue), 0);
                }

                if (Header.IsVisible)
                {
                    if (Columns.Any(c => c.IsExpandable))
                    {
                        Columns.Insert(0, new ExpandableColumn());
                    }
                }

                Parent.Controls.Add(Header);
            }

            protected internal virtual void UpdateScrolls()
            {
                UpdateScrollBounds();
                OnSizeChanged(EventArgs.Empty);
            }

            protected void UpdateScrollBounds()
            {
                int value = 0;
                Size minSize = Size.Empty;

                if (Items.HasValue() && Items.Count > 0)
                {
                    value = RowHeight * Items.Count + Padding.Vertical;
                }

                if (Width < RowMinWidth)
                {
                    minSize.Width = RowMinWidth;
                }

                if (value > Height)
                {
                    minSize.Height = value;
                }

                if (minSize.IsEmpty)
                {
                    AutoScrollMinSize = Size.Empty;
                    AutoScrollPosition = Point.Empty;
                    VerticalScroll.Value = 0;
                }
                else
                {
                    VerticalScroll.SmallChange = RowHeight;
                    AutoScrollMinSize = minSize;
                }
            }

            protected override void OnDoubleClick(EventArgs e)
            {
                base.OnDoubleClick(e);

                if (SelectedRow >= 0)
                {
                    ItemDoubleClick.InvokeSafely(this, e);
                }
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                int row = GetRowIndex(e.Y);

                if (row < 0 || row >= Items.Count)
                {
                    row = -1;
                }
                else
                {
                    if (MultiSelect && ControlModifier)
                    {
                        Items[CurrentSelectedRow = row].Selected = true;
                    }
                    else if (MultiSelect && ShiftModifier)
                    {
                        int begin = Math.Min(SelectedRow, row);
                        int end = Math.Max(Math.Max(SelectedRow, 0), row);

                        Items.ClearSelection();

                        for (int n = begin; n <= end; n++)
                        {
                            Items[n].Selected = true;
                        }
                    }
                    else
                    {
                        SelectedRow = row;

                        if (SelectedRow >= 0)
                        {
                            ItemClick.InvokeSafely(this, e);
                        }
                    }

                    if (e.Button == MouseButtons.Left && e.X >= Header.Left && e.X <= Header.Right)
                    {
                        int x = e.X - AutoScrollPosition.X;
                        Column column = Columns.FirstOrDefault(c => c.Browsable && x >= c.Bounds.Left && x <= c.Bounds.Right);

                        if (column is CheckBoxColumn)
                        {
                            OnCheckClick(Items.GetSelectedRows());
                        }
                        else
                        {
                            if (OnCellClick(e.Location, column, row))
                            {
                                return;
                            }
                        }
                    }

                    Redraw();
                }

                base.OnMouseDown(e);
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {
                if (HitItemEnabled)
                {
                    OnHitItemChanged(e);
                    this.UpdateFocus();
                }

                base.OnMouseMove(e);
            }

            protected virtual bool OnCellClick(Point point, Column column, int row)
            {
                if (column.HasValue())
                {
                    return column.OnCellClick(point, row);
                }

                return false;
            }

            protected virtual void OnHitItemChanged(MouseEventArgs e)
            {
                int n;

                if ((n = GetRowIndex(e.Location.Y)) != CurrentHitRow)
                {
                    CurrentHitRow = n;
                    Redraw();
                }
            }

            protected override bool IsInputKey(Keys keyData)
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        return true;
                }

                return base.IsInputKey(keyData);
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                e.Handled = ProcessKeyDown(e.KeyCode);

                if (ControlModifier && e.KeyCode == Keys.A)
                {
                    e.Handled = true;
                    Items.SelectAll();
                    Redraw();
                }

                base.OnKeyDown(e);
            }

            protected override void OnPreviewTextChanged(string value)
            {
                if (string.IsNullOrEmpty(value))
                {
                    SelectedRow = -1;
                }
                else
                {
                    Column c = Columns.FirstOrDefault(e => e.CanFilter);

                    if (c.HasValue())
                    {
                        Row r = Items.FirstOrDefault(e => c.DisplayText(e).StartsWithIgnoreCase(value));

                        if (r.HasValue())
                        {
                            SelectedRow = r.Index;
                        }
                        else
                        {
                            PreviewText.Clear();
                        }
                    }
                }
            }

            protected internal bool ProcessKeyDown(Keys keyCode)
            {
                switch (keyCode)
                {
                    case Keys.Up:
                        OnSelect(CurrentRow > 0 ? CurrentRow - 1 : 0);
                        return true;

                    case Keys.Down:
                        OnSelect(CurrentRow + (CurrentRow < Items.Count - 1 ? 1 : 0));
                        return true;

                    case Keys.Home:
                        OnSelect(0);
                        return true;

                    case Keys.End:
                        OnSelect(Items.Count - 1);
                        return true;

                    case Keys.PageUp:
                        OnSelect(CurrentRow != FirstVisibleRow ? FirstVisibleRow : MakeValidRow(FirstVisibleRow - VisibleItemCount));
                        return true;

                    case Keys.PageDown:
                        OnSelect(CurrentRow = CurrentRow < LastVisibleRow ? LastVisibleRow : MakeValidRow(LastVisibleRow + VisibleItemCount));
                        return true;

                    case Keys.Space:
                        return OnCheckClick(SelectedRows);

                    case Keys.Enter:
                        if (SelectedRow >= 0)
                        {
                            ItemClick.InvokeSafely(this, EventArgs.Empty);
                        }
                        break;
                }

                return false;
            }

            protected virtual void OnSelect(int row)
            {
                if (ShiftModifier && MultiSelect)
                {
                    int begin = Math.Min(SelectedRow, row);
                    int end = Math.Max(Math.Max(SelectedRow, 0), row);

                    Items.ClearSelection();

                    for (int n = begin; n <= end; n++)
                    {
                        Items[n].Selected = true;
                    }

                    EnsureVisible(CurrentRow = row);
                    Redraw();
                }
                else
                {
                    if (SelectedRow == row)
                    {
                        Items.ClearSelection();
                    }

                    SelectedRow = row;
                }
            }

            protected virtual bool OnCheckClick(IEnumerable<Row> rows)
            {
                if (IsListView && CheckBox)
                {
                    bool value;
                    IEnumerator<Row> e = rows.GetEnumerator();

                    if (e.MoveNext())
                    {
                        value = e.Current.Checked;
                        e.Current.Checked = !value;

                        while (e.MoveNext())
                        {
                            e.Current.Checked = !value;
                        }

                        var checkEdit = Header.GetControlFromPosition(0, 0) as CheckEdit;

                        if (checkEdit.HasValue())
                        {
                            checkEdit.Tag = 0;
                            checkEdit.Checked = Items.IsChecked();
                            checkEdit.Tag = null;
                        }

                        Redraw();
                        return true;
                    }
                }

                return false;
            }

            protected override void OnMouseWheel(MouseEventArgs e)
            {
                if (!e.Delta.IsZero())
                {
                    OnUpdateHeaderSize();
                    Redraw();
                }

                base.OnMouseWheel(e);
            }

            protected override void OnScroll(ScrollEventArgs e)
            {
                if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    switch (e.Type)
                    {
                        case ScrollEventType.Last:
                        case ScrollEventType.SmallIncrement:
                        case ScrollEventType.LargeIncrement:
                            int newValue = Math.Min(VerticalScroll.Maximum, e.NewValue);

                            if (Padding.Top > 0)
                            {
                                newValue = Math.Min(VerticalScroll.Maximum, newValue % Padding.Top != Padding.Top ? e.NewValue + Padding.Top : e.NewValue);
                            }

                            if (e.NewValue != newValue)
                            {
                                e.NewValue = newValue;
                            }
                            break;
                    }
                }
                else if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    OnUpdateHeaderSize();
                }

                base.OnScroll(e);
                Redraw();
            }

            protected virtual void OnUpdateHeaderSize()
            {
                Header.OnParentSizeChanged(Padding.Left + AutoScrollPosition.X, Padding.Top, Math.Max(RowMinWidth, WorkingArea.Width), RowHeight);
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                if (Width > 0 && Header.HasValue())
                {
                    Top = Header.IsVisible ? RowHeight : Padding.Top;

                    if (Header.IsVisible)
                    {
                        Height = Parent.Height - RowHeight - Padding.Vertical;
                    }

                    if (RowMinWidth > Width)
                    {
                        UpdateScrollBounds();
                    }

                    OnUpdateHeaderSize();
                    Redraw();
                }

                base.OnSizeChanged(e);
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);

                if (disposing)
                {
                    if (RowItems.HasValue())
                    {
                        RowItems.Cast<IDisposable>().ToList().DisposeItems();
                        RowItems.Clear();
                    }

                    if (List.HasValue())
                    {
                        List.Dispose();
                    }

                    RowItems = null;
                    ColumnItems = null;
                    PaintingArgs = null;

                    GC.SuppressFinalize(this);
                }
            }
        }
    }
}