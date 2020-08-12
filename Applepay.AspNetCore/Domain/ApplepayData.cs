using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Applepay.AspNetCore.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplepayData
    {
        #region 私有字段

        readonly SortedDictionary<string, string> _values;

        #endregion 私有字段

        #region 属性

        public string this[string key]
        {
            get => _values[key];
            set => _values[key] = value;
        }

        public int Count => _values.Count;

        #endregion 属性

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApplepayData()
        {
            _values = new SortedDictionary<string, string>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comparer">排序策略</param>
        public ApplepayData(IComparer<string> comparer)
        {
            _values = new SortedDictionary<string, string>(comparer);
        }

        #endregion 构造函数

        #region 公开方法

        /// <summary>
        /// 将数据转成网关数据
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public void FromObject(object res)
        {
            PropertyInfo[] pis = res.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (!pi.CanRead)
                {
                    continue;
                }

                string name = GetElementName(pi);
                object value = GetElementValue(res, pi);
                if (!string.IsNullOrEmpty(name) && value != null)
                {
                    SetValue(name, value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取对象的属性值
        /// </summary>
        /// <param name="res"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        object GetElementValue(object res, PropertyInfo pi)
        {
            if (!pi.CanRead)
            {
                return null;
            }

            object value = pi.GetValue(res, null);
            return value;
        }

        /// <summary>
        /// 获取Xml属性名
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        string GetElementName(PropertyInfo pi)
        {
            if (pi == null)
            {
                return null;
            }

            // 获取XmlElement属性
            XmlElementAttribute[] xeas = pi.GetCustomAttributes(typeof(XmlElementAttribute), true) as XmlElementAttribute[];
            string elementName = null;
            if (xeas != null && xeas.Length > 0)
            {
                elementName = xeas[0].ElementName;
            }

            // 如果获取XmlElement属性为空，则去获取XmlArray属性
            if (string.IsNullOrEmpty(elementName))
            {
                XmlArrayAttribute[] xaas = pi.GetCustomAttributes(typeof(XmlArrayAttribute), true) as XmlArrayAttribute[];
                if (xaas != null && xaas.Length > 0)
                {
                    elementName = xaas[0].ElementName;
                }
            }

            // 如果获取XmlElement属性为空，则去获取XmlArray属性
            if (string.IsNullOrEmpty(elementName))
            {
                JsonPropertyAttribute[] xaas = pi.GetCustomAttributes(typeof(JsonPropertyAttribute), true) as JsonPropertyAttribute[];
                if (xaas != null && xaas.Length > 0)
                {
                    elementName = xaas[0].PropertyName;
                }
            }

            // 如果获取XmlElement属性为空，则去获取Name属性
            if (string.IsNullOrEmpty(elementName))
            {
                elementName = pi.Name.ToLower();
            }
            return elementName;
        }

        /// <summary>
        /// 将Url格式数据转换为网关数据
        /// </summary>
        /// <param name="url">url数据</param>
        /// <param name="isUrlDecode">是否需要url解码</param>
        /// <returns></returns>
        public void FromUrl(string url, bool isUrlDecode = true)
        {
            if (!string.IsNullOrEmpty(url))
            {
                int index = url.IndexOf('?');

                if (index == 0)
                {
                    url = url.Substring(index + 1);
                }

                var regex = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
                var mc = regex.Matches(url);

                foreach (Match item in mc)
                {
                    string value = item.Result("$3");
                    SetValue(item.Result("$2"), isUrlDecode ? WebUtility.UrlDecode(value) : value);
                }
            }
        }

        /// <summary>
        /// 将Xml格式数据转换为网关数据
        /// </summary>
        /// <param name="xml">Xml数据</param>
        /// <returns></returns>
        public void FromXml(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                var xmlNode = xmlDoc.FirstChild;
                var nodes = xmlNode.ChildNodes;
                foreach (var item in nodes)
                {
                    var xe = (XmlElement)item;
                    if (!xe.IsEmpty && !string.IsNullOrEmpty(xe.InnerText))
                    {
                        SetValue(xe.Name, xe.InnerText);
                    }
                }
            }
        }

        /// <summary>
        /// 将Json格式数据转成网关数据
        /// </summary>
        /// <param name="json">json数据</param>
        /// <returns></returns>
        public void FromJson(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                var jObject = JObject.Parse(json);
                var list = jObject.Children().OfType<JProperty>();
                foreach (var item in list)
                {
                    SetValue(item.Name, item.Value.ToString());
                }
            }
        }

        /// <summary>
        /// 是否存在指定参数名
        /// </summary>
        /// <param name="key">参数名</param>
        /// <returns></returns>
        public bool Exists(string key) => _values.ContainsKey(key);

        /// <summary>
        /// 清空网关数据
        /// </summary>
        public void Clear()
        {
            _values.Clear();
        }

        /// <summary>
        /// 移除指定参数
        /// </summary>
        /// <param name="key">参数名</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _values.Remove(key);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public void SetValue(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                return;
            }

            if (Exists(key))
            {
                _values[key] = value;
            }
            else
            {
                _values.Add(key, value);
            }
        }

        /// <summary>
        /// 根据参数名获取参数值
        /// </summary>
        /// <param name="key">参数名</param>
        /// <returns>参数值</returns>
        public string GetValue(string key)
        {
            _values.TryGetValue(key, out string value);
            return value;
        }

        /// <summary>
        /// 将网关参数转为类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="stringCase">字符串策略</param>
        /// <returns></returns>
        public T ToObject<T>()
        {
            return JsonConvert.DeserializeObject<T>(ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(_values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == _values.Count)
            {
                return "";
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, string> pair in _values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    continue;
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
            }
            xml += "</xml>";
            return xml;
        }

        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, string> pair in _values)
            {
                if (string.IsNullOrEmpty(pair.Value) || string.IsNullOrEmpty(pair.Key))
                {
                    continue;
                }

                if (pair.Key != "sign")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        /// <summary>
        /// 获取Dictionary
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, string> GetValues()
        {
            return _values;
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
        */
        public string MakeSign(string signType, string signKey)
        {
            //转url格式
            string str = GetValue("outtradeno");
            if (string.IsNullOrEmpty(str))
            {
                str = GetValue("out_trade_no");
            }
            //在string后加入API KEY
            str += "&key=" + signKey;

            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
        */
        public bool CheckSign(string signType, string signKey)
        {
            //如果没有设置签名，则跳过检测
            if (!Exists("sign"))
            {
                throw new Exception("签名不存在!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                throw new Exception("签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string cal_sign = MakeSign(signType, signKey);

            if (cal_sign == return_sign)
            {
                return true;
            }

            throw new Exception("签名验证错误!");
        }

        #endregion
    }
}
