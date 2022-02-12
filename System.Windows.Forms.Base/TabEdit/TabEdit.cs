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

using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace System.Windows.Forms
{
    public class TabEdit : TablePanelEdit
    {
        public TabEdit()
        {
            Header = new TablePanelEdit
            {
                RowCount = 1,
                Height = HeaderHeight,
                Dock = DockStyle.Top,
                Padding = new Padding(1),
                Margin = new Padding(1, 1, 1, 0),
            };

            Margin = new Padding(0);
            Padding = new Padding(3, 0, 3, 3);

            AddRow(SizeType.Absolute, HeaderHeight);
            AddRow(SizeType.Percent, 100);
            AddColumn(SizeType.Percent, 100);

            AutoSize = false;
            ShowHeader = false;
            BackColor = Color.Transparent;
        }

        protected internal TablePanelEdit Header;
        protected TabEditPageCollection Pages;
        protected ContextMenu CaptionMenu;

        const int HeaderHeight = 33;
        const string MenuCloseDocument = "CloseDocument";
        const string MenuCloseAllDocuments = "CloseAllDocuments";
        const string MenuCloseAllDocumentsButThis = "CloseAllDocumentsButThis";

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabAlignment Alignment
        {
            get;
            set;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowHeader
        {
            get { return RowStyles[0].Height > 0; }
            set
            {
                if (value)
                {
                    RowStyles[0].Height = HeaderHeight;
                    Controls.Add(Header, 0, 0);
                }
                else
                {
                    RowStyles[0].Height = 0;
                    Controls.Remove(Header);
                }
            }
        }

        public TabEditPageCollection TabPages
        {
            get { return Pages ?? (Pages = OnCreatePageCollection()); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabEditPage PreviousTab
        {
            get;
            protected internal set;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabEditPage SelectedTab
        {
            get { return TabPages.FirstOrDefault(e => e.Enabled); }
            set { SelectedIndex = TabPages.IndexOf(value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return TabPages.IndexOf(SelectedTab); }
            set
            {
                int indexOld = SelectedIndex;

                if (value != indexOld && value >= 0 && value < TabPages.Count)
                {
                    var e = new TabEditCancelEventArgs();

                    SuspendLayout();

                    e.Action = TabControlAction.Selecting;
                    e.TabPageIndex = value;
                    e.TabPage = TabPages[value];

                    OnSelecting(e);

                    if (!e.Cancel)
                    {
                        PreviousTab = null;
                        TabPages.ForEach(item => item.Deactivate());

                        if (indexOld >= 0)
                        {
                            e.Action = TabControlAction.Deselected;
                            e.TabPageIndex = indexOld;
                            e.TabPage = TabPages[indexOld];

                            OnDeselected(e);

                            PreviousTab = e.TabPage;
                        }

                        e.Action = TabControlAction.Selected;
                        e.TabPageIndex = value;
                        e.TabPage = TabPages[value];

                        OnSelected(e);
                        OnSelectedIndexChanged(EventArgs.Empty);
                    }

                    ResumeLayout();
                }
            }
        }

        [Browsable(false)]
        public ContextMenu TabCaptionMenu
        {
            get { return CaptionMenu ?? (CaptionMenu = OnCreateContextMenu()); }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override AutoSizeMode AutoSizeMode
        {
            get { return base.AutoSizeMode; }
            set { base.AutoSizeMode = value; }
        }

        new int RowCount { get; set; }

        new TableLayoutRowStyleCollection RowStyles
        {
            get { return base.RowStyles; }
        }

        new int ColumnCount { get; set; }

        new TableLayoutColumnStyleCollection ColumnStyles
        {
            get { return base.ColumnStyles; }
        }

        protected virtual ContextMenu OnCreateContextMenu()
        {
            var value = new ContextMenu();
            value.Popup += OnCaptionMenuPopup;
            return value;
        }

        public void ClearPages()
        {
            CloseAllDocumentsClick(this, EventArgs.Empty);
        }

        public TabEditPage AddPage(Control value)
        {
            var page = new TabEditPage(value.Name);

            value.Dock = DockStyle.Fill;
            page.Controls.Add(value);
            TabPages.Add(page);

            return page;
        }

        public Rectangle GetTabRect(int index)
        {
            TabEditPageCaption c = Pages[index].Caption;
            return c.RectangleToScreen(c.ClientRectangle);
        }

        protected virtual TabEditPageCollection OnCreatePageCollection()
        {
            return new TabEditPageCollection(this);
        }

        protected virtual void OnSelecting(TabEditCancelEventArgs e)
        {
        }

        protected virtual void OnSelected(TabEditEventArgs e)
        {
            e.TabPage.Enabled = true;
            e.TabPage.BringToFront();
            e.TabPage.Show();
        }

        protected virtual internal void OnDeselected(TabEditEventArgs e)
        {
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
        }

        protected virtual internal void OnCaptionClick(TabEditEventArgs e)
        {
        }

        protected virtual void OnCaptionMenuPopup(object sender, EventArgs e)
        {
            TabCaptionMenu.MenuItems.Clear();
            TabCaptionMenu.MenuItems.Add(Localization.Localize(MenuCloseDocument), CloseDocumentClick).Enabled = SelectedTab.HasValue() && SelectedTab.CanRemove;
            TabCaptionMenu.MenuItems.Add(Localization.Localize(MenuCloseAllDocuments), CloseAllDocumentsClick).Enabled = TabPages.Count(item => item.CanRemove) > 0;
            TabCaptionMenu.MenuItems.Add(Localization.Localize(MenuCloseAllDocumentsButThis), CloseAllDocumentsButThisClick).Enabled = TabPages.Count(item => item != SelectedTab && item.CanRemove) > 0;
        }

        protected void CloseDocumentClick(object sender, EventArgs e)
        {
            if (SelectedTab.HasValue())
            {
                SelectedTab.Remove();
            }
        }

        protected void CloseAllDocumentsClick(object sender, EventArgs e)
        {
            TabPages.ToList().ForEach(page => page.Remove());
        }

        protected void CloseAllDocumentsButThisClick(object sender, EventArgs e)
        {
            var value = SelectedTab;

            TabPages.Where(page => !page.Equals(value)).ToList().ForEach(page => page.Remove());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                CaptionMenu.DisposeSafely();
                CaptionMenu = null;
            }
        }
    }
}