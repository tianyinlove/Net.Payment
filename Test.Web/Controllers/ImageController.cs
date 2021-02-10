using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Alipay.AspNetCore;
using Alipay.AspNetCore.Domain;
using Alipay.AspNetCore.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QRCoder;
using Wechatpay.AspNetCore;
using Wechatpay.AspNetCore.Constants;
using Wechatpay.AspNetCore.Request;

namespace Test.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IMemoryCache _memoryCache;

        /// <summary>
        /// 
        /// </summary>
        public ImageController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Alipay()
        {
            var config = new AlipayConfig
            {
                NotifyUrl = "http://mobiletest.emoney.cn/mdata/payment/api/alipay/alipaynotity",
                AppId = "2015072000178264",
                PrivateKey = "MIIEowIBAAKCAQEA92h++oFzk59dB+Xjcn+QsXMcMFREsMq1CqBXd1G8l+5wjDKetmoCbXNU3beZtWL+6XuetfPa2tmOR42v1zJeKODid5+tvgBe0ecJvgmxmSQ3al7gAu2mMnwzY6jkhl8kMG9CCUUs5PTQXAeDc5oSoXzfX06xgbCf4D4NRlRwPU5Ff3QVqlZ1desEczubyuhE7dl0pf+/Y6FB1NMTvB1MEX6PQ0ygVqe2i32ugCT/Fq2yf1UNwU6VXqOmX14mF/ZKxhWLn5bW3PVtvQkZQqR1Wtr5pNYDJ91XoKLraeSzH8fbLuxMGfwgflskMGwhMrmt69wRa5+Gc/5TdtgdLH648QIDAQABAoIBADWi8eRdLDFU21ZbAHRSM4xE6FeR7VJmn9kt6ch0b+6AQuYiE0Z4tQ4FcuCebTRlwd3cbwwDUe8gOzhH/1coFEHIf1BvzbrjWasn63THpTkOIPVunCIGY4GOa5Wvh9uZxL67YBWiaZk5efJ5roXhYSihacu/w7vaDs8OpNIafDrN0CE7KKxwo21sxR6++CikPV3ecs1lepKuiGmxSfUlZQ0LIK2kf0nt5JxAkcCmofW5wfTEQ3jD1ogdPtnqenGHqLONPU9vJ/onGLle8zu4K6m1FsIDFmpY/G9CAUDFE/Mkue6jmTFnMzSdEfQyA4mLqiSryerppzkF1eLsShUpAAECgYEA/2JrP8OzE0O3N0sMT0/KwMPrMKlz94GnLAd+YPs3Rf6c5UUEJCBgOo7wSrssvbeTg2r9K9xh9ROTsl6sIL6yAsisaIIAAqDhoMjMwoFVNPiln7qxhSi2FviJMD5fFDdNj1T8BjwOSl3iZnpUAGlebjBbH0qIFAOt/sLC9TVESLECgYEA+AEnys8efC1MQpIzf6i60DvhR88is0RoTLeI0AsAEwC3U6MXPkImW4P0WDdE5Ju4UNX2ARU5nJ5PQEUj0PPAWV+cJg7zPG1kLmMWomH4DjAIXrAE6rpDYfMVZcljzlPaWVcZpTNBW2Q25JuAD5G7LremKRJTFY/KS+WhB6a9hEECgYEA9e9JaPaFFSA46D6VdCtbDZeefhYxX0C0RGpTAgHqkLVJUmDMRB4JjVrpfI4T3/9RR1VYyUxJ6UEZEsSo7j+2HYgyv9GxQNa9caDtzsHN1F0+4jtfiORTtntRYewuk8FOJ6GnsjlTabpPtHBJRLwz+9kFZ3TW5q9Ed/oG1SyevYECgYBYloESTt3C2aNt/C9tIGZqz5jvP6xTgNII5V53ghZqs0zJEW7SDGsLMZ1sjYGMb5ttNKCFToGf0mTYvlnG3+sLgb2TUq/MG/83GqoRIxGqb7ntqM540kQTjvNLwp5mK0nJs/UzL5XfAXMiQfGCnjnQvEUcUrFe+Ff4uHPG+eakgQKBgAdQ24pGFdyC2gkwKPLPrvslfiuBxgvpsOqo0Jic+cK+QsORemqcKHM6RgwTPZ3ooaaaupjbERMTc0ykVJJGMCdnTINUwvyb2vR3p2a0PXMFVS/2OrIdRaMlmLX5BGVjupecE5wk+lEMjRWHrti4FrE8/yNMZNsFrBPxQBUGS8wr",
                AliPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxKydmUKMCWLIShH/t/67Rbn16ZrBNwANAJ6rdUGEB3cWUaLG4poXLZsd6B1roEYioLRGTerHFGfHzjsL0vzG1FSnBTRgBu/F1xqtSzvzh8P5qW9ObISwjiOA4ZfV63XL3WpTytR8lPyWYFUSLmMc2ofzAKUVVC4p5nMafJB306f21AXNS6//ulDnI4IhRl2kvBawOgBGpJ4kHJHm/qtZ0XDu5/+LiLm/Wvd2lBp1z3QsqTHLumR+w/4CeTH1R/YZXd3fUULqg/2lVEk9O8rIcYVtnMJ/oR/BRhQsCU5y3nvC3fPMOwVqlM8AUzrZvN7tjhxxdFPpyj/d0yTS9B8pwQIDAQAB"
            };

            var data = new AlipayTradePrecreateModel
            {
                Body = "产品",
                Subject = "产品",
                OutTradeNo = $"em_{DateTime.Now.ToString("yyyyMMddHHmmss")}",
                TimeoutExpress = "15m",
                TotalAmount = $"{10:0.##}"
            };

            var response = await AlipayClient.CreateOrderAsync(data, config);
            var ms = Convert.FromBase64String((await GetImage(response.QrCode)).Replace("data:image/jpeg;base64,", ""));
            return File(ms, "image/png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Wechatpay()
        {
            var config = new WechatpayConfig
            {
                AppId = "wxefd7e95dd1ec11ae",
                SignKey = "I2lgA4cnK75mHULNqAaoZ3o4SSHMAY48",
                MchId = "1495057202"
            };

            var request = new WechatTradeAppPayRequest
            {
                TotalFee = 100,
                OutTradeNo = $"test_{DateTime.Now.ToString("yyyyMMddHHmmss")}",
                ProductId = $"test_{DateTime.Now.ToString("yyyyMMddHHmmss")}",
                Body = "产品",
                Detail = "产品",
                TimeExpire = DateTime.Now.AddHours(2).ToString("yyyyMMddHHmmss"),
                TradeType = WechatConstants.MWEB,
                TimeStart = DateTime.Now.ToString("yyyyMMddHHmmss"),
                NotifyUrl = "http://mobiletest.emoney.cn/mdata/payment/api/wechat/wechatnotity"
            };

            var response = await WechatpayClient.CreateOrderAsync(request, config);
            var ms = Convert.FromBase64String((await GetImage(response.CodeUrl)).Replace("data:image/jpeg;base64,", ""));
            return File(ms, "image/png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetImage(string codeUrl)
        {
            using (var generator = new QRCodeGenerator())
            {
                using (var codeData = generator.CreateQrCode(codeUrl, QRCodeGenerator.ECCLevel.M, true))
                {
                    using (var qrcode = new QRCode(codeData))
                    {
                        var icon = await GetIcon();
                        using (var bitmap = qrcode.GetGraphic(4, Color.Black, Color.White, icon, 15, 2, false))
                        {
                            using (var ms = new MemoryStream())
                            {
                                bitmap.Save(ms, ImageFormat.Jpeg);
                                return "data:image/jpeg;base64," + Convert.ToBase64String(ms.GetBuffer());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        async Task<Bitmap> GetIcon()
        {
            var cacheKey = "icon:image:data";
            var result = _memoryCache.Get<byte[]>(cacheKey);
            if (result == null)
            {
                var iconUrl = "https://ms.emoney.cn/images/booking/ioc/20170904/zlb_s.png";
                var client = new WebClient();
                client.Proxy = null;
                result = await client.DownloadDataTaskAsync(iconUrl);
                _memoryCache.Set(cacheKey, result, TimeSpan.FromDays(1));
            }

            using (var ms = new MemoryStream(result))
            {
                return new Bitmap(ms);
            }
        }
    }
}