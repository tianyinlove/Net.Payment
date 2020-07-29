using System;
using System.Collections.Generic;
using System.Text;

namespace Alipay.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    public class AlipayConstants
    {
        public static string APP_ID = "app_id";
        public static string FORMAT = "format";
        public static string METHOD = "method";
        public static string TIMESTAMP = "timestamp";
        public static string VERSION = "version";
        public static string SIGN_TYPE = "sign_type";
        public static string ACCESS_TOKEN = "auth_token";
        public static string APP_AUTH_TOKEN = "app_auth_token";
        public static string TARGET_APP_ID = "target_app_id";
        public static string SIGN = "sign";
        public static string TERMINAL_TYPE = "terminal_type";
        public static string TERMINAL_INFO = "terminal_info";
        public static string PROD_CODE = "prod_code";
        public static string NOTIFY_URL = "notify_url";
        public static string CHARSET = "charset";
        public static string ENCRYPT_TYPE = "encrypt_type";
        public static string BIZ_CONTENT = "biz_content";
        public static string RETURN_URL = "return_url";
        public static string APP_CERT_SN = "app_cert_sn";
        public static string ALIPAY_CERT_SN = "alipay_cert_sn";
        public static string ALIPAY_ROOT_CERT_SN = "alipay_root_cert_sn";
        public static string ALIPAY_SDK = "alipay_sdk";

        public static string RESPONSE_SUFFIX = "_response";
        public static string ERROR_RESPONSE = "error_response";
        public static string ENCRYPT_NODE_NAME = "response_encrypted";

        /// <summary>
        /// 
        /// </summary>
        public static string SuccessCode = "10000";

        /// <summary>
        /// 
        /// </summary>
        public static string GATEWAYURL = "https://openapi.alipay.com/gateway.do";
        /// <summary>
        /// 
        /// </summary>
        public static string TRADE_FINISHED = "TRADE_FINISHED";
        /// <summary>
        /// 
        /// </summary>
        public static string TRADE_SUCCESS = "TRADE_SUCCESS";
        /// <summary>
        /// 
        /// </summary>
        public static string TRADE_CLOSED = "TRADE_CLOSED";
        /// <summary>
        /// 
        /// </summary>
        public static string REFUND_SUCCESS = "REFUND_SUCCESS";

        /// <summary>
        /// 统一收单下单并支付页面接口
        /// </summary>
        public static string FAST_INSTANT_TRADE_PAY = "FAST_INSTANT_TRADE_PAY";
        /// <summary>
        /// app支付接口2.0
        /// </summary>
        public static string QUICK_MSECURITY_PAY = "QUICK_MSECURITY_PAY";
        /// <summary>
        /// 统一收单交易创建/支付接口
        /// </summary>
        public static string FACE_TO_FACE_PAYMENT = "FACE_TO_FACE_PAYMENT";
        /// <summary>
        /// 手机网站支付接口2.0
        /// </summary>
        public static string QUICK_WAP_WAY = "QUICK_WAP_WAY";
    }
}
