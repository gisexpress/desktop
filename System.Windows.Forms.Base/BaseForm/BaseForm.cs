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
using System.Resources;

namespace System.Windows.Forms
{
    [Designer(typeof(Designer))]
    public partial class BaseForm : Form, IForm
    {
        public BaseForm()
        {
            KeyPreview = true;
            ShowInTaskbar = false;
            base.Padding = new Padding(3);

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;

            Font = ApplicationAppereance.Fonts.DefaultFont;
            BackColor = ApplicationAppereance.Colors.Control;
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;

            Icon = ApplicationAppereance.Icons.Application;
            ApplicationEnvironment.Changed += OnUpdateCaption;
        }

        protected bool IsLoaded;

        protected Timer Timer;
        protected Form OwnerWindow;
        protected FormBody BodyControl;
        protected FormCaption CaptionControl;
        protected ContextMenuStrip CaptionMenu;

        protected internal FormBorderStyle SelectedBorderStyle;
        protected internal DockManager DockManagerControl;
        protected internal DocumentManager DocumentManagerControl;
        protected internal StatusManager StatusManagerControl;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FormBody Body
        {
            get
            {
                if (BodyControl.IsNull())
                {
                    base.Controls.Add(BodyControl = new FormBody());
                }

                return BodyControl;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FormCaption Caption
        {
            get
            {
                if (CaptionControl.IsNull())
                {
                    base.Controls.Add(CaptionControl = new FormCaption(ProductName, string.Empty)
                    {
                        Icon = ApplicationAppereance.Images.Ico
                    });
                }

                return CaptionControl;
            }
        }

        [Browsable(false)]
        public int CaptionHeight
        {
            get { return RectangleToScreen(ClientRectangle).Top - Top; }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Caption.Text = value;
            }
        }

        protected new virtual string ProductName
        {
            get { return ApplicationEnvironment.ProductName; }
        }

        protected virtual string ProductLicense
        {
            get { return ApplicationEnvironment.ProductLicense; }
        }

        [Browsable(false)]
        public IQuickToolbar QuickToolbar
        {
            get { return Caption.QuickToolbar; }
        }

        [DefaultValue(true)]
        [Category(Categories.Appearance)]
        public new bool ShowIcon
        {
            get { return base.ShowIcon; }
            set
            {
                base.ShowIcon = value;

                if (value)
                {
                    Caption.Icon = ApplicationAppereance.Images.Ico;
                }
                else
                {
                    Caption.Icon = default(Image);
                }
            }
        }

        public new Padding Padding
        {
            get { return Body.Padding; }
            set { Body.Padding = value; }
        }

        [Browsable(false)]
        public DockManager DockManager
        {
            get { return DockManagerControl ?? (DockManagerControl = OnCreateDockManager()); }
        }

        [Browsable(false)]
        public DocumentManager DocumentManager
        {
            get { return DocumentManagerControl ?? (DocumentManagerControl = OnCreateDocumentManager()); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StatusManager StatusManager
        {
            get { return StatusManagerControl ?? (StatusManagerControl = OnCreateStatusManager()); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DoWorkEventHandler AcceptAction
        {
            get;
            set;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(0, 0, Width, Height);
        }

        public object FindValue(string name)
        {
            var edit = Body.Controls.Find(name, true).OfType<BaseEdit>().FirstOrDefault();

            if (edit.HasValue())
            {
                return edit.EditValue;
            }

            return default(object);
        }

        [Browsable(false)]
        public bool IsNormal
        {
            get { return WindowState == FormWindowState.Normal; }
        }

        [Browsable(false)]
        public bool IsMinimized
        {
            get { return WindowState == FormWindowState.Minimized; }
        }

        [Browsable(false)]
        public bool IsMaximized
        {
            get { return WindowState == FormWindowState.Maximized; }
        }

        public void Minimize()
        {
            WindowState = FormWindowState.Minimized;
        }

        public void Restore()
        {
            Dock = DockStyle.None;
            WindowState = FormWindowState.Normal;
        }

        public void Maximize()
        {
            WindowState = FormWindowState.Maximized;
        }

        public void MaximizeRestore()
        {
            if (IsNormal && Dock == DockStyle.None)
            {
                Maximize();
            }
            else
            {
                Restore();
            }
        }

        public new FormBorderStyle FormBorderStyle
        {
            get { return SelectedBorderStyle; }
            set
            {
                SelectedBorderStyle = value;

                if (OSEnvironment.IsWindows)
                {
                    ControlBox = false;
                    base.FormBorderStyle = FormBorderStyle.None;
                }
                else
                {
                    ControlBox = true;
                    base.FormBorderStyle = value;
                }

                switch (value)
                {
                    case FormBorderStyle.Sizable:
                    case FormBorderStyle.SizableToolWindow:
                        MinimizeBox = true;
                        MaximizeBox = true;
                        break;
                    default:
                        MinimizeBox = false;
                        MaximizeBox = false;
                        break;
                }

                if (Caption.HasValue())
                {
                    Caption.Properties.HasChanges = true;
                }
            }
        }

        public new bool MinimizeBox
        {
            get
            {
                return base.MinimizeBox;
            }
            set
            {
                if (Caption.HasValue())
                {
                    if (value)
                    {
                        Caption.CaptionButtons.Add(CaptionButton.Minimize);
                    }
                    else
                    {
                        Caption.CaptionButtons.Remove(CaptionButton.Minimize);
                    }

                    Caption.UpdateLayout(true);
                }

                base.MinimizeBox = value;
            }
        }

        public new bool MaximizeBox
        {
            get
            {
                return base.MaximizeBox;
            }
            set
            {
                if (Caption.HasValue())
                {
                    if (value)
                    {
                        Caption.CaptionButtons.Add(CaptionButton.Maximize);
                    }
                    else
                    {
                        Caption.CaptionButtons.Remove(CaptionButton.Maximize);
                    }

                    Caption.UpdateLayout(true);
                }

                base.MaximizeBox = value;
            }
        }

        [DefaultValue(true)]
        [Category("Window Style")]
        public bool CloseBox
        {
            get
            {
                return Caption.CaptionButtons.Contains(CaptionButton.Close);
            }
            set
            {
                if (Caption.HasValue())
                {
                    if (value)
                    {
                        Caption.CaptionButtons.Add(CaptionButton.Close);
                    }
                    else
                    {
                        Caption.CaptionButtons.Remove(CaptionButton.Close);
                    }

                    Caption.UpdateLayout(false);
                }
            }
        }

        public void PerformButtonClick(DialogResult result)
        {
            OnButtonClick(result);
        }

        protected virtual void OnButtonClick(DialogResult result)
        {
            if (result == DialogResult.OK && AcceptAction.HasValue())
            {
                OnAcceptAction(result);
            }
            else
            {
                DialogResult = result;
                Close();
            }
        }

        protected virtual void OnAcceptAction(DialogResult result)
        {
            var worker = new BackgroundWorker();

            worker.DoWork += AcceptAction;
            worker.RunWorkerCompleted += OnAcceptActionCompleted;
            worker.RunWorkerAsync(this);
        }

        protected virtual void OnAcceptActionCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            using (sender as IDisposable)
            {
                if (e.Error.IsNull())
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    e.Error.ShowMessage(this);
                }
            }
        }

        public void ShowDialog(int duration)
        {
            ShowDialog(default(IWin32Window), duration);
        }

        public void ShowDialog(IWin32Window owner, int duration)
        {
            OwnerWindow = owner as Form;

            if (duration > 0)
            {
                Opacity = 0.0;

                Timer = new Timer { Interval = duration };
                Timer.Tick += OnTimerTick;
                Timer.Start();
            }

            if (OwnerWindow.IsNull())
            {
                ShowDialog();
            }
            else
            {
                ShowDialog(owner);
            }
        }

        protected void OnTimerTick(object sender, EventArgs e)
        {
            lock (this)
            {
                this.InvokeAction(() =>
                {
                    Opacity = 1.0;
                    Invalidate(true);
                    Update();
                });

                if (Timer.HasValue())
                {
                    Timer.Stop();
                    Timer.Dispose();
                    Timer = null;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Caption.Top != base.Padding.Top)
            {
                Caption.SendToBack();
            }

            if (base.FormBorderStyle == FormBorderStyle.None)
            {
                e.Graphics.DrawBorder(Border3DSide.All, GetBounds());
            }
        }

        protected internal void PerformBodyPaint(PaintEventArgs e)
        {
            OnBodyPaint(e);
        }

        protected virtual void OnBodyPaint(PaintEventArgs e)
        {
        }

        protected internal void PerformMouseUp(MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        protected internal void PerformMouseMove(MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ValidateBounds();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                if (!OSEnvironment.IsWindows && Caption.QuickToolbar.ButtonsCount == 0)
                {
                    Controls.Remove(Caption);
                }
            }

            base.OnVisibleChanged(e);
        }

        protected virtual DockManager OnCreateDockManager()
        {
            return new DockManager(this);
        }

        protected virtual DocumentManager OnCreateDocumentManager()
        {
            return new DocumentManager(this);
        }

        protected virtual StatusManager OnCreateStatusManager()
        {
            return new StatusManager(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            IsLoaded = true;
        }

        protected virtual void OnUpdateCaption()
        {
            Caption.UpdateText(ProductName, ProductLicense);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (StatusManagerControl.HasValue())
            {
                StatusManagerControl.BringToFront();
            }

            if (Body.HasValue())
            {
                Body.BringToFront();
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            ValidateBounds();
            base.OnLocationChanged(e);
        }

        protected virtual void OnShowCaptionMenu(Point location)
        {
            string itemMaximize = Enums.GetName(CaptionButton.Maximize);
            string itemMinimize = Enums.GetName(CaptionButton.Minimize);
            string itemClose = Enums.GetName(CaptionButton.Close);

            if (CaptionMenu.IsNull())
            {
                CaptionMenu = new ContextMenuStrip();

                CaptionMenu.Items.Add(string.Empty, default(Image), OnFormMaximizeRestoreClick).Name = itemMaximize;
                CaptionMenu.Items.Add(string.Empty, Images016.WindowMinimize, OnFormMinimizeClick).Name = itemMinimize;
                CaptionMenu.Items.Add("-");
                CaptionMenu.Items.Add(string.Empty, Images016.WindowClose, OnFormCloseClick).Name = itemClose;
            }

            CaptionMenu.Items[itemMaximize].Enabled = MaximizeBox;
            CaptionMenu.Items[itemMaximize].Image = IsNormal ? Images016.WindowMaximize : Images016.WindowRestore;
            CaptionMenu.Items[itemMaximize].Text = Enums.GetDisplayName(IsNormal ? CaptionButton.Maximize : CaptionButton.Restore);

            CaptionMenu.Items[itemMinimize].Enabled = MinimizeBox;
            CaptionMenu.Items[itemMinimize].Text = Enums.GetDisplayName(CaptionButton.Minimize);

            CaptionMenu.Items[itemClose].Enabled = CloseBox;
            CaptionMenu.Items[itemClose].Text = Enums.GetDisplayName(CaptionButton.Close);

            CaptionMenu.Show(this, location);
        }

        protected virtual void OnFormMaximizeRestoreClick(object sender, EventArgs e)
        {
            MaximizeRestore();
        }

        protected virtual void OnFormMinimizeClick(object sender, EventArgs e)
        {
            Minimize();
        }

        protected virtual void OnFormCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (IsNormal)
            {
                var bounds = new Rectangle(x, y, width, height);

                if (ControlHelper.SetBounds(this, specified, MaximizedBounds, ref bounds))
                {
                    base.SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
                }
            }
        }

        protected void ValidateBounds()
        {
            if (!IsLoaded && !DesignMode)
            {
                if (IsNormal && StartPosition == FormStartPosition.CenterScreen)
                {
                    SetBoundsCore((MaximizedBounds.Width - Width) / 2, (MaximizedBounds.Height - Height) / 2, Width, Height, BoundsSpecified.Location);
                }
            }
        }

        protected override void WndProc(ref Message e)
        {
            if (OSEnvironment.IsWindows && !MaximizeBox && WindowsMessage.NCLButtonDblClk == (WindowsMessage)e.Msg)
            {
                e.Result = new IntPtr(0);
                return;
            }

            base.WndProc(ref e);

            this.WndProcWindow(ref e, IsNormal && SelectedBorderStyle == FormBorderStyle.Sizable ? Border3DSide.All : 0, Caption.Height, OnShowCaptionMenu);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    ApplicationEnvironment.Changed -= OnUpdateCaption;

                    if (OwnerWindow.HasValue())
                    {
                        OwnerWindow.Select();
                        OwnerWindow = null;
                    }

                    lock (this)
                    {
                        Timer.DisposeSafely();
                        Timer = null;
                    }
                }

                base.Dispose(disposing);
            }
            catch (Exception e)
            {
                e.Print();
            }
        }
    }
}