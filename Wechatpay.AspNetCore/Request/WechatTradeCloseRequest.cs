using Wechatpay.AspNetCore.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Wechatpay.AspNetCore.Request
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WechatTradeCloseRequest : IWechatRequest<WechatTradeCloseResponse>
    {
        /// <summary>
        /// 商户订单号(与微信订单号必须二选一)
        /// </summary>
        /// <remarks>商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一</remarks>
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

    }
}
