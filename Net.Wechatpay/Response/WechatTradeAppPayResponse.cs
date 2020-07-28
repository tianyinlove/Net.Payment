using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Net.Wechatpay.Response
{
    /// <summary>
    /// 统一下单
    /// </summary>
    public class WechatTradeAppPayResponse : WechatResponse
    {
        /// <summary>
        /// 预支付交易会话标识（微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时）
        /// </summary>
        [XmlElement("prepay_id")]
        [JsonProperty("prepay_id")]
        public string PrepayId { get; set; }
        /// <summary>
        /// 交易类型 （调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，详细说明见参数规定）
        /// </summary>
        [XmlElement("trade_type")]
        [JsonProperty("trade_type")]
        public string TradeType { get; set; }
    }
}
