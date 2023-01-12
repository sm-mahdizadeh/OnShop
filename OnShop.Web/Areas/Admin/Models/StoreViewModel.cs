using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OnShop.Domain.Stores.Commands;
using OnShop.Domain.Stores.Dtos;

namespace OnShop.Web.Areas.Admin.Models
{
    public class StoreViewModel
    {
        public StoreViewModel()
        {
            
        }
        public StoreViewModel(StoreDto obj)
        {
            Map(obj);
        }
        public void Map(StoreDto obj)
        {
            Address = obj.Address;
            Description = obj.Description;
            Title = obj.Title;
            PhoneNumber = obj.PhoneNumber;
            Code = obj.Code;
            Id = obj.Id;
        }
        public StoreUpdateCommand Map()
        {
            return new StoreUpdateCommand
            {
                Title = Title,
                Description = Description,
                PhoneNumber = PhoneNumber,
                Address = Address,
                BigLogo = BigLogoFileSrc,
                Code = Code,
                Id = Id,
            };
        }
        
        [Display(Name = "شناسه ")]
        public long Id { get; set; }
        [Display(Name = "کد ")]
        public string Code { get; set; }
        [Display(Name = "عنوان ")]
        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }
        [Display(Name = "لوگو")]
        public IFormFile BigLogoFile { get; set; }
        public string BigLogoFileSrc { get; set; }

        [Display(Name = "کلیه قوانین و مقررات را مطالعه کرده و قبول دارم")]
        public bool Agree { get; set; }
        public string StoreUrl =>$"/Store/{(string.IsNullOrWhiteSpace(Code) ? Id.ToString() : Code)}";
    }

    public class StoreDetailsViewModel : StoreViewModel
    {
        public StoreDetailsViewModel(StoreDto obj):base(obj) { }
        [Display(Name = "تعداد محصولات ثبت شده")]
        public int? ProductCount { get; set; }
        [Display(Name = "تعداد پست های نوشته شده")]
        public int? PostCount { get; set; }
    }
}
