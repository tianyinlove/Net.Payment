using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Applepay.AspNetCore.Constants;
using Applepay.AspNetCore.Domain;
using Applepay.AspNetCore.Request;
using Applepay.AspNetCore.Response;
using Applepay.AspNetCore.Util;
using System.Collections.Generic;

namespace Applepay.AspNetCore
{
    /// <summary>
    /// 苹果支付
    /// </summary>
    public class ApplepayClient
    {
        /// <summary>
        /// 支付成功回调
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static async Task<ApplepayNotifyResponse> Notify(ApplepayNotifyRequest request, ApplepayConfig config, int timeOut = 6)
        {
            var checkData = new ApplepayData();
            checkData.FromObject(request);
            //验证签名,不通过会抛异常
            if (!checkData.CheckSign(config.SignType, config.SignKey))
            {
                throw new Exception("苹果签名校验失败");
            }
            // 验证参数
            if (string.IsNullOrEmpty(request.AppleReceipt) || request.AppleReceipt.Length < 20)
            {
                throw new Exception("苹果支付凭证异常");
            }
            string strJosn = string.Format("{{\"receipt-data\":\"{0}\"}}", request.AppleReceipt);
            var url = config.IsOnline ? AppleConstants.VerifyReceipt : AppleConstants.SandboxVerifyReceipt;
            // 请求验证
            string response = await HttpService.PostAsync(url, strJosn, timeOut);
            var result = JsonConvert.DeserializeObject<ApplepayNotifyResponse>(response);
            if (result.Status != AppleConstants.SUCCESS)
            {
                throw new Exception("苹果支付凭证验证失败");
            }
            if (result.Receipt == null || result.Receipt.BundleId != config.BundleId)
            {
                throw new Exception("苹果支付订单无效");
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static AppleTradeAppPayResponse CreateOrder(AppleTradeAppPayRequest request, ApplepayConfig config)
        {
            //检测必填参数
            if (string.IsNullOrEmpty(request.OutTradeNo))
            {
                throw new Exception("支付订单号不能为空！");
            }
            if (string.IsNullOrEmpty(request.ProductId))
            {
                throw new Exception("苹果内购产品ProductId不能为空！");
            }

            var data = new ApplepayData();
            data.FromObject(request);
            var result = data.ToObject<AppleTradeAppPayResponse>();
            result.Sign = data.MakeSign(config.SignType, config.SignKey);//签名
            result.NotifyUrl = config.NotifyUrl;
            return result;
        }
    }
}
