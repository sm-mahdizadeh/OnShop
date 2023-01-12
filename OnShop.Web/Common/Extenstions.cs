using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnShop.Web.Common
{
    public static class Extenstions
    {
        public static List<SelectListItem> ToSelectItemList<T>(this IEnumerable<T> collection, Func<T, string> nameGetter, Func<T, long> idGetter)
        {
            var list = new List<SelectListItem>
            {new()
            {
                Text = "انتخاب کنید",
                Value = "",
                Selected = true

            }};
            var clList = collection.Select(m => new SelectListItem
            {
                Text = nameGetter(m),
                Value = idGetter(m).ToString()
            }).ToList();

            if (clList.Any())
            {
                list.AddRange(clList);
            }

            return list;
        }

        /// <summary>
        /// Determines whether the specified HTTP request is an AJAX request.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified HTTP request is an AJAX request; otherwise, false.
        /// </returns>
        /// <param name="request">The HTTP request.</param><exception cref="T:System.ArgumentNullException">The <paramref name="request"/> parameter is null (Nothing in Visual Basic).</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public static string ToToman(this decimal value)
        {
            var str = $"{value:n0} تومان";
            return str;
        }

    }
}