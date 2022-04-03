using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete
{
    public class EfCoreSettingsRepository : EfCoreGenericRepository<Slider>, ISettingsRepository
    {
        public EfCoreSettingsRepository(ShopContext context): base(context)
        {
            
        }
        private ShopContext ShopContext { get{ return context as ShopContext; } }
        public Arrangement ArngmntGetById(int id)
        {
            return ShopContext.Arrangement.Where(i => i.Id == id).FirstOrDefault();
        }

        public void ArngmntUpdate(Arrangement entity)
        {
            ShopContext.Set<Arrangement>().Update(entity);
        }

        public List<Slider> GetSlider()
        {
            return ShopContext.Sliders.OrderBy(i => i.Order).ToList();
        }
    }
}