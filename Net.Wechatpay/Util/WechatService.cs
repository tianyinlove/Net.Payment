using Net.Wechatpay.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Net.Wechatpay
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WechatService
    {
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="inputObj">提交给查询订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回订单查询结果，其他抛异常</returns>
        public static async Task<WechatpayData> OrderQueryAsync(WechatpayData inputObj, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            return await ExecuteAsync(inputObj, config, url, false, timeOut);
        }

        /// <summary>
        /// 撤销订单API接口
        /// </summary>
        /// <param name="inputObj">提交给撤销订单API接口的参数，out_trade_no和transaction_id必填一个</param>
        /// <param name="config"></param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        public static async Task<WechatpayData> ReverseAsyns(WechatpayData inputObj, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("撤销订单API接口中，参数out_trade_no和transaction_id必须填写一个！");
            }

            return await ExecuteAsync(inputObj, config, url, true, timeOut);
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="inputObj">提交给申请退款API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回接口调用结果，其他抛异常</returns>
        public static async Task<WechatpayData> RefundAsync(WechatpayData inputObj, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("退款申请接口中，out_trade_no、transaction_id至少填一个！");
            }
            else if (!inputObj.IsSet("out_refund_no"))
            {
                throw new Exception("退款申请接口中，缺少必填参数out_refund_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("退款申请接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("refund_fee"))
            {
                throw new Exception("退款申请接口中，缺少必填参数refund_fee！");
            }

            return await ExecuteAsync(inputObj, config, url, true, timeOut);
        }

        /// <summary>
        /// 查询退款
        /// 提交退款申请后，通过该接口查询退款状态。退款有一定延时
        /// 用零钱支付的退款20分钟内到账，银行卡支付的退款3个工作日后重新查询退款状态。
        /// </summary>
        /// <param name="inputObj">out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个</param>
        /// <param name="config"></param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<WechatpayData> RefundQueryAsync(WechatpayData inputObj, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/refundquery";
            //检测必填参数
            if (!inputObj.IsSet("out_refund_no") && !inputObj.IsSet("out_trade_no") &&
                !inputObj.IsSet("transaction_id") && !inputObj.IsSet("refund_id"))
            {
                throw new Exception("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");
            }

            return await ExecuteAsync(inputObj, config, url, false, timeOut);
        }

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="inputObj">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<WechatpayData> UnifiedOrderAsync(WechatpayData inputObj, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new Exception("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new Exception("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new Exception("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new Exception("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (!inputObj.IsSet("notify_url"))
            {
                throw new Exception("缺少统一支付接口必填参数notify_url");
            }

            var result = await ExecuteAsync(inputObj, config, url, false, timeOut);
            var data = GetAppData(config, result.GetValue("prepay_id"));
            result.SetValue("body", data.ToXml());
            return result;
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="inputObj">提交给关闭订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<WechatpayData> CloseOrderAsync(WechatpayData inputObj, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/closeorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("关闭订单接口中，out_trade_no必填！");
            }

            return await ExecuteAsync(inputObj, config, url, false, timeOut);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="config"></param>
        /// <param name="url"></param>
        /// <param name="isUseCert"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteAsync<T>(IWechatRequest<T> request, WechatpayConfig config, string url, bool isUseCert = false, int timeout = 6)
        {
            var requestData = new WechatpayData();
            requestData.FromObject(request);
            var response = await ExecuteAsync(requestData, config, url, isUseCert, timeout);
            return response.ToObject<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="prepay_id"></param>
        /// <returns></returns>
        public static WechatpayData GetAppData(WechatpayConfig config, object prepay_id)
        {
            var data = new WechatpayData();
            data.SetValue(WechatConstants.APPID, config.APPID);
            data.SetValue(WechatConstants.PARTNERID, config.MCHID);
            data.SetValue(WechatConstants.PREPAYID, prepay_id);
            data.SetValue(WechatConstants.PACKAGE, "Sign=WXPay");
            data.SetValue(WechatConstants.NONCESTR, WechatService.GenerateNonceStr());
            data.SetValue(WechatConstants.TIMESTAMP, (int)(DateTime.Now.ToUniversalTime().Ticks / 10000000 - 62135596800));
            data.SetValue(WechatConstants.SIGN, data.MakeSign(config.SignType, config.SignKey));
            return data;
        }

        /// <summary>
        /// 根据当前系统时间加随机序列来生成订单号
        /// </summary>
        /// <param name="config"></param>
        /// <returns>订单号</returns>
        public static string GenerateOutTradeNo(WechatpayConfig config)
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", config.MCHID, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }

        /// <summary>
        /// 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
        /// </summary>
        /// <returns>时间戳</returns>
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 生成随机串，随机串包含字母或数字
        /// </summary>
        /// <returns>随机串</returns>
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="certPath"></param>
        /// <param name="certPassword"></param>
        /// <param name="timeout"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        static async Task<WechatpayData> ExecuteAsync(WechatpayData inputObj, WechatpayConfig config, string url, bool isUseCert = false, int timeout = 6)
        {
            inputObj.SetValue("appid", config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", config.MCHID);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", config.SignType);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign(config.SignType, config.SignKey));//签名

            string response = await HttpService.Post(inputObj.ToXml(), url, isUseCert, timeout, config.SSLCERT_PATH, config.SSLCERT_PASSWORD);

            var result = new WechatpayData();
            result.FromXml(response);

            //验证签名,不通过会抛异常
            result.CheckSign(config.SignType, config.SignKey);

            return result;
        }
    }
}
