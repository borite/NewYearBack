
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.IO;

using System.Linq;
using System.Net;
using NewYear2020.Entity;
using System.Transactions;
using ChinaAudio.Classes.CodeHeper;
using System.Diagnostics.Contracts;
using System.Collections;

namespace ChinaAudio.Class
{
    public class Common
    {
        Code code = new Code();
        Random rd = new Random();
        #region 小方法

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string sha1(string password)
        {
            var buffer = Encoding.UTF8.GetBytes(password);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }
            string HashPwd = sb.ToString();
            return HashPwd;

        }


        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public string GetSession(string key)
        {
            if (HttpContext.Current.Session[key] == null || HttpContext.Current.Session[key].ToString() == "")
            {
                return null;
            }
            else
            {
                if (key.Length == 0)
                    return string.Empty;

                return HttpContext.Current.Session[key] as string;

            }
            //if (key.Length == 0)
            //    return string.Empty;

            //return HttpContext.Current.Session[key] as string;

        }
        /// <summary>
        /// 读取类型Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public T GetSessionType<T>(string key)
        {
            if (HttpContext.Current.Session[key] == null || HttpContext.Current.Session[key].ToString() == "")
            {
                return default(T);
            }
            else
            {
                if (key.Length == 0)
                    return default(T);
                return (T)HttpContext.Current.Session[key];

            }





        }

        /// <summary>
        /// 写入不同类型的Session 
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public string WriteSessionType<T>(string key, T value)
        {
            if (key.Length == 0)
                return "0"; //没有成功就用0
            HttpContext.Current.Session[key] = value;
            return "1"; //成功状态是1
        }


        /// <summary>
        /// 生成六位随机验证码
        /// </summary>
        /// <returns></returns>
        public string yzmRandom()
        {
            var randnum = rd.Next(100000, 1000000).ToString(); //六位随机数

            return randnum;
        }

        /// <summary>
        /// 生成guid
        /// </summary>
        /// <returns></returns>
        public string GuidFun()
        {

            var uuidN = Guid.NewGuid().ToString("N"); // e0a953c3ee6040eaa9fae2b667060e09
            return uuidN;
        }




        private static object obj = new object();
        private static int GuidInt { get { return Guid.NewGuid().GetHashCode(); } }
        private static string GuidIntStr { get { return Math.Abs(GuidInt).ToString(); } }

        /// <summary>
                /// 生成相对短一点的订单号
                /// </summary>
                /// <param name="mark">前缀</param>
                /// <param name="timeType">时间精确类型  1 日,2 时,3 分，4 秒(默认) </param>
                /// <param name="id">id 小于或等于0则随机生成id</param>
                /// <returns></returns>
        public string Gener(string mark, int timeType = 4, int id = 0)
        {
            lock (obj)
            {
                var number = mark;
                var ticks = (DateTime.Now.Ticks - GuidInt).ToString();
                int fillCount = 0;//填充位数

                number += GetTimeStr(timeType, out fillCount);
                if (id > 0)
                {
                    number += ticks.Substring(ticks.Length - (fillCount + 3)) + id.ToString().PadLeft(10, '0');
                }
                else
                {
                    number += ticks.Substring(ticks.Length - (fillCount + 3)) + GuidIntStr.PadLeft(10, '0');
                }
                return number;
            }
        }

        /// <summary>
                /// 生成长的订单号
                /// </summary>
                /// <param name="mark">前缀</param>
                /// <param name="timeType">时间精确类型  1 日,2 时,3 分，4 秒(默认)</param>
                /// <param name="id">id 小于或等于0则随机生成id</param>
                /// <returns></returns>
        public string GenerLong(string mark, int timeType = 4, long id = 0)
        {
            lock (obj)
            {
                var number = mark;
                var ticks = (DateTime.Now.Ticks - GuidInt).ToString();
                int fillCount = 0;//填充位数

                number += GetTimeStr(timeType, out fillCount);
                if (id > 0)
                {
                    number += ticks.Substring(ticks.Length - fillCount) + id.ToString().PadLeft(19, '0');
                }
                else
                {
                    number += GuidIntStr.PadLeft(10, '0') + ticks.Substring(ticks.Length - (9 + fillCount));
                }
                return number;
            }
        }

