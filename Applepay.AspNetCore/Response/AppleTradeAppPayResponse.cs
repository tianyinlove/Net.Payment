using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Applepay.AspNetCore.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class AppleTradeAppPayResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [XmlElement("outtradeno")]
        [JsonProperty("outtradeno")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [XmlElement("productid")]
        [JsonProperty("productid")]
        public string ProductId { get; set; }

        /// <summary>
        /// 加密串
        /// </summary>
        [XmlElement("sign")]
        [JsonProperty("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 支付成功回调地址
        /// </summary>
        [XmlElement("notifyurl")]
        [JsonProperty("notifyurl")]
        public string NotifyUrl { get; set; }
    }
}
