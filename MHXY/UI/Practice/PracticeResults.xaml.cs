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
    public partial class PracticeResults : PhoneApplicationPage
    {
        public PracticeResults()
        {
            InitializeComponent();
        }

        private void textbox_now_practice_GotFocus(object sender, RoutedEventArgs e)
        {

            Tool.textbox_GotFocus(textbox_now_practice);
        }

        private void textbox_now_hurt_GotFocus(object sender, RoutedEventArgs e)
        {

            Tool.textbox_GotFocus(textbox_now_hurt);
        }

        private void textbox_now_hurt_LostFocus(object sender, RoutedEventArgs e)
        {

            Tool.textbox_LostFocus(textbox_now_hurt);
        }

        private void textbox_now_practice_LostFocus(object sender, RoutedEventArgs e)
        { 
            Tool.textbox_LostFocus(textbox_now_practice); 
        }

        private void textbox_now_hurt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void textbox_now_practice_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {

                if (!e.Key.ToString().ToLower().Equals("back") && !textbox_now_practice.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textbox_now_practice.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_PracticeLevel)
                    {
                        Tool.Coding4FunForMsg("当前修炼等级不得超过" + Config.HIGH_PracticeLevel + "级", "", 1000);

                        e.Handled = true;
                    }
                }
            }
            base.OnKeyDown(e);
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

            this.Focus();
            textbox_now_hurt.Text = "0"; 
            textbox_now_practice.Text = "0";
             

            textBlock_zk_hurt.Text = "";
            textBlock_bk_hurt.Text = "";
            textBlock_target_hurt.Text = ""; 
            stackpanel_result.Visibility = Visibility.Collapsed;
        }


        
        /// <summary>
        /// 计算
        /// </summary>
        public void Calculate() {

           
            if (Convert.ToInt16(textbox_now_practice.Text) > Config.HIGH_PracticeLevel)
            {
                Tool.Coding4FunForMsg("当前修炼等级不得超过" + Config.HIGH_PracticeLevel + "级", "", 1000);
              //  textbox_now_practice.Focus();
                return;
            }

            this.Focus();
            int hurt = Convert.ToInt16(textbox_now_hurt.Text);

           

            int practice = Convert.ToInt16(textbox_now_practice.Text);

            stackpanel_result.Visibility = Visibility.Visible;
            double lHurt = hurt;
            if (hurt != 0)
            {
                if (toolkit_type.SelectedIndex == 0)
                {
                    for (int i = 1; i <= practice; i++)
                    {
                        lHurt = lHurt + (lHurt * 0.02 + 5);
                    }
                }
                else
                {
                    for (int i = 1; i <= practice; i++)
                    {
                        lHurt = lHurt - (lHurt * 0.02 + 5);
                    }
                } 
            }
            if (lHurt >= 0)
            { 
                textBlock_target_hurt.Text = Convert.ToInt16(lHurt) + "";
                textBlock_zk_hurt.Text = Convert.ToInt16(lHurt + (lHurt * 0.2)) + " ";
                textBlock_bk_hurt.Text = Convert.ToInt16(lHurt - (lHurt * 0.2)) + "";
            }
            else { 
                textBlock_target_hurt.Text ="1";
                textBlock_zk_hurt.Text = "1";
                textBlock_bk_hurt.Text = "1";
            }
        
        }



  
    }
}