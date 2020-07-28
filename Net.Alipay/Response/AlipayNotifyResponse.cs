using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Net.Alipay.Response
{
    /// <summary>
    /// 支付宝支付成功回调
    /// </summary>
    public class AlipayNotifyResponse
    {
        /// <summary>
        /// 通知时间
        /// </summary>
        [XmlElement("notify_time")]
        [JsonProperty("notify_time")]
        public DateTime NotifyTime { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        [XmlElement("notify_type")]
        [JsonProperty("notify_type")]
        public string NotifyType { get; set; }

        /// <summary>
        /// 通知校验ID
        /// </summary>
        [XmlElement("notify_id")]
        [JsonProperty("notify_id")]
        public string NotifyId { get; set; }

        /// <summary>
        /// 支付宝分配给开发者的应用Id
        /// </summary>
        [XmlElement("app_id")]
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// 编码格式
        /// </summary>
        public string Charset { get; set; }

        /// <summary>
        /// 接口版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        [XmlElement("sign_type")]
        [JsonProperty("sign_type")]
        public string SignType { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [XmlElement("trade_no")]
        [JsonProperty("trade_no")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [XmlElement("out_trade_no")]
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        [XmlElement("trade_status")]
        [JsonProperty("trade_status")]
        public string TradeStatus { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        [XmlElement("total_amount")]
        [JsonProperty("total_amount")]
        public decimal Total { get; set; }

        /// <summary>
        /// 交易付款时间
        /// </summary>
        [XmlElement("gmt_payment")]
        [JsonProperty("gmt_payment")]
        public DateTime GmtPayment { get; set; }
    }
}
