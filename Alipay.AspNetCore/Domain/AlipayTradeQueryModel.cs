using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Alipay.AspNetCore.Domain
{
    /// <summary>
    /// AlipayTradeQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayTradeQueryModel : AopObject
    {
        /// <summary>
        /// 银行间联模式下有用，其它场景请不要使用；  双联通过该参数指定需要查询的交易所属收单机构的pid;
        /// </summary>
        [XmlElement("org_pid")]
        public string OrgPid { get; set; }

        /// <summary>
        /// 订单支付时传入的商户订单号,和支付宝交易号不能同时为空。  trade_no,out_trade_no如果同时存在优先取trade_no
        /// </summary>
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 查询选项，商户通过上送该参数来定制同步需要额外返回的信息字段，数组格式。如：["trade_settle_info","fund_bill_list","voucher_detail_list","discount_goods_detail"]
        /// </summary>
        [XmlArray("query_options")]
        [XmlArrayItem("string")]
        public List<string> QueryOptions { get; set; }

        /// <summary>
        /// 支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        [XmlElement("trade_no")]
        public string TradeNo { get; set; }
    }
}
