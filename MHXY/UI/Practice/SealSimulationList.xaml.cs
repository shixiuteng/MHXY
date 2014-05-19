using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MHXY.UI.Practice
{
    public partial class SealSimulationList : PhoneApplicationPage
    {
        public SealSimulationList()
        {
            InitializeComponent();
            // 将 listbox 控件的数据上下文设置为示例数据
            DataContext = App.ViewModel;
        }



        private void ApplicationBarIconButton_back_Click(object sender, EventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        // 为 ViewModel 项加载数据
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
    }




}