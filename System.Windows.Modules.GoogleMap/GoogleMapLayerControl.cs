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
using System.Windows.Forms;
using System.Workspace;

namespace System.Windows.Modules.GoogleMap
{
    internal class GoogleMapLayerControl : MapLayerControl
    {
        protected GroupEdit MapTypeGroup;
        protected DropDownEdit MapTypeList;

        public GoogleMapLayerControl()
        {
            InitializeComponent();

            string displayName;

            MapTypeGroup.Text = Localization.Localize("GoogleMapType");

            foreach (var item in Enums.GetValues<GoogleMapType>())
            {
                if (Enums.TryGetDisplayName(item, out displayName))
                {
                    MapTypeList.Items.Add(item, displayName);
                }
            }

            MapTypeList.EditValue = GoogleMapType.StandardRoadmap;
        }

        protected override Exception OnValidate()
        {
            return default(Exception);
        }

        protected override void OnCreateLayers(MapLayer parent)
        {
            var item = new GoogleMapLayer(parent.OwnerDocument)
            {
                MapType = (GoogleMapType)(MapTypeList.EditValue ?? GoogleMapType.StandardRoadmap)
            };

            Workspace.Layers.Add(item);
        }

        protected void InitializeComponent()
        {
            this.MapTypeList = new System.Windows.Forms.DropDownEdit();
            this.MapTypeGroup = new System.Windows.Forms.GroupEdit();
            this.MapTypeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapTypeList
            // 
            this.MapTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapTypeList.Location = new System.Drawing.Point(8, 22);
            this.MapTypeList.MaxLength = 32767;
            this.MapTypeList.MinimumSize = new System.Drawing.Size(16, 22);
            this.MapTypeList.Name = "MapTypeList";
            this.MapTypeList.Padding = new System.Windows.Forms.Padding(3);
            this.MapTypeList.Size = new System.Drawing.Size(248, 23);
            this.MapTypeList.TabIndex = 0;
            // 
            // MapTypeGroup
            // 
            this.MapTypeGroup.Controls.Add(this.MapTypeList);
            this.MapTypeGroup.Location = new System.Drawing.Point(3, 3);
            this.MapTypeGroup.Name = "MapTypeGroup";
            this.MapTypeGroup.Padding = new System.Windows.Forms.Padding(8);
            this.MapTypeGroup.Size = new System.Drawing.Size(264, 53);
            this.MapTypeGroup.TabIndex = 1;
            this.MapTypeGroup.TabStop = false;
            this.MapTypeGroup.Text = " Map ";
            // 
            // GoogleMapLayerControl
            // 
            this.Controls.Add(this.MapTypeGroup);
            this.Name = "GoogleMapLayerControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(328, 117);
            this.MapTypeGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}