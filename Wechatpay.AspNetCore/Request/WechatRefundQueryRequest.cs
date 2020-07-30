using Wechatpay.AspNetCore.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wechatpay.AspNetCore.Request
{
    /// <summary>
    /// 退款查询
    /// </summary>
    [Serializable]
    public class WechatRefundQueryRequest : IWechatRequest<WechatRefundQueryResponse>
    {
        /// <summary>
        /// 微信订单号(微信订单号,商户订单号,商户退款单号,微信退款单号四选一)
        /// </summary>
        /// <remarks>微信生成的订单号，在支付通知中有返回</remarks>
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号(微信订单号,商户订单号,商户退款单号,微信退款单号四选一)
        /// </summary>
        /// <remarks>商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一</remarks>
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商户退款单号(微信订单号,商户订单号,商户退款单号,微信退款单号四选一)
        /// </summary>
        /// <remarks>商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔</remarks>
        [XmlElement("out_refund_no")]
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 微信退款单号(微信订单号,商户订单号,商户退款单号,微信退款单号四选一)
        /// </summary>
        /// <remarks>微信生成的退款单号，在申请退款接口有返回</remarks>
        [XmlElement("微信退款单号")]
        public string RefundId { get; set; }

        /// <summary>
        /// 偏移量
        /// </summary>
        /// <remarks>偏移量，当部分退款次数超过10次时可使用，表示返回的查询结果从这个偏移量开始取记录</remarks>
        [XmlElement("offset")]
        public int Offset { get; set; }
    }
}
