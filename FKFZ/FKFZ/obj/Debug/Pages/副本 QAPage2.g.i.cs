﻿#pragma checksum "..\..\..\Pages\副本 QAPage2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4B9F401BF94DA20CD8F85AAAD5934BF9"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using FKFZ.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using System.Windows.Shell;


namespace FKFZ.Pages {
    
    
    /// <summary>
    /// QAPage2
    /// </summary>
    public partial class QAPage2 : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Pages\副本 QAPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement AudioPlayer;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\副本 QAPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl ListCtrl;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Pages\副本 QAPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridScore;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Pages\副本 QAPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBScroe;
        
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
            System.Uri resourceLocater = new System.Uri("/FKFZ;component/pages/%e5%89%af%e6%9c%ac%20qapage2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\副本 QAPage2.xaml"
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
            
            #line 9 "..\..\..\Pages\副本 QAPage2.xaml"
            ((FKFZ.Pages.QAPage2)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AudioPlayer = ((System.Windows.Controls.MediaElement)(target));
            
            #line 25 "..\..\..\Pages\副本 QAPage2.xaml"
            this.AudioPlayer.MediaEnded += new System.Windows.RoutedEventHandler(this.Element_MediaEnded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ListCtrl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 4:
            this.GridScore = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.TBScroe = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

