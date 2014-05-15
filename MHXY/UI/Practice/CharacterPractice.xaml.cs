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
    public partial class CharacterPractice : PhoneApplicationPage
    {
        /// <summary>
        /// 25length
        /// </summary>
        private long[] xlexpchecks = new long[] { 150, 210, 290, 390, 510, 650, 810, 990, 1190, 1410, 1650, 1910, 2190, 2490, 2810, 3150, 3510, 3890, 4290, 4710, 5150, 5610, 6090, 6590, 7110};
        /// <summary>
        /// 26length
        /// </summary>
        private long[] xlrwlvs = new long[] { 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145 };
        private long[] xlbgs = new long[] { 0, 150, 300, 450, 600, 750, 900, 1050, 1200, 1350, 1500, 1650, 1800, 1950, 2100, 2250, 2400, 2550, 2700, 2850, 3000, 3150, 3300, 3450, 3600, 3750};

     
        


        public CharacterPractice()
        {
            InitializeComponent();
          
        }

      
        private void textbox_now_grade_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            } 
            base.OnKeyDown(e);
        }

        private void textbox_now_empirical_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            } 
            base.OnKeyDown(e);
        }

        private void textbox_target_grade_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            } 
            base.OnKeyDown(e);
        }

        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_calculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
           
        }

        private void textbox_now_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(textbox_now_grade.Text) > Config.HIGH_PracticeLevel)
            {
                Tool.Coding4FunForMsg("修炼等级不得超过" + Config.HIGH_PracticeLevel + "级", "", 1000);
                textbox_now_grade.Focus();
            }

        }

        private void textbox_now_empirical_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(textbox_now_empirical.Text) % 10 != 0)
            {
                Tool.Coding4FunForMsg("当前修炼经验只能是10的倍数", "", 1000);
                textbox_now_empirical.Focus();
            }
        }

        private void textbox_target_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            

            if (Convert.ToInt16(textbox_target_grade.Text) > Config.HIGH_PracticeLevel)
            {
                Tool.Coding4FunForMsg("修炼等级不得超过" + Config.HIGH_PracticeLevel + "级", "", 1000);
                textbox_target_grade.Focus();
            }


        }

        private void button_empty_Click(object sender, RoutedEventArgs e)
        {
            Empty();
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
        public void Empty() {

            textBlock_character_grade.Text = "";
            textBlock_sum_tribute.Text = "";
            textBlock_sum_empirical.Text = "";
            textBlock_needed_money.Text = "";
            textBlock_riches.Text = "";
            stackpanel_result.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 计算
        /// </summary>
        public void Calculate() {

            if (textbox_now_grade.Text.Equals(""))
            {
                textbox_now_grade.Text = "0";
            }

            if (textbox_now_empirical.Text.Equals(""))
            {
                textbox_now_empirical.Text = "0";
            }

            if (textbox_target_grade.Text.Equals(""))
            {
                textbox_target_grade.Text = "0";
            }


            if (Convert.ToInt16(textbox_now_grade.Text) > Config.HIGH_PracticeLevel)
            {
                Tool.Coding4FunForMsg("修炼等级不得超过" + Config.HIGH_PracticeLevel + "级", "", 1000);
                textbox_now_grade.Focus();
            }


            if (Convert.ToInt16(textbox_now_empirical.Text) % 10 != 0)
            {
                Tool.Coding4FunForMsg("当前修炼经验只能是10的倍数", "", 1000);
            }


            if (Convert.ToInt16(textbox_target_grade.Text) > Config.HIGH_PracticeLevel)
            {
                Tool.Coding4FunForMsg("修炼等级不得超过" + Config.HIGH_PracticeLevel + "级", "", 1000);
                textbox_target_grade.Focus();
            } 
            textBlock_character_grade.Text = xlrwlvs[Convert.ToInt16(textbox_target_grade.Text)].ToString()+"级";
            textBlock_sum_tribute.Text = xlbgs[Convert.ToInt16(textbox_target_grade.Text)].ToString();
            long sumExp = 0;
            for (int i = Convert.ToInt16(textbox_now_grade.Text); i < Convert.ToInt16(textbox_target_grade.Text); i++)
            {
                sumExp = sumExp + xlexpchecks[i]; 
            }
             
            textBlock_sum_empirical.Text = sumExp + "";
            textBlock_needed_money.Text = (sumExp * 0.2).ToString()+"万";
            textBlock_riches.Text = (sumExp * 0.5).ToString();
            stackpanel_result.Visibility = Visibility.Visible; 
        }

    }
}