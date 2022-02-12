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
    public class DateTimeEdit : PopupContainerEdit, ISupportInitialize
    {
        public DateTimeEdit()
        {
            Initialize();
            EditValue = DateTime.Now;
        }

        protected bool Initializing;

        public new DateTime EditValue
        {
            get { return (DateTime)base.EditValue; }
            set { base.EditValue = value; }
        }

        protected override void OnBeginEdit(Point point, ITypeDescriptorContext context)
        {
            DayEdit.BeginEdit(point, context);
        }

        public void BeginInit()
        {
            Initializing = true;
        }

        public void EndInit()
        {
            Initializing = false;
        }

        protected override void OnEditValueChanged()
        {
            using (this.Begin())
            {
                DayEdit.EditValue = EditValue.Day;
                MonthEdit.EditValue = EditValue.Month;
                YearEdit.EditValue = EditValue.Year;
            }

            base.OnEditValueChanged();
        }

        protected DateTimePickerEdit Picker;
        protected NumericEdit<int> DayEdit;
        protected NumericEdit<int> MonthEdit;
        protected NumericEdit<int> YearEdit;

        protected void Initialize()
        {
            var panel = new TablePanelEdit();

            panel.AddRow(SizeType.Absolute, 22);
            panel.AddColumn(SizeType.Absolute, 3);
            panel.AddColumn(SizeType.Absolute, ControlHelper.MeasureText("00", Font).Width);
            panel.AddColumn(SizeType.Absolute, 3);
            panel.AddColumn(SizeType.Absolute, ControlHelper.MeasureText("00", Font).Width);
            panel.AddColumn(SizeType.Absolute, 3);
            panel.AddColumn(SizeType.Absolute, ControlHelper.MeasureText("0000", Font).Width);

            panel.Controls.Add(DayEdit = new NumericEdit<int> { MaxLength = 2, MinimumValue = 1, MaximumValue = 31, NumberFormat = "00", Dock = DockStyle.Fill, BorderStyle = default(Border3DSide), Margin = new Padding(1), Padding = new Padding(1), TextAlign = ContentAlignment.MiddleCenter }, 1, 0);
            panel.Controls.Add(new LabelEdit { AutoSize = false, Text = ".", Width = 3, Height = 21, TextAlign = ContentAlignment.BottomCenter, Margin = new Padding(0) }, 2, 0);
            panel.Controls.Add(MonthEdit = new NumericEdit<int> { MaxLength = 2, MinimumValue = 1, MaximumValue = 12, NumberFormat = "00", Dock = DockStyle.Fill, BorderStyle = default(Border3DSide), Margin = new Padding(1), Padding = new Padding(1), TextAlign = ContentAlignment.MiddleCenter }, 3, 0);
            panel.Controls.Add(new LabelEdit { AutoSize = false, Text = ".", Width = 3, Height = 21, TextAlign = ContentAlignment.BottomCenter, Margin = new Padding(0) }, 4, 0);
            panel.Controls.Add(YearEdit = new NumericEdit<int> { MaxLength = 4, MinimumValue = 1900, MaximumValue = 9999, NumberFormat = "0000", Dock = DockStyle.Fill, BorderStyle = default(Border3DSide), Margin = new Padding(1), Padding = new Padding(1), TextAlign = ContentAlignment.MiddleCenter }, 5, 0);

            BackColor = ApplicationAppereance.Colors.Window;
            MinimumSize = new Size((int)panel.ColumnStyles.Cast<ColumnStyle>().Sum(c => c.Width) + GlyphButtons.Width + 6, 22);

            Controls.Add(panel);
            Control = Picker = new DateTimePickerEdit();

            DayEdit.EditValueChanged += OnEditValueChanged;
            MonthEdit.EditValueChanged += OnEditValueChanged;
            YearEdit.EditValueChanged += OnEditValueChanged;
            Picker.EditValueChanged += OnPickerValueChanged;
        }

        protected void OnEditValueChanged(object sender, EventArgs e)
        {
            if (!Initializing)
            {
                if (DayEdit.EditValue > 0 && MonthEdit.EditValue > 0 && YearEdit.EditValue > 0)
                {
                    EditValue = new DateTime(YearEdit.EditValue, MonthEdit.EditValue, DayEdit.EditValue);
                }
            }
        }

        protected override void OnPopupOpening(object sender, CancelEventArgs e)
        {
            using (this.Begin())
            {
                Picker.EditValue = EditValue;
            }

            base.OnPopupOpening(sender, e);
        }

        protected void OnPickerValueChanged(object sender, EventArgs e)
        {
            if (!Initializing)
            {
                EditValue = Picker.EditValue;
                Close();
            }
        }

        protected override string OnDisplayText(object value, IFormatProvider provider)
        {
            return LocalizedDateTimeConverter.ToString(value);
        }

        protected override void OnPaintValue(Graphics g, Rectangle bounds, Image image, object value)
        {
            bounds.Width -= 3;
            base.OnPaintValue(g, bounds, image, value);
        }
    }
}
