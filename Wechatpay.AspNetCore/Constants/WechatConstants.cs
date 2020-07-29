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
        public const string MD5 = "MD5";
        /// <summary>
        /// 
        /// </summary>
        public const string HMAC_SHA256 = "HMAC-SHA256";
        /// <summary>
        /// 
        /// </summary>
        public const string APP = "APP";

        /// <summary>
        /// 
        /// </summary>
        public const string SUCCESS = "SUCCESS";
        /// <summary>
        /// 
        /// </summary>
        public const string REFUND = "REFUND";
        /// <summary>
        /// 
        /// </summary>
        public const string CLOSED = "CLOSED";

        #region APP端调起支付的参数列表

        /// <summary>
        /// 应用ID
        /// </summary>
        public const string APPID = "appid";
        /// <summary>
        /// 商户号mch_id
        /// </summary>
        public const string PARTNERID = "partnerid";
        /// <summary>
        /// 预支付交易会话ID
        /// </summary>
        public const string PREPAYID = "prepayid";
        /// <summary>
        /// 扩展字段
        /// </summary>
        public const string PACKAGE = "package";
        /// <summary>
        /// 随机字符串
        /// </summary>
        public const string NONCESTR = "noncestr";
        /// <summary>
        /// 时间戳
        /// </summary>
        public const string TIMESTAMP = "timestamp";
        /// <summary>
        /// 签名
        /// </summary>
        public const string SIGN = "sign";

        #endregion APP端调起支付的参数列表

        #region 接口地址
        /// <summary>
        /// 创建订单接口
        /// </summary>
        public const string UnifiedOrderUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        /// <summary>
        /// 订单查询接口
        /// </summary>
        public const string QeuryOrderUrl = "https://api.mch.weixin.qq.com/pay/orderquery";

        /// <summary>
        /// 订单撤销接口
        /// </summary>
        public const string ReverseOrderUrl = "https://api.mch.weixin.qq.com/secapi/pay/reverse";

        /// <summary>
        /// 订单退款接口
        /// </summary>
        public const string RefundOrderUrl = "https://api.mch.weixin.qq.com/secapi/pay/refund";

        /// <summary>
        /// 订单退款查询接口
        /// </summary>
        public const string RefundOrderQueryUrl = "https://api.mch.weixin.qq.com/pay/refundquery";

        /// <summary>
        /// 订单关闭接口
        /// </summary>
        public const string CloseOrderUrl = "https://api.mch.weixin.qq.com/pay/closeorder";

        #endregion 接口地址
    }
}
