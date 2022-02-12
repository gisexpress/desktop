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
    partial class DocumentManager
    {
        public class Document : TabEditPage
        {
            public Document(Control value)
                : this(value.Name, value)
            {
            }

            public Document(string name, Control value)
                : base(name)
            {
                Name = name;
                Text = value.Text;
                Padding = new Padding(1);
                BorderStyle = Border3DSide.All;

                value.Dock = DockStyle.Fill;
                value.TextChanged += OnTextChanged;
                value.GotFocus += OnGotFocus;
                value.LostFocus += OnLostFocus;

                Controls.Add(Control = value);
            }

            protected internal DocumentManager DocumentManager;

            public Control Control
            {
                get;
                protected set;
            }

            protected void OnTextChanged(object sender, EventArgs e)
            {
                Text = (sender as Control).Text;
            }

            protected void OnGotFocus(object sender, EventArgs e)
            {
                DocumentManager.View.Invalidate(false);
            }

            protected void OnLostFocus(object sender, EventArgs e)
            {
                DocumentManager.View.Invalidate(false);
            }

            protected override bool OnRemove()
            {
                var e = new CancelEventArgs();
                DocumentManager.OnDocumentClosing(this, e);

                if (!e.Cancel)
                {
                    return base.OnRemove();
                }

                return false;
            }

            public bool Close()
            {
                return Remove();
            }
        }
    }
}