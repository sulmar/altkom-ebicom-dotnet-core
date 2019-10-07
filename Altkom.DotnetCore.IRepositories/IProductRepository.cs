using Altkom.DotnetCore.Models;
using System.Collections.Generic;

namespace Altkom.DotnetCore.IRepositories
{
    public interface IProductRepository : IEntityRepository<Product, int>
    {
        ICollection<Product> GetByColor(string color);
    } 

}
