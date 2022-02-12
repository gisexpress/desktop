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

namespace System.Windows.Forms
{
    [ToolboxItem(false)]
    public class ButtonEditCollection : PanelEdit
    {
        public ButtonEditCollection()
        {
            Panel = new TablePanelEdit
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(3)
            };

            InitializeComponent();
        }

        protected readonly TableLayoutPanel Panel;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return Panel.Padding; }
            set { Panel.Padding = value; }
        }

        protected void InitializeComponent()
        {
            ButtonSize = new Size(90, 26);

            AutoSize = true;
            Anchor = AnchorStyles.None;
            Controls.Add(Panel);
        }

        public Size ButtonSize
        {
            get;
            set;
        }

        public ButtonEdit this[string name]
        {
            get { return Panel.Controls[name] as ButtonEdit; }
        }

        public void SetVisible(bool value)
        {
            foreach (Control c in Panel.Controls)
            {
                if (c is ButtonEdit)
                {
                    c.Visible = value;
                }
            }
        }

        public void SetButtons(EventHandler onClick, params string[] buttonNames)
        {
            Panel.RowStyles.Clear();
            Panel.RowStyles.Add(new RowStyle(SizeType.Absolute, ButtonSize.Height + 6));

            Panel.ColumnStyles.Clear();
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            foreach (string name in buttonNames)
            {
                var c = new ButtonEdit
                {
                    Name = name,
                    Dock = DockStyle.Fill,
                    Anchor = AnchorStyles.None
                };

                c.Click += onClick;
                c.VisibleChanged += OnButtonVisibleChanged;
                Localization.Register(c, e => c.Text = e(name));

                Panel.Controls.Add(c, Panel.ColumnStyles.Count, 0);
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ButtonSize.Width));
            }
        }

        protected virtual void OnButtonVisibleChanged(object sender, EventArgs e)
        {
            var c = sender as ButtonEdit;

            if (c.HasValue())
            {
                c.Size = ButtonSize;
                Panel.ColumnStyles[Panel.Controls.IndexOf(c) + 1].Width = c.Visible ? ButtonSize.Width : 0;
                Panel.Refresh();
            }
        }
    }
}
