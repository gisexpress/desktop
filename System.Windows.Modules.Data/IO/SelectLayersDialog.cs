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

using System.Data;
using System.Data.IO;
using System.Windows.Forms;

namespace System.Windows.Modules
{
    internal class SelectLayersDialog : ApplicationForm
    {
        public SelectLayersDialog()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private ListViewEdit List;

        public void SetLayers(SqlTableSourceList tables)
        {
            List.CheckBox = true;
            List.MultiSelect = true;

            List.Columns.Clear();
            List.Columns.Add(SizeType.Percent, 100).Text = Localization.Localize("Layer");

            foreach (SqlTableSource table in tables)
            {
                List.Rows.Add(table, table.DisplayName).Checked = true;
            }
        }

        public void AcceptChanges(SqlTableSourceList tables)
        {
            foreach (ListViewEdit.Row item in List.Rows)
            {
                if (item.Checked)
                {
                    continue;
                }

                tables.Remove(item.Value as SqlTableSource);
            }
        }
    
        private void InitializeComponent()
        {
            this.List = new System.Windows.Forms.ListViewEdit();
            this.BodyControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // BodyControl
            // 
            this.BodyControl.Controls.Add(this.List);
            this.BodyControl.Padding = new System.Windows.Forms.Padding(6);
            // 
            // listViewEdit1
            // 
            this.List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List.Location = new System.Drawing.Point(6, 6);
            this.List.Name = "listViewEdit1";
            this.List.Padding = new System.Windows.Forms.Padding(1);
            this.List.Size = new System.Drawing.Size(462, 304);
            this.List.TabIndex = 0;
            this.List.TabStop = false;
            // 
            // SelectLayersDialog
            // 
            this.ClientSize = new System.Drawing.Size(480, 400);
            this.Name = "SelectLayersDialog";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowFooter = true;
            this.Controls.SetChildIndex(this.CaptionControl, 0);
            this.Controls.SetChildIndex(this.BodyControl, 0);
            this.BodyControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}