using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Wechatpay.AspNetCore.Response
{
    /// <summary>
    /// 微信响应通用数据
    /// </summary>
    [Serializable]
    public abstract class WechatResponse
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        [XmlElement("return_code")]
        [JsonProperty("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("return_msg")]
        [JsonProperty("return_msg")]
        public string ReturnMsg { get; set; }

        #region 以下字段在return_code为SUCCESS的时候有返回

        /// <summary>
        /// 业务结果
        /// </summary>
        [XmlElement("result_code")]
        [JsonProperty("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [XmlElement("nonce_str")]
        [JsonProperty("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign")]
        [JsonProperty("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        [XmlElement("sign_type")]
        [JsonProperty("sign_type")]
        public string SignType { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        /// <remarks>列表详见错误码列表</remarks>
        [XmlElement("err_code")]
        [JsonProperty("err_code")]
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        /// <remarks>结果信息描述</remarks>
        [XmlElement("err_code_des")]
        [JsonProperty("err_code_des")]
        public string ErrCodeDes { get; set; }

        /// <summary>
        /// 公众账号ID(必填)
        /// </summary>
        /// <remarks>微信分配的公众账号ID（企业号corpid即为此appId）</remarks>
        [XmlElement("appid")]
        [JsonProperty("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 商户号(必填)
        /// </summary>
        /// <remarks>微信支付分配的商户号</remarks>
        [XmlElement("mch_id")]
        [JsonProperty("mch_id")]
        public string MchId { get; set; }

        #endregion 以下字段在return_code为SUCCESS的时候有返回
    }
}
