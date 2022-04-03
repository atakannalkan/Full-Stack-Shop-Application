using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface ISettingsRepository : IRepository<Slider>
    {
        List<Slider> GetSlider();
        
        Arrangement ArngmntGetById(int id);
        void ArngmntUpdate(Arrangement entity);
    }
}