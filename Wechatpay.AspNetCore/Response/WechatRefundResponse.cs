using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Wechatpay.AspNetCore.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class WechatRefundResponse : WechatResponse
    {
        /// <summary>
        /// 微信订单号(必填)
        /// </summary>
        /// <remarks>微信生成的订单号，在支付通知中有返回</remarks>
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号(必填) 
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
        /// 微信退款单号(必填)
        /// </summary>
        [XmlElement("微信退款单号")]
        public string RefundId { get; set; }

        /// <summary>
        /// 退款金额(必填)
        /// </summary>
        /// <remarks>退款总金额,单位为分,可以做部分退款</remarks>
        [XmlElement("refund_fee")]
        public int RefundFee { get; set; }

        /// <summary>
        /// 应结退款金额
        /// </summary>
        /// <remarks>去掉非充值代金券退款金额后的退款金额，退款金额=申请退款金额-非充值代金券退款金额，退款金额＜=申请退款金额</remarks>
        [XmlElement("settlement_refund_fee")]
        public int SettlementRefundFee { get; set; }

        /// <summary>
        /// 标价金额(必填)
        /// </summary>
        /// <remarks>订单总金额，单位为分，只能为整数</remarks>
        [XmlElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>
        /// 应结订单金额
        /// </summary>
        /// <remarks>去掉非充值代金券金额后的订单总金额，应结订单金额=订单金额-非充值代金券金额，应结订单金额＜=订单金额。 </remarks>
        [XmlElement("settlement_total_fee")]
        public int SettlementTotalFee { get; set; }

        /// <summary>
        /// 标价币种
        /// </summary>
        /// <remarks>订单金额货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY</remarks>
        [XmlElement("fee_type")]
        public string FeeType { get; set; }

        /// <summary>
        /// 现金支付金额(必填)
        /// </summary>
        /// <remarks>现金支付金额，单位为分，只能为整数</remarks>
        [XmlElement("cash_fee")]
        public string CashFee { get; set; }

        /// <summary>
        /// 现金支付币种
        /// </summary>
        /// <remarks>货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY</remarks>
        [XmlElement("cash_fee_type")]
        public string CashFeeType { get; set; }

        /// <summary>
        /// 现金退款金额
        /// </summary>
        /// <remarks>现金退款金额，单位为分，只能为整数</remarks>
        [XmlElement("cash_refund_fee")]
        public string CashRefundFee { get; set; }
    }
}
