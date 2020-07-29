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
        /// <param name="inputObj"></param>
        /// <param name="certPath"></param>
        /// <param name="certPassword"></param>
        /// <param name="timeout"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<WechatpayData> ExecuteAsync(WechatpayData inputObj, WechatpayConfig config, string url, bool isUseCert = false, int timeout = 6)
        {
            inputObj.SetValue("appid", config.AppId);//公众账号ID
            inputObj.SetValue("mch_id", config.MchId);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", config.SignType);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign(config.SignType, config.SignKey));//签名

            string response = await HttpService.Post(inputObj.ToXml(), url, isUseCert, timeout, config.CertPath, config.CertPassword);

            var result = new WechatpayData();
            result.FromXml(response);

            //验证签名,不通过会抛异常
            result.CheckSign(config.SignType, config.SignKey);

            return result;
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
            data.SetValue(WechatConstants.APPID, config.AppId);
            data.SetValue(WechatConstants.PARTNERID, config.MchId);
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
            return string.Format("{0}{1}{2}", config.MchId, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
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
    }
}
