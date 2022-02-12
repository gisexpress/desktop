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

using System.ComponentModel.Design;
using System.Drawing;
using System.Geometries;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Ribbon;

namespace System.Windows.Modules
{
    partial class MainModule
    {
        protected bool CopyModeChecked;
        protected bool IgnoreSelectionEvents;
        protected FeatureSelectionComponent SelectionComponent;

        public bool CopyMode
        {
            get
            {
                return CopyModeChecked;
            }
            set
            {
                var command = Form.CommandManager[CommandSelectionCopy];

                CopyModeChecked = value;

                if (command.HasValue())
                {
                    command.IsChecked = value;
                }
            }
        }

        void BeginSelectFeature(object argument)
        {
            if (ActiveProject.HasValue())
            {
                ActiveProject.FeatureSelect -= SelectFeature;
                ActiveProject.FeatureSelect -= SelectFeatures;
                ActiveProject.FeatureSelect += SelectFeature;

                UpdateSelectionCommands(ActiveProject);
                Application.CopyMode = false;
            }
        }

        void BeginSelectFeatures(object argument)
        {
            Application.CopyMode = false;

            ActiveProject.FeatureSelect -= SelectFeature;
            ActiveProject.FeatureSelect -= SelectFeatures;
            ActiveProject.FeatureSelect += SelectFeatures;

            SelectionComponent.DisposeSafely();
            SelectionComponent = new FeatureSelectionComponent(ActiveProject, UpdateSelectionCommands);
            SelectionComponent.EditCompleted += (e) => EndSelectFeature();

            ActiveProject.Designer.BeginEdit(SelectionComponent);
            UpdateSelectionCommands(ActiveProject);
        }

        void EndSelectFeature()
        {
            SelectionComponent.DisposeSafely();
            SelectionComponent = null;

            ActiveProject.FeatureSelect -= SelectFeature;
            ActiveProject.FeatureSelect -= SelectFeatures;
            ActiveProject.Workspace.GetLayers().ForEach(item => item.Selection.Clear());

            UpdateSelectionCommands(ActiveProject);
            ActiveProject.Render();
        }

        void SelectionSelectAll(object argument)
        {
            if (SelectionComponent.HasValue())
            {
                SelectionComponent.SelectAll();
            }
        }

        void SelectionDeselectAll(object argument)
        {
            if (SelectionComponent.HasValue())
            {
                SelectionComponent.DeselectAll();
            }
        }

        void SelectionReverseSelection(object argument)
        {
            if (SelectionComponent.HasValue())
            {
                SelectionComponent.ReverseSelection();
            }
        }

        void SelectionSelectByRect(object argument)
        {
            SelectionComponent.SelectByRect();
        }

        void SelectionSelectByArea(object argument)
        {
            SelectionComponent.SelectByPolygon();
        }

        void SelectionCopy(object argument)
        {
            if (SelectionComponent.HasValue())
            {
                SelectionComponent.Copy();
            }
        }

        void SelectionIntersection(object argument)
        {
            SelectionComponent.Overlay(SpatialFunctions.Intersection);
        }

        void SelectionUnion(object argument)
        {
            SelectionComponent.Overlay(SpatialFunctions.Union);
        }

        void SelectionDifference(object argument)
        {
            SelectionComponent.Overlay(SpatialFunctions.Difference);
        }

        void SelectionSymmetricDifference(object argument)
        {
            SelectionComponent.Overlay(SpatialFunctions.SymDifference);
        }

        void SelectionSplit(object argument)
        {
            if (SelectionComponent == null)
            {
                SelectionComponent = new FeatureSelectionComponent(ActiveProject, UpdateSelectionCommands);
                SelectionComponent.EditCompleted += (e) => EndSelectFeature();
            }

            SelectionComponent.Split();
            SelectionCancel(argument);
        }

