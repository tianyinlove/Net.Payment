using Net.Alipay.Domain;
using Net.Alipay.Request;
using Net.Alipay.Response;
using Net.Alipay.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Net.Alipay
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
        public AlipayNotifyResponse Notify(AlipayData request, AlipayConfig config)
        {
            var response = request.ToObject<AlipayNotifyResponse>();
            //签名校验
            if (!AlipaySignature.RSACheckV1(request.GetValues(), config.AliPublicKey, response.Charset, response.SignType, false))
            {
                throw new Exception("签名校验失败");
            }
            return response;
        }

        /// <summary>
        /// App统一下单
        /// </summary>
        /// <param name="request">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static AlipayTradeAppPayResponse CreateAppOrderAsync(AlipayTradeAppPayModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeAppPayRequest();
            requestData.SetNotifyUrl(config.NotifyUrl);
            requestData.SetReturnUrl(config.QuitUrl);
            requestData.SetBizModel(request);

            return client.SdkExecute(requestData);
        }

        /// <summary>
        /// Wap统一下单
        /// </summary>
        /// <param name="request">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradeWapPayResponse> CreateWapOrderAsync(AlipayTradeWapPayModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeWapPayRequest();
            requestData.SetNotifyUrl(config.NotifyUrl);
            requestData.SetReturnUrl(config.QuitUrl);
            requestData.SetBizModel(request);

            return await client.PageExecute(requestData);
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="request">提交给查询订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回订单查询结果，其他抛异常</returns>
        public static async Task<AlipayTradeQueryResponse> OrderQueryAsync(AlipayTradeQueryModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeQueryRequest();
            requestData.SetBizModel(request);

            return await client.ExecuteAsync(requestData);
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="inputObj">提交给申请退款API的参数</param>
        /// <param name="certPath"></param>
        /// <param name="certPassword"></param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回接口调用结果，其他抛异常</returns>
        public static async Task<AlipayTradeRefundResponse> RefundAsync(AlipayTradeRefundModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeRefundRequest();
            requestData.SetBizModel(request);

            return await client.ExecuteAsync(requestData);
        }

        /// <summary>
        /// 查询退款
        /// 提交退款申请后，通过该接口查询退款状态。退款有一定延时
        /// 用零钱支付的退款20分钟内到账，银行卡支付的退款3个工作日后重新查询退款状态。
        /// </summary>
        /// <param name="request">out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个</param>
        /// <param name="config"></param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradeFastpayRefundQueryResponse> RefundQueryAsync(AlipayTradeFastpayRefundQueryModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeFastpayRefundQueryRequest();
            requestData.SetBizModel(request);

            return await client.ExecuteAsync(requestData);
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="request">提交给关闭订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<AlipayTradeCloseResponse> CloseOrderAsync(AlipayTradeCloseModel request, AlipayConfig config, int timeOut = 6)
        {
            IAopClient client = new AopClient(AlipayConstants.GATEWAYURL, config.AppId, config.PrivateKey, config.Format, config.Version, config.SignType, config.AliPublicKey, config.Charset, config.KeyFromFile);
            var requestData = new AlipayTradeCloseRequest();
            requestData.SetBizModel(request);

            return await client.ExecuteAsync(requestData);
        }
    }
}
