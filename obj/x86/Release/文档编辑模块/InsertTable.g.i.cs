﻿#pragma checksum "..\..\..\..\文档编辑模块\InsertTable.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "62280ABE18818B5A816299877A2384E8"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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
using System.Windows.Forms;
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


namespace MMAWPF.文档编辑模块 {
    
    
    /// <summary>
    /// InsertTable
    /// </summary>
    public partial class InsertTable : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\文档编辑模块\InsertTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label3;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\文档编辑模块\InsertTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tableTitleTxt;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\文档编辑模块\InsertTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OKBtn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\文档编辑模块\InsertTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label5;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\文档编辑模块\InsertTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label4;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\文档编辑模块\InsertTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Forms.NumericUpDown colNum;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\文档编辑模块\InsertTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Forms.NumericUpDown rowNum;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\文档编辑模块\InsertTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/MMAWPF;component/%e6%96%87%e6%a1%a3%e7%bc%96%e8%be%91%e6%a8%a1%e5%9d%97/insertta" +
                    "ble.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\文档编辑模块\InsertTable.xaml"
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
            
            #line 6 "..\..\..\..\文档编辑模块\InsertTable.xaml"
            ((MMAWPF.文档编辑模块.InsertTable)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 6 "..\..\..\..\文档编辑模块\InsertTable.xaml"
            ((MMAWPF.文档编辑模块.InsertTable)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.label3 = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.tableTitleTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.OKBtn = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\文档编辑模块\InsertTable.xaml"
            this.OKBtn.Click += new System.Windows.RoutedEventHandler(this.OKBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.label5 = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.label4 = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.colNum = ((System.Windows.Forms.NumericUpDown)(target));
            return;
            case 8:
            this.rowNum = ((System.Windows.Forms.NumericUpDown)(target));
            return;
            case 9:
            this.CancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\文档编辑模块\InsertTable.xaml"
            this.CancelBtn.Click += new System.Windows.RoutedEventHandler(this.CancelBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

