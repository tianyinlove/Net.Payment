using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Wechatpay.AspNetCore.Response;

namespace Wechatpay.AspNetCore.Request
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WechatTradeDownloadRequest : IWechatRequest<WechatTradeDownloadResponse>
    {
        /// <summary>
        /// 账单时间：下载对账单的日期，格式：20140603
        /// </summary>
        [XmlElement("bill_date")]
        public string BillDate { get; set; }
        /// <summary>
        /// 账单类型，ALL（默认值），返回当日所有订单信息（不含充值退款订单）
        /// SUCCESS，返回当日成功支付的订单（不含充值退款订单）
        /// REFUND，返回当日退款订单（不含充值退款订单）
        /// RECHARGE_REFUND，返回当日充值退款订单
        /// </summary>
        [XmlElement("bill_type")]
        public string BillType { get; set; }
        /// <summary>
        /// 压缩账单 非必传参数，固定值：GZIP，返回格式为.gzip的压缩包账单。不传则默认为数据流形式。
        /// </summary>
        [XmlElement("tar_type")]
        public string TarType { get; set; }
    }
}
