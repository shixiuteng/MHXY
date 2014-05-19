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
namespace MHXY.UI.Skill
{
    public partial class SecondarySkills : PhoneApplicationPage
    {
        private long[] fzskillexp = new long[] { 0, 16, 32, 52, 75, 103, 136, 179, 231, 295, 372, 466, 578, 711, 867, 1049, 1260, 1503, 1780, 2096, 2452, 2854, 3304, 3807, 4364, 4983, 5664, 6415, 7238, 8138, 9120, 10188, 11347, 12602, 13959, 15423, 16998, 18692, 20508, 22452, 24532, 26753, 29121, 31642, 34323, 37169, 40188, 43388, 46773, 50352, 54132, 58120, 62324, 66750, 71407, 76303, 81444, 86840, 92500, 98430, 104640, 111136, 117931, 125031, 132444, 140183, 148253, 156666, 165430, 174556, 184052, 193930, 204198, 214868, 225948, 237449, 249383, 261760, 274589, 287884, 301652, 315908, 330662, 345924, 361708, 378023, 394882, 412297, 430280, 448844, 468000, 487760, 508137, 529145, 550796, 573103, 596078, 619735, 644088, 669149, 694932, 721452, 748722, 776755, 805566, 835169, 865579, 896809, 928876, 961792, 995572, 1030234, 1065790, 1102256, 1139649, 1177983, 1217273, 1257536, 1298787, 1341043, 1384320, 1428632, 1473999, 1520435, 1567957, 1616583, 1666328, 1717211, 1769248, 1822456, 1876852, 1932456, 1989284, 2047353, 2106682, 2167289, 2229192, 2292410, 2356960, 2422861, 2490132, 2558792, 2628860, 2700356, 2773296, 2847703, 2923593, 3000989, 3079908, 3160372, 3242400, 6652022, 6822452, 6996132, 7173104, 7353406, 11305620, 11586254, 11872072, 12163140, 12459518 };
        private long[] fzskillmoney = new long[] { 0, 3, 6, 9, 14, 19, 25, 33, 43, 55, 69, 87, 108, 133, 162, 196, 236, 281, 333, 393, 459, 535, 619, 713, 818, 934, 1062, 1202, 1357, 1525, 1710, 1910, 2127, 2362, 2617, 2891, 3187, 3504, 3845, 4209, 4599, 5016, 5460, 5932, 6435, 6969, 7535, 8135, 8770, 9441, 10149, 10897, 11685, 12515, 13388, 14306, 15270, 16282, 17343, 18455, 19620, 20838, 22112, 23443, 24833, 26284, 27797, 29374, 31018, 32729, 34509, 36361, 38287, 40287, 42365, 44521, 46759, 49080, 51485, 53978, 56559, 59232, 61999, 64860, 67820, 70879, 74040, 77305, 80677, 84158, 87750, 91455, 95275, 99214, 103274, 107456, 111764, 116200, 120766, 125465, 130299, 135272, 140385, 145641, 151043, 156594, 162296, 168151, 174164, 180336, 186669, 193168, 199835, 206673, 213684, 220871, 228238, 235788, 243522, 251445, 259560, 267868, 276374, 285081, 293992, 303109, 312436, 321977, 331734, 341710, 351909, 362335, 372990, 383878, 395002, 406366, 417973, 429826, 441930, 454286, 466899, 479773, 492911, 506316, 519993, 533944, 548173, 562685, 577482, 592569, 607950, 997803, 1023367, 1049419, 1075965, 1103010, 1695843, 1737938, 1780810, 1824471, 1868927 };
        private long[] fzskillneedbg = new long[] { 5, 1, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165 };
        private long[] fzskillusebg = new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160 };

        private long sumExp = 0;
        private long sumMoney = 0;
        private long sumNeedbg = 0;
        private long sumUsebg = 0;
        private long sumRq = 0;


        private long[] fzskillexp2 = new long[] { 2000, 2001, 2008, 2027, 2064, 2125, 2216, 2343, 2512, 2729, 3000, 3331, 3728, 4197, 4744, 5375, 6096, 6913, 7832, 8859, 10000, 11261, 12648, 14167, 15824, 17625, 19576, 21683, 23952, 26389, 29000, 31791, 34768, 37937, 41304, 44875, 48656, 52653, 56872, 61319, 66000};
        private long[] fzskillmoney2 = new long[] { 300, 301, 308, 327, 364, 425, 516, 643, 812, 1029, 1300, 1631, 2028, 2497, 3044, 3675, 4396, 5213, 6132, 7159, 8300, 9561, 10948, 12467, 14124, 15925, 17876, 19983, 22252, 24689, 27300, 30091, 33068, 36237, 39604, 43175, 46956, 50953, 55172, 59619, 64300};
        private long[] fzskillrq = new long[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 9 };



        /// <summary>
        /// 最高辅助等级
        /// </summary>
        private int skillsLevel = Config.HIGH_SecondarySkillsLevel;

