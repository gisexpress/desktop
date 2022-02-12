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

namespace System.Windows.Forms
{
    [ToolboxItem(false)]
    public class ApplicationFormFooter : UserControl
    {
        public ApplicationFormFooter()
        {
            InitializeComponent();
        }

        public MessageBoxButtons DisplayButtons
        {
            get { return CurrentDisplayButtons; }
            set { UpdateButtons(value); }
        }

        public ButtonEditCollection Buttons
        {
            get;
            protected set;
        }

        protected MessageBoxButtons CurrentDisplayButtons;

        protected void InitializeComponent()
        {
            Buttons = new ButtonEditCollection { Dock = DockStyle.Fill };
            Buttons.SetButtons(ButtonClick, "OK", "Cancel", "Yes", "No", "Abort", "Retry", "Ignore");

            UpdateButtons(MessageBoxButtons.OKCancel);

            Height = 48;
            Dock = DockStyle.Bottom;
            Padding = new Padding(8, 8, 4, 4);
            Controls.Add(Buttons);

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected void ButtonClick(object sender, EventArgs e)
        {
            OnButtonClick(sender as ButtonEdit);
        }

        protected virtual void OnButtonClick(ButtonEdit button)
        {
            if (ParentForm.HasValue())
            {
                DialogResult r;

                if (Enum.TryParse<DialogResult>(button.Name, out r))
                {
                    var form = ParentForm as BaseForm;

                    if (form.HasValue())
                    {
                        form.PerformButtonClick(r);
                    }
                    else
                    {
                        ParentForm.DialogResult = r;
                        ParentForm.Close();
                    }
                }
            }
        }

        protected void UpdateButtons(MessageBoxButtons value)
        {
            if (CurrentDisplayButtons != value)
            {
                Buttons.SetVisible(false);

                switch (CurrentDisplayButtons = value)
                {
                    case MessageBoxButtons.OK:
                        Buttons["OK"].Visible = true;
                        break;
                    case MessageBoxButtons.OKCancel:
                        Buttons["OK"].Visible = true;
                        Buttons["Cancel"].Visible = true;
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:
                        Buttons["Abort"].Visible = true;
                        Buttons["Retry"].Visible = true;
                        Buttons["Ignore"].Visible = true;
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        Buttons["Yes"].Visible = true;
                        Buttons["No"].Visible = true;
                        Buttons["Cancel"].Visible = true;
                        break;
                    case MessageBoxButtons.YesNo:
                        Buttons["Yes"].Visible = true;
                        Buttons["No"].Visible = true;
                        break;
                    case MessageBoxButtons.RetryCancel:
                        Buttons["Retry"].Visible = true;
                        Buttons["Cancel"].Visible = true;
                        break;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawBorder3D(0, 0, Width, Height - 6, Border3DSide.Top);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            SendToBack();
            base.OnVisibleChanged(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (ParentForm.HasValue())
            {
                switch (CurrentDisplayButtons)
                {
                    case MessageBoxButtons.OK:
                        ParentForm.AcceptButton = Buttons["OK"];
                        break;
                    case MessageBoxButtons.OKCancel:
                        ParentForm.AcceptButton = Buttons["OK"];
                        ParentForm.CancelButton = Buttons["Cancel"];
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:
                        ParentForm.AcceptButton = Buttons["Retry"];
                        ParentForm.CancelButton = Buttons["Ignore"];
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        ParentForm.AcceptButton = Buttons["Yes"];
                        ParentForm.CancelButton = Buttons["Cancel"];
                        break;
                    case MessageBoxButtons.YesNo:
                        ParentForm.AcceptButton = Buttons["Yes"];
                        ParentForm.CancelButton = Buttons["No"];
                        break;
                    case MessageBoxButtons.RetryCancel:
                        ParentForm.AcceptButton = Buttons["Retry"];
                        ParentForm.CancelButton = Buttons["Cancel"];
                        break;
                }
            }
        }

        protected override void WndProc(ref Message e)
        {
            base.WndProc(ref e);
            this.WndProcTransparentHit(ref e);
        }
    }
}