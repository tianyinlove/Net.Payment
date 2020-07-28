using Net.Wechatpay.Constants;
using Net.Wechatpay.Request;
using Net.Wechatpay.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Net.Wechatpay
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WechatpayClient
    {
        /// <summary>
        /// 支付成功回调
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static WechatNotifyResponse Notify(WechatpayData request, WechatpayConfig config)
        {
            //验证签名,不通过会抛异常
            if (!request.CheckSign(config.SignType, config.SignKey))
            {
                throw new Exception("签名校验失败");
            }
            var result = request.ToObject<WechatNotifyResponse>();
            if (result.ReturnCode != WechatConstants.SUCCESS && result.ResultCode != WechatConstants.SUCCESS)
            {
                throw new Exception(result.ReturnMsg);
            }
            return result;
        }

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="request">提交给统一下单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<WechatTradeAppPayResponse> CreateOrderAsync(WechatTradeAppPayRequest request, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //检测必填参数
            if (string.IsNullOrEmpty(request.OutTradeNo))
            {
                throw new Exception("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (string.IsNullOrEmpty(request.Body))
            {
                throw new Exception("缺少统一支付接口必填参数body！");
            }
            else if (request.TotalFee <= 0)
            {
                throw new Exception("缺少统一支付接口必填参数total_fee！");
            }
            else if (string.IsNullOrEmpty(request.TradeType))
            {
                throw new Exception("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (request.TradeType == "JSAPI" && string.IsNullOrEmpty(request.OpenId))
            {
                throw new Exception("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (request.TradeType == "NATIVE" && string.IsNullOrEmpty(request.ProductId))
            {
                throw new Exception("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (string.IsNullOrEmpty(request.NotifyUrl))
            {
                throw new Exception("缺少统一支付接口必填参数notify_url");
            }

            var result = await WechatService.ExecuteAsync(request, config, url);
            if (result.ReturnCode != WechatConstants.SUCCESS && result.ResultCode != WechatConstants.SUCCESS)
            {
                throw new Exception(result.ReturnMsg);
            }
            var data = new WechatpayData();
            data.SetValue(WechatConstants.APPID, result.AppId);
            data.SetValue(WechatConstants.PARTNERID, result.MchId);
            data.SetValue(WechatConstants.PREPAYID, result.PrepayId);
            data.SetValue(WechatConstants.PACKAGE, "Sign=WXPay");
            data.SetValue(WechatConstants.NONCESTR, WechatService.GenerateNonceStr());
            data.SetValue(WechatConstants.TIMESTAMP, (int)(DateTime.Now.ToUniversalTime().Ticks / 10000000 - 62135596800));
            data.SetValue(WechatConstants.SIGN, data.MakeSign(config.SignType, config.SignKey));
            result.Body = data.ToXml();
            return result;
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="request">提交给查询订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回订单查询结果，其他抛异常</returns>
        public static async Task<WechatTradeQueryResponse> OrderQueryAsync(WechatTradeQueryRequest request, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (string.IsNullOrEmpty(request.OutTradeNo) && string.IsNullOrEmpty(request.TransactionId))
            {
                throw new Exception("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            var result = await WechatService.ExecuteAsync(request, config, url);
            if (result.ReturnCode != WechatConstants.SUCCESS && result.ResultCode != WechatConstants.SUCCESS)
            {
                throw new Exception(result.ReturnMsg);
            }
            return result;
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="request">提交给申请退款API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回接口调用结果，其他抛异常</returns>
        public static async Task<WechatRefundResponse> RefundAsync(WechatRefundRequest request, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            //检测必填参数
            if (string.IsNullOrEmpty(request.OutTradeNo) && string.IsNullOrEmpty(request.TransactionId))
            {
                throw new Exception("退款申请接口中，out_trade_no、transaction_id至少填一个！");
            }
            else if (string.IsNullOrEmpty(request.OutRefundNo))
            {
                throw new Exception("退款申请接口中，缺少必填参数out_refund_no！");
            }
            else if (request.TotalFee <= 0)
            {
                throw new Exception("退款申请接口中，缺少必填参数total_fee！");
            }
            else if (request.RefundFee <= 0)
            {
                throw new Exception("退款申请接口中，缺少必填参数refund_fee！");
            }

            var result = await WechatService.ExecuteAsync(request, config, url);
            if (result.ReturnCode != WechatConstants.SUCCESS && result.ResultCode != WechatConstants.SUCCESS)
            {
                throw new Exception(result.ReturnMsg);
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
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<WechatRefundQueryResponse> RefundQueryAsync(WechatRefundQueryRequest request, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/refundquery";
            //检测必填参数
            if (string.IsNullOrEmpty(request.OutRefundNo) && string.IsNullOrEmpty(request.OutTradeNo) &&
                string.IsNullOrEmpty(request.RefundId) && string.IsNullOrEmpty(request.TransactionId))
            {
                throw new Exception("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");
            }

            var result = await WechatService.ExecuteAsync(request, config, url);
            if (result.ReturnCode != WechatConstants.SUCCESS && result.ResultCode != WechatConstants.SUCCESS)
            {
                throw new Exception(result.ReturnMsg);
            }
            return result;
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="request">提交给关闭订单API的参数</param>
        /// <param name="config"></param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回，其他抛异常</returns>
        public static async Task<WechatTradeCloseResponse> CloseOrderAsync(WechatTradeCloseRequest request, WechatpayConfig config, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/closeorder";
            //检测必填参数
            if (string.IsNullOrEmpty(request.OutTradeNo))
            {
                throw new Exception("关闭订单接口中，out_trade_no必填！");
            }

            var result = await WechatService.ExecuteAsync(request, config, url);
            if (result.ReturnCode != WechatConstants.SUCCESS && result.ResultCode != WechatConstants.SUCCESS)
            {
                throw new Exception(result.ReturnMsg);
            }
            return result;
        }
    }
}
