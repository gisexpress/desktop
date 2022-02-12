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

using System.Drawing;
using System.Windows.Forms.Ribbon;
using System.Windows.Modules;
using System.Windows.Modules.Controls;

namespace System.Windows.Forms
{
    internal class ApplicationCommandManager : IApplicationCommandManager
    {
        public ApplicationCommandManager(ApplicationMainForm form)
        {
            Application = form.Application;
            Ribbon = form.Ribbon;
        }

        public IApplication Application
        {
            get;
            private set;
        }

        public RibbonControl Ribbon
        {
            get;
            private set;
        }

        public RibbonPageCollection Pages
        {
            get { return Ribbon.TabPages; }
        }

        public ApplicationMenu ApplicationMenu
        {
            get { return Ribbon.Menu; }
        }

        public IApplicationCommand this[string name]
        {
            get { return (IApplicationCommand)Ribbon.FindCommand(name); }
        }

        public IApplicationCommand CreateCommand(string name, Bitmap image, Bitmap largeImage, Action<object> action)
        {
            return CreateCommand(name, RibbonCommandVisibility.Toolbar, Keys.None, image, largeImage, action, true, false);
        }

        public IApplicationCommand CreateCommand(string name, Bitmap image, Bitmap largeImage, Action<object> action, bool enabled)
        {
            return CreateCommand(name, RibbonCommandVisibility.Toolbar, Keys.None, image, largeImage, action, enabled, false);
        }

        public IApplicationCommand CreateCommand(string name, Bitmap image, Bitmap largeImage, Action<object> action, bool enabled, bool beginGroup)
        {
            return CreateCommand(name, RibbonCommandVisibility.Toolbar, Keys.None, image, largeImage, action, enabled, beginGroup);
        }

        public IApplicationCommand CreateCommand(string name, RibbonCommandVisibility visibility, Bitmap image, Bitmap largeImage, Action<object> action)
        {
            return CreateCommand(name, visibility, Keys.None, image, largeImage, action, true, false);
        }

        public IApplicationCommand CreateCommand(string name, RibbonCommandVisibility visibility, Bitmap image, Bitmap largeImage, Action<object> action, bool enabled)
        {
            return CreateCommand(name, visibility, Keys.None, image, largeImage, action, enabled, false);
        }

        public IApplicationCommand CreateCommand(string name, RibbonCommandVisibility visibility, Bitmap image, Bitmap largeImage, Action<object> action, bool enabled, bool beginGroup)
        {
            return CreateCommand(name, visibility, Keys.None, image, largeImage, action, enabled, beginGroup);
        }

        public IApplicationCommand CreateCommand(string name, RibbonCommandVisibility visibility, Keys shortcut, Bitmap image, Bitmap largeImage, Action<object> action)
        {
            return CreateCommand(name, visibility, shortcut, image, largeImage, action, true, false);
        }

        public IApplicationCommand CreateCommand(string name, RibbonCommandVisibility visibility, Keys shortcut, Bitmap image, Bitmap largeImage, Action<object> action, bool enabled)
        {
            return CreateCommand(name, visibility, shortcut, image, largeImage, action, enabled, false);
        }

        public IApplicationCommand CreateCommand(string name, RibbonCommandVisibility visibility, Keys shortcut, Bitmap image, Bitmap largeImage, Action<object> action, bool enabled, bool beginGroup)
        {
            var command = Ribbon.FindCommand(name) as ApplicationCommand;

            if (command.IsNull())
            {
                command = new ApplicationCommand(Ribbon, action)
                {
                    Name = name,
                    Enabled = enabled,
                    SmallImage = image,
                    LargeImage = largeImage,
                    Shortcut = shortcut,
                    Visibility = visibility
                };

                command.Create(beginGroup);
            }

            return command;
        }

        public bool RemoveCommand(string key)
        {
            return Ribbon.RemoveCommand(key);
        }

        public bool PerformCommand(Keys key)
        {
            return Ribbon.PerformCommand(key);
        }
    }

    internal class ApplicationCommand : RibbonToolbarButton, IApplicationCommand
    {
        public ApplicationCommand(RibbonControl ribbon, Action<object> action)
            : base(ribbon)
        {
            Action = action;
        }

        public readonly Action<object> Action;

        public void Create(bool beginGroup)
        {
            RibbonToolbarButton parent;
            RibbonPage page = Ribbon.TabPages[PageName] ?? Ribbon.TabPages.Add(PageName);
            RibbonPageGroup group = page.Groups[GroupName ?? string.Empty] ?? page.Groups.Add(GroupName ?? string.Empty);

            if (string.IsNullOrEmpty(ParentName) || (parent = group[ParentName]).IsNull())
            {
                if ((Visibility & RibbonCommandVisibility.ApplicationMenu) == RibbonCommandVisibility.ApplicationMenu)
                {
                    var buttonLink = Ribbon.Menu.Add(Name, Text, LargeImage, Shortcut, OnClick, beginGroup);

                    buttonLink.Enabled = Enabled;
                    EnabledChanged += (s, e) => buttonLink.InvokeAction(buttonLink.SetEnabled, Enabled);
                }

                if ((Visibility & RibbonCommandVisibility.QuickToolbar) == RibbonCommandVisibility.QuickToolbar)
                {
                    var buttonLink = Ribbon.Form.QuickToolbar.AddButton(Name, Text, SmallImage, OnClick, beginGroup || Ribbon.Form.QuickToolbar.ButtonsCount == 0);

                    buttonLink.Enabled = Enabled;
                    EnabledChanged += (s, e) => buttonLink.InvokeAction(buttonLink.SetEnabled, Enabled);
                }

                group.AddCommand(this, beginGroup);
            }
            else
            {
                Visibility = RibbonCommandVisibility.None;
                group.AddCommand(this);
                parent.Items.Add(Text, OnClick, SmallImage);
            }
        }

        public void Execute()
        {
            if (Enabled)
            {
                Execute(default(object));
            }
        }

        public void Execute(object argument)
        {
            if (Enabled)
            {
                Action(argument);
            }
        }

        protected void OnClick(object sender, EventArgs e)
        {
            if (Enabled)
            {
                OnClick(e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (Enabled)
            {
                Action(this);
                base.OnClick(e);
            }
        }
    }
}