using Net.Alipay;
using Net.Alipay.Domain;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NUnitTestProject1
{
    /// <summary>
    /// 
    /// </summary>
    public class AlipayUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void Test1()
        {
            var config = new AlipayConfig
            {
                NotifyUrl = "",
                AppId = "",
                PrivateKey = "",
                AliPublicKey = ""
            };
            var request = new AlipayTradeAppPayModel
            {
                Body = "产品",
                Subject = "产品",
                OutTradeNo = "202007297777",
                TimeoutExpress = "15m",
                TotalAmount = $"{10:0.##}",
                ProductCode = AlipayConstants.QUICK_MSECURITY_PAY
            };
            var response = AlipayClient.CreateOrder(request, config);

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
            var config = new AlipayConfig
            {
                NotifyUrl = "",
                AppId = "",
                PrivateKey = "",
                AliPublicKey = "",
                QuitUrl = ""
            };
            var request = new AlipayTradeWapPayModel
            {
                Body = "产品",
                Subject = "产品",
                OutTradeNo = "202007297777",
                TimeoutExpress = "15m",
                TotalAmount = $"{10:0.##}",
                QuitUrl = config.QuitUrl,
                ProductCode = AlipayConstants.QUICK_WAP_WAY
            };
            var response = await AlipayClient.CreateOrderAsync(request, config);

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
        public async Task Test3()
        {
            var config = new AlipayConfig
            {
                NotifyUrl = "",
                AppId = "",
                PrivateKey = "",
                AliPublicKey = "",
                QuitUrl = ""
            };
            var request = new AlipayTradeQueryModel
            {
                OutTradeNo = "202007297777"
            };
            var response = await AlipayClient.OrderQueryAsync(request, config);

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
        public async Task Test5()
        {
            var config = new AlipayConfig
            {
                NotifyUrl = "",
                AppId = "",
                PrivateKey = "",
                AliPublicKey = "",
                QuitUrl = ""
            };
            var request = new AlipayTradeRefundModel
            {
                OutTradeNo = "202007297777",
                RefundAmount = $"{10:0.##}"
            };
            var response = await AlipayClient.RefundAsync(request, config);

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
        public async Task Test6()
        {
            var config = new AlipayConfig
            {
                NotifyUrl = "",
                AppId = "",
                PrivateKey = "",
                AliPublicKey = "",
                QuitUrl = ""
            };
            var request = new AlipayTradeCloseModel
            {
                OutTradeNo = "202007297777"
            };
            var response = await AlipayClient.CloseOrderAsync(request, config);

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
        public void Test4()
        {
            var data = "{}";//json请求数据
            var request = new AlipayData();
            request.FromJson(data);

            var config = new AlipayConfig
            {
                NotifyUrl = "",
                AppId = "",
                PrivateKey = "",
                AliPublicKey = "",
                QuitUrl = ""
            };
            var response = AlipayClient.Notify(request, config);

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