﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace ChinaAudio.Class
{

    public class Code
    {
        public string code;
        public dynamic message;
        public dynamic data;
        public dynamic data1;

        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        JsonSerializerSettings settings = new JsonSerializerSettings();

        //public string ServerManNull()
        //{
        //    var b = new Code { code = "213", message = "没有业务员记录", data = "" };

        //    var returnval = jsonSerializer.Serialize(b);

        //    return returnval;
        //}
        /// <summary>
        /// 返回成功状态200 ，需要自己返回提示信息
        /// </summary>
        /// <param name="message">返回一些</param>
        /// <returns></returns>
        public string returnSussess(dynamic message)
        {
            var b = new Code { code = "200", message = "操作成功", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }
        /// <summary>
        /// 成功2个参数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string returnSussess2(dynamic message,dynamic data)
        {
            var b = new Code { code = "200", message = message, data = data };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }
        /// <summary>
        ///  这是第一次登陆201
        /// </summary>
        /// <param name="tokenVal">返回的token</param>
        /// <param name="userInfo">返回的用户信息</param>
        /// <returns></returns>
        public string loginSuccessFirst(dynamic tokenVal ,dynamic userInfo)
        {
            var b = new Code { code = "201", message = tokenVal, data1 = userInfo };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }



        /// <summary>
        /// 更改密码成功202
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Changepwd(dynamic message)
        {
            var b = new Code { code = "202", message = "更改密码成功", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }
        /// <summary>
        /// 登陆成功203
        /// </summary>
        /// <param name="tokenVal"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public string loginSuccess(dynamic tokenVal, dynamic userInfo)
        {
            var b = new Code { code = "203", message = tokenVal, data1 = userInfo };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }

        /// <summary>
        /// 失败,返回500
        /// </summary>
        /// <param name="message">需要说明情况</param>
        /// <returns></returns>
        public string returnFail(dynamic message)
        {
            var b = new Code { code = "500", message = "操作失败", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }
        /// <summary>
        /// 上传信息不全501
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string InfoNull(dynamic message)
        {
            var b = new Code { code = "501", message = "信息不全", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }

        /// <summary>
        /// 验证码是空的，还有没有验证过502
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string yzmIsNull(dynamic message)
        {
            var b = new Code { code = "502", message = "验证码是空的", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }

        /// <summary>
        /// 已经注册了503
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string registered(dynamic message)
        {
            var b = new Code { code = "503", message = "已经有人注册过了", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }
        /// <summary>
        /// 验证码错误504
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string yzmError(dynamic message)
        {
            var b = new Code { code = "504", message = "验证码错误", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }

        /// <summary>
        /// 验证码超时505
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string yzmTimeError(dynamic message)
        {
            var b = new Code { code = "505", message = "验证码超时", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }
        /// <summary>
        /// token是空的,没有找到506
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string TokenIsNull(dynamic message)
        {
            var b = new Code { code = "506", message = "token是空的,没有找到", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }


        /// <summary>
        /// 
        /// token过期，重新登陆507
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string TokenTimeExpiress(dynamic message)
        {
            var b = new Code { code = "507", message = "token过期，重新登陆！", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }
        /// <summary>
        /// 验证码发送失败508
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string sendError(dynamic message)
        {
            var b = new Code { code = "508", message = "验证码失败", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }

        /// <summary>
        /// 509账户被锁定
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string AccountLocked(dynamic message)
        {
            var b = new Code { code = "509", message = "账户被锁定", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }

        /// <summary>
        /// 密码输入错误510
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string passwordError(dynamic message)
        {
            var b = new Code { code = "510", message = "密码输入错误", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }

        /// <summary>
        /// 没有找到该账户511
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string AccountNull(dynamic message)
        {
            var b = new Code { code = "511", message = "没有找到该账户", data1 = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }


        /// <summary>
        /// 错误通用2 512
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string infoSecondError(dynamic message)
        {
            var b = new Code { code = "512", message = message };


            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(b, settings);
            return result;
        }

        //public string yzmIsNull(dynamic message)
        //{
        //    var b = new Code { code = "512", message = "验证码是空的，需要先找到发送验证码的api", data1 = message };


        //    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //    string result = JsonConvert.SerializeObject(b, settings);
        //    return result;
        //}



        /// <summary>
        /// 判断阿里云是否发送成功的类 成功返回200，失败返回508
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>

        public string duanxin(string message)
        {
            aliyunSendMessage xulie = JsonConvert.DeserializeObject<aliyunSendMessage>(message); //反序列化一下  json到字符串是序列化  字符串到json是反序列化


            string panduan = xulie.Code;
            if (panduan == "OK")
            {
                return returnSussess("注册验证码发送成功");
            }
            else
            {

                return sendError(panduan);



            }



        }
        public class aliyunSendMessage
        {
            public string Code; //状态码
            public string Message; //信息
            public string RequestId;
            public string BizId;
        }

    }
}