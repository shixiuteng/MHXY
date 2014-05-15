using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP7_WebLib.HttpPost;
using MHXY.Hepler;
using MHXY.Model;


namespace MHXY.UI.Character
{   
    public partial class EmpiricalRight : PhoneApplicationPage
    { 
        private long sumEmpirical = 0;

        /// <summary>
        /// 等级对应经验
        /// </summary>
        private long[] empiricals = new long[] { 40, 110, 237, 451, 780, 1252, 1898, 2745, 3823, 5160, 6784, 8726, 11013, 13675, 16740, 20236, 24194, 28641, 33607, 39120, 45208, 51902, 59229, 67219, 75900, 85300, 95450, 106377, 118111, 130680, 144112, 158438, 173685, 189883, 207060, 225244, 244466, 264753, 286135, 308640, 332296, 357134, 383181, 410467, 439020, 468868, 500042, 532569, 566479, 601800, 638560, 676790, 716517, 757771, 800580, 844972, 890978, 938625, 987943, 1038960, 1091704, 1146206, 1202493, 1260595, 1320540, 1382356, 1446074, 1511721, 1579327, 1648920, 1720528, 1794182, 1869909, 1947739, 2027700, 2109820, 2194130, 2280657, 2369431, 2460480, 2553832, 2649518, 2747565, 2848003, 2950860, 3056164, 3163946, 3274233, 3387055, 3502440, 3620416, 3741014, 3864261, 3990187, 4118820, 4250188, 4384322, 4521249, 4660999, 4803600, 4998572, 5199420, 5406262, 5619215, 5838399, 6063935, 6295943, 6534547, 6779869, 7032037, 7291174, 7557409, 7830871, 8111688, 8399991, 8695914, 8999587, 9311146, 9630727, 9958464, 10294498, 10638965, 10992006, 11353762, 11724375, 12103990, 12492750, 12890801, 13298290, 13715364, 14142175, 14578870, 15025603, 15482526, 15949791, 16427556, 16915975, 17415205, 17925406, 18446736, 18979357, 19523431, 20079119, 20646587, 21225999, 43635046, 44842652, 46075152, 47332888, 48616200, 74888152, 76891406, 78934587, 81018220, 83142837, 85308972, 87517162, 89767948, 92061874, 94399488, 129041789, 132277319, 135573318, 138930535, 142349724, 218747465, 224065581, 229480087, 234992140, 240602905, 533679362, 819407100, 1118169947, 1430306664, 1756161225 };
 
        public EmpiricalRight()
        {
            InitializeComponent(); 
        }



        private void textBox_start_grade_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
             
           
            if (e.Key.GetHashCode() == 255)
            {
                e.Handled = true;
            }
            else { 
               if (!e.Key.ToString().ToLower().Equals("back") && !textBox_start_grade.Text.Equals(""))
                { 
                    int num = Convert.ToInt16(textBox_start_grade.Text + e.Key.ToString().Substring(1));
                    if (num > Config.HIGH_CharacteLevel)
                    {
                        Tool.Coding4FunForMsg("人物等级不得超过" + Config.HIGH_CharacteLevel + "级", "", 1000); 
                        e.Handled = true;
                    } 
                }
                
            } 
            base.OnKeyDown(e);
        }


        private void textBox_end_grade_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
                    if (num > Config.HIGH_CharacteLevel)
                    {
                        Tool.Coding4FunForMsg("人物等级不得超过" + Config.HIGH_CharacteLevel + "级", "", 1000); 
                        e.Handled = true;
                    }
                }

            }
            base.OnKeyDown(e);
        }

        private void button_calculate_Click(object sender, RoutedEventArgs e)
        {
            sumEmpirical = 0;
            if (textBox_start_grade.Text.Equals("") || textBox_end_grade.Text.Equals(""))
            {
               
                Tool.Coding4FunForMsg("请录入等级", "", 1000); 
                return;
            } 

            if (Convert.ToInt16(textBox_end_grade.Text) < Convert.ToInt16(textBox_start_grade.Text)) {
               
                Tool.Coding4FunForMsg("目标等级不得小于当前等级", "", 1000); 
            }
            else if (textBox_end_grade.Text.Equals(textBox_start_grade.Text))
            {
                textbox_requires_experience.Text = "0";
            }
            else {
                textbox_requires_experience.Text = "计算中，请耐心等候！";
                for (int i = Convert.ToInt16(textBox_start_grade.Text); i < Convert.ToInt16(textBox_end_grade.Text); i++)
                {
                  //  MessageBox.Show(i + "    " + empiricals[i]);
                    sumEmpirical = sumEmpirical + empiricals[i];
                    //Dictionary<string, string> parameters = new Dictionary<string, string>
                    //{ {"lv",i+""},};
                    //WebClient client = Tool.SendWebClient("http://calculator.webapp.163.com/xyq_lvexp/exps", parameters);
                    //client.DownloadStringCompleted += (s, e1) =>
                    //   {

                    //       if (e1.Error != null)
                    //       {
                    //           MessageBox.Show("获取 SearchList 网络错误: {0}" + e1.Error);
                    //           System.Diagnostics.Debug.WriteLine("获取 SearchList 网络错误: {0}", e1.Error.Message);
                    //           return;
                    //       }
                         
                    //       sumEmpirical = sumEmpirical + Convert.ToInt16(e1.Result.Substring(5, e1.Result.Length - 7));
                    //       MessageBox.Show("获取 SearchList  : " + e1.Result.Substring(5, e1.Result.Length - 7) + "  " + sumEmpirical);
                    //   }; 
                 } 
                textbox_requires_experience.Text = string.Format("{0:0,0}", sumEmpirical);
                 
            }
        }

        private void button_empty_Click(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show(Convert.ToInt16(textBox_start_grade.Text) + "    " + Convert.ToInt16(textBox_end_grade.Text));

           // MessageBox.Show(empiricals[Convert.ToInt16(textBox_start_grade.Text)] + "    " + empiricals[Convert.ToInt16(textBox_end_grade.Text)]);

             textBox_start_grade.Text = "0";
             textBox_end_grade.Text = "0";
             textbox_requires_experience.Text = "";

        }
         
         
        
    }
    
}