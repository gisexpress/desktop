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

namespace System.Windows.Forms
{
    public class TimeSpanEdit : BaseEdit, ISupportInitialize
    {
        public TimeSpanEdit()
        {
            Initialize();
        }

        protected bool Initializing;
        protected NumericEdit<int> HourEdit;
        protected NumericEdit<int> MinuteEdit;
        protected NumericEdit<int> SecondEdit;

        public new TimeSpan EditValue
        {
            get { return (TimeSpan)base.EditValue; }
            set { base.EditValue = value; }
        }

        protected override void OnBeginEdit(Point point, ITypeDescriptorContext context)
        {
            HourEdit.BeginEdit(point, context);
            base.OnBeginEdit(point, context);
        }

        public void BeginInit()
        {
            Initializing = true;
        }

        public void EndInit()
        {
            Initializing = false;
        }

        protected void Initialize()
        {
            var panel = new TablePanelEdit();

            panel.AddRow(SizeType.Absolute, 22);
            panel.AddColumn(SizeType.Absolute, 3);
            panel.AddColumn(SizeType.Absolute, ControlHelper.MeasureText("00", Font).Width);
            panel.AddColumn(SizeType.Absolute, 3);
            panel.AddColumn(SizeType.Absolute, ControlHelper.MeasureText("00", Font).Width);
            panel.AddColumn(SizeType.Absolute, 3);
            panel.AddColumn(SizeType.Absolute, ControlHelper.MeasureText("00", Font).Width);

            panel.Controls.Add(HourEdit = new NumericEdit<int> { MaxLength = 2, MinimumValue = 0, MaximumValue = 24, NumberFormat = "00", Dock = DockStyle.Fill, BorderStyle = default(Border3DSide), Margin = new Padding(1), Padding = new Padding(1), TextAlign = ContentAlignment.MiddleCenter }, 1, 0);
            panel.Controls.Add(new LabelEdit { AutoSize = false, Text = ":", Width = 3, Height = 21, TextAlign = ContentAlignment.BottomCenter, Margin = new Padding(0) }, 2, 0);
            panel.Controls.Add(MinuteEdit = new NumericEdit<int> { MaxLength = 2, MinimumValue = 0, MaximumValue = 60, NumberFormat = "00", Dock = DockStyle.Fill, BorderStyle = default(Border3DSide), Margin = new Padding(1), Padding = new Padding(1), TextAlign = ContentAlignment.MiddleCenter }, 3, 0);
            panel.Controls.Add(new LabelEdit { AutoSize = false, Text = ":", Width = 3, Height = 21, TextAlign = ContentAlignment.BottomCenter, Margin = new Padding(0) }, 4, 0);
            panel.Controls.Add(SecondEdit = new NumericEdit<int> { MaxLength = 2, MinimumValue = 0, MaximumValue = 60, NumberFormat = "00", Dock = DockStyle.Fill, BorderStyle = default(Border3DSide), Margin = new Padding(1), Padding = new Padding(1), TextAlign = ContentAlignment.MiddleCenter }, 5, 0);

            MaximumSize =
            MinimumSize = new Size((int)panel.ColumnStyles.Cast<ColumnStyle>().Sum(c => c.Width) + 6, 24);
            BackColor = ApplicationAppereance.Colors.Window;

            Controls.Add(panel);

            HourEdit.EditValueChanged += OnEditValueChanged;
            MinuteEdit.EditValueChanged += OnEditValueChanged;
            SecondEdit.EditValueChanged += OnEditValueChanged;
        }

        protected override void OnEditValueChanged()
        {
            using (this.Begin())
            {
                HourEdit.EditValue = EditValue.Hours;
                MinuteEdit.EditValue = EditValue.Minutes;
                SecondEdit.EditValue = EditValue.Seconds;
            }

            base.OnEditValueChanged();
        }

        protected void OnEditValueChanged(object sender, EventArgs e)
        {
            if (!Initializing)
            {
                EditValue = new TimeSpan(HourEdit.EditValue, MinuteEdit.EditValue, SecondEdit.EditValue);
            }
        }

        protected override string OnDisplayText(object value, IFormatProvider provider)
        {
            return @"{0:hh} : {0:mm} : {0:ss}".FormatInvariant(value);
        }

        protected override void OnPaintValue(Graphics g, Rectangle bounds, Image image, object value)
        {
            bounds.Offset(0, -1);
            base.OnPaintValue(g, bounds, image, value);
        }
    }
}