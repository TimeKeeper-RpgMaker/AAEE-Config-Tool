﻿#pragma checksum "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "56855C1EFF9C140EC76A592965CB539E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34011
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace AAEE.Windows {
    
    
    /// <summary>
    /// win_AF_ChangeMaximum
    /// </summary>
    public partial class win_AF_ChangeMaximum : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_EquipName;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Up;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Down;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_OK;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Cancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AAEE;component/windows/win_af_changemaximum%20-%20copier%20(3).xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txt_EquipName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btn_Up = ((System.Windows.Controls.Button)(target));
            
            #line 7 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
            this.btn_Up.Click += new System.Windows.RoutedEventHandler(this.btn_Up_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_Down = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
            this.btn_Down.Click += new System.Windows.RoutedEventHandler(this.btn_Down_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_OK = ((System.Windows.Controls.Button)(target));
            
            #line 9 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
            this.btn_OK.Click += new System.Windows.RoutedEventHandler(this.btn_OK_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\Windows\win_AF_ChangeMaximum - Copier (3).xaml"
            this.btn_Cancel.Click += new System.Windows.RoutedEventHandler(this.btn_Cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

