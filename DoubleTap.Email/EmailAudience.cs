﻿using System;
using System.Linq;

namespace DoubleTap.Email
{
    public class EmailAudience : IEmailAudience
    {
        readonly string[] _categories;

        public EmailAudience(string[] categories, string[] to, string @from)
        {
            _categories = categories;
            To = to;
            From = @from;
        }

        public string[] To { get; }
        public string From { get; }
        public bool Targets(string category)
        {
            return _categories.Contains(category, StringComparer.OrdinalIgnoreCase);
        }
    }
}