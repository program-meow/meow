﻿namespace Meow.Microservice.Dapr.Events.Filters;

/// <summary>
/// 集成事件订阅错误日志过滤器
/// </summary>
public class SubscriptionErrorLogFilterAttribute : ExceptionFilterAttribute {
    /// <summary>
    /// 异常处理
    /// </summary>
    public override void OnException( ExceptionContext context ) {
        if( context == null )
            return;
        ILogger<SubscriptionErrorLogFilterAttribute> log = context.HttpContext.RequestServices.GetService<ILogger<SubscriptionErrorLogFilterAttribute>>();
        SystemException exception = context.Exception.GetRawException();
        if( exception is Warning warning ) {
            log.LogWarning( warning , $"集成事件订阅处理失败:{exception.Message}" );
            return;
        }
        log.LogError( exception , $"集成事件订阅处理失败:{exception.Message}" );
    }
}