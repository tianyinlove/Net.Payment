﻿源代码：https://github.com/tianyinlove/Net.Payment.git

eg:
/// <summary>
/// 
/// </summary>
[Test]
public async Task Test1()
{
    var config = new WechatpayConfig
    {
        NotifyUrl = "",
        AppId = "",
        MchId = "",
        SignKey = ""
    };

    var request = new WechatTradeAppPayRequest
    {
        TotalFee = 100,
        OutTradeNo = "202007297777",
        Body = "产品",
        Detail = "产品",
        TimeExpire = DateTime.Now.AddDays(15).ToString("yyyyMMddHHmmss"),
        TradeType = WechatConstants.APP,
        TimeStart = DateTime.Now.ToString("yyyyMMddHHmmss"),
        NotifyUrl = config.NotifyUrl
    };

    var response = await WechatpayClient.CreateOrderAsync(request, config);

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
    var config = new WechatpayConfig
    {
        NotifyUrl = "",
        AppId = "",
        MchId = "",
        SignKey = ""
    };

    var request = new WechatTradeQueryRequest
    {
        OutTradeNo = "202007297777"
    };

    var response = await WechatpayClient.OrderQueryAsync(request, config);

    if (string.IsNullOrEmpty(response.TransactionId))
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
    var config = new WechatpayConfig
    {
        NotifyUrl = "",
        AppId = "",
        MchId = "",
        SignKey = ""
    };

    var request = new WechatRefundRequest
    {
        OutTradeNo = "202007297777",
        TotalFee = 100,
        RefundFee = 100,
        OutRefundNo = WechatService.GenerateOutTradeNo(config)
    };

    var response = await WechatpayClient.RefundAsync(request, config);

    if (string.IsNullOrEmpty(response.TransactionId))
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
    var config = new WechatpayConfig
    {
        NotifyUrl = "",
        AppId = "",
        MchId = "",
        SignKey = ""
    };

    var request = new WechatTradeCloseRequest
    {
        OutTradeNo = "202007297777"
    };

    var response = await WechatpayClient.CloseOrderAsync(request, config);

    if (string.IsNullOrEmpty(response.ReturnCode))
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
    var data = "";//xml请求数据
    var request = new WechatpayData();
    request.FromXml(data);

    var config = new WechatpayConfig
    {
        NotifyUrl = "",
        AppId = "",
        MchId = "",
        SignKey = ""
    };
    var response = WechatpayClient.Notify(request, config);

    if (string.IsNullOrEmpty(response.OutTradeNo))
    {
        Assert.Fail();
    }
    else
    {
        Assert.Pass();
    }
}