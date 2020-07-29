using System;
using System.Collections.Generic;
using System.Text;

namespace Wechatpay.AspNetCore.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public class WechatConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public static string MD5 = "MD5";
        /// <summary>
        /// 
        /// </summary>
        public static string HMAC_SHA256 = "HMAC-SHA256";
        /// <summary>
        /// 
        /// </summary>
        public static string APP = "APP";

        /// <summary>
        /// 
        /// </summary>
        public static string SUCCESS = "SUCCESS";
        /// <summary>
        /// 
        /// </summary>
        public static string REFUND = "REFUND";
        /// <summary>
        /// 
        /// </summary>
        public static string CLOSED = "CLOSED";

        #region APP端调起支付的参数列表

        /// <summary>
        /// 应用ID
        /// </summary>
        public static string APPID = "appid";
        /// <summary>
        /// 商户号mch_id
        /// </summary>
        public static string PARTNERID = "partnerid";
        /// <summary>
        /// 预支付交易会话ID
        /// </summary>
        public static string PREPAYID = "prepayid";
        /// <summary>
        /// 扩展字段
        /// </summary>
        public static string PACKAGE = "package";
        /// <summary>
        /// 随机字符串
        /// </summary>
        public static string NONCESTR = "noncestr";
        /// <summary>
        /// 时间戳
        /// </summary>
        public static string TIMESTAMP = "timestamp";
        /// <summary>
        /// 签名
        /// </summary>
        public static string SIGN = "sign";

        #endregion APP端调起支付的参数列表
    }
}
