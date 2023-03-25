using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.interfaces
{
    public  interface IGenericRepository
        <in T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
    }
}
