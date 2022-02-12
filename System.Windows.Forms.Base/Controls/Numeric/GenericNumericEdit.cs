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

namespace System.Windows.Forms
{
    [ToolboxItem(false)]
    public class NumericEdit<T> : TextEdit where T : struct
    {
        public NumericEdit()
        {
            Comparer = Comparer<T>.Default;
            TextAlign = ContentAlignment.MiddleRight;
            Converter = new NumericConverter<T>();
        }

        protected IComparer<T> Comparer;
        protected NumericConverter<T> Converter;

        public new T EditValue
        {
            get { return (T)base.EditValue; }
            set { base.EditValue = value; }
        }

        [Browsable(false)]
        public virtual bool IsFloatValue
        {
            get
            {
                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        return true;
                }

                return false;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string NumberFormat
        {
            get { return Converter.Format; }
            set { Converter.Format = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public T? MinimumValue
        {
            get;
            set;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public T? MaximumValue
        {
            get;
            set;
        }

        protected override object OnParse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return MinimumValue;
            }

            T newValue = NumericConverter<T>.FromString(value);

            if (MinimumValue.HasValue && Comparer.Compare(newValue, MinimumValue.Value) < 0)
            {
                return MinimumValue;
            }

            if (MaximumValue.HasValue && Comparer.Compare(MaximumValue.Value, MinimumValue.Value) > 0 && Comparer.Compare(newValue, MaximumValue.Value) > 0)
            {
                return MaximumValue;
            }

            return newValue;
        }

        protected override string OnDisplayText(object value, IFormatProvider provider)
        {
            return Converter.ToString(value ?? default(T), provider);
        }

        protected override TextEditBase OnCreateBaseEdit()
        {
            return new NumericTextEdit(this)
            {
                IsFloatValue = IsFloatValue
            };
        }

        protected class NumericTextEdit : TextEditBase
        {
            public NumericTextEdit(TextEdit owner)
                : base(owner)
            {
            }

            public bool IsFloatValue
            {
                get;
                set;
            }

            protected bool Contains(char value)
            {
                return Text.Contains(value) && !SelectedText.Contains(value);
            }

            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                if (!e.Handled)
                {
                    if (!char.IsControl(e.KeyChar))
                    {
                        if (IsFloatValue && (e.KeyChar == '.' || e.KeyChar == ','))
                        {
                            e.Handled = Contains(e.KeyChar = '.');
                        }
                        else if (e.KeyChar == '-' || e.KeyChar == '+')
                        {
                            e.Handled = Contains(e.KeyChar);
                        }
                        else
                        {
                            e.Handled = !char.IsNumber(e.KeyChar);
                        }
                    }
                }

                base.OnKeyPress(e);
            }
        }
    }
}