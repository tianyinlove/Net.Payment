using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Alipay.AspNetCore.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class AlipayData
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
        public AlipayData()
        {
            _values = new SortedDictionary<string, string>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comparer">排序策略</param>
        public AlipayData(IComparer<string> comparer)
        {
            _values = new SortedDictionary<string, string>(comparer);
        }

        #endregion 构造函数

        #region 公开方法

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

        /// <summary>
        /// 获取Dictionary
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, string> GetValues()
        {
            return _values;
        }

        #endregion
    }
}
