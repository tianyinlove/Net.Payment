using Net.Wechatpay.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Net.Wechatpay.Request
{
    /// <summary>
    /// 退款
    /// </summary>
    public class WechatRefundRequest :IWechatRequest<WechatRefundResponse>
    {
        /// <summary>
        /// 微信订单号(与商户订单号必须二选一)
        /// </summary>
        /// <remarks>微信生成的订单号，在支付通知中有返回</remarks>
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号(与微信订单号必须二选一)
        /// </summary>
        /// <remarks>商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一</remarks>
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商户退款单号(必填)
        /// </summary>
        /// <remarks>商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔</remarks>
        [XmlElement("out_refund_no")]
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 订单金额(必填)
        /// </summary>
        /// <remarks>订单总金额，单位为分，只能为整数</remarks>
        [XmlElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>
        /// 退款金额(必填)
        /// </summary>
        /// <remarks>退款总金额，订单总金额，单位为分，只能为整数</remarks>
        [XmlElement("refund_fee")]
        public int RefundFee { get; set; }

        /// <summary>
        /// 退款货币种类
        /// </summary>
        /// <remarks>退款货币类型，需与支付一致，或者不填。符合ISO 4217标准的三位字母代码，默认人民币：CNY</remarks>
        [XmlElement("refund_fee_type")]
        public string RefundFeeType { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        /// <remarks>若商户传入，会在下发给用户的退款消息中体现退款原因</remarks>
        [XmlElement("refund_desc")]
        public string RefundDesc { get; set; }

        /// <summary>
        /// 退款资金来源
        /// </summary>
        /// <remarks>仅针对老资金流商户使用 REFUND_SOURCE_UNSETTLED_FUNDS---未结算资金退款（默认使用未结算资金退款）REFUND_SOURCE_RECHARGE_FUNDS---可用余额退款</remarks>
        [XmlElement("refund_account")]
        public string RefundAccount { get; set; }

        /// <summary>
        /// 退款结果通知url
        /// </summary>
        /// <remarks>异步接收微信支付退款结果通知的回调地址，通知URL必须为外网可访问的url，不允许带参数,如果参数中传了notify_url，则商户平台上配置的回调地址将不会生效</remarks>
        [XmlElement("notify_url")]
        public string NotifyUrl { get; set; }
    }
}
