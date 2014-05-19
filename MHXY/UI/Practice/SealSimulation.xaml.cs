using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MHXY.ViewModels;
using System.Threading.Tasks;
using MHXY.Hepler;


namespace MHXY.UI.Practice
{
    public partial class SealSimulation : PhoneApplicationPage
    {
        /// <summary>
        /// 随机数
        /// </summary>
        private Random rand = new Random();

        public SealSimulation()
        {
            InitializeComponent();

           
        }

        private void textbox_my_grade_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textbox_my_grade);
        }

        private void textbox_my_practice_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textbox_my_practice);
        }

        private void textbox_object_grade_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textbox_object_grade);
        }

        private void textbox_object_practice_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textbox_object_practice);
        }

        private void textbox_my_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textbox_my_grade);
        }

        private void textbox_my_practice_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textbox_my_practice);
        }

        private void textbox_object_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textbox_object_grade);
        }

        private void textbox_object_practice_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textbox_object_practice);
        }

        private void textbox_my_grade_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textbox_my_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textbox_my_grade.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_CharacteSkillsLevel+10)
                    {
                        Tool.Coding4FunForMsg("自身技能等级不得超过" + (Config.HIGH_CharacteSkillsLevel+10), "", 1000);

                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }

        private void textbox_my_practice_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textbox_my_practice.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textbox_my_practice.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_PracticeLevel)
                    {
                        Tool.Coding4FunForMsg("自身法术修炼不得超过" + Config.HIGH_PracticeLevel, "", 1000);
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);

        }

        private void textbox_object_grade_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textbox_object_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textbox_object_grade.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_CharacteSkillsLevel)
                    {
                        Tool.Coding4FunForMsg("对象人物等级不得超过" + Config.HIGH_CharacteSkillsLevel, "", 1000);
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }

        private void textbox_object_practice_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textbox_object_practice.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textbox_object_practice.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_PracticeLevel)
                    {
                        Tool.Coding4FunForMsg("对象法抗等级不得超过" + Config.HIGH_PracticeLevel, "", 1000);
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        } 

        private void ApplicationBarIconButton_calculate_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void ApplicationBarIconButton_empty_Click(object sender, EventArgs e)
        {
            Empty();
        }

        private void ApplicationBarIconButton_back_Click(object sender, EventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
         
        /// <summary>
        /// 清空
        /// </summary>
        public void Empty()
        {
            this.Focus();
            stackpanel_result.Visibility=Visibility.Collapsed;
            textbox_my_grade.Text = "0";
            textbox_my_practice.Text = "0";
            textbox_object_grade.Text = "0";
            textbox_object_practice.Text = "0";


            textBlock_no_practice.Text ="";
            textBlock_have_practice.Text = "";

            textbox_success.Text ="";
            textbox_failure.Text = "";
        }

        /// <summary>
        /// 计算
        /// </summary>
        public void Calculate()
        {
            this.Focus();
            int myGrade = Convert.ToInt16(textbox_my_grade.Text);
            int myPractice = Convert.ToInt16(textbox_my_practice.Text);
            int oGrade = Convert.ToInt16(textbox_object_grade.Text);
            int oPractice = Convert.ToInt16(textbox_object_practice.Text);
            
            int noPractice = 50;
            int havePractice = 50;
            if (oGrade > myGrade)
            { 
                if (oGrade - myGrade >= 23)
                {
                    noPractice = 5;
                }
                else
                {
                    noPractice = noPractice - (oGrade - myGrade) * 2;
                }
            }
            else {

                if (myGrade - oGrade >= 15)
                {
                    noPractice = 80;
                }
                else
                {
                    noPractice = noPractice + (myGrade - oGrade) * 2;
                }
            
            }


            if (oPractice + oGrade > myPractice + myGrade)
            {
                if (oPractice + oGrade - myPractice - myGrade >= 23)
                {
                    havePractice = 5;
                }
                else
                {
                    havePractice = havePractice - (oPractice + oGrade - myPractice - myGrade) * 2;

                }
            
            }
            else
            {

                if (myPractice + myGrade - oPractice - oGrade >= 15)
                {
                    havePractice = 80;
                }
                else
                { 
                    havePractice = havePractice + (myPractice + myGrade - oPractice - oGrade) * 2;
                }

            }
            // rand.Next(100).ToString() ;
            textBlock_no_practice.Text = noPractice + "%";
            textBlock_have_practice.Text = havePractice + "%";
            int successnum = 0;

            List<String> list1 = new List<String>();
            List<String> list2 = new List<String>();
            List<String> list3 = new List<String>();
            List<String> list4 = new List<String>();
            String str = "";
            for (int i = 0; i < 100; i++) { 
                if (rand.Next(100) <= havePractice)
                {
                    successnum = successnum + 1;
                    str = ""+(i + 1) + ".成功"; 
                }
                else { 
                    str = (i + 1) + ".失败";
                }
                if (i < 25)
                {
                    list1.Add(str);
                }
                else if (i >= 25 && i < 50)
                {
                    list2.Add(str);
                }
                else if (i >= 50 && i < 75)
                {
                    list3.Add(str);
                }
                else if (i >= 75)
                {
                    list4.Add(str);
                }


            }
            stackpanel_result.Visibility = Visibility.Visible;
            longlistselector_result1.ItemsSource = list1;
            longlistselector_result2.ItemsSource = list2;
            longlistselector_result3.ItemsSource = list3;
            longlistselector_result4.ItemsSource = list4;

          

            textbox_success.Text = successnum.ToString();
            textbox_failure.Text = 100 - successnum + "";
        }
         
       
    }
     
}