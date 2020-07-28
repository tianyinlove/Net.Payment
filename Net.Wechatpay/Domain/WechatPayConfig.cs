using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Wechatpay
{
    /// <summary>
    /// 配置账号信息
    /// </summary>
    public class WechatPayConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public WechatPayConfig()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public WechatPayConfig(string appid, string mchid, string key)
        {
            APPID = appid;
            MCHID = mchid;
            SignKey = key;
        }

        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        /// <summary>
        /// 应用APPID
        /// </summary>
        public string APPID { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MCHID { get; set; }

        /// <summary>
        /// 用于签名的Key
        /// </summary>
        public string SignKey { get; set; }

        /// <summary>
        /// 签名类型，目前支持HMAC-SHA256和MD5，默认为MD5
        /// </summary>
        public string SignType { get; set; } = "MD5";

        /// <summary>
        /// 
        /// </summary>
        public string APPSECRET { get; set; } = "";

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public string SSLCERT_PATH { get; set; } = "Config/WechatCert/apiclient_cert.p12";
        /// <summary>
        /// 
        /// </summary>
        public string SSLCERT_PASSWORD { get; set; } = "";

        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public string NOTIFY_URL { get; set; } = "";

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public string IP { get; set; } = "8.8.8.8";

        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public string PROXY_URL { get; set; } = "http://0.0.0.0:0";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public int REPORT_LEVENL { get; set; } = 1;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public int LOG_LEVENL { get; set; } = 3;
    }
}
