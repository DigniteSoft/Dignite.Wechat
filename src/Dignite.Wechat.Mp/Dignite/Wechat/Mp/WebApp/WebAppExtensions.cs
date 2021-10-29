﻿using Microsoft.AspNetCore.Builder;


namespace Dignite.Wechat.Mp.WebApp
{
    public static class WebAppExtensions
    {

        /// <summary>
        /// 向中间件管道注册微信登陆中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWechatWebAppSignIn(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SignInMiddleware>();
        }

        /// <summary>
        /// 向中间件管道注册用户网页授权中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWechatWebAppAuthorizationUrl(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationUrlMiddleware>();
        }

        /// <summary>
        /// 向中间件管道注册获取微信JSAPI签名中间件
        /// 此中间件用于生成wx.config中的几个参数值
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWechatWebAppJsapiSignature(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JsapiSignatureMiddleware>();
        }
    }
}
