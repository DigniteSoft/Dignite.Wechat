## 介绍

Dignite BlobStoring是基于[ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)开发，为BLOB存储提供授权验证、 数据流处理管道、BLOB 信息记录等辅助功能。

这些功能是扩展了ABP BlobStoring功能特征，本文着重介绍在ABP BlobStoring功能基础上添加的功能.

## 安装

[Dignite.Abp.BlobStoring](https://www.nuget.org/packages/Dignite.Abp.BlobStoring)是定义BLOB存储服务的主要包. 

在项目中安装 `Dignite.Abp.BlobStoring`NuGet包，然后将`[DependsOn(typeof(DigniteAbpBlobStoringModule))]`添加到项目内的[ABP模块](https://docs.abp.io/zh-Hans/abp/latest/Module-Development-Basics)类中.

## 微信网页授权

用于通过微信网页授权机制，来获取微信用户基本信息，从而实现微信用户自动登陆、引导注册等功能。

关于微信网页授权机制详细介绍，请[点击这里](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html)了解。

### 引导用户授权

服务器端

````csharp
public override void OnApplicationInitialization(ApplicationInitializationContext context)
{
    ...
    
    //配置微信公众号登陆、微信小程序登陆等中间件
    app.UseWechatMp();

    ...
}
````

客户端
访问如下地址:
`/wechat/mp/webapp/authorization?scope=snsapi_base|snsapi_userinfo&callbackUrl=用户同意授权后的回调地址`

如果用户同意授权，页面将跳转至 callbackUrl?code=CODE&state=STATE。

用 code 换取AccessToken信息后，




用openId查询微信用户是否注册，如没有注册，拿token再获取用户资料并注册


微信小程序登录模块集成
1、