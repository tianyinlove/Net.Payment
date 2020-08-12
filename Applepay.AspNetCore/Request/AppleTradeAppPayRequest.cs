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
    public class AppleTradeAppPayRequest : IApplepayRequest<AppleTradeAppPayResponse>
    {
        /// <summary>
        /// 支付订单号
        /// </summary>
        [XmlElement("outtradeno")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [XmlElement("productid")]
        public string ProductId { get; set; }
    }
}
