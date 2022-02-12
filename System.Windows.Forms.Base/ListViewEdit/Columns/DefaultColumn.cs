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

namespace System.Windows.Forms
{
    partial class ListViewEdit
    {
        internal class DefaultColumn : Column
        {
            DefaultColumn()
                : base(SizeType.Percent, 100)
            {
            }

            public static Column Create()
            {
                return new DefaultColumn();
            }

            public override bool Browsable
            {
                get { return true; }
            }

            public override bool AllowResize
            {
                get { return false; }
            }

            public override bool AllowSort
            {
                get { return false; }
                set { }
            }

            public override bool CanFilter
            {
                get { return false; }
            }

            public override bool AllowFilter
            {
                get { return false; }
                set { }
            }

            public override Border3DSide BorderStyle
            {
                get { return Owner.View.List.IsNull() ? base.BorderStyle : default(Border3DSide); }
            }

            public override ColumnStyle Style
            {
                get { return default(ColumnStyle); }
            }

            protected override void OnPaintValue(Graphics g, Rectangle bounds, Image image, object value)
            {
                if (Owner.View.List.IsNull())
                {
                    base.OnPaintValue(g, bounds, image, value);
                }
            }
        }
    }
}