﻿using System.Collections.Generic;
using System.ComponentModel;
using Meow.Extension.Helper;
using Newtonsoft.Json;

namespace Meow.Parameter.Object
{
    /// <summary>
    /// 列表项树
    /// </summary>
    public class ItemTree : Item
    {
        /// <summary>
        /// 初始化列表项树
        /// </summary>
        public ItemTree() : this(null, null)
        {
        }

        /// <summary>
        /// 初始化列表项树
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        public ItemTree(string text, object value, int? sortId = 1) : this(text, value, new List<ItemTree>(), sortId)
        {
        }

        /// <summary>
        /// 初始化列表项树
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="subsets">子集集合</param>
        /// <param name="sortId">排序号</param>
        public ItemTree(string text, object value, IEnumerable<ItemTree> subsets, int? sortId = 1) : base(text, value, sortId)
        {
            Subsets = new List<ItemTree>();
            AddSubset(subsets);
        }

        /// <summary>
        /// 子集
        /// </summary>
        [DisplayName("子集")]
        [JsonProperty("subsets", NullValueHandling = NullValueHandling.Ignore)]
        public List<ItemTree> Subsets { get; set; }

        /// <summary>
        /// 添加子集
        /// </summary>
        /// <param name="subset">子集</param>
        public ItemTree AddSubset(ItemTree subset)
        {
            Subsets.AddNoNull(subset);
            return this;
        }

        /// <summary>
        /// 添加子集
        /// </summary>
        /// <param name="subsets">子集集合</param>
        public ItemTree AddSubset(IEnumerable<ItemTree> subsets)
        {
            if (subsets.IsNull())
                return this;
            foreach (var item in subsets)
                AddSubset(item);
            return this;
        }
    }
}