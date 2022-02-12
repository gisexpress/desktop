﻿////////////////////////////////////////////////////////////////////////////////////////////////////
////
////  Copyright © GISExpress 2015-2022. All Rights Reserved.
////  
////  GISExpress .NET API and Component Library
////  
////  The entire contents of this file is protected by local and International Copyright Laws.
////  Unauthorized reproduction, reverse-engineering, and distribution of all or any portion of
////  the code contained in this file is strictly prohibited and may result in severe civil and 
////  criminal penalties and will be prosecuted to the maximum extent possible under the law.
////  
////  RESTRICTIONS
////  
////  THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES ARE CONFIDENTIAL AND PROPRIETARY TRADE SECRETS OF GISExpress
////  THE REGISTERED DEVELOPER IS LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET COMPONENTS AS PART OF AN EXECUTABLE PROGRAM ONLY.
////  
////  THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE
////  COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT
////  AND PERMISSION FROM GISExpress
////  
////  CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON ADDITIONAL RESTRICTIONS.
////  
////  Warning: This content was generated by GISExpress tools.
////  Changes to this content may cause incorrect behavior and will be lost if the content is regenerated.
////
/////////////////////////////////////////////////////////////////////////////////////////////////////

//using System.ComponentModel;
//using System.Drawing;
//using System.Resources;

//namespace System.Windows.Forms
//{
//    public class TreeViewEdit : BaseEdit
//    {
//        public TreeViewEdit()
//        {
//            Args = new DrawTextEventArgs
//            {
//                Font = Font,
//                FormatFlags = TextFormatFlags.EndEllipsis
//            };

//            Padding = new Padding(1, 3, 1, 3);
//            BackColor = ApplicationAppereance.Colors.Window;

//            Controls.Add(OwnerEdit = new TreeViewBaseEdit(this) { Dock = DockStyle.Fill });
//        }

//        public event DrawTreeNodeEventHandler DrawNode;

//        protected DrawTextEventArgs Args;

//        [Browsable(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public TreeViewBaseEdit OwnerEdit
//        {
//            get;
//            protected set;
//        }

//        public new event KeyEventHandler KeyDown
//        {
//            add { OwnerEdit.KeyDown += value; }
//            remove { OwnerEdit.KeyDown -= value; }
//        }

//        public new event MouseEventHandler MouseDown
//        {
//            add { OwnerEdit.MouseDown += value; }
//            remove { OwnerEdit.MouseDown -= value; }
//        }

//        public new event MouseEventHandler MouseMove
//        {
//            add { OwnerEdit.MouseMove += value; }
//            remove { OwnerEdit.MouseMove -= value; }
//        }

//        public void BeginUpdate()
//        {
//            OwnerEdit.InvokeAction(OwnerEdit.BeginUpdate);
//            OwnerEdit.InvokeAction(OwnerEdit.Hide);
//        }

//        public void EndUpdate()
//        {
//            OwnerEdit.InvokeAction(OwnerEdit.EndUpdate);
//            OwnerEdit.InvokeAction(OwnerEdit.Show);
//        }

//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public override bool AllowDrop
//        {
//            get { return OwnerEdit.AllowDrop; }
//            set { OwnerEdit.AllowDrop = value; }
//        }

//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public bool AutoWidth
//        {
//            get { return OwnerEdit.AutoWidth; }
//            set { OwnerEdit.AutoWidth = value; }
//        }

//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public bool CheckBoxes
//        {
//            get { return OwnerEdit.CheckBoxes; }
//            set { OwnerEdit.CheckBoxes = value; }
//        }

//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public int Indent
//        {
//            get { return OwnerEdit.Indent; }
//            set { OwnerEdit.Indent = value; }
//        }

//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public int ItemHeight
//        {
//            get { return OwnerEdit.ItemHeight; }
//            set { OwnerEdit.ItemHeight = value; }
//        }

//        public void Clear()
//        {
//            OwnerEdit.Nodes.Clear();
//        }

//        public ImageList ImageList
//        {
//            get { return OwnerEdit.ImageList; }
//        }

//        public TreeNodeCollection Nodes
//        {
//            get { return OwnerEdit.Nodes; }
//        }

//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public TreeNode SelectedNode
//        {
//            get { return OwnerEdit.SelectedNode; }
//            set { OwnerEdit.SelectedNode = value; }
//        }

//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public TreeNode[] CheckedNodes
//        {
//            get { return OwnerEdit.CheckedNodes; }
//            set { OwnerEdit.CheckedNodes = value; }
//        }

//        public override Rectangle WorkingArea
//        {
//            get
//            {
//                Rectangle r = base.WorkingArea;
//                r.Width = OwnerEdit.ClientRectangle.Width;
//                return r;
//            }
//        }

