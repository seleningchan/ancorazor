﻿using Blog.API.Common.Constants;
using Microsoft.Extensions.Options;
using Siegrain.Common;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Blog.API.Common
{
    public class UrlHelper
    {
        private readonly SEOConfiguration _seoConfiguration;
        public UrlHelper(IOptions<SEOConfiguration> seoConfiguration)
        {
            _seoConfiguration = seoConfiguration.Value;
        }

        public static string ToUrlSafeString(string source, bool convertToPinyin)
        {
            var str = convertToPinyin ? CHNToPinyin.ConvertToPinYin(source) : source;
            return Regex.Replace(str, Constants.Constants.Article.RouteReplaceRegex, " ")
                .Trim()
                .Replace(" ", "-")
                .ToLowerInvariant();
        }

        public static string UrlStringEncode(string source)
        {
            return WebUtility.HtmlEncode(source.Replace(" ", "-"));
        }

        public string GetArticleRoutePath(int id, DateTime date, string alias, string category)
        {
            category = category ?? Constants.Constants.Article.DefaultCategoryName;
            var path = _seoConfiguration.ArticleRouteMapping
                .Replace(nameof(id), id.ToString())
                .Replace(nameof(date), date.ToString("yyyy/MM/dd"))
                .Replace(nameof(category), CHNToPinyin.ConvertToPinYin(category))
                .Replace(nameof(alias), alias)
                .ToLowerInvariant();
            return path;
        }
    }
}