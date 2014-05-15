using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MHXY
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 将 listbox 控件的数据上下文设置为示例数据
            DataContext = App.ViewModel;
        }

        // 为 ViewModel 项加载数据
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
        private void Border_Tap_EmpiricalRight(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/UI/Character/EmpiricalRight.xaml", UriKind.Relative));
        }

        private void Border_Tap_EmpiricalLeft(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/UI/Character/EmpiricalLeft.xaml", UriKind.Relative));
        }


        private void Border_Tap_GradeInfo(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/UI/Character/GradeInfo.xaml", UriKind.Relative));
        }

        private void Border_Tap_Judging(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/UI/Character/Judging.xaml", UriKind.Relative));
        }


        private void Border_Tap_TeacherSkill(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/UI/Skill/TeacherSkill.xaml", UriKind.Relative));
        }
        private void Border_Tap_SecondarySkills(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/UI/Skill/SecondarySkills.xaml", UriKind.Relative));
        }

        private void Border_Tap_CharacterPractice(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/UI/Practice/CharacterPractice.xaml", UriKind.Relative));
        }

    }
}