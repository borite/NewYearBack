using NewYear2020.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewYear2020.DTO
{

    /// <summary>
    /// 返回字段说明
    /// </summary>
    public class UserInfoReturnDTO
    {


        /// <summary>
        /// openid
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string JobID { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 礼物ID
        /// </summary>
        public Nullable<int> PrizeID { get; set; }
        /// <summary>
        /// 鼠你富
        /// </summary>
        public Nullable<int> c1snf { get; set; }
        /// <summary>
        /// 鼠你美
        /// </summary>
        public Nullable<int> c2snm { get; set; }
        /// <summary>
        /// 鼠你强
        /// </summary>
        public Nullable<int> c3snq { get; set; }
        /// <summary>
        /// 鼠你乐
        /// </summary>
        public Nullable<int> c4snl { get; set; }
        /// <summary>
        /// 鼠你甜
        /// </summary>
        public Nullable<int> c5snt { get; set; }
        /// <summary>
        /// 尊重卡
        /// </summary>
        public Nullable<int> c6zzk { get; set; }
        /// <summary>
        /// 爱奇艺序列号
        /// </summary>
        public string aiqiyiKey { get; set; }
        /// <summary>
        /// 获得礼物的时间
        /// </summary>
        public Nullable<System.DateTime> GetPrizeTime { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string WeChatNickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadIMG { get; set; }
    }


    /// <summary>
    /// 用户注册传入参数说明
    /// </summary>
    public class UserRegisterDTO
    {
        /// <summary>
        /// openid
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string JobID { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string WeChatNickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadIMG { get; set; }






    }


    /// <summary>
    /// 赠卡返回
    /// </summary>

    public class GetCardListDTO
    {

        public int ID { get; set; }
        public string OpenID { get; set; }
        public string ToOpenID { get; set; }
        public int CardID { get; set; }
        public System.DateTime GiveTime { get; set; }
        public string UserName { get; set; }
        public string CardName { get; set; }

        public virtual CardList CardList { get; set; }



    }


    /// <summary>
    /// 用户送一张卡
    /// </summary>
    public class GivenCardDTO
    {
        /// <summary>
        /// 发起赠送用户
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 被给与人用户
        /// </summary>
        public string toOpenID { get; set; }
        /// <summary>
        /// 1.属你富2.鼠你美4.鼠你强5.鼠你甜6.鼠你乐7.尊重卡
        /// </summary>
        public int CardID { get; set; }


    }

    /// <summary>
    /// 中奖输入地址DTO
    /// </summary>
    public class AddressDTO
    {
        /// <summary>
        /// 发起赠送用户
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 被给与人用户
        /// </summary>
        public string Address { get; set; }
      


    }

}