        void SelectionLinesToPolygon(object argument)
        {
            if (SelectionComponent == null)
            {
                SelectionComponent = new FeatureSelectionComponent(ActiveProject, UpdateSelectionCommands);
                SelectionComponent.EditCompleted += (e) => EndSelectFeature();
            }

            SelectionComponent.LinesToPolygon();
            SelectionCancel(argument);
        }

        void SelectionDelete(object argument)
        {
            if (ActiveProject.Designer.ActiveComponent.HasValue() && !ActiveProject.Designer.ActiveComponent.IsBusy())
            {
                ActiveProject.Designer.ActiveComponent.EndEdit(ComponentEditCompleteAction.Delete);
            }
        }

        void SelectionCancel(object argument)
        {
            if (ActiveProject.Designer.ActiveComponent.HasValue())
            {
                ActiveProject.Designer.ActiveComponent.EndEdit(ComponentEditCompleteAction.Cancel);
            }
            else
            {
                EndSelectFeature();
            }
        }

        void SelectFeature(FeatureEventArgs e)
        {
            OnFeatureChoose(e, (item) =>
            {
                IApplicationComponent current;
                IGeometry geometry = item.Query(e.Bounds);

                var isNull = (current = e.Project.Designer.ActiveComponent) == null;
                var currentGeometry = isNull ? null : current.Value as IGeometry;

                if (geometry.HasValue() && geometry != currentGeometry)
                {
                    var component = SelectFeature(item, geometry);

                    if (current.HasValue())
                    {
                        using (current)
                        {
                            IgnoreSelectionEvents = true;
                            current.EndEdit(ComponentEditCompleteAction.Complete);
                        }

                        BeginSelectFeature(default(object));
                        IgnoreSelectionEvents = false;
                    }

                    current = component;
                    item.Layer.Selection.Add(item.Feature.GetFeatureId());
                    UpdateSelectionCommands(item.Project);
                }
            });
        }

        void SelectFeatures(FeatureEventArgs e)
        {
            if (SelectionComponent.HasValue())
            {
                OnFeatureChoose(e, (item) => SelectionComponent.AddRemoveFeature(item.Layer, item.Feature));
            }
        }

        void OnFeatureChoose(FeatureEventArgs e, Action<FeatureItem> onClick)
        {
            FeatureItem[] features = e.Features.Take(10).ToArray();

            if (features.Length > 1)
            {
                ContextMenuStrip popupMenu = e.Project.Designer.PopupMenu;

                popupMenu.Items.Clear();

                foreach (FeatureItem feature in features)
                {
                    ToolStripItem item = popupMenu.Items.Add(string.Concat(feature.Layer.Name, " - ", feature.Feature.GetFeatureId()), feature.Layer.GetIcon(16), (s2, e2) => onClick(feature));

                    item.MouseLeave += (s2, e2) =>
                    {
                        e.Project.Designer.Flush();
                    };

                    item.MouseEnter += (s2, e2) =>
                    {
                        e.Project.Designer.Flush();

                        using (Graphics g = feature.Project.Designer.CreateGraphics())
                        {
                            e.Project.Workspace.Canvas.Graphics.Draw(g, feature.Geometry, PaintStyle.Highlight, false);
                        }
                    };
                }

                popupMenu.Show(e.Project.Designer.WorldToScreen(e.Bounds.Max.X, e.Bounds.Min.Y));
            }
            else if (features.Length == 1)
            {
                onClick(features.First());
            }
        }

        protected override void OnPopupMenuClosed(object sender, EventArgs e)
        {
            if (ActiveProject.HasValue())
            {
                ActiveProject.Designer.Flush();
            }

            base.OnPopupMenuClosed(sender, e);
        }

