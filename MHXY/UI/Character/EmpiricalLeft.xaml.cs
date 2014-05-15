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
    public partial class Empiricalleft : PhoneApplicationPage
    {
        /// <summary>
        /// 到达等级
        /// </summary>
        private int reachGrade;
        /// <summary>
        /// 剩余经验
        /// </summary>
        private long surplusExperience;
        /// <summary>
        /// 等级对应经验
        /// </summary>
        private long[] empiricals = new long[] { 40, 110, 237, 451, 780, 1252, 1898, 2745, 3823, 5160, 6784, 8726, 11013, 13675, 16740, 20236, 24194, 28641, 33607, 39120, 45208, 51902, 59229, 67219, 75900, 85300, 95450, 106377, 118111, 130680, 144112, 158438, 173685, 189883, 207060, 225244, 244466, 264753, 286135, 308640, 332296, 357134, 383181, 410467, 439020, 468868, 500042, 532569, 566479, 601800, 638560, 676790, 716517, 757771, 800580, 844972, 890978, 938625, 987943, 1038960, 1091704, 1146206, 1202493, 1260595, 1320540, 1382356, 1446074, 1511721, 1579327, 1648920, 1720528, 1794182, 1869909, 1947739, 2027700, 2109820, 2194130, 2280657, 2369431, 2460480, 2553832, 2649518, 2747565, 2848003, 2950860, 3056164, 3163946, 3274233, 3387055, 3502440, 3620416, 3741014, 3864261, 3990187, 4118820, 4250188, 4384322, 4521249, 4660999, 4803600, 4998572, 5199420, 5406262, 5619215, 5838399, 6063935, 6295943, 6534547, 6779869, 7032037, 7291174, 7557409, 7830871, 8111688, 8399991, 8695914, 8999587, 9311146, 9630727, 9958464, 10294498, 10638965, 10992006, 11353762, 11724375, 12103990, 12492750, 12890801, 13298290, 13715364, 14142175, 14578870, 15025603, 15482526, 15949791, 16427556, 16915975, 17415205, 17925406, 18446736, 18979357, 19523431, 20079119, 20646587, 21225999, 43635046, 44842652, 46075152, 47332888, 48616200, 74888152, 76891406, 78934587, 81018220, 83142837, 85308972, 87517162, 89767948, 92061874, 94399488, 129041789, 132277319, 135573318, 138930535, 142349724, 218747465, 224065581, 229480087, 234992140, 240602905, 533679362, 819407100, 1118169947, 1430306664, 1756161225 };
 
        public Empiricalleft()
        {
            InitializeComponent();
        }

        private void textBox_now_experience_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            } 
            base.OnKeyDown(e);
        }

        private void textBox_now_grade_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else
            {
                if (!e.Key.ToString().ToLower().Equals("back") && !textBox_now_grade.Text.Equals(""))
                {
                    int num = Convert.ToInt16(textBox_now_grade.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_CharacteLevel)
                    {
                        Tool.Coding4FunForMsg("人物等级不得超过" + Config.HIGH_CharacteLevel + "级", "", 1000); 
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);

        }

        private void button_calculate_Click_1(object sender, RoutedEventArgs e)
        {
            surplusExperience = 0;
            reachGrade = 0; 
           
            if (textBox_now_grade.Text.Equals("") ) {
                textBox_now_grade.Text="0"; 
            }
            if (textBox_now_experience.Text.Equals("") ) {
                textBox_now_experience.Text="0"; 
            }
            surplusExperience =long.Parse(textBox_now_experience.Text);
            reachGrade = Convert.ToInt16(textBox_now_grade.Text);

            for (int i = reachGrade; i < Config.HIGH_CharacteLevel; i++)
            { 
                if (surplusExperience >= empiricals[i])
                {
                    reachGrade = reachGrade + 1;
                    surplusExperience = surplusExperience - empiricals[i]; 
                }
                else {
                    break;
                }
            } 
            textBox_reach_grade.Text = reachGrade.ToString();
            String experience=string.Format("{0:0,0}", surplusExperience);
            textBox_Surplus_experience.Text =Tool.DelFist0(experience);
   
        }

        private void button_empty_Click_1(object sender, RoutedEventArgs e)
        {
            textBox_now_experience.Text = "1"; 
            textBox_now_grade.Text = "0";
            textBox_reach_grade.Text = "";
            textBox_Surplus_experience.Text = ""; 
        }
    }
}