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
    public class ApplicationFormHeader : BaseEdit
    {
        public ApplicationFormHeader()
        {
            InitializeComponent();
        }

        protected LabelEdit LabelPrimary;
        protected LabelEdit LabelSecondary;
        protected ImageEdit IconControl;

        public string Caption
        {
            get { return LabelPrimary.Text; }
            set { LabelPrimary.Text = value; }
        }

        public string Title
        {
            get { return LabelSecondary.Text; }
            set { LabelSecondary.Text = value; }
        }

        protected void InitializeComponent()
        {
            var panel = new TablePanelEdit
            {
                Height = 60,
                AutoSize = false,
                Dock = DockStyle.Fill,
                BorderStyle = Border3DSide.Bottom,
                BackColor = ApplicationAppereance.Colors.Control
            };

            panel.AddRow(SizeType.Absolute, 8);
            panel.AddRow(SizeType.Percent, 22);
            panel.AddRow(SizeType.Percent, 22);
            panel.AddRow(SizeType.Absolute, 8);

            panel.AddColumn(SizeType.Absolute, 8);
            panel.AddColumn(SizeType.Percent, 100);
            panel.AddColumn(SizeType.Absolute, 48);
            panel.AddColumn(SizeType.Absolute, 8);

            Height = 60;
            Dock = DockStyle.Top;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            panel.Controls.Add(LabelPrimary = CreateLabelPrimary(), 1, 1);
            panel.Controls.Add(IconControl = CreateIconControl(), 2, 1);
            panel.Controls.Add(LabelSecondary = CreateLabelSecondary(), 1, 2);
            panel.SetRowSpan(IconControl, 2);

            LabelPrimary.Padding = new Padding(8, 2, 2, 2);
            LabelPrimary.AutoSize = false;

            LabelSecondary.Padding = new Padding(8, 2, 2, 2);
            LabelSecondary.AutoSize = false;

            Controls.Add(panel);
        }

        protected LabelEdit CreateLabelPrimary()
        {
            return new LabelEdit
            {
                Dock = DockStyle.Fill,
                Font = new Font(Font, FontStyle.Bold)
            };
        }

        protected LabelEdit CreateLabelSecondary()
        {
            return new LabelEdit { Dock = DockStyle.Fill };
        }

        protected ImageEdit CreateIconControl()
        {
            return new ImageEdit
            {
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Image = ApplicationAppereance.Images.SmallLogo
            };
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            SendToBack();
            base.OnVisibleChanged(e);
        }
    }
}