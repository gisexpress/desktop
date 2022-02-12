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
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace System.Windows.Modules.Controls
{
    [ToolboxItem(false)]
    public partial class ApplicationStartPage : BaseEdit
    {
        static ApplicationStartPage()
        {
            EmptyPreview = BitmapExtensions.NewImage(256, 128);
        }

        public ApplicationStartPage(IApplication application)
        {
            Name = "{259CA5B3-5BCF-4BDA-9643-699CE525401F}";

            Application = application;

            Padding = new Padding(6);
            BorderStyle = default(Border3DSide);

            var panel = new TablePanelEdit
            {
                Dock = DockStyle.Fill
            };

            panel.AddRow(SizeType.Absolute, 56);
            panel.AddRow(SizeType.Absolute, 24);
            panel.AddRow(SizeType.Percent, 100);

            panel.AddColumn(SizeType.Absolute, 120);
            panel.AddColumn(SizeType.Absolute, 120);
            panel.AddColumn(SizeType.Percent, 100);

            var newProjectLink = new HyperlinkLabelEdit
            {
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,
                Image = Images032.AddFile,
                TextAlign = ContentAlignment.MiddleLeft,
                ImageAlign = ContentAlignment.MiddleLeft,
            };

            newProjectLink.Click += OnNewProjectLinkClick;
            panel.Controls.Add(newProjectLink, 0, 0);

            var openProjectLink = new HyperlinkLabelEdit
            {
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,
                Image = Images032.OpenFile,
                TextAlign = ContentAlignment.MiddleLeft,
                ImageAlign = ContentAlignment.MiddleLeft
            };

            openProjectLink.Click += OnOpenProjectLinkClick;
            panel.Controls.Add(openProjectLink, 1, 0);

            var recentLabel = new LabelEdit();

            panel.Controls.Add(recentLabel, 0, 1);
            panel.SetColumnSpan(recentLabel, 3);

            RecentList = new ListViewEdit
            {
                Dock = DockStyle.Fill,
                BorderStyle = Border3DSide.All,
                FilterMode = ListViewFilterMode.Disabled
            };

            panel.Controls.Add(RecentList, 0, 2);
            panel.SetColumnSpan(RecentList, 3);

            Localization.Register(this, (e) => Text = Localization.Localize("ApplicationStartPage"));
            Localization.Register(this, (e) => newProjectLink.Text = string.Concat("         ", Localization.Localize(MainModule.CommandNewProject)));
            Localization.Register(this, (e) => openProjectLink.Text = string.Concat("         ", Localization.Localize(MainModule.CommandOpenProject)));
            Localization.Register(this, (e) => recentLabel.Text = Localization.Localize("Recent"));

            RecentList.View.ItemDoubleClick += OnItemDoubleClick;
            Controls.Add(panel);
        }

        protected IApplication Application;
        protected ListViewEdit RecentList;
        protected static Bitmap EmptyPreview;

        protected override bool ShowFocusRectangle
        {
            get { return false; }
        }

        protected void OnNewProjectLinkClick(object sender, EventArgs e)
        {
            Application.Form.CommandManager[MainModule.CommandNewProject].Execute();
        }

        protected void OnOpenProjectLinkClick(object sender, EventArgs e)
        {
            Application.Form.CommandManager[MainModule.CommandOpenProject].Execute();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible && RecentList.RowHeight < 140)
            {
                OnRecentListChanged(this, default(PropertyChangedEventArgs));
            }
        }

        protected void OnRecentListChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                Application.Settings.RecentList.PropertyChanged -= OnRecentListChanged;

                if (!InvokeRequired)
                {
                    RecentList.View.SuspendLayout();
                    RecentList.Rows.ForEach(r => r.Image.DisposeSafely());
                    RecentList.Rows.Clear();
                    RecentList.RowHeight = 140;

                    foreach (string item in Application.Settings.RecentList.GetValues())
                    {
                        IProjectFileInfo info;
                        IProject project = Application.Projects.FirstOrDefault(n => n.File.HasValue() && n.File.FileName.EqualsIgnoreCase(item));

                        if (project.HasValue())
                        {
                            info = project.FileInfo;
                        }
                        else
                        {
                            if (!File.Exists(item))
                            {
                                continue;
                            }

                            IApplicationFile file = Application.Files.Find(item);

                            if (file.HasValue())
                            {
                                file.Read(info = new ProjectFileInfo(), item);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        RecentList.Rows.Add(item, string.Concat(Path.GetFileNameWithoutExtension(item), Environment.NewLine, item), (info.Preview ?? EmptyPreview).CloneSafely());
                    }

                    RecentList.View.UpdateLayout(false);
                    RecentList.View.ResumeLayout();
                }
            }
            catch (Exception error)
            {
                error.Print();
            }
            finally
            {
                Application.Settings.RecentList.PropertyChanged += OnRecentListChanged;
            }
        }

        protected void OnItemDoubleClick(object sender, EventArgs e)
        {
            if (RecentList.SelectedRow.HasValue())
            {
                Application.Form.CommandManager[MainModule.CommandOpenProject].Execute(RecentList.SelectedRow.Value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Application.Settings.RecentList.PropertyChanged -= OnRecentListChanged;
                Application = null;
            }

            base.Dispose(disposing);
        }
    }
}
