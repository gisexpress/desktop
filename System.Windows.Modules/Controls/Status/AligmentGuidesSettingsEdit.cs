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
using System.Linq;
using System.Windows.Forms;

namespace System.Windows.Modules.Controls
{
    [ToolboxItem(false)]
    public class AligmentGuidesSettingsEdit : TablePanelEdit
    {
        public AligmentGuidesSettingsEdit(IApplication application)
        {
            Application = application;

            Initialize();

            AlignmentGuide90.Tag = 90;
            AlignmentGuide45.Tag = 45;
            AlignmentGuide30.Tag = 30;
            AlignmentGuide15.Tag = 15;

            AlignmentGuide90.Checked = Application.Settings.AlignmentGuides.Angle == 90;
            AlignmentGuide45.Checked = Application.Settings.AlignmentGuides.Angle == 45;
            AlignmentGuide30.Checked = Application.Settings.AlignmentGuides.Angle == 30;
            AlignmentGuide15.Checked = Application.Settings.AlignmentGuides.Angle == 15;

            AlignmentGuidesEnabled.Checked = Application.Settings.AlignmentGuides.Enabled;

            AlignmentGuide90.CheckedChanged += AlignmentGuideAngleChanged;
            AlignmentGuide45.CheckedChanged += AlignmentGuideAngleChanged;
            AlignmentGuide30.CheckedChanged += AlignmentGuideAngleChanged;
            AlignmentGuide15.CheckedChanged += AlignmentGuideAngleChanged;

            AlignmentGuidesEnabled.CheckedChanged += OnAlignmentGuidesEnabledChanged;

            OnAlignmentGuidesEnabledChanged(AlignmentGuidesEnabled, EventArgs.Empty);
        }

        protected readonly IApplication Application;

        protected CheckEdit AlignmentGuide90;
        protected CheckEdit AlignmentGuide45;
        protected CheckEdit AlignmentGuide30;
        protected CheckEdit AlignmentGuide15;
        protected CheckEdit AlignmentGuidesEnabled;

        protected void OnAlignmentGuidesEnabledChanged(object sender, EventArgs e)
        {
            AlignmentGuide90.Enabled =
            AlignmentGuide45.Enabled =
            AlignmentGuide30.Enabled =
            AlignmentGuide15.Enabled = AlignmentGuidesEnabled.Checked;

            Application.Settings.AlignmentGuides.Enabled = AlignmentGuidesEnabled.Checked;
        }

        protected void AlignmentGuideAngleChanged(object sender, EventArgs e)
        {
            var edit = sender as CheckEdit;

            if (edit.Checked)
            {
                Application.Settings.AlignmentGuides.Angle = (int)edit.Tag;
            }
        }

        void Initialize()
        {
            TransparentHit = false;
            BackColor = ApplicationAppereance.Colors.Control;

            AddRow(SizeType.Absolute, 10);
            AddRow(SizeType.Absolute, 22);
            AddRow(SizeType.Absolute, 22);
            AddRow(SizeType.Absolute, 22);
            AddRow(SizeType.Absolute, 22);
            AddRow(SizeType.Absolute, 10);
            AddRow(SizeType.Absolute, 24);
            AddRow(SizeType.Absolute, 10);

            AddColumn(SizeType.Absolute, 16);
            AddColumn(SizeType.Absolute, 100);
            AddColumn(SizeType.Absolute, 36);

            Controls.Add(AlignmentGuide90 = new CheckEdit { IsRadio = true, RadioGroupIndex = 1, Dock = DockStyle.Fill }, 1, 1);
            Controls.Add(AlignmentGuide45 = new CheckEdit { IsRadio = true, RadioGroupIndex = 1, Dock = DockStyle.Fill }, 1, 2);
            Controls.Add(AlignmentGuide30 = new CheckEdit { IsRadio = true, RadioGroupIndex = 1, Dock = DockStyle.Fill }, 1, 3);
            Controls.Add(AlignmentGuide15 = new CheckEdit { IsRadio = true, RadioGroupIndex = 1, Dock = DockStyle.Fill }, 1, 4);
            Controls.Add(new HorizontalLine { Padding = new Padding(0) }, 1, 5);
            Controls.Add(AlignmentGuidesEnabled = new CheckEdit { Dock = DockStyle.Fill }, 1, 6);

            Localization.Register(this, (e) => AlignmentGuide90.Text = e("AlignmentGuide90.Text"));
            Localization.Register(this, (e) => AlignmentGuide45.Text = e("AlignmentGuide45.Text"));
            Localization.Register(this, (e) => AlignmentGuide30.Text = e("AlignmentGuide30.Text"));
            Localization.Register(this, (e) => AlignmentGuide15.Text = e("AlignmentGuide15.Text"));
            Localization.Register(this, (e) => AlignmentGuidesEnabled.Text = e("AlignmentGuidesEnabled.Text"));

            Width = (int)ColumnStyles.Cast<ColumnStyle>().Sum(e => e.Width);
            Height = (int)RowStyles.Cast<RowStyle>().Sum(e => e.Height);
        }
    }
}
