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
using System.Resources;

namespace System.Windows.Forms
{
    [ToolboxItem(false)]
    public class PopupMessage : PopupContainerEdit, ISupportInitialize
    {
        static PopupMessage()
        {
            Default = new PopupMessage();
        }

        PopupMessage()
        {
            base.Control = new LabelEdit
            {
                AutoSize = true,
                Padding = new Padding(8, 8, 16, 8)
            };
        }

        protected static PopupMessage Default;

        public static void Show(Point screenLocation, string message, MessageBoxIcon icon)
        {
            Show(default(Control), screenLocation, message, icon, ContentAlignment.TopLeft);
        }

        public static void Show(Point screenLocation, string message, MessageBoxIcon icon, ContentAlignment alignment)
        {
            Show(default(Control), screenLocation, message, icon, alignment);
        }

        public static void Show(Control owner, string message, MessageBoxIcon icon)
        {
            Show(owner, Point.Empty, message, icon, ContentAlignment.TopLeft);
        }

        public static void Show(Control owner, string message, MessageBoxIcon icon, ContentAlignment alignment)
        {
            Show(owner, Point.Empty, message, icon, alignment);
        }

        static void Show(Control owner, Point screenLocation, string message, MessageBoxIcon icon, ContentAlignment alignment)
        {
            Default.Icon = icon;
            Default.Message = message;

            if (owner.HasValue())
            {
                screenLocation = owner.PointToScreen(Point.Empty);

                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                        break;
                    case ContentAlignment.TopCenter:
                        screenLocation.Offset(owner.Width / 2, -owner.Height / 2);
                        break;
                    case ContentAlignment.TopRight:
                        break;
                    case ContentAlignment.BottomRight:
                        screenLocation.Offset(-Default.Control.Width, Default.Control.Height);
                        break;
                }
            }

            if (Screen.PrimaryScreen.WorkingArea.Height < screenLocation.Y + Default.Control.Height)
            {
                screenLocation.Y -= (Default.Control.Height + 6);

                if (owner.HasValue())
                {
                    screenLocation.Y -= owner.Height;
                }
            }

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    break;
                case ContentAlignment.TopCenter:
                    screenLocation.Offset(-Default.Control.Width / 2, -Default.Control.Height);
                    break;
                case ContentAlignment.TopRight:
                    break;
                case ContentAlignment.BottomRight:
                    screenLocation.Offset(-Default.Control.Width, Default.Control.Height);
                    break;
            }

            Default.ShowPopup(screenLocation);
        }

        protected new LabelEdit Control
        {
            get { return (LabelEdit)base.Control; }
        }

        public void BeginInit()
        {
            Control.MinimumSize = Size.Empty;
        }

        public void EndInit()
        {
            Control.UpdateLayout(true);
            Control.MinimumSize = Control.Size;
        }

        public MessageBoxIcon Icon
        {
            get { return MessageBoxIcon.Error; }
            set
            {
                using (this.Begin())
                {
                    switch (value)
                    {
                        case MessageBoxIcon.Error:
                            Control.Image = Images032.Error;
                            Control.Size = Control.PreferredSize;
                            break;
                    }
                }
            }
        }

        public string Message
        {
            get { return Control.Text; }
            set
            {
                using (this.Begin())
                {
                    Control.Text = value;
                    Control.Size = Control.PreferredSize;
                }
            }
        }
    }
}
