using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.business.Concrete
{
    public class SettingsManager : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private data.Concrete.ShopContext _context;
        public SettingsManager(IUnitOfWork unitOfWork, data.Concrete.ShopContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public Arrangement ArngmntGetById(int id)
        {
            return _unitOfWork.Settings.ArngmntGetById(id);
        }

        public void ArngmntUpdate(Arrangement entity)
        {
            _unitOfWork.Settings.ArngmntUpdate(entity);
            _unitOfWork.Save();
        }

        public bool Create(Slider entity)
        {
            _unitOfWork.Settings.Create(entity);
            _unitOfWork.Save();
            return true;
        }

        public void Delete(Slider entity)
        {
            _unitOfWork.Settings.Delete(entity);
            _unitOfWork.Save();
        }

        public List<Slider> GetAll()
        {
            return _unitOfWork.Settings.GetAll();
        }

        public int GetArrangement()
        {
            
            return _context.Arrangement.Select(i => i.Content).FirstOrDefault();            
        }

        public Slider GetById(int id)
        {
            return _unitOfWork.Settings.GetById(id);
        }

        public List<Slider> GetSlider()
        {
            return _unitOfWork.Settings.GetSlider();
        }

        public void Update(Slider entity)
        {
            _unitOfWork.Settings.Update(entity);
            _unitOfWork.Save();
        }
    }
}