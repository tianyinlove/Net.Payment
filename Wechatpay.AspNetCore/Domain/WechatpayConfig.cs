using System;
using System.Collections.Generic;
using System.Text;
using Wechatpay.AspNetCore.Constants;

namespace Wechatpay.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    public class WechatpayConfig
    {
        /// <summary>
        /// 是否上线
        /// </summary>
        public bool IsOnline { get; set; } = true;

        /// <summary>
        /// 支付成功回调接口
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 账号配置
        /// </summary>
        public List<WechatAccountConfig> AccountList { get; set; }
    }

    /// <summary>
    /// 配置账号信息
    /// </summary>
    public class WechatAccountConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public string TradeType { get; set; } = WechatConstants.APP;

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
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 用于签名的Key
        /// </summary>
        public string SignKey { get; set; }

        /// <summary>
        /// 签名类型，目前支持HMAC-SHA256和MD5，默认为MD5
        /// </summary>
        public string SignType { get; set; } = WechatConstants.MD5;

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public string CertPath { get; set; } = "Config\\apiclient_cert.p12";
        /// <summary>
        /// 
        /// </summary>
        public string CertPassword { get; set; } = "";
    }
}
