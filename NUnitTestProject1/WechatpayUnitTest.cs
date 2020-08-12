using Wechatpay.AspNetCore;
using Wechatpay.AspNetCore.Constants;
using Wechatpay.AspNetCore.Request;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject1
{
    /// <summary>
    /// 
    /// </summary>
    public class WechatpayUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task Test1()
        {
            var config = new WechatpayConfig();

            var request = new WechatTradeAppPayRequest
            {
                TotalFee = 100,
                OutTradeNo = "202007297777",
                Body = "产品",
                Detail = "产品",
                TimeExpire = DateTime.Now.AddDays(15).ToString("yyyyMMddHHmmss"),
                TradeType = WechatConstants.APP,
                TimeStart = DateTime.Now.ToString("yyyyMMddHHmmss"),
                NotifyUrl = config.NotifyUrl
            };

            var response = await WechatpayClient.CreateOrderAsync(request, config.AccountList[0]);

            if (string.IsNullOrEmpty(response.Body))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task Test2()
        {
            var config = new WechatpayConfig();

            var request = new WechatTradeQueryRequest
            {
                OutTradeNo = "202007297777"
            };

            var response = await WechatpayClient.OrderQueryAsync(request, config.AccountList[0]);

            if (string.IsNullOrEmpty(response.TransactionId))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task Test5()
        {
            var config = new WechatpayConfig();

            var request = new WechatRefundRequest
            {
                OutTradeNo = "202007297777",
                TotalFee = 100,
                RefundFee = 100,
                OutRefundNo = HttpService.GenerateOutTradeNo(config.AccountList[0])
            };

            var response = await WechatpayClient.RefundAsync(request, config.AccountList[0]);

            if (string.IsNullOrEmpty(response.TransactionId))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task Test6()
        {
            var config = new WechatpayConfig();

            var request = new WechatTradeCloseRequest
            {
                OutTradeNo = "202007297777"
            };

            var response = await WechatpayClient.CloseOrderAsync(request, config.AccountList[0]);

            if (string.IsNullOrEmpty(response.ReturnCode))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void Test4()
        {
            var data = "";//xml请求数据
            var request = new WechatpayData();
            request.FromXml(data);

            var config = new WechatpayConfig();

            var response = WechatpayClient.Notify(request, config.AccountList[0]);

            if (string.IsNullOrEmpty(response.OutTradeNo))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }
    }
}
