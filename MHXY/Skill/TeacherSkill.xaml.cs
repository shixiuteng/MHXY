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

namespace MHXY.Skill
{
    public partial class TeacherSkill : PhoneApplicationPage
    {
        /// <summary>
        /// 技能经验
        /// </summary>
        private long[] skillsExperiences = new long[] { 0, 16, 32, 52, 74, 103, 136, 179, 231, 294, 372, 466, 578, 711, 867, 1049, 1260, 1502, 1780, 2095, 2452, 2854, 3304, 3806, 4364, 4983, 5664, 6414, 7237, 8137, 9120, 10187, 11346, 12602, 13958, 15423, 16998, 18691, 20507, 22452, 24532, 26753, 29120, 31642, 34322, 37169, 40188, 43388, 46773, 50352, 54132, 58120, 62324, 66750, 71407, 76303, 81444, 86840, 92500, 98429, 104640, 111136, 117930, 125030, 132444, 140183, 148253, 156665, 165430, 174556, 184052, 193930, 204198, 214867, 225947, 237449, 249383, 261759, 274589, 287883, 301652, 315908, 330661, 345924, 361707, 378023, 394882, 412296, 430280, 448843, 468000, 487760, 508137, 529145, 550796, 573103, 596078, 619735, 644088, 669148, 694932, 721452, 748721, 776755, 805565, 835169, 865579, 896809, 928875, 961791, 995572, 1030233, 1065789, 1102256, 1139648, 1177983, 1217272, 1257536, 1298787, 1341042, 1384320, 1428632, 1473998, 1520434, 1567956, 1616583, 1666328, 1717210, 1769247, 1822455, 1876852, 1932456, 1989283, 2047352, 2106681, 2167289, 2229192, 2292409, 2356960, 2422860, 2490132, 2558792, 2628860, 2700356, 2773296, 2847703, 2923593, 3000988, 3079908, 3160372, 3242400, 6652022, 6822452, 6996132, 7173102, 7353406, 11305620, 11586254, 11872072, 12163137, 12459518, 15033471, 15315219, 15600468, 15889236, 16181550, 16477425, 16776885, 17079954, 17386650, 17697000, 24014692, 24438308, 24866880, 25300432, 25739000, 32728255, 33289095, 33856310, 34429930, 40842000};
         /// <summary>
        /// 技能金钱
        /// </summary>
        private long[] skillsMoney = new long[] { 0, 6, 12, 19, 27, 38, 51, 67, 86, 110, 139, 174, 216, 266, 325, 393, 472, 563, 667, 785, 919, 1070, 1239, 1427, 1636, 1868, 2124, 2405, 2714, 3051, 3420, 3820, 4254, 4725, 5234, 5783, 6374, 7009, 7690, 8419, 9199, 10032, 10920, 11865, 12870, 13938, 15070, 16270, 17540, 18882, 20299, 21795, 23371, 25031, 26777, 28613, 30541, 32565, 34687, 36911, 39240, 41676, 44223, 46886, 49666, 52568, 55595, 58749, 62036, 65458, 69019, 72723, 76574, 80575, 84730, 89043, 93518, 98159, 102971, 107956, 113119, 118465, 123998, 129721, 135640, 141758, 148080, 154611, 161355, 168316, 175500, 182910, 190551, 198429, 206548, 214913, 223529, 232400, 241533, 250930, 260599, 270544, 280770, 291283, 302087, 313188, 324592, 336303, 348328, 360671, 373339, 386337, 399671, 413346, 427368, 441743, 456477, 471576, 487045, 502890, 519120, 535737, 552749, 570162, 587983, 606218, 624873, 643953, 663467, 683420, 703819, 724671, 745981, 767757, 790005, 812733, 835947, 859653, 883860, 908572, 933799, 959547, 985822, 1012633, 1039986, 1067888, 1096347, 1125370, 1154965, 1185139, 1215900, 2494508, 2558419, 2623549, 2689913, 2757527, 4239607, 4344845, 4452027, 4561176, 4672319, 4510041, 4594563, 4680138, 4766769, 4854465, 4943226, 5033064, 5123985, 5215995, 5309100, 7204407, 7331490, 7460064, 7590129, 7721700, 9818475, 9986727, 10156893, 10328979, 12252600 };
        /// <summary>
        /// 计算总经验
        /// </summary>
        private long sumSkillsExperience = 0;
        /// <summary>
        /// 计算总金钱
        /// </summary>
        private long sumSkillsMoney = 0;
        public TeacherSkill()
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
                    if (num > Config.highLevel + 5)
                    {
                        MessageBox.Show("技能等级不得超过" + (Config.highLevel + 5) + "级");
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
                    if (num > Config.highLevel + 5)
                    {
                        MessageBox.Show("目标等级不得超过" + (Config.highLevel + 5) + "级");
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }




        private void button_calculate_Click(object sender, RoutedEventArgs e)
        {
            sumSkillsExperience = 0;
            sumSkillsMoney = 0;
            if (textBox_start_grade.Text.Equals(""))
            {
                textBox_start_grade.Text = "0";
            }

            if (textBox_end_grade.Text.Equals(""))
            {
                textBox_end_grade.Text = "0";
            }



            int startGrade = Convert.ToInt16(textBox_start_grade.Text);
            int endGrade = Convert.ToInt16(textBox_end_grade.Text);
            if (startGrade == endGrade) {
                textbox_requires_experience.Text = "0";
                textbox_requires_money.Text = "0";
                return;
            }
            if (endGrade < startGrade)
            {
                MessageBox.Show("目标等级不得小于当前等级");
                return;
            }

            for (int i = startGrade+1; i <= endGrade; i++)
            {
                sumSkillsExperience = sumSkillsExperience + skillsExperiences[i];
                sumSkillsMoney = sumSkillsMoney + skillsMoney[i];
            }

            textbox_requires_experience.Text = string.Format("{0:0,0}", sumSkillsExperience);
            textbox_requires_money.Text = string.Format("{0:0,0}", sumSkillsMoney);

        }

        private void button_empty_Click(object sender, RoutedEventArgs e)
        {
            textBox_start_grade.Text = "0";
            textBox_end_grade.Text = "0";
            textbox_requires_experience.Text = "";
            textbox_requires_money.Text = "";


        }
    }
}