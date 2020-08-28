﻿using System;

namespace Meow.Extensions.Helpers
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : Enum
        {
            return Meow.Helpers.Enum.Parse<TEnum>(value);
        }

        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
        {
            return Meow.Helpers.Enum.Parse<TEnum>(value);
        }

        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static TEnum ToEnum<TEnum>(this int? value) where TEnum : Enum
        {
            return Meow.Helpers.Enum.Parse<TEnum>(value);
        }
    }
}