//        public int AddImage(string key, Image image)
//        {
//            return ImageList.AddImage(key, image);
//        }

//        public void PaintNode(DrawTreeNodeEventArgs e)
//        {
//            if (Visible && e.Bounds.IsEmpty == false)
//            {
//                var left = OwnerEdit.GetLeft(e.Node.Level);
//                var expanding = e.Node.Tag as NodeExpandingEventArgs;
//                var bounds = new Rectangle(e.Bounds.Left + left, e.Bounds.Top, e.Bounds.Width - left, e.Bounds.Height);

//                Image imgNode = ImageList.Images[e.Node.ImageKey];
//                Image imgPlusMinus = expanding.HasValue() ? expanding.Image : e.Node.IsExpanded ? Images016.CollapseNode : Images016.ExpandNode;

//                Graphics g = e.Graphics;

//                g.FillRectangle(SystemBrushes.Window, e.Bounds);

//                if ((expanding.HasValue() || e.Node.Nodes.Count > 0) && imgPlusMinus.HasValue())
//                {
//                    lock (imgPlusMinus)
//                    {
//                        g.DrawImage(imgPlusMinus, bounds.Left, e.Bounds.Y + (e.Bounds.Height - 16) / 2);
//                    }
//                }

//                bounds.Offset(Math.Max(Indent, 16), 0);

//                if (e.Node.IsSelected)
//                {
//                    var rect = new Rectangle(bounds.Left - 2, bounds.Y + 1, e.Bounds.Right - ImageList.ImageSize.Width - bounds.Left + 12, e.Bounds.Height - 2);

//                    g.FillRectangle(ControlHelper.Brushes.Highlight, rect);
//                    g.DrawRectangle(ControlHelper.Pens.HighlightBorder, rect);
//                }

//                if (CheckBoxes)
//                {
//                    g.DrawCheckBox(bounds, true, e.Node.Checked);
//                    bounds.Offset(19, 0);
//                }

//                if (string.IsNullOrEmpty(e.Node.ImageKey) == false)
//                {
//                    var image = ImageList.Images[e.Node.IsExpanded ? e.Node.SelectedImageKey : e.Node.ImageKey];

//                    if (image.HasValue())
//                    {
//                        var location = new PointF(bounds.Left, e.Bounds.Y + (e.Bounds.Height - ImageList.ImageSize.Height) / 2);

//                        lock (image)
//                        {
//                            g.DrawImage(image, location.X, location.Y, ImageList.ImageSize.Width, ImageList.ImageSize.Height);
//                        }
//                    }
//                }

//                var position = new Point(bounds.Left + ImageList.ImageSize.Width, e.Bounds.Y + (e.Bounds.Height - Font.Height) / 2);
//                var layoutRectangle = new Rectangle(position.X, position.Y, Width - position.X, e.Bounds.Height);

//                OnPaintNodeText(e, layoutRectangle);
//            }
//        }

//        protected internal new virtual void OnPaint(PaintEventArgs e)
//        {
//            OwnerEdit.Paint(e);
//        }

//        protected internal virtual void OnPaintNode(DrawTreeNodeEventArgs e)
//        {
//            e.DrawDefault = false;
//            PaintNode(e);
//            DrawNode.InvokeSafely(this, e);
//        }

//        protected internal virtual void OnItemDrag(ItemDragEventArgs e)
//        {
//        }

//        protected internal virtual new void OnDragEnter(DragEventArgs e)
//        {
//            base.OnDragEnter(e);
//        }

//        protected internal virtual new void OnDragOver(DragEventArgs e)
//        {
//            base.OnDragOver(e);
//        }

//        protected internal virtual new void OnDragDrop(DragEventArgs e)
//        {
//            base.OnDragDrop(e);
//        }

//        protected internal virtual new void OnDragLeave(EventArgs e)
//        {
//            base.OnDragLeave(e);
//        }

//        public TreeNode GetNodeAt(Point point)
//        {
//            return OwnerEdit.GetNodeAt(point);
//        }

//        protected virtual void OnPaintNodeText(DrawTreeNodeEventArgs e, Rectangle bounds)
//        {
//            Args.Font = Font;
//            Args.Text = e.Node.Text;
//            Args.Bounds = bounds;
//            Args.ForeColor = e.Node.IsSelected ? ApplicationAppereance.Colors.ControlHighlightText : ApplicationAppereance.Colors.ControlText;

//            e.Graphics.DrawText(Args);
//        }

//        public virtual bool IsDisposing()
//        {
//            return OwnerEdit.IsDisposing();
//        }

//        public new virtual bool IsDisposed()
//        {
//            return OwnerEdit.IsDisposed();
//        }
//    }
//}