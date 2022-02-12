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

//using System.Drawing;
//using System.Resources;

//namespace System.Windows.Forms
//{
//    public class NodeExpandingEventArgs : EventArgs, IDisposable
//    {
//        public NodeExpandingEventArgs(TreeViewBaseEdit view, TreeNode node)
//        {
//            View = view;
//            Node = node;
//            Node.Tag = this;
//            Invoke(Node.Nodes.Clear);
//            ImageAnimator.Animate(Image = Images016.Wait.Clone() as Bitmap, OnFrameChangedHandler);
//        }

//        protected bool Disposed;

//        public TreeViewBaseEdit View
//        {
//            get;
//            protected set;
//        }

//        public TreeNode Node
//        {
//            get;
//            protected set;
//        }

//        public bool IsValid()
//        {
//            return Node.TreeView.HasValue() && !View.IsDisposing() && !View.IsDisposed();
//        }

//        protected internal readonly Bitmap Image;

//        public bool Invalidate()
//        {
//            return Invalidate(false);
//        }

//        public bool Invalidate(bool forced)
//        {
//            if (IsValid())
//            {
//                if (forced || ApplicationEnvironment.InvalidateRequired())
//                {
//                    Invoke(Node.TreeView.Refresh);
//                }

//                return true;
//            }

//            return false;
//        }

//        public void Invoke(Action action)
//        {
//            if (IsValid())
//            {
//                Node.TreeView.Invoke(action);
//            }
//        }

//        void OnFrameChangedHandler(object sender, EventArgs e)
//        {
//            if (IsValid())
//            {
//                if (!Disposed)
//                {
//                    ImageAnimator.UpdateFrames(Image);
//                    Invalidate(true);
//                }
//                else
//                {
//                    using (Image)
//                    {
//                        ImageAnimator.StopAnimate(Image, OnFrameChangedHandler);
//                    }
//                }
//            }
//        }

//        public void Dispose()
//        {
//            Node.Tag = null;
//            Disposed = true;

//            if (Invalidate(true) && Node.Nodes.Count == 0)
//            {
//                Node.SelectedImageKey = Node.ImageKey;
//            }

//            GC.SuppressFinalize(this);
//        }
//    }
//}
