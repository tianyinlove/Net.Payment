using Wechatpay.AspNetCore.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Wechatpay.AspNetCore
{
    /// <summary>
    /// 微信支付协议接口数据类，所有的API接口通信都依赖这个数据结构，
    /// 在调用接口之前先填充各个字段的值，然后进行接口通信，
    /// 这样设计的好处是可扩展性强，用户可随意对协议进行更改而不用重新设计数据结构，
    /// 还可以随意组合出不同的协议数据包，不用为每个协议设计一个数据包结构
    /// </summary>
    public class WechatpayData
    {
        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        /**
        * @将xml转为WxPayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws WxPayException
        */
        public SortedDictionary<string, object> FromXml(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.XmlResolver = null;
                xmlDoc.LoadXml(xml);
                var xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
                var nodes = xmlNode.ChildNodes;
                foreach (var xn in nodes)
                {
                    var xe = (XmlElement)xn;
                    if (!xe.IsEmpty && !string.IsNullOrEmpty(xe.InnerText))
                    {
                        SetValue(xe.Name, xe.InnerText);//获取xml的键值对到WxPayData内部的数据中
                    }
                }
            }
            return m_values;
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
                    SetValue(name, value);
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

        /**
        * 设置某个字段的值
        * @param key 字段名
         * @param value 字段值
        */
        public void SetValue(string key, object value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return;
            }

            if (IsSet(key))
            {
                m_values[key] = value;
            }
            else
            {
                m_values.Add(key, value);
            }
        }

        /**
        * 根据字段名获取某个字段的值
        * @param key 字段名
         * @return key对应的字段值
        */
        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
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

        /**
        * @将Dictionary转成xml
        * @return 经转换得到的xml串
        * @throws WxPayException
        **/
        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                throw new Exception("WxPayData数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    continue;
                }

                if (pair.Value.GetType() == typeof(int) || pair.Value.GetType() == typeof(decimal) || pair.Value.GetType() == typeof(double))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    throw new Exception("WxPayData字段数据类型错误!");
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
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    continue;
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        /**
        * @Dictionary格式化成Json
         * @return json串数据
        */
        public string ToJson()
        {
            return JsonConvert.SerializeObject(m_values);
        }

        /**
        * @values格式化成能在Web页面上显示的结果（因为web页面上不能直接输出xml格式的字符串）
        */
        public string ToPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    continue;
                }

                str += string.Format("{0}={1}<br>", pair.Key, pair.Value.ToString());
            }
            return str;
        }

        /**
         * 判断某个字段是否已设置
         * @param key 字段名
         * @return 若字段key已被设置，则返回true，否则返回false
         */
        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
        */
        public string MakeSign(string signType, string signKey)
        {
            //转url格式
            string str = ToUrl();
            //在string后加入API KEY
            str += "&key=" + signKey;
            if (signType == WechatConstants.HMAC_SHA256)
            {
                return CalcHMACSHA256Hash(str, signKey);
            }
            else if (signType == WechatConstants.MD5)
            {
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
            else
            {
                throw new Exception("sign_type 不合法");
            }
        }

        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
        */
        public bool CheckSign(string signType, string signKey)
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                throw new Exception("签名存在但不合法!");
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

        /**
        * @获取Dictionary
        */
        public SortedDictionary<string, object> GetValues()
        {
            return m_values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string CalcHMACSHA256Hash(string plaintext, string salt)
        {
            string result = "";
            var enc = Encoding.Default;
            byte[]
            baText2BeHashed = enc.GetBytes(plaintext),
            baSalt = enc.GetBytes(salt);
            HMACSHA256 hasher = new HMACSHA256(baSalt);
            byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
            result = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
            return result;
        }
    }
}
