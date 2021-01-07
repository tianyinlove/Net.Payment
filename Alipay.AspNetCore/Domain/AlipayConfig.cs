using System;
using System.Collections.Generic;
using System.Text;

namespace Alipay.AspNetCore.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class AlipayConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PrivateKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Format { get; set; } = "json";
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; } = "1.0";
        /// <summary>
        /// 
        /// </summary>
        public string SignType { get; set; } = "RSA2";
        /// <summary>
        /// 
        /// </summary>
        public string AliPublicKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Charset { get; set; } = "utf-8";
        /// <summary>
        /// 是否从文件加载支付宝公钥内容，如果为true，则PrivateKey/AliPublicKey为支付宝公钥文件路径
        /// </summary>
        public bool KeyFromFile { get; set; } = false;
        /// <summary>
        /// 支付成功回调接口
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 用户付款中途退出返回商户网站的地址
        /// </summary>
        public string QuitUrl { get; set; }
    }
}
