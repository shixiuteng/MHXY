﻿#pragma checksum "E:\wp\MHXY\MHXY\UI\Practice\PracticeResults.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EFCBCFCAF3312CC4A6A3E5DD982433E0"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34011
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MHXY.UI.Practice {
    
    
    public partial class PracticeResults : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.ListPicker toolkit_type;
        
        internal System.Windows.Controls.TextBox textbox_now_hurt;
        
        internal System.Windows.Controls.TextBox textbox_now_practice;
        
        internal System.Windows.Controls.StackPanel stackpanel_result;
        
        internal System.Windows.Controls.TextBlock textBlock_target_hurt;
        
        internal System.Windows.Controls.TextBlock textBlock_zk_hurt;
        
        internal System.Windows.Controls.TextBlock textBlock_bk_hurt;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MHXY;component/UI/Practice/PracticeResults.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.toolkit_type = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("toolkit_type")));
            this.textbox_now_hurt = ((System.Windows.Controls.TextBox)(this.FindName("textbox_now_hurt")));
            this.textbox_now_practice = ((System.Windows.Controls.TextBox)(this.FindName("textbox_now_practice")));
            this.stackpanel_result = ((System.Windows.Controls.StackPanel)(this.FindName("stackpanel_result")));
            this.textBlock_target_hurt = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock_target_hurt")));
            this.textBlock_zk_hurt = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock_zk_hurt")));
            this.textBlock_bk_hurt = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock_bk_hurt")));
        }
    }
}

