﻿using Meow.Dependency;

namespace Meow.Web;

/// <summary>
/// IP访问器
/// </summary>
public interface IIpAccessor : ISingletonDependency {
    /// <summary>
    /// 获取IP地址
    /// </summary>
    string GetIp();
}