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

namespace System.Windows.Forms.Ribbon
{
    [ToolboxItem(false)]
    public class ApplicationMenu : PopupContainerEdit
    {
        public ApplicationMenu(RibbonControl ribbon)
        {
            Ribbon = ribbon;

            base.Control = new TablePanelEdit
            {
                AutoSize = false,
                BackColor = ApplicationAppereance.Colors.Window
            };

            Control.AddColumn(SizeType.Percent, 100);
        }

        protected RibbonControl Ribbon;

        protected new TablePanelEdit Control
        {
            get { return base.Control as TablePanelEdit; }
        }

        public RibbonApplicationMenuItem[] Items
        {
            get { return Control.Controls.Cast<Control>().OfType<RibbonApplicationMenuItem>().ToArray(); }
        }

        public RibbonApplicationMenuItem Add(string name, string title, Image image, Keys shortcut, EventHandler onClick, bool beginGroup)
        {
            var button = new RibbonApplicationMenuItem
            {
                Name = name,
                Text = name,
                Title = title,
                LargeImage = image,
                Shortcut = shortcut
            };

            button.Click += OnButtonClick;
            button.Click += onClick;

            Localization.Register(button, (e) => button.Text = e(button.Name));

            if (beginGroup)
            {
                Control.AddSeperator();
            }

            Control.Controls.Add(button, 0, Control.AddRow(SizeType.Absolute, 42));

            return button;
        }

        protected void OnButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnPopupOpening(object sender, CancelEventArgs e)
        {
            base.OnPopupOpening(sender, e);

            Control.Location = Point.Empty;
            Control.MinimumSize = Popup.MinimumSize = new Size(Control.Controls.Cast<Control>().Max(c => c.PreferredSize.Width), (int)Control.RowStyles.Cast<RowStyle>().Sum(c => c.Height));
        }
    }
}
