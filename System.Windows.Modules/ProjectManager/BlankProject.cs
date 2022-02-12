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
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Geometries;
using System.IO;
using System.Windows.Forms;
using System.Workspace;
using System.Xml;

namespace System.Windows.Modules.Controls
{
    public abstract class BlankProject : XmlDocumentBase, IProject
    {
        public BlankProject(IApplication application)
        {
            Application = application;
            FileInfo = new ProjectFileInfo(this);
            Properties = new PropertyNodeCollection();
        }

        protected IApplicationComponentDesigner Design;
        protected MapWorkspace Mws;
        protected IUnitConverter Converter;
        protected MouseEventArgs MouseArgs;
        protected FeatureEventHandler FeatureSelectAction;

        public event Action Cancel;
        public event ProjectEventHandler Saved;
        public event ProjectEventHandler Loaded;
        public event ProjectEventHandler Changed;
        public event ProjectEventHandler Disposed;
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event EventHandler CursorEnter;
        public event MapEventHandler CursorButtonUp;
        public event MapEventHandler CursorButtonDown;
        public event MapEventHandler CursorLocationChanged;
        public event MapEventHandler ScaleChanged;

        public event FeatureEventHandler FeatureSelect
        {
            add { BeginFeatureSelect(value); }
            remove { EndFeatureSelect(value); }
        }

        public event ApplicationComponentEventHandler ActiveComponentChanged;

        public new string Name
        {
            get { return Workspace.Name; }
            set
            {
                if (Workspace.Name.EqualsIgnoreCase(value))
                {
                    return;
                }

                Workspace.Name = value;
                Changed.InvokeSafely(new ProjectEventArgs(this));
            }
        }

        public object Component
        {
            get { return this; }
        }

        public IProjectFileInfo FileInfo
        {
            get;
            protected set;
        }

        public IPropertyCollection Properties
        {
            get;
            protected set;
        }

        public MapWorkspace Workspace
        {
            get { return Mws ?? (Mws = (MapWorkspace)GetOrCreate(Constants.Xml.Document)); }
        }

        public IPropertyGrid View
        {
            get;
            protected set;
        }

        public IApplication Application
        {
            get;
            protected set;
        }

        public ISnapObjectCollection SnapObjects
        {
            get;
            protected set;
        }

        public ITransactionLog TransactionLog
        {
            get { return Workspace.TransactionLog; }
        }

        public IUnitConverter UnitConverter
        {
            get
            {
                if (Converter.IsNull())
                {
                    var linearUnit = Properties["LinearUnit"].GetValue<LinearUnits>();
                    var linearDisplayUnit = Properties["LinearDisplayUnit"].GetValue<LinearUnits>();
                    var angularDisplayUnit = Properties["AngularDisplayUnit"].GetValue<AngularUnits>();

                    Converter = new UnitConverter(linearUnit, linearDisplayUnit, AngularUnits.Radian, angularDisplayUnit);
                }

                return Converter;
            }
        }

        public IApplicationComponentDesigner Designer
        {
            get
            {
                if (Design == null)
                {
                    Design = new MapControl();
                    Design.Cancel += OnCancel;
                    Design.KeyDown += OnKeyDown;
                    Design.KeyUp += OnKeyUp;
                    Design.MouseEnter += OnMouseEnter;
                    Design.MouseUp += OnMouseUp;
                    Design.MouseDown += OnMouseDown;
                    Design.MouseMove += OnCursorLocationChanged;
                    Design.MouseWheel += OnCursorLocationChanged;
                    Design.MouseLeave += (sender, e) => OnCursorLocationChanged(sender, null);

                    Design.Init(this);
                    Design.Transform.ScaleChanged += OnScaleChanged;
                    Design.ActiveComponentChanged += OnActiveComponentChanged;

                    SnapObjects = new SnapObjectCollection(this);
                }

                return Design;
            }
        }

        public IApplicationFile File
        {
            get;
            set;
        }

        public bool IsActive
        {
            get { return Application.Form.DocumentManager.Find(this).IsActive; }
        }

        public bool IsModified
        {
            get;
            protected set;
        }

        public void Activate()
        {
            Application.Form.DocumentManager.Activate(this);
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            Mws = default;
            View = new ProjectProperties(this);

            if (TransactionLog.HasValue())
            {
                TransactionLog.Changed += OnTransactionLogChanged;
            }

            Workspace.BeginInit();
            Application.Form.DocumentManager.AddDocument((Control)Designer);
            Workspace.EndInit();
            
            View.SelectedObject = Workspace.Properties;
        }

        public new bool Normalize()
        {
            return Workspace.Normalize();
        }

        public bool ZoomToExtent()
        {
            return Workspace.ZoomToExtent();
        }

        public bool Zoom(int percent)
        {
            return Workspace.Zoom(percent);
        }

        public bool ZoomTo(IGeometry g)
        {
            return Workspace.ZoomTo(g.GetBounds());
        }

        public bool ZoomTo(RectangleF rect)
        {
            return Workspace.ZoomTo(rect);
        }

        public bool ZoomTo(IEnvelope bounds)
        {
            return Workspace.ZoomTo(bounds);
        }

        public void Render()
        {
            Render(Designer.Mouse);
        }

        protected virtual void Render(MouseEventArgs e)
        {
            if (Workspace.Canvas.Graphics.Transform.IsEmpty())
            {
                if (Workspace.CurrentWindow.HasValue() && Workspace.CurrentWindow.IsEmpty() == false)
                {
                    ZoomTo(Workspace.CurrentWindow);
                }
                else
                {
                    ZoomToExtent();
                }
            }

            Workspace.Render();
        }

