﻿#pragma checksum "..\..\..\Pages\MutiPage2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7AB07489E5CC573CAA2827C8685C638F"
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
    /// MutiPage2
    /// </summary>
    public partial class MutiPage2 : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\Pages\MutiPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgTop;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\MutiPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgBottom;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\MutiPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_Lrc;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\MutiPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement AudioPlayer;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Pages\MutiPage2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox RTB;
        
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
            System.Uri resourceLocater = new System.Uri("/FKFZ;component/pages/mutipage2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\MutiPage2.xaml"
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
            
            #line 9 "..\..\..\Pages\MutiPage2.xaml"
            ((FKFZ.Pages.MutiPage2)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded_1);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\Pages\MutiPage2.xaml"
            ((FKFZ.Pages.MutiPage2)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Page_Unloaded_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ImgTop = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.ImgBottom = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.Grid_Lrc = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.AudioPlayer = ((System.Windows.Controls.MediaElement)(target));
            
            #line 38 "..\..\..\Pages\MutiPage2.xaml"
            this.AudioPlayer.MediaFailed += new System.EventHandler<System.Windows.ExceptionRoutedEventArgs>(this.Element_MediaFailed);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\Pages\MutiPage2.xaml"
            this.AudioPlayer.MediaOpened += new System.Windows.RoutedEventHandler(this.Element_MediaOpened);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\Pages\MutiPage2.xaml"
            this.AudioPlayer.MediaEnded += new System.Windows.RoutedEventHandler(this.Element_MediaEnded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RTB = ((System.Windows.Controls.RichTextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
