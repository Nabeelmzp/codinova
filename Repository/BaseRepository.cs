//using CodinovaTask.Entities;
//using CodinovaTask.Model;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CodinovaTask.Repository
//{
//    public class BaseRepository<T> : IBaseRepository<T>  where T : BaseEntity  
//    {
//        private readonly CodinovaTaskcontext _context;
//        private DbSet<T> entities;  

//        public BaseRepository(CodinovaTaskcontext context)
//        {
//            _context = context;  
//        }

//        public IQueryable<T> Get()
//        {

//            return entities.AsQueryable();

//            //return entities.AsNoTracking().AsEnumerable();
//        }
//        public async Task<T> GetAsync(int id)
//        {
//            return await entities.Where(x => x.IsDelated == false).SingleOrDefaultAsync(s => s == id);

//        }
//        public async Task<T> InsertAsync(T entity)
//        {
//            if (entity == null)
//            {
//                throw new ArgumentNullException("entity");
//            }
//            try
//            {
//                var data = entities.Add(entity);
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return entity;
//        }
//        public async Task<T> UpdateAsync(T entity)
//        {
//            if (entity == null)
//            {
//                throw new ArgumentNullException("entity");
//            }
//            await _context.SaveChangesAsync();
//            return entity;
//        }
//        public async Task<bool> DeleteAsync(T entity)
//        {
//            if (entity == null)
//            {
//                throw new ArgumentNullException("entity");
//            }
//            entities.Remove(entity);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//        public async Task<bool> DeleteAsync(List<T> entity)
//        {
//            if (entity == null)
//            {
//                throw new ArgumentNullException("entity");
//            }
//            entities.RemoveRange(entity);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<List<T>> InsertAsync(List<T> entity)
//        {
//            using (var transaction = _context.Database.BeginTransaction())
//            {
//                try
//                {
//                    if (entity == null)
//                    {
//                        throw new ArgumentNullException("entity");
//                    }
//                    entities.AddRange(entity);
//                    await _context.SaveChangesAsync();
//                    transaction.Commit();
//                    return entity;
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    throw ex;
//                }
//            }
//        }
//    }
//}
