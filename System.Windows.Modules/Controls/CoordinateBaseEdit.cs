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
using System.ComponentModel.Design;
using System.Geometries;
using System.Windows.Forms;

namespace System.Windows.Modules.Controls
{
    [ToolboxItem(false)]
    internal abstract class CoordinateEdit : GlyphButtonEdit
    {
        protected override void OnButtonClick(GlyphButton button, EventArgs e)
        {
            var grid = Parent.GetParent<ApplicationPropertyGridControl>();

            if (grid.HasValue() && grid.SelectedItem.HasValue())
            {
                IProject project = grid.Application.ActiveProject;

                if (project.HasValue())
                {
                    BeginEdit(grid, project, EditValue as ICoordinate ?? TypeFactory.Default.Create<ICoordinate>());
                }
            }
        }

        protected void BeginEdit(ApplicationPropertyGridControl grid, IProject project, ICoordinate value)
        {
            IApplicationComponent component = value.GetComponent(project.Designer);

            OnBeginEdit(grid, project, value);

            component.EditCompleted += (args) =>
            {
                if (args.Action == ComponentEditCompleteAction.Complete)
                {
                    OnEditCompleted(args.Component.Value as ICoordinate);
                }
            };

            project.Designer.BeginEdit(component);
        }

        protected virtual void OnBeginEdit(ApplicationPropertyGridControl grid, IProject project, ICoordinate value)
        {
        }

        protected virtual void OnEditCompleted(ICoordinate value)
        {
        }

        protected override string OnDisplayText(object value, IFormatProvider provider)
        {
            var c = value as ICoordinate;

            if (c.IsNull() || c.IsEmpty())
            {
                return base.OnDisplayText(value, provider);
            }

            return "({0:N} {1:N})".Format(provider, c.X, c.Y);
        }
    }
}