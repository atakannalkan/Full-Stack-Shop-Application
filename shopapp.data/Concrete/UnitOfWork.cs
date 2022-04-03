using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;
        public UnitOfWork(ShopContext context)
        {
            _context = context;
        }
        private EfCoreProductRepository _productRepository;
        private EfCoreCategoryRepository _categoryRepository;
        private EfCoreSettingsRepository _settingsRepository;
        
        public IProductRepository Products =>
        _productRepository = _productRepository ?? new EfCoreProductRepository(_context); // Geriye bir ProductRepository tipi döndürdüm ancak obje null ise oluşturup gönderdim.
        public ICategoryRepository Categories =>
        _categoryRepository = _categoryRepository ?? new EfCoreCategoryRepository(_context);
        public ISettingsRepository Settings =>
        _settingsRepository = _settingsRepository ?? new EfCoreSettingsRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}