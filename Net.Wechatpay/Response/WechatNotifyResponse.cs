using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Net.Wechatpay.Response
{
    /// <summary>
    /// 微信支付成功回调
    /// </summary>
    [Serializable]
    public class WechatNotifyResponse: WechatResponse
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        [XmlElement("openid")]
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        /// <summary>
        /// 是否关注公众账号 (用户是否关注公众账号，Y-关注，N-未关注)
        /// </summary>
        [XmlElement("is_subscribe")]
        [JsonProperty("is_subscribe")]
        public string IsSubscribe { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [XmlElement("trade_type")]
        [JsonProperty("trade_type")]
        public string TradeType { get; set; }
        /// <summary>
        /// 交易状态
        /// SUCCESS—支付成功
        /// REFUND—转入退款
        /// NOTPAY—未支付
        /// CLOSED—已关闭
        /// REVOKED—已撤销（刷卡支付）
        /// USERPAYING--用户支付中
        /// PAYERROR--支付失败(其他原因，如银行返回失败)
        /// 支付状态机请见下单API页面
        /// </summary>
        [XmlElement("trade_state")]
        [JsonProperty("trade_state")]
        public string TradeState { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        [XmlElement("bank_type")]
        [JsonProperty("bank_type")]
        public string BankType { get; set; }
        /// <summary>
        /// 总金额(订单总金额，单位为分)
        /// </summary>
        [XmlElement("total_fee")]
        [JsonProperty("total_fee")]
        public int TotalFee { get; set; }
        /// <summary>
        /// 货币种类
        /// </summary>
        [XmlElement("fee_type")]
        [JsonProperty("fee_type")]
        public string FeeType { get; set; }
        /// <summary>
        /// 现金支付金额(单位为分)
        /// </summary>
        [XmlElement("cash_fee")]
        [JsonProperty("cash_fee")]
        public int CashFee { get; set; }
        /// <summary>
        /// 现金支付货币类型
        /// </summary>
        [XmlElement("cash_fee_type")]
        [JsonProperty("cash_fee_type")]
        public string CashFeeType { get; set; }
        /// <summary>
        /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。详见商户订单号
        /// </summary>
        [XmlElement("out_trade_no")]
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        [XmlElement("transaction_id")]
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
        /// <summary>
        /// 附加数据，原样返回
        /// </summary>
        [XmlElement("attach")]
        [JsonProperty("attach")]
        public string Attach { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        [XmlElement("time_end")]
        [JsonProperty("time_end")]
        public string TimeEnd { get; set; }
    }
}
