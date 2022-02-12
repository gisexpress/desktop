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
    public class FormCaptionButton : BaseEdit
    {
        public FormCaptionButton(CaptionButton buttonType)
        {
            Size = new Size(22, 22);
            ButtonType = buttonType;
            BackColor = ApplicationAppereance.Colors.WindowCaption;
        }

        protected bool Over;
        protected ToolTip Hint;
        protected CaptionButton Type;

        public CaptionButton ButtonType
        {
            get
            {
                return Type;
            }
            protected set
            {
                Hint = Hint ?? new ToolTip
                {
                    ShowAlways = true,
                    ReshowDelay = 500,
                    AutoPopDelay = 5000,
                    InitialDelay = 500,
                };

                Hint.SetToolTip(this, Enums.GetDisplayName(Type = value));
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Over = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Over = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (OSEnvironment.IsWindows)
            {
                OnPaintButtons(e);
            }
        }

        protected virtual void OnPaintButtons(PaintEventArgs e)
        {
            Rectangle bounds = ClientRectangle;

            bounds.Inflate(-2, -2);

            if (Over)
            {
                e.Graphics.FillRectangle(SystemBrushes.Menu, bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveBorder, bounds);
            }

            UpdateButton();
            bounds.Inflate(-4, -4);

            switch (ButtonType)
            {
                case CaptionButton.Close:
                    e.Graphics.DrawCloseButton(bounds);
                    break;
                case CaptionButton.Minimize:
                    e.Graphics.DrawMinimizeButton(bounds);
                    break;
                case CaptionButton.Maximize:
                    e.Graphics.DrawMaximizeButton(bounds);
                    break;
                case CaptionButton.Restore:
                    e.Graphics.DrawRestoreButton(bounds);
                    break;
            }
        }

        protected void UpdateButton()
        {
            if (Type == CaptionButton.Restore || Type == CaptionButton.Maximize)
            {
                if (Type != CaptionButton.Restore && FormBase.IsMaximized)
                {
                    ButtonType = CaptionButton.Restore;
                }
                else if (Type != CaptionButton.Maximize && FormBase.IsNormal)
                {
                    ButtonType = CaptionButton.Maximize;
                }
            }
        }

        protected override void OnFormSizeChanged(object sender, EventArgs e)
        {
            Top = 0;

            switch (ButtonType)
            {
                case CaptionButton.Close:
                    Left = (sender as Form).Width - Width;
                    break;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            var mea = e as MouseEventArgs;

            if (mea.IsNull() || mea.Button == MouseButtons.Left)
            {
                switch (ButtonType)
                {
                    case CaptionButton.Minimize:
                        FormBase.Minimize();
                        Invalidate(false);
                        break;
                    case CaptionButton.Restore:
                    case CaptionButton.Maximize:
                        FormBase.MaximizeRestore();
                        Invalidate(false);
                        break;
                    case CaptionButton.Close:
                        FormBase.Close();
                        break;
                }
            }

            base.OnClick(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Hint.Dispose();
                Hint = null;
            }

            base.Dispose(disposing);
        }
    }
}
