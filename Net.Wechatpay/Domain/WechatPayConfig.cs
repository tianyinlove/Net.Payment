using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Wechatpay
{
    /// <summary>
    /// 配置账号信息
    /// </summary>
    public class WechatpayConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public WechatpayConfig()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public WechatpayConfig(string appid, string mchid, string key)
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

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public string SSLCERT_PATH { get; set; } = "Config/WechatCert/apiclient_cert.p12";
        /// <summary>
        /// 
        /// </summary>
        public string SSLCERT_PASSWORD { get; set; } = "";
    }
}
