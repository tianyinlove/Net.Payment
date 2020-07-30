using System;
using System.Collections.Generic;
using System.Text;

namespace Wechatpay.AspNetCore.Response
{
    /// <summary>
    /// 下载交易账单
    /// </summary>
    [Serializable]
    public class WechatTradeDownloadResponse : WechatResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }
    }
}
