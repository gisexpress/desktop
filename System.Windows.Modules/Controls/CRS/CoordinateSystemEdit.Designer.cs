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

namespace System.Windows.Modules.Controls
{
    partial class CoordinateSystemEdit
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components.HasValue()))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CrsGroup = new System.Windows.Forms.GroupEdit();
            this.CrsDropDown = new System.Windows.Modules.Controls.CoordinateSystemDropDownEdit();
            this.CrsLabel = new System.Windows.Forms.LabelEdit();
            this.CrsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // CrsGroup
            // 
            this.CrsGroup.Controls.Add(this.CrsLabel);
            this.CrsGroup.Location = new System.Drawing.Point(11, 44);
            this.CrsGroup.Name = "CrsGroup";
            this.CrsGroup.Padding = new System.Windows.Forms.Padding(3);
            this.CrsGroup.Size = new System.Drawing.Size(353, 210);
            this.CrsGroup.TabIndex = 4;
            this.CrsGroup.TabStop = false;
            // 
            // CrsDropDown
            // 
            this.CrsDropDown.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.CrsDropDown.Location = new System.Drawing.Point(11, 11);
            this.CrsDropDown.MaxLength = 32767;
            this.CrsDropDown.MinimumSize = new System.Drawing.Size(16, 22);
            this.CrsDropDown.Name = "CrsDropDown";
            this.CrsDropDown.Size = new System.Drawing.Size(353, 23);
            this.CrsDropDown.TabIndex = 5;
            this.CrsDropDown.TabStop = false;
            // 
            // LabelCrs
            // 
            this.CrsLabel.BackColor = System.Drawing.Color.Transparent;
            this.CrsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CrsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.CrsLabel.Location = new System.Drawing.Point(8, 22);
            this.CrsLabel.Name = "LabelCrs";
            this.CrsLabel.Size = new System.Drawing.Size(337, 180);
            this.CrsLabel.TabIndex = 0;
            this.CrsLabel.TabStop = false;
            // 
            // CoordinateSystemEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CrsDropDown);
            this.Controls.Add(this.CrsGroup);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Name = "CoordinateSystemEdit";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Size = new System.Drawing.Size(446, 353);
            this.CrsGroup.ResumeLayout(false);
            this.CrsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.GroupEdit CrsGroup;
        private CoordinateSystemDropDownEdit CrsDropDown;
        private Forms.LabelEdit CrsLabel;


    }
}