 
using System;
using System. Collections. Generic;
using System. Collections. ObjectModel;
using System. Diagnostics;
using System. IO;
using System. Linq;
using System. Net;
using System. Text;
using System. Text. RegularExpressions;
using System. Threading;
using System. Windows;
using System. Windows. Controls;
using System. Windows. Documents;
using System. Windows. Media;
using System. Windows. Media. Imaging;
using System. Xml. Linq; 
using Microsoft. Phone. Controls;
using Microsoft. Phone. Net. NetworkInformation;
using Microsoft. Phone. Shell;
using Microsoft. Phone. Tasks; 
using WP7_ControlsLib. Controls;
using WP7_WebLib. HttpGet;
using WP7_WebLib.HttpPost;
using MHXY.Model;
using MHXY.Model.AppOnly;
using HtmlAgilityPack;

namespace MHXY.Hepler
{
    public static class Tool
    {
        

        #region Http操作
        public static WebClient SendWebClient(string urlPrefix, Dictionary<string, string> parameters)
        {
            
            WebClient client = new WebClient( );
            client. Headers[ "User-Agent" ] = Config. UserAgent;
            if ( Config.Cookie.IsNotNullOrWhitespace() )
            {
                client. Headers[ "Cookie" ] = Config. Cookie;
            }
            /*
             * WP7 会缓存相同url 的返回结果 所以这里需要添加 guid 参数
             */
            if ( parameters != null && parameters.ContainsKey("guid") == false)
            {
                parameters. Add( "guid", Guid. NewGuid( ). ToString( ) );
            }
            string uri = HttpGetHelper. GetQueryStringByParameters( urlPrefix, parameters );
            client. DownloadStringAsync( new Uri( uri, UriKind. Absolute ) );
            return client;
        }

        public static PostClient SendPostClient(string urlPrefix, Dictionary<string, object> parameters)
        {
            /*
             * WP7 会缓存相同url 的返回结果 所以这里需要添加 guid 参数
             */
            if ( parameters != null && parameters.ContainsKey("guid") == false)
            {
                parameters. Add( "guid", Guid. NewGuid( ). ToString( ) );
            }
            PostClient client = new PostClient( parameters )
            {
                UserAgent = Config. UserAgent,
            };
            //抓取到 Cookie
            client. OnGetCookie += (cookie) =>
            {
                Config. Cookie = cookie;
            };
            client. DownloadStringAsync( new Uri( urlPrefix, UriKind. Absolute ), Config. Cookie.EnsureNotNull() );
            return client;
        }
         
        #endregion

        #region Html组装以及站内链接处理以及软件更新


  
        public static ApiResult GetApiResult(string response)
        {
          
            try
            {
                XElement result = XElement.Parse(response).Element("result");
                return new ApiResult
                {
                    errorCode = result.Element("errorCode").Value.ToInt32(),
                    errorMessage = result.Element("errorMessage").Value,
                };
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ApiResult 解析错误: {0}", e.Message);
                return null;
            }
        }


        /// <summary>
        /// 检查是否需要更新版本
        /// </summary>
        /// <param name="url">xml文件的url访问地址</param>
        public static void CheckVersionNeedUpdate( string url )
        {
            WebClient client = Tool. SendWebClient( url, null );
            client. DownloadStringCompleted += (s, e) =>
                {
                    if ( e.Error != null )
                    {
                        return;
                    }
                    try
                    {
                        XElement root = XElement. Parse( e. Result );
                        if ( root != null )
                        {
                            XElement wp7 = root. Element( "update" ). Element( "wp7" );
                            if ( wp7 != null )
                            {
                                if ( Config. AppVersion != wp7. Value && Config.AppVersion.ToFloat() < wp7.Value.ToFloat())
                                {
                                    if ( MessageBox. Show( "有新版本可用，您确定要升级本客户端吗?", "温馨提示", MessageBoxButton. OKCancel ) == MessageBoxResult. OK )
                                    {
                                        MarketplaceDetailTask task = new MarketplaceDetailTask { };
                                        task. Show( );
                                    }
                                }
                                else
                                {
                                    MessageBox. Show( "您使用的版本已经是最新的" );
                                }
                            }
                        }
                    }
                    catch ( Exception )
                    {
                    }
                };
        }

