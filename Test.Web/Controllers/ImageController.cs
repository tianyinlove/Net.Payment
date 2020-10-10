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
        public async Task<IActionResult> GetImage(string url = "http://www.baidu.com")
        {
            using (var generator = new QRCodeGenerator())
            {
                using (var codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true))
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