        /// <summary>
                /// 获取时间字符串
                /// </summary>
                /// <param name="timeType">时间精确类型  1 日,2 时,3 分，4 秒(默认)</param>
                /// <param name="fillCount">填充位数</param>
                /// <returns></returns>
        private static string GetTimeStr(int timeType, out int fillCount)
        {
            var time = DateTime.Now;
            if (timeType == 1)
            {
                fillCount = 6;
                return time.ToString("yyyyMMdd");
            }
            else if (timeType == 2)
            {
                fillCount = 4;
                return time.ToString("yyyyMMddHH");
            }
            else if (timeType == 3)
            {
                fillCount = 2;
                return time.ToString("yyyyMMddHHmm");
            }
            else
            {
                fillCount = 0;
                return time.ToString("yyyyMMddHHmmss");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">传入数据的类型</typeparam>
        /// <param name="list">把整理的</param>
        /// <returns></returns>

        public string ToJsonString<T>(List<T> list)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(list, settings);
            return result;
        }

        /// <summary>
        /// 随机生成验证码
        /// </summary>


        /// <summary>
        /// 一个时间到现在过了多久（一天，一周，一月）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DateToNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else
            {
                if (span.TotalDays > 30)
                {
                    return
                    "1个月前";
                }
                else
                {
                    if (span.TotalDays > 14)
                    {
                        return
                        "2周前";
                    }
                    else
                    {
                        if (span.TotalDays > 7)
                        {
                            return
                            "1周前";
                        }
                        else
                        {
                            if (span.TotalDays > 1)
                            {
                                return
                                string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
                            }
                            else
                            {
                                if (span.TotalHours > 1)
                                {
                                    return
                                    string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                                }
                                else
                                {
                                    if (span.TotalMinutes > 1)
                                    {
                                        return
                                        string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                                    }
                                    else
                                    {
                                        if (span.TotalSeconds >= 1)
                                        {
                                            return
                                            string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                                        }
                                        else
                                        {
                                            return
                                            "1秒前";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 日期比较
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="writeDate">输入日期</param>
        /// <param name="n">比较天数</param>
        /// <returns>当前日期大（第一个参数大是false）， 写入天数大是true</returns>
        public bool CompareDate(string today, string writeDate)
        {
            DateTime Today = Convert.ToDateTime(today);
            DateTime WriteDate = Convert.ToDateTime(writeDate);

            if (Today >= WriteDate)
                return false;
            else
                return true;
        }






        public class yzmClass
        {


            public string Message { get; set; }
            public string RequestId { get; set; }
            public string BizId { get; set; }
            /// <summary>
            /// Code OK 表示发送成功 前端判断这里状态即可  一般短信过于频繁会报 如果遇到其他问题详见：https://help.aliyun.com/document_detail/101346.html?spm=a2c4g.11186623.6.621.24ce2246Fo3AOU
            /// </summary>
            public string Code { get; set; }

        }








        /// <summary>
        /// 转guid
        /// </summary>
        /// <param name="guidval"></param>
        /// <returns>返回guid格式</returns>
        public dynamic toGuidFun(string guidval)
        {
            Guid gv = new Guid();
            gv = new Guid(guidval);
            return gv;

        }

        /// <summary>
        /// 验证OpenID的方法
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public dynamic GetOpenIDFun(string Code)
        {




            string appid = "wx4a873d58da246519";
            string secret = "65f3c061efbc76fe6a93a9c26c0fc9ef";

            byte[] bs = Encoding.ASCII.GetBytes(Code);    //参数转化为ascii码
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + secret + "&code=" + Code + "&grant_type=authorization_code");  //创建请求
            req.Method = "POST";    //确定传值的方式，此处为post方式传值
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }

            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理(第一次 通过code查openid 和 access_token)

                //坑   这里需要把流数据解析出来再放到字符串
                string codeVal = new StreamReader(wr.GetResponseStream()).ReadToEnd();//解析出来再放到字符串
                TokenMessage jsonData = JsonConvert.DeserializeObject<TokenMessage>(codeVal); //反序列化成json
                TokenMessage tsave = new TokenMessage();  //实例化一个接收两个参数的类
                tsave.openid = jsonData.openid; //赋值接收到的值
                tsave.access_token = jsonData.access_token; //赋值接收到的值
                //if (string.IsNullOrEmpty(tsave.openid) || string.IsNullOrEmpty(tsave.access_token)) //如果请求失败
                //{
                //    return re.SystemError();


                //}
                //再次请求api
                HttpWebRequest req2 = (HttpWebRequest)HttpWebRequest.Create("https://api.weixin.qq.com/sns/userinfo?access_token=" + tsave.access_token + "&openid=" + tsave.openid + "&lang=zh_CN");

                req2.Method = "GET";
                using (WebResponse wr2 = req2.GetResponse())
                {
                    string codeVal2 = new StreamReader(wr2.GetResponseStream()).ReadToEnd();//解析出来再放到字符串
                    //if (string.IsNullOrEmpty(codeVal2)) //如果请求失败
                    //{
                    //    return re.SystemError();
                    //}

                    //序列化成json
                    TokenMessage jsonval = JsonConvert.DeserializeObject<TokenMessage>(codeVal2);
                    return jsonval;

                    //var cc = play.Yl_User.Where(a => a.OpenID == jsonData.openid).FirstOrDefault();

                    //if (cc == null) //如果没有注册
                    //{
                    //    Yl_User res = new Yl_User();
                    //    res.OpenID = jsonval.openid;
                    //    res.Levels = "入门青铜"; //段位
                    //    res.StarsCount = 0; //星星数量
                    //    res.AllGameCount = 0; //总场次
                    //    res.WinGameCount = 0; //赢得比赛
                    //    res.WinProbability = 0; //胜率0
                    //    res.WeChatName = jsonval.nickname; //微信名字
                    //    res.WeChatNum = ""; //微信号
                    //    res.HeadImg = jsonval.headimgurl;

                    //    play.Yl_User.Add(res);
                    //    play.SaveChanges();
                    //    //return re.Success(res);
                    //    return re.CheckOpenIDNull(res.ID.ToString());


                    //}





                }



            }




        }
        public class TokenMessage
        {

            public string access_token;
            /// <summary>
            /// openid
            /// </summary>
            public string openid;
            /// <summary>
            /// 昵称
            /// </summary>
            public string nickname;
            /// <summary>
            /// 头像
            /// </summary>
            public string headimgurl;


        }


        /// <summary>
        /// 卡片增加方法 返回(null 库存空了不能抽奖 success成功结算库存用户+1  busy 请重新尝试，事务回滚或者发生并发 )
        /// </summary>
        /// <param name="openID"></param>
        /// <param name="type">卡片类型：1鼠你富2.鼠你美4.鼠你强5.鼠你甜6.鼠你乐7.尊重卡</param>
        /// <returns></returns>
        public dynamic CardManagerFun(string openID, int type)
        {

            //  启用事务
            using (TransactionScope ts = new TransactionScope())
            {
                //上下文
                using (YL_NewYear2020Entities NY = new YL_NewYear2020Entities())
                {

                    //库存表
                    var insertUser = NY.CardList.Where(a => a.ID == type).First();
                    if (insertUser.Total <= 0)
                    {
                        return "null";
                        //   return CodeNew.Success(HttpStatusCode.OK, "鼠你富库存-1用户卡+1", "");

                    }

                    insertUser.Total -= 1;

                    //查找个人用户
                    var userAdd = NY.User.Where(a => a.OpenID == openID).First();

                    switch (type)
                    {
                        //鼠你富
                        case 1:



                            ////并发测试 进行第二个上下文并保存  第一个上下文数据不保存，放在下面的try尝试保存
                            //using (YL_NewYear2020Entities NYc = new YL_NewYear2020Entities())
                            //{
                            //    var dd = NYc.CardList.Where(a => a.ID == 1).First();
                            //    dd.Total -= 1000;
                            //    NYc.SaveChanges();


                            //}


                            try
                            {
                                //这里是事务测试
                                //   NY.SaveChanges(); //上面保存，如果不加这个事务这条数据将会更改，加上事务，下面发生错误此条数据将不会发生改变
                                //var userAdd = NY.User.Where(a => a.OpenID == openID).First(); 
                                //int c = 0;
                                //userAdd.c1snf -= 1000 / c;//出现错误，测试回滚，是否可以到catch
                                userAdd.c1snf += 1;

                                NY.SaveChanges();

                                ts.Complete();

                                return "success";
                            }
                            catch (Exception ex) //并发或者事务会报busy，需要重试
                            {
                                //  ts.Complete();
                                return "busy";
                            }



                        case 2:

                            try
                            {
                                userAdd.c2snm += 1;
                                NY.SaveChanges();
                                ts.Complete();

                                return "success";
                            }
                            catch (Exception ex)
                            {
                                //  ts.Complete();
                                return "busy";
                            }
                        case 4:

                            try
                            {
                                userAdd.c3snq += 1;
                                NY.SaveChanges();
                                ts.Complete();
                                return "success";
                            }
                            catch (Exception ex)
                            {
                                //  ts.Complete();
                                return "busy";
                            }
                        case 5:

                            try
                            {
                                userAdd.c5snt += 1;
                                NY.SaveChanges();
                                ts.Complete();
                                return "success";
                            }
                            catch (Exception ex)
                            {
                                //  ts.Complete();
                                return "busy";
                            }
                        case 6:

                            try
                            {
                                userAdd.c4snl += 1;
                                NY.SaveChanges();
                                ts.Complete();
                                return "success";
                            }
                            catch (Exception ex)
                            {
                                //  ts.Complete();
                                return "busy";
                            }
                        case 7:

                            try
                            {
                                userAdd.c6zzk += 1;

                                NY.SaveChanges();
                                ts.Complete();
                                return "success";
                            }
                            catch (Exception ex)
                            {
                                //  ts.Complete();
                                return "busy";
                            }
                        default:

                            return CodeNew.Success(HttpStatusCode.BadRequest, "参数无效", "");





                    }

                }


            }

        }


        //public static bool IsBetween(this DateTime thisDateTime, DateTime start, DateTime end)
        //{
        //    return thisDateTime >= start && thisDateTime <= end;


        //}

        public static bool isToday(DateTime dt)
        {
            DateTime today = DateTime.Today;
            DateTime tempToday = new DateTime(dt.Year, dt.Month, dt.Day);
            if (today == tempToday)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 全局时间管理
        /// </summary>
        /// <returns></returns>
        public static dynamic TimeManager()
        {
            DateTime end1 = new DateTime(2020, 1, 20);
            DateTime end21 = new DateTime(2020, 1, 21);
            DateTime end22 = new DateTime(2020, 1, 22);
            DateTime end23 = new DateTime(2020, 1, 23);
            DateTime end24 = new DateTime(2020, 1, 24);


            //时间判断布尔判断是否是今天
            bool day20 = isToday(end1);
            bool day21 = isToday(end21);
            bool day22 = isToday(end22);
            bool day23 = isToday(end23);
            bool day24 = isToday(end24);
            var ListVal = new { day20, day21, day22, day23, day24 };

            //ArrayList TimeList = new ArrayList();
            //TimeList.Add(ListVal);
            return ListVal;



        }

        /// <summary>
        /// 每日抽牌数量管理（全局）
        /// </summary>

        public class  CardManager
        { 

            /// <summary>
            /// 20号抽牌限制 （低于8000不再抽该牌）
            /// </summary>
            public static int num20 = 8000;
      
            public static int num21 = 6000;
            public static int num22 = 4000;
             public static int num23 = 2000;
            public static int num24 = 0;

            //每天抽几次
            DateTime nowtimeDate = DateTime.Now;
            int rate = 2;


        }


        /// <summary>
        /// 尊重卡使用，随机抽一张牌方法
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public dynamic CardFun(string openID)
        {
           // int num20 = CardManager.num20;
           // int num21 = CardManager.num21;
           // int num22 = 4000;
          //  int num23 = 2000;
           // int num24 = 0;
            var manager = TimeManager();
            bool day20 = manager.day20;
            bool day21 = manager.day21;
            bool day22 = manager.day22;
            bool day23 = manager.day23;
            bool day24 = manager.day24;

            //每天抽几次
         //   DateTime nowtimeDate = DateTime.Now;
           // int rate = 2;

            using (YL_NewYear2020Entities NY = new YL_NewYear2020Entities())
            {

                if (day20) //20日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= CardManager.num20).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();
                  
                    var res = CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                        
                        NY.SaveChanges();



                        return ( CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return (CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }

                if (day21) //21日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= CardManager.num21).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res =CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                       
                        NY.SaveChanges();



                        return ( CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return ( CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }
                if (day22) //21日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= CardManager.num22).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res = CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                        
                        NY.SaveChanges();



                        return ( CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return (CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }
                if (day23) //21日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= CardManager.num23).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res = CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                        
                        NY.SaveChanges();



                        return ( CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return ( CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }
                if (day24) //21日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= CardManager.num24).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res = CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                       
                        NY.SaveChanges();



                        return ( CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return (CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }
                else
                {
                    return ( CodeNew.Success(HttpStatusCode.NotFound, "游戏活动时间已经结束 ,不能抽牌", ""));
                }
            }
        }





        #endregion







    }
}