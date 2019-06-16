//using CodinovaTask.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CodinovaTask.Repository
//{
//    public interface IBaseRepository<T> where T : BaseEntity  
//    {
//        IQueryable<T> Get(); 

//        Task<T> GetAsync(int id);

//        Task<T> InsertAsync(T entity);

//        Task<T> UpdateAsync(T entity);

//        Task<bool> DeleteAsync(T entity);

//        Task<bool> DeleteAsync(List<T> entity);

//        Task<List<T>> InsertAsync(List<T> entity);  
//    }
//}
