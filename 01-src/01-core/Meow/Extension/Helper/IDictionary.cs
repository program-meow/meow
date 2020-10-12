﻿using System.Collections.Generic;
using System.Linq;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 字典扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <typeparam name="TKey">键元素类型</typeparam>
        /// <typeparam name="TValue">值元素类型</typeparam>
        /// <param name="value">值</param>
        public static bool IsEmpty<TKey, TValue>(this IDictionary<TKey, TValue> value)
        {
            if (value.IsNull())
                return true;
            return !value.Any();
        }
    }
}