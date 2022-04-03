using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface ISettingsService
    {
        List<Slider> GetAll();
        Slider GetById(int id);
        bool Create(Slider entity);
        void Update(Slider entity);
        void Delete(Slider entity);

        List<Slider> GetSlider();
        int GetArrangement();
        Arrangement ArngmntGetById(int id);
        void ArngmntUpdate(Arrangement entity);
    }
}