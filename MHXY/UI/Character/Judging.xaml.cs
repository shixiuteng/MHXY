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
namespace MHXY.UI.Character
{
    public partial class Judging : PhoneApplicationPage
    {
        public Judging()
        {
            InitializeComponent();
        }




        private void button_calculate_Click_1(object sender, RoutedEventArgs e)
        {

            if (textBox_release_grade.Text.Equals(""))
            {
                textBox_release_grade.Text = "0";
            }

            if (textBox_catch_grade.Text.Equals(""))
            {
                textBox_catch_grade.Text = "0";
            }

            int release = Convert.ToInt16(textBox_release_grade.Text); 
            int catch_ = Convert.ToInt16(textBox_catch_grade.Text);
            if (catch_ > release) { 
                Tool.Coding4FunForMsg("“放生召唤兽等级”不得小于“捕捉时召唤兽等级”", "", 1000); 
                return;
            } 
            if (release - 19 - catch_ <= 0)
            {
                textBox_judging.Text = "0"; 
            }
            else {
                textBox_judging.Text = (release - 19 - catch_).ToString(); 
            }
            


        }

        private void button_empty_Click_1(object sender, RoutedEventArgs e)
        {
            textBox_release_grade.Text = "0";
            textBox_catch_grade.Text = "0";
            textBox_judging.Text = ""; 


        }

        private void textBox_release_grade_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textBox_release_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textBox_release_grade.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_SummonLevel)
                    {
                        Tool.Coding4FunForMsg("召唤兽等级不得超过" + Config.HIGH_SummonLevel + "级", "", 1000); 
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }

        private void textBox_catch_grade_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textBox_catch_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textBox_catch_grade.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_SummonLevel)
                    {
                        Tool.Coding4FunForMsg("召唤兽等级不得超过" + Config.HIGH_SummonLevel + "级", "", 1000); 
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }

        private void textBox_release_grade_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textBox_release_grade);
        }

        private void textBox_release_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textBox_release_grade);
        }

        private void textBox_catch_grade_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textBox_catch_grade);
        }

        private void textBox_catch_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textBox_catch_grade);
        }
    }
}