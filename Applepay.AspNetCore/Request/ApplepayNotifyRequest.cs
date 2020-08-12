using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Applepay.AspNetCore.Response;

namespace Applepay.AspNetCore.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplepayNotifyRequest : IApplepayRequest<ApplepayNotifyResponse>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [XmlElement("outtradeno")]
        [JsonProperty("outtradeno")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 加密串
        /// </summary>
        [XmlElement("sign")]
        [JsonProperty("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 苹果内购的验证收据
        /// </summary>
        [XmlElement("applereceipt")]
        [JsonProperty("applereceipt")]
        public string AppleReceipt { get; set; }
    }
}
