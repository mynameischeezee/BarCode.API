using System.Collections.Generic;
using DataAccess.Daos;

namespace Barcode.Services.Abstracitons
{
    public interface IProductService
    {
        Product Get(string code);
        void Create(string code, string name);
        Product Remove(string code);
        Product Edit(string code, string name);
    }
}