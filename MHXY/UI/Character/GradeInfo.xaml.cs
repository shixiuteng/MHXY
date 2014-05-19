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
    public partial class GradeInfo : PhoneApplicationPage
    {
        /// <summary>
        /// 人物技能数据
        /// </summary>
        private int[] needskills = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 7, 8, 9, 10, 10, 11, 12, 13, 14, 14, 15, 16, 17, 18, 18, 19, 20, 21, 22, 22, 23, 24, 25, 26, 26, 27, 28, 29, 30, 30, 31, 32, 33, 34, 34, 35, 36, 37, 38, 38, 39, 40, 41, 42, 42, 43, 44, 45, 46, 46, 47, 48, 49, 50, 50, 51, 52, 53, 54, 54, 55, 56, 57, 58, 58, 59, 60, 61, 62, 62, 63, 64, 65, 66, 66, 67, 68, 69, 70, 70, 71, 72, 73, 74, 74, 75, 76, 77, 78, 78, 79, 80, 81, 82, 82, 83, 84, 85, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86, 86 };

        /// <summary>
        /// 人物携带金钱上限
        /// </summary>
        private long[] needmoneys = new long[] { 10000, 12000, 18000, 28000, 42000, 60000, 82000, 108000, 138000, 172000, 210000, 252000, 298000, 348000, 402000, 460000, 522000, 588000, 658000, 732000, 810000, 892000, 978000, 1068000, 1162000, 1260000, 1362000, 1468000, 1578000, 1692000, 1810000, 1932000, 2058000, 2188000, 2322000, 2460000, 2602000, 2748000, 2898000, 3052000, 3210000, 3372000, 3538000, 3708000, 3882000, 4060000, 4242000, 4428000, 4618000, 4812000, 5010000, 5212000, 5418000, 5628000, 5842000, 6060000, 6282000, 6508000, 6738000, 6972000, 7210000, 7452000, 7698000, 7948000, 8202000, 8460000, 8722000, 8988000, 9258000, 9532000, 9810000, 10092000, 10378000, 10668000, 10962000, 11260000, 11562000, 11868000, 12178000, 12492000, 12810000, 13132000, 13458000, 13788000, 14122000, 14460000, 14802000, 15148000, 15498000, 15852000, 16210000, 16572000, 16938000, 17308000, 17682000, 18060000, 18442000, 18828000, 19218000, 19612000, 20010000, 20412000, 20818000, 21228000, 21642000, 22060000, 22482000, 22908000, 23338000, 23772000, 24210000, 24652000, 25098000, 25548000, 26002000, 26460000, 26922000, 27388000, 27858000, 28332000, 28810000, 29292000, 29778000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000, 30000000 };

        /// <summary>
        /// 在家训宠等级
        /// </summary>
        private int[] needpets = new int[] { -30, -29, -28, -27, -27, -26, -25, -25, -24, -23, -23, -22, -21, -20, -20, -19, -18, -18, -17, -16, -16, -15, -14, -13, -13, -12, -11, -11, -10, -9, -9, -8, -7, -6, -6, -5, -4, -4, -3, -2, -2, -1, 0, 1, 1, 2, 3, 3, 4, 5, 5, 6, 7, 8, 8, 9, 10, 10, 11, 12, 12, 13, 14, 15, 15, 16, 17, 17, 18, 19, 19, 20, 21, 22, 22, 23, 24, 24, 25, 26, 26, 27, 28, 29, 29, 30, 31, 31, 32, 33, 33, 34, 35, 36, 36, 37, 38, 38, 39, 40, 40, 41, 42, 43, 43, 44, 45, 45, 46, 47, 47, 48, 49, 50, 50, 51, 52, 52, 53, 54, 54, 55, 56, 57, 57, 58, 59, 59, 60, 61, 61, 62, 63, 64, 64, 65, 66, 66, 67, 68, 68, 69, 70, 71, 71, 72, 73, 73, 74, 75, 75, 76, 77, 78, 78, 79, 80, 80, 81, 82, 82, 83, 84, 85, 85, 86, 87, 87, 88, 89, 89, 90, 91, 92, 92, 93 };




        public GradeInfo()
        {
            InitializeComponent();
        }


        private void button_calculate_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBox_grade.Text.Equals(""))
           {
               textBox_grade.Text = "0";
            }
            textBox_money.Text = "计算中，请稍后";
            textBox_skill.Text = "计算中，请稍后";
            textBox_pet.Text = "计算中，请稍后"; 



            int grade = Convert.ToInt16(textBox_grade.Text);



             
            /// <summary>
            /// 金钱携带上限
            /// </summary>
            textBox_money.Text = string.Format("{0:0,0}", needmoneys[grade]);
           
           
            if (grade >= 20)
            {
                /// <summary>
                /// 人物技能要求
                /// </summary>
                textBox_skill.Text =  needskills[grade].ToString();
            }
            else {
                textBox_skill.Text = "没有人物技能要求";
            }
            if (grade >= 43)
            {
                /// <summary>
                /// 在家训宠等级
                /// </summary>
                textBox_pet.Text = needpets[grade].ToString();
            }
            else {
                textBox_pet.Text = "没有在家训宠资格";
            }

        }

        private void button_empty_Click_1(object sender, RoutedEventArgs e)
        { 
            textBox_grade.Text = "0";
            textBox_money.Text = "";
            textBox_skill.Text = "";
            textBox_pet.Text = "";

            
        }

        private void textBox_grade_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textBox_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textBox_grade.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_CharacteLevel)
                    {
                        Tool.Coding4FunForMsg("人物等级不得超过" + Config.HIGH_CharacteLevel + "级", "", 1000); 
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }

        private void textBox_grade_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textBox_grade);
        }

        private void textBox_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textBox_grade);
        }
    }
}