        void BeginFeatureSelect(FeatureEventHandler value)
        {
            FeatureSelectAction += value;
            Designer.SelectPoint += SelectFeatureAt;
        }

        void EndFeatureSelect(FeatureEventHandler value)
        {
            FeatureSelectAction -= value;
            Designer.SelectPoint -= SelectFeatureAt;
        }

        void SelectFeatureAt(Point location)
        {
            if (Designer.Keyboard.Control || Designer.IsBusy)
            {
                return;
            }

            IEnvelope bounds = MapEventArgs.GetCursorBounds(Workspace.Canvas.Graphics.Transform, location, 5);

            if (bounds.HasValue())
            {
                FeatureSelectAction.InvokeSafely<FeatureEventArgs>(this, GetFeatures(bounds), bounds);
            }
        }

        IEnumerable<FeatureItem> GetFeatures(IEnvelope bounds)
        {
            foreach (MapLayer layer in Workspace.GetLayers())
            {
                if (layer.Visibility)
                {
                    foreach (IFeature feature in layer.GetFeatures(bounds))
                    {
                        IGeometry g = feature.GetGeometry();

                        if (g.HasValue() && g.IsIntersects(bounds))
                        {
                            yield return new FeatureItem(this, layer, feature);
                        }
                    }
                }
            }
        }

        public bool Load()
        {
            if (OnLoad())
            {
                Loaded.InvokeSafely<ProjectEventArgs>(this, File.FileName);
                return true;
            }

            return false;
        }

        public bool Save()
        {
            if (File.HasValue() && File.CanSave)
            {
                if (OnSave())
                {
                    File.Write(this);
                    return true;
                }

                return false;
            }

            return SaveAs();
        }

        public bool SaveAs()
        {
            using (var dialog = new FileSaveDialog())
            {
                foreach (IApplicationFile file in Application.Files)
                {
                    if (file.CanWrite)
                    {
                        foreach (string e in file.Extensions)
                        {
                            dialog.AddFilter(string.Concat(file.Name, " ", e.Substring(1)), e);
                        }
                    }
                }

                if (File.HasValue())
                {
                    dialog.FileName = Path.GetFileNameWithoutExtension(File.FileName);
                }
                else
                {
                    dialog.FileName = Name;
                }

                if (dialog.ShowDialog(Application.Form) == DialogResult.OK)
                {
                    var selectedFile = Application.Files.Find(dialog.FileName);

                    if (selectedFile.HasValue())
                    {
                        File = File ?? selectedFile;

                        if (File.CanRead)
                        {
                            Name = Path.GetFileNameWithoutExtension(dialog.FileName);
                        }

                        selectedFile.FileName = dialog.FileName;
                        selectedFile.Write(this);

                        if (selectedFile.CanRead)
                        {
                            File = selectedFile;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        protected abstract bool OnLoad();

        protected abstract bool OnSave();

        protected void OnCancel()
        {
            Cancel.InvokeSafely();
        }

        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!Application.Form.CommandManager.PerformCommand(e.KeyCode))
            {
                KeyDown.InvokeSafely(sender, e);
            }
        }

        protected void OnKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp.InvokeSafely(sender, e);
        }

        protected void OnMouseEnter(object sender, EventArgs e)
        {
            CursorEnter.InvokeSafely(sender, e);
        }

        protected void OnMouseUp(object sender, MouseEventArgs e)
        {
            CursorButtonUp.InvokeSafely((MapEventArgs)Designer.Map);
        }

        protected void OnMouseDown(object sender, MouseEventArgs e)
        {
            CursorButtonDown.InvokeSafely((MapEventArgs)Designer.Map);
        }

        protected void OnCursorLocationChanged(object sender, MouseEventArgs e)
        {
            if (e.IsNull() || MouseArgs.IsNull() || e.Location != MouseArgs.Location)
            {
                MouseArgs = e;
                CursorLocationChanged.InvokeSafely((MapEventArgs)Designer.Map);
            }
        }

        protected void OnScaleChanged()
        {
            ScaleChanged.InvokeSafely((MapEventArgs)Designer.Map);
        }

        protected void OnActiveComponentChanged(ApplicationComponentEventArgs e)
        {
            ActiveComponentChanged.InvokeSafely(e);
        }

        protected void OnTransactionLogChanged()
        {
            SetModified();
            Changed.InvokeSafely<ProjectEventArgs>(this);
        }

        public void SetModified()
        {
            IsModified = true;
            Changed.InvokeSafely<ProjectEventArgs>(this);
        }

        public bool Close()
        {
            return Application.Projects.Close(this);
        }

        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            switch (localName)
            {
                case Constants.Xml.Document:
                    return Application.Files[prefix].CreateWorkspace(this);

                case Constants.Xml.Folder:
                    return Application.Files[prefix].CreateLayer(this);
            }

            return base.CreateElement(prefix, localName, NamespaceURI);
        }

        public void Dispose()
        {
            Cancel = null;
            Saved = null;
            Loaded = null;
            Changed = null;
            Disposed = null;
            KeyDown = null;
            KeyUp = null;
            CursorEnter = null;
            CursorButtonUp = null;
            CursorButtonDown = null;
            CursorLocationChanged = null;
            ActiveComponentChanged = null;
            FeatureSelectAction = null;

            View.DisposeSafely();
            Designer.DisposeSafely();
            FileInfo.DisposeSafely();
            Properties.DisposeSafely();
            SnapObjects.DisposeSafely();
            TransactionLog.DisposeSafely();
            Workspace.DisposeSafely();

            View = null;
            Converter = null;
            FileInfo = null;
            Properties = null;
            SnapObjects = null;
            Application = null;

            Disposed.InvokeSafely<ProjectEventArgs>(this);
            Disposed = null;

            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}