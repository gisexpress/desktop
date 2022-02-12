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

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using ToolBarState = System.Windows.Forms.VisualStyles.ToolBarState;

namespace System.Windows.Forms.Ribbon
{
    [ToolboxItem(false)]
    public class RibbonToolbarButton : RibbonToolbarLabel
    {
        public RibbonToolbarButton()
            : this(default(RibbonControl))
        {
        }

        public RibbonToolbarButton(RibbonControl ribbon)
        {
            Ribbon = ribbon;
        }

        public readonly RibbonControl Ribbon;

        public event Action<bool> CheckedChanged;

        protected bool FlagDown;

        protected Image ImageSmall;
        protected Image ImageLarge;

        protected ItemCollection MenuItems;
        protected ButtonEdit MenuDropDown;

        public new string Name
        {
            get { return base.Name; }
            set
            {
                if (Ribbon.HasValue())
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        var nameArray = new List<string>((value).Split('.'));

                        if (nameArray.Count > 0)
                        {
                            nameArray.Remove(base.Name = nameArray.Last());
                            nameArray.Remove(GroupName = nameArray.LastOrDefault());
                            nameArray.Remove(PageName = nameArray.LastOrDefault() ?? "MainPage");

                            if (Ribbon.GetGroupByName(PageName).HasValue())
                            {
                                ParentName = GroupName;
                                GroupName = PageName;
                                PageName = "MainPage";
                            }

                            Localization.Register(e => Text = e(base.Name));
                        }
                    }
                }
                else
                {
                    base.Name = value;
                }
            }
        }

        public string ParentName
        {
            get;
            set;
        }

        public string PageName
        {
            get;
            set;
        }

        public string GroupName
        {
            get;
            set;
        }

        public bool IsChecked
        {
            get;
            set;
        }

        public bool IsCheckButton
        {
            get;
            set;
        }

        public new Border3DSide BorderStyle
        {
            get;
            set;
        }

        public Keys Shortcut
        {
            get;
            set;
        }

        public Image SmallImage
        {
            get { return ImageSmall; }
            set
            {
                ImageSmall = value;

                if (LargeImage.IsNull() && ImageSmall.HasValue())
                {
                    Multiline = false;
                    MinimumSize = new Size(22, 20);
                    Image = BitmapExtensions.NewImage(ImageSmall, 16, 16);
                    ImageAlign = ContentAlignment.MiddleLeft;
                    TextAlign = ContentAlignment.MiddleLeft;
                }
            }
        }

        public virtual Image LargeImage
        {
            get { return ImageLarge; }
            set
            {
                ImageLarge = value;

                if (ImageLarge.HasValue())
                {
                    Multiline = true;
                    MinimumSize = new Size(22, 60);
                    Image = ImageLarge;
                    ImageAlign = ContentAlignment.TopCenter;
                    TextAlign = ContentAlignment.BottomCenter;
                }
            }
        }

        public RibbonCommandVisibility Visibility
        {
            get;
            set;
        }

        public void EnabledWhenInternetStatusIs(InternetStatus value)
        {
            Network.Current.EnabledWhenInternetStatusIs(this, value);
        }

        public ItemCollection Items
        {
            get { return (MenuItems ?? (MenuItems = OnCreateItems())); }
        }

        protected ItemCollection OnCreateItems()
        {
            var value = new ItemCollection(this);

            MenuDropDown = new ButtonEdit
            {
                Text = Text,
                Margin = new Padding(0),
                Padding = new Padding(0),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleLeft,
                BorderStyle = default,
            };

            Controls.Add(MenuDropDown);

            MenuDropDown.Click += OnShowPopupClick;
            MenuDropDown.Paint += OnDropDownPaint;
            MenuDropDown.MouseEnter += (s, e) => MenuDropDown.BorderStyle = Border3DSide.All;
            MenuDropDown.MouseLeave += (s, e) => MenuDropDown.BorderStyle = default;

            Properties.HasChanges = true;

            return value;
        }

        public void ShowPopup()
        {
            if (Enabled)
            {
                MenuItems.Show();
            }
        }

        protected void OnShowPopupClick(object sender, EventArgs e)
        {
            ShowPopup();
        }

        protected override string OnDisplayText(object value, IFormatProvider provider)
        {
            if (MenuDropDown.IsNull())
            {
                return base.OnDisplayText(value, provider);
            }

            return string.Empty;
        }

        protected void OnDropDownPaint(object sender, PaintEventArgs e)
        {
            var c = sender as Control;

            if (c.HasValue())
            {
                using (var brush = new SolidBrush(SystemColors.ControlText))
                {
                    e.Graphics.DrawGlyphButton(GlyphButtonType.Down, c.Width - 16, (c.Height - 16) / 2, 16, 16);
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate(false);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate(false);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Invalidate(false);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FlagDown = e.Button == MouseButtons.Left;
            Invalidate(false);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            FlagDown = false;
            Invalidate(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Enabled)
            {
                var bounds = new Rectangle(0, 0, Width, Height);

                if (IsChecked)
                {
                    e.Graphics.DrawToolbarButton(bounds, ToolBarState.HotChecked);
                }
                else if (IsMouseOver || FlagDown)
                {
                    e.Graphics.DrawToolbarButton(bounds, FlagDown ? ToolBarState.Pressed : ToolBarState.Hot);
                }
            }

            base.OnPaint(e);

            e.Graphics.DrawBorder(BorderStyle, 0, 0, Width, Height);
        }

        protected override void OnClick(EventArgs e)
        {
            if (Enabled)
            {
                if (IsCheckButton)
                {
                    CheckedChanged.InvokeSafely(IsChecked = !IsChecked);
                }

                base.OnClick(e);
            }
        }

        protected override Size OnCalculateSize()
        {
            Size newSize = base.OnCalculateSize();

            if (MenuDropDown.HasValue())
            {
                newSize.Width = Math.Max(newSize.Width, ControlHelper.MeasureText(MenuDropDown.Text = Localization.Localize(Name), Font).Width + 30);
                newSize.Height += MenuDropDown.Height;
            }

            return newSize;
        }

        public class Item : MenuItem
        {
            public Item(ItemCollection owner, string text, EventHandler onClick, Image image)
                : base(text, onClick)
            {
                Owner = owner;
                OwnerDraw = true;

                Image = image;

                if (Image.HasValue())
                {
                    Owner.ItemLeft = Math.Max(Owner.ItemLeft, Image.Width + 6);
                    Owner.ItemHeight = Math.Max(Owner.ItemHeight, Image.Height + 6);
                }
            }

            protected ItemCollection Owner;

            public Image Image
            {
                get;
                protected set;
            }

            protected override void OnMeasureItem(MeasureItemEventArgs e)
            {
                using (var c = new Label { Text = Text, Image = Image, ImageAlign = ContentAlignment.MiddleLeft, TextAlign = ContentAlignment.MiddleRight, Padding = new Padding(6) })
                {
                    e.ItemWidth = c.PreferredWidth;
                    e.ItemHeight = Math.Max(Owner.ItemHeight, c.PreferredHeight);

                    if (Image.HasValue())
                    {
                        e.ItemWidth += Image.Width;
                        e.ItemHeight = Math.Max(e.ItemHeight, Image.Height);
                    }
                }
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                var isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                var rect = new Rectangle(Owner.ItemLeft + e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - Owner.ItemLeft, e.Bounds.Height);

                using (var brush = new SolidBrush(ApplicationAppereance.Colors.Control))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                    e.Graphics.DrawToolbarButton(e.Bounds, isSelected ? ToolBarState.HotChecked : ToolBarState.Normal);
                }

                rect.Offset(6, 0);

                e.Graphics.DrawText(new DrawTextEventArgs
                {
                    Text = Text,
                    Bounds = rect,
                    FormatFlags = TextFormatFlags.VerticalCenter,
                    ForeColor = isSelected ? ApplicationAppereance.Colors.ControlHighlightText : ApplicationAppereance.Colors.ControlText
                });

                if (Image.HasValue())
                {
                    e.Graphics.DrawImage(Image, e.Bounds.Left + 3, e.Bounds.Top + 3);
                }
            }
        }

        public class ItemCollection : ContextMenu
        {
            public ItemCollection(RibbonToolbarButton owner)
            {
                Owner = owner;
                ItemHeight = 24;
            }

            protected RibbonToolbarButton Owner;

            protected internal int ItemHeight;
            protected internal int ItemLeft;

            public int Count
            {
                get { return MenuItems.Count; }
            }

            public void Add(string caption, EventHandler onClick)
            {
                Add(caption, onClick, default(Image));
            }

            public void Add(string caption, EventHandler onClick, Image image)
            {
                MenuItems.Add(new Item(this, caption, onClick, image));
            }

            public void AddSeperator()
            {
                MenuItems.Add("-");
            }

            public Item this[int index]
            {
                get { return MenuItems[index] as Item; }
            }

            public void Show()
            {
                Show(Owner, new Point(0, Owner.Bottom));
            }
        }
    }
}