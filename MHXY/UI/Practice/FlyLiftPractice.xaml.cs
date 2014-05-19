using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MHXY.Hepler;

namespace MHXY.UI.Practice
{
    public partial class FlyLiftPractice : PhoneApplicationPage
    {


        /// <summary>
        /// 25length
        /// </summary>
        private long[] xlexpchecks = new long[] { 150, 210, 290, 390, 510, 650, 810, 990, 1190, 1410, 1650, 1910, 2190, 2490, 2810, 3150, 3510, 3890, 4290, 4710, 5150, 5610, 6090, 6590, 7110 };
       



        public FlyLiftPractice()
        {
            InitializeComponent();
             
        }


         


        private void ApplicationBarIconButton_back_Click(object sender, EventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }



        private void ApplicationBarIconButton_calculate_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void ApplicationBarIconButton_empty_Click(object sender, EventArgs e)
        {
            Empty();
        }




        /// <summary>
        /// 清空
        /// </summary>
        public void Empty()
        {
             
            stackpanel_result.Visibility = Visibility.Collapsed;
        }



        /// <summary>
        /// 计算
        /// </summary>
        public void Calculate()
        {


            if (checkbox_isfly.IsChecked==false)
            { 
                //未飞升 
                long sumExp = 0;
                for (int i = Convert.ToInt16(textbox_now_grade.Text); i < 10; i++)
                {
                    sumExp = sumExp + xlexpchecks[i];
                }


            }
            else { 
                ///已飞升
                
                
            
            }

            List<String> list = new List<String>();
            for (int i = 0; i < 5; i++)
            {
                list.Add("当前等级从 0→23  (需金钱16215万)"); 
            }
            stackpanel_result.Visibility = Visibility.Visible;
            longlistselector_result.ItemsSource = list;
 

        }

        private void textbox_now_grade_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textbox_now_grade); 
        }

        private void textbox_now_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textbox_now_grade); 
        }

        private void textbox_now_grade_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {

                if (!e.Key.ToString().ToLower().Equals("back") && !textbox_now_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textbox_now_grade.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_PracticeLevel)
                    {
                        Tool.Coding4FunForMsg("修炼等级不得超过" + Config.HIGH_PracticeLevel + "级", "", 1000);

                        e.Handled = true;
                    }
                }
            }
            base.OnKeyDown(e);
        }
    }
}