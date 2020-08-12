using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Applepay.AspNetCore.Constants;
using Applepay.AspNetCore.Domain;

namespace Applepay.AspNetCore.Util
{
    /// <summary>
    /// 
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
        /// <param name="url"></param>
        /// <param name="datas"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string datas, int timeout)
        {
            System.GC.Collect();
            string result = "";

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
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                request.Timeout = timeout * 1000;
                request.Proxy = null;

                //设置POST的数据类型和长度
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] data = Encoding.UTF8.GetBytes(datas);
                request.ContentLength = data.Length;

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
        /// <param name="inputObj"></param>
        /// <param name="certPath"></param>
        /// <param name="certPassword"></param>
        /// <param name="timeout"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<ApplepayData> ExecuteAsync(ApplepayData inputObj, ApplepayConfig config, string url, int timeout = 6)
        {
            string response = await PostAsync(inputObj.ToJson(), url, timeout);
            var result = new ApplepayData();
            result.FromJson(response);
            return result;
        }
    }
}