        IApplicationComponent SelectFeature(FeatureItem item, IGeometry g)
        {
            IApplicationComponent component = g.GetComponent(item.Project.Designer);

            if (component.HasValue())
            {
                var designMode = item.Project.Designer.DesignMode;

                component.TypeDescriptor = item;
                component.EditCompleted += (e) =>
                {
                    try
                    {
                        var i = component.TypeDescriptor as FeatureItem;

                        if (e.Action == ComponentEditCompleteAction.Delete)
                        {
                            i.Feature.BeginEdit();
                            i.Feature.Delete();
                            i.Feature.EndEdit();
                        }
                        else if (e.Action == ComponentEditCompleteAction.Complete)
                        {
                            //i.Feature.BeginEdit();
                            i.Feature.SetGeometry(component.ValueOwner as IGeometry);
                            i.Feature.EndEdit();
                        }
                    }
                    catch (Exception error)
                    {
                        error.ShowMessage();
                    }
                    finally
                    {
                        EndSelectFeature();
                    }
                };

                item.Feature.BeginEdit();
                item.Project.Designer.BeginEdit(component);

                if (!designMode)
                {
                    item.Project.Render();
                }
            }

            return component;
        }

        void UpdateSelectionCommands(IProject project)
        {
            if (!IgnoreSelectionEvents)
            {
                RibbonPage pageSelection = Form.CommandManager.Pages[CommandSelectionPage];

                if (pageSelection.HasValue())
                {
                    if (project.Designer.SelectionMode)
                    {
                        UpdateMultiSelectionCommands(project, pageSelection);
                        pageSelection.Activate();
                    }
                    else if (pageSelection.IsActive)
                    {
                        pageSelection.Deactivate();
                        pageSelection.Caption.Hide();
                    }
                }
            }

            Form.CommandManager[CommandSelectFeature].Enabled =
            Form.CommandManager[CommandSelectFeatures].Enabled = !project.Designer.SelectionMode && !project.Designer.DesignMode;
        }

        void UpdateMultiSelectionCommands(IProject project, RibbonPage pageSelection)
        {
            bool isEmpty = project.Workspace.SelectionIsEmpty;
            bool isMultiple = SelectionComponent.HasValue();

            if (isMultiple)
            {
                Form.CommandManager[CommandSelectionSelectAll].Enabled = !SelectionComponent.IsSelectedAll();

                Form.CommandManager[CommandSelectionDeselectAll].Enabled =
                Form.CommandManager[CommandSelectionReverseSelection].Enabled =
                Form.CommandManager[CommandSelectionLinesToPolygon].Enabled = !isEmpty;

                Form.CommandManager[CommandSelectionSelectByRect].Enabled =
                Form.CommandManager[CommandSelectionSelectByArea].Enabled = isMultiple && project.Designer.ActiveComponent == SelectionComponent;

                using (var e = project.Workspace.GetSelectedFeatures().GetEnumerator())
                {
                    bool enabled = e.MoveNext() && e.MoveNext();

                    Form.CommandManager[CommandSelectionIntersection].Enabled = enabled;
                    Form.CommandManager[CommandSelectionUnion].Enabled = enabled;
                    Form.CommandManager[CommandSelectionDifference].Enabled = enabled;
                }
            }
            else
            {
                Form.CommandManager[CommandSelectionSelectAll].Enabled =
                Form.CommandManager[CommandSelectionDeselectAll].Enabled =
                Form.CommandManager[CommandSelectionReverseSelection].Enabled =
                Form.CommandManager[CommandSelectionSelectByRect].Enabled =
                Form.CommandManager[CommandSelectionSelectByArea].Enabled =
                Form.CommandManager[CommandSelectionIntersection].Enabled =
                Form.CommandManager[CommandSelectionUnion].Enabled =
                Form.CommandManager[CommandSelectionDifference].Enabled =
                Form.CommandManager[CommandSelectionLinesToPolygon].Enabled = false;
            }

            Form.CommandManager[CommandSelectionSplit].Enabled =
            Form.CommandManager[CommandSelectionDelete].Enabled = !isEmpty;

            Form.CommandManager[CommandSelectionCopy].Enabled =
            Form.CommandManager[CommandSelectionCancel].Enabled = true;
        }
    }
}