        #region  浏览器缩放图片处理
        public static string ProcessHTMLString(string sourceHTML, bool isImgsHidden = false, int imgWidth = 0)
        {
            var info = NetworkInterface. NetworkInterfaceType;
            switch ( info )
            {
                case NetworkInterfaceType. MobileBroadbandCdma:
                case NetworkInterfaceType. MobileBroadbandGsm:
                    isImgsHidden = !Config. IsImgsVisible;
                    break;
            }  
            //然后转码
            HtmlDocument document = new HtmlDocument( );
            document. LoadHtml( sourceHTML );
            if ( document == null )
            {
                return sourceHTML;
            }
            var images = document. DocumentNode. SelectNodes( "//img" );
            if ( images == null )
            {
                return sourceHTML;
            }
            foreach ( HtmlNode image in images )
            {
                if ( isImgsHidden )
                {
                    if ( image. Attributes[ "src" ] != null )
                    {
                        image. Attributes[ "src" ]. Value = string. Empty;
                    }
                    continue;
                }
                if ( imgWidth != 0 )
                {
                    image. Attributes. Add( "width", string.Format("{0}px", imgWidth) );
                }
                if ( image. Attributes[ "width" ] != null )
                {
                    //修改高宽
                    int width = GetNumFromString( image. Attributes[ "width" ]. Value );
                    if ( width > 0 )
                    {
                        image. Attributes[ "width" ]. Value = width. ToString( );
                    }
                    else
                    {
                        image. Attributes[ "width" ]. Remove( );
                    }
                }
                if ( image. Attributes[ "height" ] != null )
                {
                    image. Attributes[ "height" ]. Remove( );
                }
            }
            return document. DocumentNode. InnerHtml;
        }
        private static int GetNumFromString(string originStr)
        {
            string num_str = Regex. Replace( originStr, @"[^\d]*", "" );
            int num;
            if ( int. TryParse( num_str, out num ) )
            {
                return num <= 480 ? num * 2 : 960;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// 处理站内链接地址的跳转 这里需要分析  oschina.net 的url关系
        /// </summary>
        /// <param name="link">站内链接</param>
        public static void ProcessAppLink( string link )
        {
            link = link. EnsureNotNull( );

            #region 如果是动弹图片的链接点击
            if ( link == "http://wangjuntom" )
            {
                return;
            }
            #endregion
        //http://www.oschina.net/question/tag/{1}
            //对 link 进行分析
            string search = "oschina.net";
            if ( link.IndexOf(search) >= 0 )
            {
                link = link. Substring( 7 );
                string prefix = link. Substring( 0, 3 );
                switch ( prefix )
                {
    #region 此情况为博客，动弹，个人专页
                    case "my.":
                        {
                            string[ ] array = link. Split( '/' );
                            //个人专页 用户名形式
                            if ( array.Length <= 2 )
                            {
                                //进入用户专页 其中 array[1] 表示用户名
                                ToUserPage( -1, array[ 1 ] );
                                return;
                            }
                            else if ( array.Length <= 3 )
                            {
                                if ( array[1] == "u" )
                                {
                                    //进入用户专页 其中 array[2] 表示用户 uid
                                    ToUserPage( array[ 2 ]. ToInt32( ), null );
                                    return;
                                }
                            }
                            else if ( array.Length <= 4 )
                            {
                                switch ( array[2] )
                                {
                                        //进入博客专页
                                    case "blog":
                                        //博客id 是 array[3]
                                        Tool. ToDetailPage( array[ 3 ], DetailType. Blog );
                                        return;
                                        //进入动弹专页
                                    case "tweet":
                                        Tool. ToDetailPage( array[ 3 ], DetailType. Tweet );
                                        //动弹id 是 array[3]
                                        return;
                                }
                            }
                            else if ( array.Length <= 5 )
                            {
                                if ( array[3] == "blog" )
                                {
                                    //进入博客专页 博客id 是 array[4]
                                    Tool. ToDetailPage( array[ 4 ], DetailType. Blog );
                                    return;
                                }
                            }
                        }
                        break;
    #endregion

    #region 此情况为 新闻，软件，问答，标签
                    case "www":
                        {
                            string[ ] array = link. Split( '/' );
                            if ( array.Length >= 3 )
                            {
                                switch ( array[1] )
                                {
                                    case "news":
                                        //进入新闻专页 id 为 array[2]
                                        Tool. ToDetailPage( array[ 2 ], DetailType. News );
                                        return;
                                    case "p":
                                        //进入软件专页 软件ident 为 array[2]
                                        Tool. ToDetailPage( array[ 2 ], DetailType. Software );
                                        return;
                                    case "question":
                                        if (array.Length == 3)
	                                    {
		                                      string[ ] array2 = array[ 2 ]. Split( '_' );
                                              if ( array2.Length >= 2 )
                                              {
                                                  //进入问答专页 id为 array2[1]
                                                  Tool. ToDetailPage( array2[ 1 ], DetailType. Post );
                                                  return;
                                              }
	                                    }
                                        else if (array.Length >=  4)
	                                    {
                                            string tag = "";
                                            if ( array. Length == 4 )
                                            {
                                                tag = array[ 3 ];
                                            }
                                            else
                                            {
                                                array = array. Skip( 3 ). ToArray( );
                                                tag = array. Aggregate((i, j) => string. Format( @"{0}/{1}", i, j ) );
                                            }
                                            tag = System. Net. HttpUtility. UrlEncode( tag );
                                            //string _url =System.Net.HttpUtility.UrlEncode( );
                                            Tool. To( string. Format( "/PostsPage.xaml?tag={0}", tag ) );
                                            return;
	                                    }
                                        break;
                                }
                            }
                        }
   #endregion
                        break;
                }
                //非站内链接将使用IE打开
                WebBrowserTask task = new WebBrowserTask { Uri = new Uri( link.Substring(0,4)=="http" ? link: string.Format("http://{0}", link), UriKind. Absolute ) };
                task. Show( );
            }
         }

        /// <summary>
        /// 跳转到某页
        /// </summary>
        /// <param name="appPageUrl">跳转目的地的查询字符串</param>
        public static void To(string appPageUrl)
        {
            ( Application. Current. RootVisual as PhoneApplicationFrame ). Navigate( new Uri( appPageUrl, UriKind. Relative ) );
        }

        /// <summary>
        /// 跳转到文章详情页
        /// </summary>
        /// <param name="id">文章详情ID</param>
        /// <param name="detailType">文章详情类型</param>
        public static void ToDetailPage( string id, DetailType detailType )
        {
            //Tool. To( string. Format( "/DetailPage.xaml?id={0}&type={1}", id, ( int ) detailType ) );
            Tool. To( string. Format( "/DetailPage2.xaml?id={0}&type={1}", id, ( int ) detailType ) );
        }

        /// <summary>
        /// 进入用户专页 优先uid 如果使用用户名 则将uid = -1 即可
        /// </summary>
        /// <param name="uid">用户uid</param>
        /// <param name="userName">用户姓名</param>
        public static void ToUserPage(int uid, string userName = "")
        {
            //考虑uid
            if ( uid > 0 )
            {
                Tool. To( string. Format( "/UserPage.xaml?uid={0}", uid ) );
            }
            //考虑userName
            else
            {
                Tool. To( string. Format( "/UserPage.xaml?name={0}", userName ) );
            }
        }

        #endregion

        #region 字符串转换

        /// <summary>
        /// 删除首字母是0的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelFist0(String str) {

            if (str.IndexOf("0", 0) == 0)
            {
              return  str.Substring(1);
            }
            return str;
        }

        /// <summary>  
        /// 取文本中间内容  
        /// </summary>  
        /// <param name="str">原文本</param>  
        /// <param name="leftstr">左边文本</param>  
        /// <param name="rightstr">右边文本</param>  
        /// <returns>返回中间文本内容</returns>  
        public static string Getween(string str, string leftstr, string rightstr)
        {
            int i = str.IndexOf(leftstr) + leftstr.Length;
            string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            return temp;
        }


        /// <summary>  
        /// 取文本左边内容  
        /// </summary>  
        /// <param name="str">文本</param>  
        /// <param name="s">标识符</param>  
        /// <returns>左边内容</returns>  
        public static string GetLeft(string str, string s)
        {
            string temp = str.Substring(0, str.IndexOf(s));
            return temp;
        }


        /// <summary>  
        /// 取文本右边内容  
        /// </summary>  
        /// <param name="str">文本</param>  
        /// <param name="s">标识符</param>  
        /// <returns>右边内容</returns>  
        public static string GetRight(string str, string s)
        {
            string temp = str.Substring(str.IndexOf(s), str.Length - str.Substring(0, str.IndexOf(s)).Length);
            return temp;
        }  
        /// <summary>
        /// 金钱格式化
        /// </summary> 
        public static string ToFriendlyMoney(String num)
        {
            return string.Format("{0:N}", num);
            
        }


        /// <summary>
        /// 时间显示字符串转换
        /// </summary>
        /// <param name="sourceTime">原始时间</param>
        /// <returns>转换后的时间</returns>
        public static string IntervalSinceNow(string sourceTime)
        {
            DateTime source = DateTime. Now;
            DateTime. TryParse( sourceTime, out source );
            TimeSpan span = DateTime. Now - source;
            long seconds = (long)span. TotalSeconds;
            string timeString = string. Empty;
            //一小时内
            if ( seconds / 3600 < 1 )
            {
                if ( seconds / 60 < 1 )
                {
                    timeString = "1";
                }
                else
                {
                    timeString = ( seconds / 60 ). ToString( );
                }
                return string. Format( "{0}分钟前", timeString );
            }
                //一天内
            else if ( seconds / 3600 >= 1 && seconds / 86400 < 1 )
            {
                return string. Format( "{0}小时前", seconds / 3600 );
            }
                //十天内
            else if ( seconds / 86400 >= 1 && seconds / 864000 < 1 )
            {
                return string. Format( "{0}天前", seconds / 86400 );
            }
                //十天以上
            else
            {
                return source. ToString( "yyyy-MM-dd" );
            }
        }

        /// <summary>
        /// 动弹的客户端平台显示
        /// </summary>
        public static string GetAppClientString(AppClientType type)
        {
            switch ( type )
            {
                case AppClientType.None:
                case AppClientType. Web:
                    return string. Empty;
                case AppClientType. Mobile:
                    return "来自手机";
                case AppClientType. Android:
                    return "来自Android";
                case AppClientType. iOS:
                    return "来自iPhone";
                case AppClientType. WP7:
                    return "来自Windows Phone";
                default:
                    return string. Empty;
            }
        }

        //个人动态的字符串组装
        private static Run blueText(string text)
        {
            return new Run
            {
                Text = text,
                FontSize = 24,
                //Foreground = new SolidColorBrush( Color. FromArgb( 255, 13, 109, 168 ) ),
                Foreground = Application. Current. Resources[ "PhoneAccentBrush" ] as SolidColorBrush,
            };
        }
        private static Run grayText(string text)
        {
            return new Run
            {
                Text = text,
                FontSize = 22,
                Foreground = new SolidColorBrush( Color. FromArgb( 255, 120, 120, 120 ) ),
            };
        }


        private static Run orangeText(string text)
        {
            return new Run
            {
                Text = text,
                FontSize = 21,
                FontStyle = FontStyles.Italic,
                //Foreground = new SolidColorBrush( Color. FromArgb( 195, 255, 80, 0 ) ),
                //Foreground = new SolidColorBrush( (Application. Current. Resources[ "PhoneAccentBrush" ] as SolidColorBrush).Color == Colors.Orange ? Colors.Magenta : Colors.Orange ),
                Foreground = new SolidColorBrush(Colors.Orange),
            };
        }
        private static Run themeText(string text)
        {
            return new Run
            {
                Text = text,
                FontSize = 24,
                Foreground = Application. Current. Resources[ "PhoneAccentBrush" ] as SolidColorBrush,
            };
        }
        private static Run blackText(string text)
        {
            return new Run
            {
                Text = text,
                FontSize = 24,
            };
        }
        private static Run smallGrayText(string text)
        {
            return new Run
            {
                Text = text,
                FontSize = 20,
                Foreground = new SolidColorBrush( Color. FromArgb( 255, 153, 153, 153 ) ),
            };
        }

        /// <summary>
        /// 动态列表单元处理
        /// </summary>
        /// <param name="active">动态对象</param>
        /// <param name="txt">TextBlock控件引用</param>
        public static void ProcessActiveUnit(ActiveUnit active, TextBlock txt)
        {
            txt. Inlines. Clear( );
            Run author = blueText( active. author );

            //注意 pubDate前必须有LineBreak
            Run pubDate = smallGrayText( string. Format( "{0} {1}", Tool. IntervalSinceNow( active. pubDate ), Tool. GetAppClientString( ( AppClientType ) active. appClient ) ) );

            //注意 reply 前必须有LineBreak
            Run reply = null;
            if ( active.objReplyBody.IsNotNullOrWhitespace( ) )
            {
                reply = orangeText( string. Format( "{0}:{1}", active. objReplyName, active. objReplyBody ) );
            }

            List<Inline> msgs = new List<Inline>( );
            #region 针对 msgs 的处理
            switch ( active.objType )
            {
                case 6:
                    if ( active.objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 发布了一个职位 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 20:
                    if ( active.objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 在职位 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( grayText( " 发表评论" ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 32:
                    if ( active.objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 加入了开源中国" ) );
                    }
                    break;
                case 1:
                    if ( active.objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 添加了开源项目 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 2:
                    if ( active. objCatalog == 1 )
                    {
                        msgs. Add( grayText( " 在讨论区提问: " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    else if(active.objCatalog == 2)
                    {
                        msgs. Add( grayText( " 发表了新话题: " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 3:
                    if ( active.objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 发表了博客 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 4:
                    if ( active. objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 发表一篇新闻 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 5:
                    if ( active. objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 分享了一段代码 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 16:
                    if ( active. objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 在新闻 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( grayText( " 发表评论" ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 17:
                    if ( active.objCatalog == 1 )
                    {
                        msgs. Add( grayText( " 回答了问题: " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    else if ( active.objCatalog == 2 )
                    {
                        msgs. Add( grayText( " 回复了话题: " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    else if ( active.objCatalog == 3 )
                    {
                        msgs. Add( grayText( " 在 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( grayText( " 对回帖发表评论" ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 18:
                    if ( active. objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 在博客 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( grayText( " 发表评论" ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 19:
                    if ( active. objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 在代码 " ) );
                        msgs. Add( themeText( active. objTitle ) );
                        msgs. Add( grayText( " 发表评论" ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 100:
                    if ( active. objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 更新了动态" ) );
                        msgs. Add( new LineBreak { FontSize = 6 } );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak () );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
                case 101:
                    if ( active. objCatalog == 0 )
                    {
                        msgs. Add( grayText( " 回复了动态" ) );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( new Run { Text = " ", FontSize = 6 } );
                        msgs. Add( new LineBreak( ) );
                        msgs. Add( blackText( active. message ) );
                    }
                    break;
            }
            #endregion
            //开始添加
            txt. Inlines. Add( author );
            msgs. ForEach( m => txt. Inlines. Add( m ) );
            if ( reply != null )
            {
                txt. Inlines. Add( new LineBreak( ) );
                txt. Inlines. Add( new Run { Text = " ", FontSize = 8 } );
                txt. Inlines. Add( new LineBreak( ) );
                txt. Inlines. Add( reply );
            }
            txt. Inlines. Add( new LineBreak( ) );
            txt. Inlines. Add( new Run { Text = " ", FontSize = 12 } );
            txt. Inlines. Add( new LineBreak( ) );
            txt. Inlines. Add( pubDate );
        }
        #endregion
  
        #region 图像处理
        public static Stream ReduceSize(BitmapImage g_bmp)
        {
            WriteableBitmap wb = new WriteableBitmap( g_bmp );
            MemoryStream g_MS = new MemoryStream( );
            System. Windows. Media. Imaging. Extensions. SaveJpeg( wb, g_MS, 800, 640, 0, 82 );
            g_MS. Seek( 0, SeekOrigin. Begin );
            return g_MS;
        }
        #endregion
    }
}