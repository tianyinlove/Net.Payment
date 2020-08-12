using Wechatpay.AspNetCore.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Wechatpay.AspNetCore
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class HttpService
    {
        #region http连接基础类，负责底层的http通信

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="url"></param>
        /// <param name="isUseCert"></param>
        /// <param name="timeout"></param>
        /// <param name="certPath"></param>
        /// <param name="certPassword"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string xml, string url, bool isUseCert, int timeout, string certPath = "", string certPassword = "")
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 1000;
                request.Proxy = null;
                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;

                //是否使用证书
                if (isUseCert)
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, certPath);
                    var cert = new X509Certificate2(path, certPassword);
                    request.ClientCertificates.Add(cert);
                }

                //往服务器写入数据
                reqStream = await request.GetRequestStreamAsync();
                await reqStream.WriteAsync(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)(await request.GetResponseAsync());

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = (await sr.ReadToEndAsync()).Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                System.Threading.Thread.ResetAbort();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }

        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static async Task<string> GetAsync(string url)
        {
            System.GC.Collect();
            string result = "";

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Proxy = null;
                request.Method = "GET";

                //设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)(await request.GetResponseAsync());

                //获取HTTP返回数据
                var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = (await sr.ReadToEndAsync()).Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                System.Threading.Thread.ResetAbort();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }

        #endregion http连接基础类，负责底层的http通信

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
        public static async Task<T> ExecuteAsync<T>(IWechatRequest<T> request, WechatAccountConfig config, string url, bool isUseCert = false, int timeout = 6)
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
        public static async Task<WechatpayData> ExecuteAsync(WechatpayData inputObj, WechatAccountConfig config, string url, bool isUseCert = false, int timeout = 6)
        {
            if (config == null || string.IsNullOrEmpty(config.AppId) || string.IsNullOrEmpty(config.MchId))
            {
                throw new Exception("收款账号配置不能为空");
            }
            if (string.IsNullOrEmpty(config.SignKey))
            {
                throw new Exception("密钥配置不能为空");
            }
            inputObj.SetValue("appid", config.AppId);//公众账号ID
            inputObj.SetValue("mch_id", config.MchId);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", config.SignType);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign(config.SignType, config.SignKey));//签名

            string response = await PostAsync(inputObj.ToXml(), url, isUseCert, timeout, config.CertPath, config.CertPassword);

            var result = new WechatpayData();
            //若接口调用失败会返回xml格式的结果
            if (response.Substring(0, 5) == "<xml>")
            {
                result.FromXml(response);
                if (result.GetValue("return_code").ToString() != WechatConstants.SUCCESS)
                {
                    throw new Exception(result.GetValue("return_msg").ToString());
                }
                //验证签名,不通过会抛异常
                result.CheckSign(config.SignType, config.SignKey);
            }
            //接口调用成功则返回非xml格式的数据
            else
            {
                result.SetValue("return_code", "SUCCESS");
                result.SetValue("return_msg", "");
                result.SetValue("result_code", "SUCCESS");
                result.SetValue("body", response);
            }

            return result;
        }

        /// <summary>
        /// 创建支付调起微信参数
        /// </summary>
        /// <param name="config"></param>
        /// <param name="prepay_id"></param>
        /// <param name="tradeType"></param>
        /// <returns></returns>
        public static WechatpayData GetAppData(WechatAccountConfig config, object prepay_id, string tradeType)
        {
            if (config == null || string.IsNullOrEmpty(config.AppId) || string.IsNullOrEmpty(config.MchId))
            {
                throw new Exception("收款账号配置不能为空");
            }
            if (string.IsNullOrEmpty(config.SignKey))
            {
                throw new Exception("密钥配置不能为空");
            }
            var data = new WechatpayData();
            data.SetValue(WechatConstants.APPID, config.AppId);
            data.SetValue(WechatConstants.NONCESTR, HttpService.GenerateNonceStr());
            data.SetValue(WechatConstants.TIMESTAMP, (int)(DateTime.Now.ToUniversalTime().Ticks / 10000000 - 62135596800));
            switch (tradeType)
            {
                case WechatConstants.APP:
                    data.SetValue(WechatConstants.PARTNERID, config.MchId);
                    data.SetValue(WechatConstants.PREPAYID, prepay_id);
                    data.SetValue(WechatConstants.PACKAGE, "Sign=WXPay");
                    data.SetValue(WechatConstants.SIGN, data.MakeSign(config.SignType, config.SignKey));
                    break;
                case WechatConstants.MWEB:
                case WechatConstants.JSAPI:
                    data.SetValue(WechatConstants.PACKAGE, "prepay_id=" + prepay_id);
                    data.SetValue(WechatConstants.SIGNTYPE, config.SignType);
                    data.SetValue(WechatConstants.PAYSIGN, data.MakeSign(config.SignType, config.SignKey));
                    break;
                case WechatConstants.NATIVE:
                    break;
            }
            return data;
        }

        /// <summary>
        /// 根据当前系统时间加随机序列来生成订单号
        /// </summary>
        /// <param name="config"></param>
        /// <returns>订单号</returns>
        public static string GenerateOutTradeNo(WechatAccountConfig config)
        {
            if (config == null || string.IsNullOrEmpty(config.AppId) || string.IsNullOrEmpty(config.MchId))
            {
                throw new Exception("收款账号不能为空");
            }
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
