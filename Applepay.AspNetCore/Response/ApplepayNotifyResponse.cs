using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Applepay.AspNetCore.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplepayNotifyResponse
    {
        /// <summary>
        /// 交易状态
        /// </summary>
        [XmlElement("status")]
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// 收据生成的环境。 可能的值： Sandbox, Production
        /// </summary>
        [XmlElement("environment")]
        [JsonProperty("environment")]
        public string Environment { get; set; }

        /// <summary>
        /// 发送用于验证的收据的JSON表示形式。
        /// </summary>
        [XmlElement("receipt")]
        [JsonProperty("receipt")]
        public AppleReceipt Receipt { get; set; }
    }

    /// <summary>
    /// 发送用于验证的收据的JSON表示形式。
    /// </summary>
    public class AppleReceipt
    {
        /// <summary>
        /// 应用程序的版本号。该应用程序的版本号对应于（在iOS中）或（在macOS中）中的值。在生产中，此值为基于的设备上应用程序的当前版本。在沙盒中，该值始终为。CFBundleVersionCFBundleShortVersionStringInfo.plistreceipt_creation_date_ms"1.0"
        /// </summary>
        [XmlElement("application_version")]
        [JsonProperty("application_version")]
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// 收据所属应用的捆绑标识符。您在App Store Connect上提供此字符串。这相当于价值中的应用程序的文件。CFBundleIdentifierInfo.plist
        /// </summary>
        [XmlElement("bundle_id")]
        [JsonProperty("bundle_id")]
        public string BundleId { get; set; }

        /// <summary>
        /// 包含所有应用内购买交易的应用内购买收据字段的数组。
        /// </summary>
        [XmlElement("in_app")]
        [JsonProperty("in_app")]
        public List<AppleInApp> InApp { get; set; }

        /// <summary>
        /// 生成的收据类型。该值对应于购买应用程序或VPP的环境。
        /// 可能的值： Production, ProductionVPP, ProductionSandbox, ProductionVPPSandbox
        /// </summary>
        [XmlElement("receipt_type")]
        [JsonProperty("receipt_type")]
        public string ReceiptType { get; set; }
    }

    /// <summary>
    /// 包含所有应用内购买交易的数组。这不包括已被您的应用标记为完成的消耗品交易。仅针对包含自动续订的收据返回。
    /// </summary>
    public class AppleInApp
    {
        /// <summary>
        /// Apple客户支持取消交易的时间，其日期时间格式类似于ISO8601。此字段仅用于退款交易。
        /// </summary>
        [XmlElement("cancellation_date")]
        [JsonProperty("cancellation_date")]
        public string CancellationDate { get; set; }

        /// <summary>
        /// Apple客户支持取消交易的时间，或者自动更新的订阅计划的升级时间（以UNIX纪元时间格式），以毫秒为单位。此字段仅适用于退款交易。使用此时间格式来处理日期。请参阅以获取更多信息。cancellation_date_ms
        /// </summary>
        [XmlElement("cancellation_date_ms")]
        [JsonProperty("cancellation_date_ms")]
        public long CancellationDateMs { get; set; }

        /// <summary>
        /// Apple客户支持取消交易的时间（太平洋时间）。此字段仅适用于退款交易。
        /// </summary>
        [XmlElement("cancellation_date_pst")]
        [JsonProperty("cancellation_date_pst")]
        public string CancellationDatePst { get; set; }

        /// <summary>
        /// 交易退款的原因。当客户取消交易时，App Store会给他们退款并为此密钥提供价值。值“1”表示客户由于您的应用程序中存在实际或可感知的问题而取消了交易。值“0”表示交易因其他原因被取消；例如，如果客户意外购买。
        /// 可能的值： 1, 0
        /// </summary>
        [XmlElement("cancellation_reason")]
        [JsonProperty("cancellation_reason")]
        public string CancellationReason { get; set; }

        /// <summary>
        /// 订阅到期的时间或续订的时间，其日期时间格式类似于ISO 8601。
        /// </summary>
        [XmlElement("expires_date")]
        [JsonProperty("expires_date")]
        public string ExpiresDate { get; set; }

        /// <summary>
        /// 订阅到期的时间或续订的时间（以UNIX纪元时间格式），以毫秒为单位。使用此时间格式来处理日期。请参阅以获取更多信息。expires_date_ms
        /// </summary>
        [XmlElement("expires_date_ms")]
        [JsonProperty("expires_date_ms")]
        public long ExpiresDateMs { get; set; }

        /// <summary>
        /// 订阅到期的时间或续订的时间，在太平洋时区。
        /// </summary>
        [XmlElement("expires_date_pst")]
        [JsonProperty("expires_date_pst")]
        public string ExpiresDatePst { get; set; }

        /// <summary>
        /// 原始应用购买时间，格式类似于ISO 8601。
        /// </summary>
        [XmlElement("original_purchase_date")]
        [JsonProperty("original_purchase_date")]
        public string OriginalPurchaseDate { get; set; }

        /// <summary>
        /// 原始应用购买时间（以UNIX纪元时间格式），以毫秒为单位。使用此时间格式来处理日期。对于自动续订的订阅，此值指示订阅的首次购买日期。原始购买日期适用于所有产品类型，并且在相同商品ID的所有交易中都保持不变。该值对应于StoreKit中原始交易的属性。transactionDate
        /// </summary>
        [XmlElement("original_purchase_date_ms")]
        [JsonProperty("original_purchase_date_ms")]
        public long OriginalPurchaseDateMs { get; set; }

        /// <summary>
        /// 原始应用购买时间（太平洋时间）。
        /// </summary>
        [XmlElement("original_purchase_date_pst")]
        [JsonProperty("original_purchase_date_pst")]
        public string OriginalPurchaseDatePst { get; set; }

        /// <summary>
        /// 原始购买的交易标识符。请参阅以获取更多信息。original_transaction_id
        /// </summary>
        [XmlElement("original_transaction_id")]
        [JsonProperty("original_transaction_id")]
        public string OriginalTransactionId { get; set; }

        /// <summary>
        /// 购买产品的唯一标识符。您在App Store Connect中创建产品时提供此值，它对应于交易的付款属性中存储的对象的属性。productIdentifierSKPayment
        /// </summary>
        [XmlElement("product_id")]
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// 原始应用购买时间，格式类似于ISO 8601。
        /// </summary>
        [XmlElement("purchase_date")]
        [JsonProperty("purchase_date")]
        public string PurchaseDate { get; set; }

        /// <summary>
        /// 原始应用购买时间（以UNIX纪元时间格式），以毫秒为单位。使用此时间格式来处理日期。对于自动续订的订阅，此值指示订阅的首次购买日期。原始购买日期适用于所有产品类型，并且在相同商品ID的所有交易中都保持不变。该值对应于StoreKit中原始交易的属性。transactionDate
        /// </summary>
        [XmlElement("purchase_date_ms")]
        [JsonProperty("purchase_date_ms")]
        public long PurchaseDateMs { get; set; }

        /// <summary>
        /// 原始应用购买时间（太平洋时间）。
        /// </summary>
        [XmlElement("purchase_date_pst")]
        [JsonProperty("purchase_date_pst")]
        public string PurchaseDatePst { get; set; }

        /// <summary>
        /// 购买的消费品数量。该值对应于SKPayment存储在交易的付款属性中的对象的数量属性。该值通常是“1”除非可以通过可变付款进行修改。最大值为10。
        /// </summary>
        [XmlElement("quantity")]
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 交易的唯一标识符，例如购买，还原或续订。请参阅以获取更多信息。transaction_id
        /// </summary>
        [XmlElement("transaction_id")]
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// 跨设备购买事件（包括订阅更新事件）的唯一标识符。此值是识别订阅购买的主键。
        /// </summary>
        [XmlElement("web_order_line_item_id")]
        [JsonProperty("web_order_line_item_id")]
        public string WebOrderLineItemId { get; set; }
    }
}
