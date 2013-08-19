using System;
using System.Net;
using KCPlayer.Json;
using KCPlayer.Plugin.LiuXing.Controls;

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class XunLeiLoginHelper
    {

        public static void GetXlHisdoryList()
        {
            if (PublicStatic.NowUserOne != null)
            {
                PublicStatic.LoginCookies = new CookieContainer();
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Sessionid))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("sessionid", PublicStatic.NowUserOne.Sessionid, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Userid))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("userid", PublicStatic.NowUserOne.Userid, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.VerifyKey))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("VERIFY_KEY", PublicStatic.NowUserOne.VerifyKey, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.CheckResult))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("check_result", PublicStatic.NowUserOne.CheckResult, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Active))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("active", PublicStatic.NowUserOne.Active, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Blogresult))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("blogresult", PublicStatic.NowUserOne.Blogresult, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.DownByte))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("downbyte", PublicStatic.NowUserOne.DownByte, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.DownFile))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("downfile", PublicStatic.NowUserOne.DownFile, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Isspwd))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("isspwd", PublicStatic.NowUserOne.Isspwd, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Jumpkey))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("jumpkey", PublicStatic.NowUserOne.Jumpkey, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Logintype))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("logintype", PublicStatic.NowUserOne.Logintype, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Lsessionid))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("lsessionid", PublicStatic.NowUserOne.Lsessionid, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Luserid))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("luserid", PublicStatic.NowUserOne.Luserid, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Nickname))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("nickname", PublicStatic.NowUserOne.Nickname, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Onlinetime))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("onlinetime", PublicStatic.NowUserOne.Onlinetime, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Order))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("order", PublicStatic.NowUserOne.Order, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Safe))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("safe", PublicStatic.NowUserOne.Safe, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Score))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("score", PublicStatic.NowUserOne.Score, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Sessionid))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("sessionid", PublicStatic.NowUserOne.Sessionid, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Sex))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("sex", PublicStatic.NowUserOne.Sex, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Upgrade))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("upgrade", PublicStatic.NowUserOne.Upgrade, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Userid))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("userid", PublicStatic.NowUserOne.Userid, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Usernewno))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("usernewno", PublicStatic.NowUserOne.Usernewno, "/", "xunlei.com"));
                }
                if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Usernick))
                {
                    PublicStatic.LoginCookies.Add(new Cookie("usernick", PublicStatic.NowUserOne.Usernick, "/", "xunlei.com"));
                }
            }
            PublicStatic.ClientUri = new Uri("http://i.vod.xunlei.com/req_history_play_list/req_num/30/req_offset/0?type=all&order=create&t=1375654135070");
            // 开始请求
            using
            (
                var check = new HttpClient
                {
                    Proxy = null,
                    Encoding = System.Text.Encoding.UTF8,
                    Cookies = PublicStatic.LoginCookies
                }
            )
            {
                check.Headers.Add(System.Net.HttpRequestHeader.Referer, "http://vod.xunlei.com/list.html?userid=" + PublicStatic.NowUserOne.Userid);
                check.DownloadStringAsync(PublicStatic.ClientUri);
                check.DownloadStringCompleted += check_DownloadStringCompleted;
            }
        }

        private static void check_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            string resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;


        }


        /// <summary>
        /// 从本地读取用户信息
        /// </summary>
        public static void LoadLocaLXunLeiUserInfo()
        {
            #region 从本地读取用户信息
            try
            {
                var localTxt = FileCachoHelper.ReadFile(PublicStatic.KcPlayerUserXunLeiInfoDb);
                if (!string.IsNullOrEmpty(localTxt))
                {
                    localTxt = TestVodSafe.DES_Dec_Str(localTxt, PublicStatic.KcPlayerUserXunLeiInfoKeys[0], PublicStatic.KcPlayerUserXunLeiInfoKeys[1]);
                    if (!string.IsNullOrEmpty(localTxt))
                    {
                        PublicStatic.NowUserOne = JsonMapper.ToObject<TestVodVip>(localTxt);
                    }
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {

            }
            #endregion
        }

        /// <summary>
        /// 验证码 - 新建请求 - 开始执行
        /// </summary>
        /// <param name="username"></param>
        public static void LoginThunderYan(string username)
        {
            #region 验证码 - 新建请求 - 开始执行
            // 组合随机数组
            const string fmtDate = "ddd MMM d HH:mm:ss \"\"UTC\"\"zz\"\"00\"\" yyyy";
            var ciDate = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            var jSstring = System.DateTime.Now.ToString(fmtDate, ciDate);
            // 合并请求Uri
            PublicStatic.ClientUri = new System.Uri(string.Format("http://login.xunlei.com/check?u={0}&t={1}", username, System.Web.HttpUtility.UrlEncode(jSstring)));
            PublicStatic.LoginCookies = new System.Net.CookieContainer();
            // 开始请求
            using 
            (
                var check = new HttpClient
                {
                    Proxy = null,
                    Encoding = System.Text.Encoding.UTF8,
                    Cookies = PublicStatic.LoginCookies
                }
            )
            {
                check.Headers.Add(System.Net.HttpRequestHeader.Referer, "http://vod.xunlei.com/home.html");
                check.DownloadStringAsync(PublicStatic.ClientUri);
                check.DownloadStringCompleted += Check_DownloadStringCompleted; 
            }
            #endregion
        }
        /// <summary>
        /// 验证码 - 查看请求 - 接收完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Check_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            #region  验证码 - 查看请求 - 接收完毕
            // 收集Cookies
            PublicStatic.LoginCollection = PublicStatic.LoginCookies.GetCookies(PublicStatic.ClientUri);
            // 取得我们要的值
            var checkResultCookie = PublicStatic.LoginCollection["check_result"];
            if (checkResultCookie != null) PublicStatic.NowUserOne.CheckResult = checkResultCookie.Value;
            var verifyKeyCookie = PublicStatic.LoginCollection["VERIFY_KEY"];
            if (verifyKeyCookie != null) PublicStatic.NowUserOne.VerifyKey = verifyKeyCookie.Value;
            // 获取登陆的校验码
            GetLoginVerifyImage(); 
            #endregion
        }
        /// <summary>
        /// 验证码 - 再次请求 - 接收图片
        /// </summary>
        private static void GetLoginVerifyImage()
        {
            #region 验证码 - 再次请求 - 接收图片
            if (PublicStatic.NowUserOne.CheckResult == "1")
            {
                // 合并请求Uri
                var clientUri = new System.Uri("http://verify.xunlei.com/image");
                // 开始请求
                var check = new HttpClient
                    {
                    Proxy = null,
                    Encoding = System.Text.Encoding.UTF8,
                    Cookies = PublicStatic.LoginCookies
                };
                check.Headers.Add(System.Net.HttpRequestHeader.Referer, "http://vod.xunlei.com/home.html");
                var stream = check.OpenRead(clientUri);
                if (stream != null)
                {
                    var img = System.Drawing.Image.FromStream(stream);
                    if (PublicStatic.YanZhengCode != null)
                    {
                        PublicStatic.YanZhengCode.BackgroundImage = img;
                        PublicStatic.YanZhengCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                        // 修改状态符为True
                        PublicStatic.NowUserOne.IsNeedVerifyImage = true;
                    }
                    else
                    {
                        // 接收失败，再次请求
                        GetLoginVerifyImage();
                    }
                }
                else
                {
                    // 接收失败，再次请求
                    GetLoginVerifyImage();
                }
            }
            else
            {
                // 修改标识符为False
                PublicStatic.NowUserOne.IsNeedVerifyImage = false;
            } 
            #endregion
        }

        /// <summary>
        /// 登　陆 - 新建请求 - 开始登陆
        /// </summary>
        public static void LoginThunder()
        {
            #region 登　陆 - 新建请求 - 开始登陆
            if (PublicStatic.NowUserOne == null) return;
            if (string.IsNullOrEmpty(PublicStatic.NowUserOne.XlUsername)) return;
            if (string.IsNullOrEmpty(PublicStatic.NowUserOne.XlUserpwd)) return;
            if (PublicStatic.NowUserOne.IsNeedVerifyImage)
            {
                if (string.IsNullOrEmpty(PublicStatic.NowUserOne.XlVerifyCode))
                {
                    return;
                }
            }
            // 合成用户密码，三层MD5，在第二层之后加入验证码值
            PublicStatic.NowUserOne.SecurityPassWord =
                PublicStatic.NowUserOne.IsNeedVerifyImage
                ?
                GetMd5Encoding
                (
                    GetMd5Encoding
                    (
                        GetMd5Encoding
                        (
                            PublicStatic.NowUserOne.XlUserpwd
                        )
                    )
                    + PublicStatic.NowUserOne.XlVerifyCode.ToUpper()
                )
                :
                GetMd5Encoding
                (
                    GetMd5Encoding
                    (
                        GetMd5Encoding
                        (
                            PublicStatic.NowUserOne.XlUserpwd
                        )
                    )
                    + PublicStatic.NowUserOne.CheckResult.Substring(2).ToUpper()
                );
            // 合成登陆字符串，最后的校验码还是后几位
            PublicStatic.NowUserOne.LoginPostData =
                string.Format
                (
                    @"u={0}&login_enable=1&login_hour=720&p={1}&verifycode={2}",
                    System.Web.HttpUtility.UrlEncode
                    (
                        PublicStatic.NowUserOne.XlUsername
                    ),
                    PublicStatic.NowUserOne.SecurityPassWord,
                    System.Web.HttpUtility.UrlEncode
                    (
                        PublicStatic.NowUserOne.IsNeedVerifyImage
                        ? PublicStatic.NowUserOne.XlVerifyCode
                        : PublicStatic.NowUserOne.CheckResult.Substring(2).ToUpper()
                    )
                );
            // 合成请求地址
            PublicStatic.ClientUri = new System.Uri("http://login.xunlei.com/sec2login/");
            // 开始请求
            using
            (
                var login = new HttpClient
                {
                    Proxy = null,
                    Encoding = System.Text.Encoding.UTF8,
                    Cookies = PublicStatic.LoginCookies,
                }
            )
            {
                login.Headers.Add(System.Net.HttpRequestHeader.Referer, "http://vod.xunlei.com/");
                login.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                login.UploadStringAsync(PublicStatic.ClientUri, "POST", PublicStatic.NowUserOne.LoginPostData);
                login.UploadStringCompleted += Login_UploadStringCompleted;
            } 
            #endregion
        }

        /// <summary>
        /// 登　陆 - 接收请求 - 开始解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Login_UploadStringCompleted(object sender, System.Net.UploadStringCompletedEventArgs e)
        {
            #region 登　陆 - 接收请求 - 开始解析
            // 收集Cookies
            PublicStatic.LoginCollection = PublicStatic.LoginCookies.GetCookies(PublicStatic.ClientUri);
            if (PublicStatic.LoginCollection == null) return;
            // blogresult
            var blogresultCookie = PublicStatic.LoginCollection["blogresult"];
            if (blogresultCookie != null)
            {
                PublicStatic.NowUserOne.Blogresult = blogresultCookie.Value;
            }
            // active
            var activeCookie = PublicStatic.LoginCollection["active"];
            if (activeCookie != null)
            {
                PublicStatic.NowUserOne.Active = activeCookie.Value;
            }
            // downbyte
            var downbyteCookie = PublicStatic.LoginCollection["downbyte"];
            if (downbyteCookie != null)
            {
                PublicStatic.NowUserOne.DownByte = downbyteCookie.Value;
            }
            // downfile
            var downfileCookie = PublicStatic.LoginCollection["downfile"];
            if (downfileCookie != null)
            {
                PublicStatic.NowUserOne.DownFile = downfileCookie.Value;
            }
            // isspwd
            var isspwdCookie = PublicStatic.LoginCollection["isspwd"];
            if (isspwdCookie != null)
            {
                PublicStatic.NowUserOne.Isspwd = isspwdCookie.Value;
            }
            // isvip
            var isvipCookie = PublicStatic.LoginCollection["isvip"];
            if (isvipCookie != null)
            {
                PublicStatic.NowUserOne.Isvip = isvipCookie.Value;
            }
            // jumpkey
            var jumpkeyCookie = PublicStatic.LoginCollection["jumpkey"];
            if (jumpkeyCookie != null)
            {
                PublicStatic.NowUserOne.Jumpkey = jumpkeyCookie.Value;
            }
            // logintype
            var logintypeCookie = PublicStatic.LoginCollection["logintype"];
            if (logintypeCookie != null)
            {
                PublicStatic.NowUserOne.Logintype = logintypeCookie.Value;
            }
            // lsessionid
            var lsessionidCookie = PublicStatic.LoginCollection["lsessionid"];
            if (lsessionidCookie != null)
            {
                PublicStatic.NowUserOne.Lsessionid = lsessionidCookie.Value;
            }
            // luserid
            var luseridCookie = PublicStatic.LoginCollection["luserid"];
            if (luseridCookie != null)
            {
                PublicStatic.NowUserOne.Luserid = luseridCookie.Value;
            }
            // nickname
            var nicknameCookie = PublicStatic.LoginCollection["nickname"];
            if (nicknameCookie != null)
            {
                PublicStatic.NowUserOne.Nickname = nicknameCookie.Value;
            }
            // onlinetime
            var onlinetimeCookie = PublicStatic.LoginCollection["onlinetime"];
            if (onlinetimeCookie != null)
            {
                PublicStatic.NowUserOne.Onlinetime = onlinetimeCookie.Value;
            }
            // order
            var orderCookie = PublicStatic.LoginCollection["order"];
            if (orderCookie != null)
            {
                PublicStatic.NowUserOne.Order = orderCookie.Value;
            }
            // safe
            var safeCookie = PublicStatic.LoginCollection["safe"];
            if (safeCookie != null)
            {
                PublicStatic.NowUserOne.Safe = safeCookie.Value;
            }
            // score
            var scoreCookie = PublicStatic.LoginCollection["score"];
            if (scoreCookie != null)
            {
                PublicStatic.NowUserOne.Score = scoreCookie.Value;
            }
            // sessionid
            var sessionidCookie = PublicStatic.LoginCollection["sessionid"];
            if (sessionidCookie != null)
            {
                PublicStatic.NowUserOne.Sessionid = sessionidCookie.Value;
            }
            // sex
            var sexCookie = PublicStatic.LoginCollection["sex"];
            if (sexCookie != null)
            {
                PublicStatic.NowUserOne.Sex = sexCookie.Value;
            }
            // upgrade
            var upgradeCookie = PublicStatic.LoginCollection["upgrade"];
            if (upgradeCookie != null)
            {
                PublicStatic.NowUserOne.Upgrade = upgradeCookie.Value;
            }
            // userid
            var useridCookie = PublicStatic.LoginCollection["userid"];
            if (useridCookie != null)
            {
                PublicStatic.NowUserOne.Userid = useridCookie.Value;
            }
            // usernewno
            var usernewnoCookie = PublicStatic.LoginCollection["usernewno"];
            if (usernewnoCookie != null)
            {
                PublicStatic.NowUserOne.Usernewno = usernewnoCookie.Value;
            }
            // usernick
            var usernickCookie = PublicStatic.LoginCollection["usernick"];
            if (usernickCookie != null)
            {
                PublicStatic.NowUserOne.Usernick = usernickCookie.Value;
            }
            // usertype
            var usertypeCookie = PublicStatic.LoginCollection["usertype"];
            if (usertypeCookie != null)
            {
                PublicStatic.NowUserOne.Usertype = usertypeCookie.Value;
            }
            // usrname
            var usrnameCookie = PublicStatic.LoginCollection["usrname"];
            if (usrnameCookie != null)
            {
                PublicStatic.NowUserOne.Usrname = usrnameCookie.Value;
            }
            // 登　陆 - 完成解析 - 做出响应
            Login_MakeRepose(); 
            #endregion
        }

        /// <summary>
        /// 登　陆 - 完成解析 - 做出响应
        /// </summary>
        private static void Login_MakeRepose()
        {
            #region 登　陆 - 完成解析 - 做出响应
            switch (PublicStatic.NowUserOne.Blogresult)
            {
                case "0":
                    {
                        PublicStatic.NowUserOne.Message = "登录成功！";
                    }
                    break;
                case "1":
                    {
                        PublicStatic.NowUserOne.Message = "验证码错误！";
                    }
                    break;
                case "2":
                    {
                        PublicStatic.NowUserOne.Message = "密码错误！";
                    }
                    break;
                case "4":
                    {
                        PublicStatic.NowUserOne.Message = "用户不存在！";
                    }
                    break;
                case "5":
                    {
                        PublicStatic.NowUserOne.Message = "验证错误！请重新登陆！";
                    }
                    break;
                default:
                    {
                        PublicStatic.NowUserOne.Message = "未知错误！请重新登陆！";
                    }
                    break;
            }
            // 校验是否是可用的VIP
            if (!string.IsNullOrEmpty(PublicStatic.NowUserOne.Sessionid) &&
                !string.IsNullOrEmpty(PublicStatic.NowUserOne.Userid))
            {
                PublicStatic.NowUserOne.Message = "登录成功！";
                if (PublicStatic.NowUserOne.Isvip != "0")
                {
                    PublicStatic.NowUserOne.IsVip = true;
                }
            }
            // 合并存储文本并加密存储
            var saveTxt = JsonMapper.ToJson(PublicStatic.NowUserOne);
            if (!string.IsNullOrEmpty(saveTxt))
            {
                saveTxt = TestVodSafe.DES_Enc_Str(saveTxt, "z,x.", "c/1,");
                if (!string.IsNullOrEmpty(saveTxt))
                {
                    SaveFile(saveTxt, "ProgramData/KCPlayer_User_XunLei_Info.db");
                }
            }
            // 弹出提示对话框
            AutoCloseDlg.ShowMessageBoxTimeout(PublicStatic.NowUserOne.Message, @"登陆结果", System.Windows.Forms.MessageBoxButtons.OK, 1000); 
            #endregion
        }

        /// <summary>
        /// String -> Path -> SaveFile
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="path"></param>
        public static void SaveFile(string txt, string path)
        {
            #region String -> Path -> SaveFile

            try
            {
                System.IO.File.WriteAllText(path, txt);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                
            }

            #endregion
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetMd5Encoding(string password)
        {
            #region MD5加密
            var s = string.Empty;
            var md5 = System.Security.Cryptography.MD5.Create();
            var bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (var i = 0; i < bytes.Length; i++)
                s += bytes[i].ToString("x2");
            return s; 
            #endregion
        }
    }

    public class TestVodVip
    {
        public bool IsVip { get; set; }
        public bool IsNeedVerifyImage { get; set; }
        public string SecurityPassWord { get; set; }
        public string XlUsername { get; set; }
        public string XlUserpwd { get; set; }
        public string XlVerifyCode { get; set; }
        public string CheckResult { get; set; }
        public string VerifyKey { get; set; }
        public string LoginPostData { get; set; }
        public string Blogresult { get; set; }
        public string Active { get; set; }
        public string DownByte { get; set; }
        public string DownFile { get; set; }
        public string Isspwd { get; set; }
        public string Isvip { get; set; }
        public string Jumpkey { get; set; }
        public string Logintype { get; set; }
        public string Lsessionid { get; set; }
        public string Luserid { get; set; }
        public string Nickname { get; set; }
        public string Onlinetime { get; set; }
        public string Order { get; set; }
        public string Safe { get; set; }
        public string Score { get; set; }
        public string Sessionid { get; set; }
        public string Sex { get; set; }
        public string Upgrade { get; set; }
        public string Userid { get; set; }
        public string Usernewno { get; set; }
        public string Usernick { get; set; }
        public string Usertype { get; set; }
        public string Usrname { get; set; }
        public string Message { get; set; }
    }

    public class TestVodSafe
    {
        #region DES对称解密算法DES_Dec
        public static string DES_Dec_Str(string str, string key, string vit)
        {
            try
            {
                return System.Text.Encoding.UTF8.GetString(DES_Dec(str, key, vit));
            }
            catch { return null; }
        }
        public static byte[] DES_Dec(string str, string key, string vit)
        {
            //判断向量是否为空,进行默认赋值;
            if (string.IsNullOrEmpty(key)) { key = "KCCT"; }
            //判断向量是否为空,进行默认赋值;
            if (string.IsNullOrEmpty(vit)) { vit = "MNSN"; }
            try
            {
                //实例化加解密类的对象;
                using (var descsp = new System.Security.Cryptography.DESCryptoServiceProvider())
                {
                    //定义字节数组,用来存储要解密的字符串;
                    var data = System.Convert.FromBase64String(str);
                    //实例化内存流对象;
                    using (var mStream = new System.IO.MemoryStream())
                    {
                        //使用内存流实例化解密流对象;
                        using (var cStream = new System.Security.Cryptography.CryptoStream(mStream, descsp.CreateDecryptor(System.Text.Encoding.Unicode.GetBytes(key), System.Text.Encoding.Unicode.GetBytes(vit)), System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            //向解密流中写入数据;
                            cStream.Write(data, 0, data.Length);
                            //释放解密流;
                            cStream.FlushFinalBlock();
                            //返回解密后的字符串;
                            return mStream.ToArray();
                        }
                    }
                }
            }
            catch { return null; }
        }
        #endregion

        #region DES对称加密算法DES_Enc
        public static string DES_Enc_Str(string str, string key, string vit)
        {
            try
            {
                return System.Convert.ToBase64String(DES_Enc(str, key, vit));
            }
            catch { return null; }
        }
        public static byte[] DES_Enc(string str, string key, string vit)
        {
            //判断向量是否为空,进行默认赋值;
            if (string.IsNullOrEmpty(key)) { key = "KCCT"; }
            //判断向量是否为空,进行默认赋值;
            if (string.IsNullOrEmpty(vit)) { vit = "MNSN"; }
            try
            {
                //实例化加解密类的对象;
                using (var descsp = new System.Security.Cryptography.DESCryptoServiceProvider())
                {
                    //定义字节数组,用来存储要加密的字符串;
                    var data = System.Text.Encoding.UTF8.GetBytes(str);
                    //实例化内存流对象;
                    using (var mStream = new System.IO.MemoryStream())
                    {
                        //使用内存流实例化加密流对象;
                        using (var cStream = new System.Security.Cryptography.CryptoStream(mStream, descsp.CreateEncryptor(System.Text.Encoding.Unicode.GetBytes(key), System.Text.Encoding.Unicode.GetBytes(vit)), System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            //向加密流中写入数据;
                            cStream.Write(data, 0, data.Length);
                            //释放加密流;
                            cStream.FlushFinalBlock();
                            //返回加密后的字符串;
                            return mStream.ToArray();
                        }
                    }
                }
            }
            catch { return null; }
        }
        #endregion
    }
}
