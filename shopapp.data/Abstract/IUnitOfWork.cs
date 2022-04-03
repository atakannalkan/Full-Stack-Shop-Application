using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products {get;}
        ICategoryRepository Categories {get;}
        ISettingsRepository Settings {get;}

        void Save();
    }
}