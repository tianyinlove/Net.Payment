using Alipay.AspNetCore.Domain;
using Alipay.AspNetCore.Request;
using Alipay.AspNetCore.Response;
using Alipay.AspNetCore.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alipay.AspNetCore
{
    /// <summary>
    /// 支付宝支付
    /// </summary>
    public class AlipayClient
    {
        /// <summary>
        /// 支付成功回调
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static AlipayNotifyResponse Notify(AlipayData request, AlipayConfig config)
        {
            var response = request.ToObject<AlipayNotifyResponse>();
            //签名校验
            if (!AlipaySignature.RSACheckV1(request.GetValues(), config.AliPublicKey, response.Charset, response.SignType, config.KeyFromFile))
            {
                throw new Exception("签名校验失败");
            }
            return response;
        }

        /// <summary>
        /// 统一收单交易支付接口
        /// </summary>
        /// <param name="request">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradePayResponse> CreateOrder(AlipayTradePayModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradePayRequest();
            requestData.SetNotifyUrl(config.NotifyUrl);
            requestData.SetReturnUrl(config.QuitUrl);
            requestData.SetBizModel(request);

            return await client.ExecuteAsync(requestData);
        }

        /// <summary>
        /// 电脑网站支付统一下单
        /// </summary>
        /// <param name="request">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradePagePayResponse> CreateOrder(AlipayTradePagePayModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradePagePayRequest();
            requestData.SetNotifyUrl(config.NotifyUrl);
            requestData.SetReturnUrl(config.QuitUrl);
            requestData.SetBizModel(request);

            return await client.PageExecute(requestData);
        }

        /// <summary>
        /// App统一下单
        /// </summary>
        /// <param name="request">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static AlipayTradeAppPayResponse CreateOrder(AlipayTradeAppPayModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeAppPayRequest();
            requestData.SetNotifyUrl(config.NotifyUrl);
            requestData.SetBizModel(request);

            return client.SdkExecute(requestData);
        }

        /// <summary>
        /// Wap统一下单
        /// </summary>
        /// <param name="request">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="accessToken">用户授权码</param>
        /// <param name="method">请求方式,两个值可选：POST、GET;</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradeWapPayResponse> CreateOrderAsync(AlipayTradeWapPayModel request, AlipayConfig config, string accessToken = null, string method = "GET", int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeWapPayRequest();
            requestData.SetNotifyUrl(config.NotifyUrl);
            requestData.SetReturnUrl(config.QuitUrl);
            requestData.SetBizModel(request);

            return await client.PageExecute(requestData, accessToken, method);
        }

        /// <summary>
        /// 预付统一下单
        /// </summary>
        /// <param name="request">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="accessToken">用户授权码</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradePrecreateResponse> CreateOrderAsync(AlipayTradePrecreateModel request, AlipayConfig config, string accessToken = null, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradePrecreateRequest();
            requestData.SetNotifyUrl(config.NotifyUrl);
            requestData.SetBizModel(request);

            return await client.ExecuteAsync(requestData, accessToken);
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="request">提交给查询订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="accessToken">用户授权token</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回订单查询结果，其他抛异常</returns>
        public static async Task<AlipayTradeQueryResponse> OrderQueryAsync(AlipayTradeQueryModel request, AlipayConfig config, string accessToken = null, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeQueryRequest();
            requestData.SetBizModel(request);

            return await client.ExecuteAsync(requestData, accessToken);
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="request">提交给申请退款API的参数</param>
        /// <param name="config"></param>
        /// <param name="accessToken">用户授权token</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回接口调用结果，其他抛异常</returns>
        public static async Task<AlipayTradeRefundResponse> RefundAsync(AlipayTradeRefundModel request, AlipayConfig config, string accessToken = null, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeRefundRequest();
            requestData.SetBizModel(request);

            var result = await client.ExecuteAsync(requestData, accessToken);
            if (result.Code != AlipayConstants.SuccessCode)
            {
                throw new Exception(result.SubMsg);
            }
            return result;
        }

        /// <summary>
        /// 查询退款
        /// 提交退款申请后，通过该接口查询退款状态。退款有一定延时
        /// 用零钱支付的退款20分钟内到账，银行卡支付的退款3个工作日后重新查询退款状态。
        /// </summary>
        /// <param name="request">out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个</param>
        /// <param name="config"></param>
        /// <param name="accessToken">用户授权token</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradeFastpayRefundQueryResponse> RefundQueryAsync(AlipayTradeFastpayRefundQueryModel request, AlipayConfig config, string accessToken = null, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeFastpayRefundQueryRequest();
            requestData.SetBizModel(request);

            var result = await client.ExecuteAsync(requestData, accessToken);
            if (result.Code != AlipayConstants.SuccessCode)
            {
                throw new Exception(result.SubMsg);
            }
            return result;
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="request">提交给关闭订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="accessToken">用户授权token</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradeCloseResponse> CloseOrderAsync(AlipayTradeCloseModel request, AlipayConfig config, string accessToken = null, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeCloseRequest();
            requestData.SetBizModel(request);

            var result = await client.ExecuteAsync(requestData, accessToken);
            if (result.Code != AlipayConstants.SuccessCode)
            {
                throw new Exception(result.SubMsg);
            }
            return result;
        }

        /// <summary>
        /// 查询对账单下载地址
        /// </summary>
        /// <param name="request">提交给关闭订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="accessToken">用户授权token</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayDataDataserviceBillDownloadurlQueryResponse> DownloadBillAsync(AlipayDataDataserviceBillDownloadurlQueryModel request, AlipayConfig config, string accessToken = null, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayDataDataserviceBillDownloadurlQueryRequest();
            requestData.SetBizModel(request);

            var result = await client.ExecuteAsync(requestData, accessToken);
            if (result.Code != AlipayConstants.SuccessCode)
            {
                throw new Exception(result.SubMsg);
            }
            return result;
        }
    }
}
