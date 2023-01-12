using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Slider.Repositories;

namespace OnShop.DAL.Slider.Repositories
{
    public class SliderRepository :EfRepository<Domain.Slider.Entities.Slider>, ISliderRepository
    {
        public SliderRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}