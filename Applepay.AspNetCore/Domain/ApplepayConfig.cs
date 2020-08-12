using System;
using System.Collections.Generic;
using System.Text;

namespace Applepay.AspNetCore.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplepayConfig
    {
        /// <summary>
        /// 是否上线
        /// </summary>
        public bool IsOnline { get; set; } = true;

        /// <summary>
        /// 用于签名的Key
        /// </summary>
        public string SignKey { get; set; }

        /// <summary>
        /// 签名类型，目前支持HMAC-SHA256和MD5，默认为MD5
        /// </summary>
        public string SignType { get; set; } = "MD5";

        /// <summary>
        /// 支付成功回调接口
        /// </summary>
        public string NotifyUrl { get; set; }
    }
}