        public SecondarySkills()
        {
            InitializeComponent();
        }




        private void textBox_start_grade_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textBox_start_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textBox_start_grade.Text + e.Key.ToString().Substring(1));
                    if (num >skillsLevel)
                    { 

                        Tool.Coding4FunForMsg("技能等级不得超过" + skillsLevel + "级", "", 1000); 
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }

        private void textBox_end_grade_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textBox_end_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textBox_end_grade.Text + e.Key.ToString().Substring(1));
                    if (num >skillsLevel)
                    { 
                        Tool.Coding4FunForMsg("目标等级不得超过" + skillsLevel + "级", "", 1000); 
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }






        private void button_calculate_Click(object sender, RoutedEventArgs e)
        {

            sumExp = 0;
            sumMoney = 0;
            sumNeedbg = 0;
            sumUsebg = 0;
            sumRq = 0; 
            if (textBox_start_grade.Text.Equals(""))
            {
                textBox_start_grade.Text = "0";
            }

            if (textBox_end_grade.Text.Equals(""))
            {
                textBox_end_grade.Text = "0";
            }
            if (Convert.ToInt16(textBox_start_grade.Text) > skillsLevel)
            { 
                Tool.Coding4FunForMsg("目标等级不得超过" + skillsLevel + "级", "", 1000); 
                textBox_start_grade.Focus(); 
                return;
            }

            if (Convert.ToInt16(textBox_end_grade.Text) > skillsLevel)
            { 
                Tool.Coding4FunForMsg("目标等级不得超过" + skillsLevel + "级", "", 1000); 
                textBox_end_grade.Focus(); 
                return;
            }
             
            int startGrade = Convert.ToInt16(textBox_start_grade.Text);
            int endGrade = Convert.ToInt16(textBox_end_grade.Text);
            if (startGrade == endGrade)
            {
                textbox_needed_experience.Text = "0";
                textbox_needed_money.Text = "0";
                textbox_needed_tribute.Text = "0";
                textbox_consume_tribute.Text = "0";
                textbox_consume_popularity.Text = "0";
                return;
            }
            if (endGrade < startGrade)
            { 
                Tool.Coding4FunForMsg("目标等级不得小于当前等级", "", 1000); 
                return;
            }

            if (skillsLevel == 40)
            { 
                for (int i = startGrade + 1; i <= endGrade; i++)
                { 
                    sumExp = sumExp + fzskillexp2[i];
                    sumMoney = sumMoney + fzskillmoney2[i]; 
                    sumRq = sumRq + fzskillrq[i];
                } 
            }
            else {

                for (int i = startGrade + 1; i <= endGrade; i++)
                {

                    sumExp = sumExp + fzskillexp[i];
                    sumMoney = sumMoney + fzskillmoney[i];
                    sumNeedbg = sumNeedbg + fzskillneedbg[i];
                    sumUsebg = sumUsebg + fzskillusebg[i];
                } 
            
            } 
            textbox_needed_experience.Text = string.Format("{0:0,0}", sumExp);
            textbox_needed_money.Text = Tool.DelFist0(string.Format("{0:0,0}", sumMoney).ToString());
            textbox_needed_tribute.Text = sumNeedbg.ToString();
            textbox_consume_tribute.Text = sumUsebg.ToString();
            textbox_consume_popularity.Text = sumRq.ToString(); 

        }

        private void button_empty_Click(object sender, RoutedEventArgs e)
        {
            textBox_start_grade.Text = "0";
            textBox_end_grade.Text = "0";
            textbox_needed_experience.Text = ""; 
            textbox_needed_money.Text = "";
            textbox_needed_tribute.Text = "";
            textbox_consume_tribute.Text = "";
            textbox_consume_popularity.Text = ""; 

        }

      

        private void radioButton_faction_Checked_1(object sender, RoutedEventArgs e)
        {
            skillsLevel = 160;
        }

        private void radioButton_NPC_Checked_1(object sender, RoutedEventArgs e)
        {
            skillsLevel = 40;
             
            if (Convert.ToInt16(textBox_start_grade.Text) > skillsLevel)
            { 

                Tool.Coding4FunForMsg("目标等级不得超过" + skillsLevel + "级", "", 1000); 
                textBox_start_grade.Focus();  
                return;
            }

            if (Convert.ToInt16(textBox_end_grade.Text) > skillsLevel)
            { 

                Tool.Coding4FunForMsg("目标等级不得超过" + skillsLevel + "级", "", 1000); 
                textBox_end_grade.Focus(); 
                return;
            }
        }

        private void textBox_start_grade_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textBox_start_grade);
        }

        private void textBox_start_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textBox_start_grade);
        }

        private void textBox_end_grade_GotFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_GotFocus(textBox_end_grade);
        }

        private void textBox_end_grade_LostFocus(object sender, RoutedEventArgs e)
        {
            Tool.textbox_LostFocus(textBox_end_grade);
        }

    }
}