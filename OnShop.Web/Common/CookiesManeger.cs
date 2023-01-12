using System;
using Microsoft.AspNetCore.Http;

namespace OnShop.Web.Common
{
    public class CookiesManager
    {
        public void Add(HttpContext context, string token, string value)
        {
            context.Response.Cookies.Append(token, value, getCookieOptions(context));
        }

        public bool Contains(HttpContext context, string token)
        {
            return context.Request.Cookies.ContainsKey(token);
        }

        public string GetValue(HttpContext context, string token)
        {
            if (!context.Request.Cookies.TryGetValue(token, out var cookieValue))
            {
                return null;
            }
            return cookieValue;
        }

        public void Remove(HttpContext context, string token)
        {
            if (context.Request.Cookies.ContainsKey(token))
            {
                context.Response.Cookies.Delete(token);
            }
        }


        private CookieOptions getCookieOptions(HttpContext context)
        {
            return new()
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(100),
            };
        }

        public Guid GetBrowserId(HttpContext context)
        {
            var browserId = GetValue(context, "BrowserId");
            if (string.IsNullOrEmpty(browserId))
            {
                var value = Guid.NewGuid().ToString();
                Add(context, "BrowserId", value);
                browserId = value;
            }

            Guid.TryParse(browserId, out var guidBrowserId);
            return guidBrowserId;
        }
    }
}
