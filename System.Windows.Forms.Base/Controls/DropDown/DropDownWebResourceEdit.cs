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
    public class DropDownWebResourceEdit : DropDownEdit, ISupportInitialize
    {
        protected byte Init;
        //protected Browser Client;
        //protected const string HtmlElementName = "Select";

        public void BeginInit()
        {
            Init++;
        }

        public void EndInit()
        {
            Init--;
        }

        //protected bool IsInitializing
        //{
        //    get { return Init > 0; }
        //}

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Browser Browser
        //{
        //    get { return Client; }
        //    set
        //    {
        //        if (!Equals(Client, value))
        //        {
        //            if (Client.HasValue())
        //            {
        //                Client.DocumentCompleted -= OnDocumentCompleted;
        //            }

        //            Client = value;
        //            Client.DocumentCompleted += OnDocumentCompleted;
        //        }
        //    }
        //}

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public int HtmlSelectIndex
        //{
        //    get;
        //    set;
        //}

        //protected override void OnItemClick(object sender, EventArgs e)
        //{
        //    base.OnItemClick(sender, e);

        //    if (!IsInitializing)
        //    {
        //        HtmlOptionElement option = Browser.GetElementByTagNameAt(HtmlElementName, HtmlSelectIndex).GetOptionByValue(EditValue as string);

        //        if (option.HasValue() && !option.Selected)
        //        {
        //            option.Selected = true;
        //        }
        //    }
        //}

        //protected virtual void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    if (HtmlSelectIndex >= 0)
        //    {
        //        using (this.Begin())
        //        {
        //            object valueOld = EditValue;

        //            Items.Clear();

        //            foreach (HtmlOptionElement option in Browser.GetElementByTagNameAt(HtmlElementName, HtmlSelectIndex).GetOptions())
        //            {
        //                Items.Add(option.Value, option.Text);
        //            }

        //            EditValue = valueOld;

        //            if (SelectedIndex < 0)
        //            {
        //                SelectedIndex = 0;
        //            }
        //        }
        //    }
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);

        //    if (disposing)
        //    {
        //        if (Client.HasValue())
        //        {
        //            Client.DocumentCompleted -= OnDocumentCompleted;
        //            Client = null;
        //        }
        //    }
        //}
    }
}