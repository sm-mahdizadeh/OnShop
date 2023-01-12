using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using OnShop.Domain.Notifications.Commands;
using OnShop.Domain.Notifications.Queries;
using OnShop.Web.Common;
using System.Collections.Generic;
using System.Linq;

namespace OnShop.Web.Areas.Admin.Controllers
{
    public class BaseController<T> : OnShop.Framework.Web.BaseController<T> where T : class
    {
        public BaseController(IMediator mediator, UserManager<T> userManager) : base(mediator, userManager)
        {

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (Request.IsAjaxRequest())
            {
                base.OnActionExecuting(context);
                return;
            }
            
            var page = $"/{context.ActionDescriptor.RouteValues["area"]}/{context.ActionDescriptor.RouteValues["controller"]}/{context.ActionDescriptor.RouteValues["action"]}".ToLower();

            ViewData["PageId"] = page.Replace("/", "");
            if (PageInfo.TryGetValue(page, out var info))
            {
                ViewData["PageTitle"] = info.Title;


                var dic = new Dictionary<string, string>();
                if (page != "/admin/home/index")
                {
                    dic.Add(info.Title, page);
                    page = info.Parent;
                    while (PageInfo.TryGetValue(page, out info))
                    {
                        dic.Add(info.Title, page);
                        page = info.Parent;
                    }
                }
                ViewData["PageHeader"] = dic;
            }

            base.OnActionExecuting(context);
        }

        public Dictionary<string, (string Title, string Parent)> PageInfo = new Dictionary<string, (string Title, string Parent)>
        {
            {"/admin/home/index" ,("داشبورد","")},
            {"/admin/home/usermanagment" ,("مدیریت کاربران","")},
            {"/admin/roles/index" ,("دسترسی","/admin/home/usermanagment")},
            {"/admin/user/index" ,("لیست کاربران","/admin/home/usermanagment")},

            {"/admin/home/productmanagment" ,("مدیریت کالا","")},
            {"/admin/brand/index" ,("برند","/admin/home/productmanagment")},
            {"/admin/category/index" ,("دسته بندی","/admin/home/productmanagment")},
            {"/admin/category/create" ,("افزودن دسته بندی","/admin/category/index")},
            {"/admin/category/edit" ,("ویرایش دسته بندی","/admin/category/index")},

            {"/admin/product/index" ,("محصولات","/admin/home/productmanagment")},
            {"/admin/product/create" ,("افزودن محصولات ","/admin/product/index")},

            {"/admin/home/sitemanagment" ,("مدیریت سایت","")},
            {"/admin/store/index" ,("فروشگاه","/admin/home/sitemanagment")},
            {"/admin/store/create" ,("افزودن فروشگاه","/admin/store/index")},
            {"/admin/store/edit" ,("ویرایش فروشگاه","/admin/store/index")},
            {"/admin/store/details" ,("اطلاعات فروشگاه","/admin/store/index")},
            
            {"/admin/slider/index" ,("اسلایدر","/admin/home/sitemanagment")},
            {"/admin/slider/create" ,("افزودن اسلایدر","/admin/slider/index")},
            {"/admin/slider/edit" ,("ویرایش اسلایدر","/admin/slider/index")},

            {"/admin/arrangement/index" ,("صفحه اول","/admin/home/sitemanagment")},
            {"/admin/arrangement/create" ,("افزودن چیدمان","/admin/arrangement/index")},
            {"/admin/arrangement/edit" ,("ویرایش چیدمان","/admin/arrangement/index")},

            {"/admin/home/blogmanagment" ,("مدیریت وبلاگ","")},
            {"/admin/postcategory/index" ,("دسته بندی ","/admin/home/blogmanagment")},
            {"/admin/postcategory/create" ,("افزودن دسته بندی","/admin/postcategory/index")},
            {"/admin/postcategory/edit" ,("ویرایش دسته بندی","/admin/postcategory/index")},

            {"/admin/post/index" ,("پست","/admin/home/blogmanagment")},
            {"/admin/post/create" ,("افزودن پست","/admin/post/index")},
            {"/admin/post/edit" ,("ویرایش پست","/admin/post/index")},

            {"/admin/home/others" ,("سایر","")},
            {"/admin/home/calendar" ,("تقویم","/admin/home/others")},
            {"/admin/notification/index" ,("اعلان ها","/admin/home/others")},
        };


    }
}
