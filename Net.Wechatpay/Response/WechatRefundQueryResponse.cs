using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Net.Wechatpay.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class WechatRefundQueryResponse : WechatResponse
    {
        /// <summary>
        /// 订单总退款次数
        /// </summary>
        /// <remarks>订单总共已发生的部分退款次数，当请求参数传入offset后有返回</remarks>
        [XmlElement("total_refund_count")]
        public int TotalRefundCount { get; set; }

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
        /// 退款笔数
        /// </summary>
        /// <remarks>当前返回退款笔数</remarks>
        [XmlElement("refund_count")]
        public int RefundCount { get; set; }

        #region 以下字段有下标
        /// <summary>
        /// 商户退款单号
        /// </summary>
        /// <remarks>商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔</remarks>
        [XmlElement("out_refund_no_0")]
        public string OutRefundNo_0 { get; set; }

        /// <summary>
        /// 微信退款单号
        /// </summary>
        [XmlElement("refund_id_0")]
        public string RefundId_0 { get; set; }

        /// <summary>
        /// 退款渠道
        /// </summary>
        /// <remarks>ORIGINAL—原路退款 BALANCE—退回到余额 OTHER_BALANCE—原账户异常退到其他余额账户 OTHER_BANKCARD—原银行卡异常退到其他银行卡</remarks>
        [XmlElement("refund_channel_0")]
        public string RefundChannel_0 { get; set; }

        /// <summary>
        /// 申请退款金额
        /// </summary>
        /// <remarks>退款总金额,单位为分,可以做部分退款</remarks>
        [XmlElement("refund_fee_0")]
        public int RefundFee_0 { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        /// <remarks>退款金额=申请退款金额-非充值代金券退款金额，退款金额＜=申请退款金额</remarks>
        [XmlElement("settlement_refund_fee_0")]
        public int SettlementRefundFee_0 { get; set; }

        /// <summary>
        /// 退款状态
        /// </summary>
        /// <remarks>退款状态：SUCCESS—退款成功 REFUNDCLOSE—退款关闭。PROCESSING—退款处理中CHANGE—退款异常，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，可前往商户平台（pay.weixin.qq.com）-交易中心，手动处理此笔退款</remarks>
        [XmlElement("refund_status_0")]
        public string RefundStatus_0 { get; set; }

        /// <summary>
        /// 退款资金来源
        /// </summary>
        /// <remarks>REFUND_SOURCE_RECHARGE_FUNDS---可用余额退款/基本账户 REFUND_SOURCE_UNSETTLED_FUNDS---未结算资金退款</remarks>
        [XmlElement("refund_account_0")]
        public string RefundAccount_0 { get; set; }

        /// <summary>
        /// 退款入账账户
        /// </summary>
        /// <remarks>取当前退款单的退款入账方 1）退回银行卡：{银行名称}{卡类型}{卡尾号} 2）退回支付用户零钱:支付用户零钱 3）退还商户:商户基本账户 商户结算银行账户 4）退回支付用户零钱通:支付用户零钱通</remarks>
        [XmlElement("refund_recv_accout_0")]
        public string RefundRecvAccout_0 { get; set; }

        /// <summary>
        /// 退款成功时间
        /// </summary>
        /// <remarks>退款成功时间，当退款状态为退款成功时有返回</remarks>
        [XmlElement("refund_success_time_0")]
        public string RefundSuccessTime_0 { get; set; }
        #endregion
    }
}
