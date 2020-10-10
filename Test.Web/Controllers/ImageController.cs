using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// 
        /// </summary>
        public ImageController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetImage()
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
                OutTradeNo = "202007297777",
                ProductId = "202007297777",
                Body = "产品",
                Detail = "产品",
                TimeExpire = DateTime.Now.AddHours(2).ToString("yyyyMMddHHmmss"),
                TradeType = WechatConstants.NATIVE,
                TimeStart = DateTime.Now.ToString("yyyyMMddHHmmss"),
                NotifyUrl = "http://mobiletest.emoney.cn"
            };

            var order = await WechatpayClient.CreateOrderAsync(request, config);
            using (var generator = new QRCodeGenerator())
            {
                using (var codeData = generator.CreateQrCode(order.CodeUrl, QRCodeGenerator.ECCLevel.M, true))
                {
                    using (var qrcode = new QRCode(codeData))
                    {
                        var icon = await GetIcon();
                        using (var bitmap = qrcode.GetGraphic(4, Color.Black, Color.White, icon, 15, 2, false))
                        {
                            using (var ms = new MemoryStream())
                            {
                                bitmap.Save(ms, ImageFormat.Jpeg);
                                return File(ms.GetBuffer(), "image/jpeg");
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
            var iconUrl = "https://ms.emoney.cn/images/booking/ioc/20170904/zlb_s.png";
            var client = new WebClient();
            client.Proxy = null;
            var data = await client.OpenReadTaskAsync(iconUrl);
            return new Bitmap(data);
        }
    }
}