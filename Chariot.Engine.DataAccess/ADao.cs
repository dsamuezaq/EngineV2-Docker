using Chariot.Engine.DataObject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Chariot.Engine.DataAccess
{

    public class ADao
    {
        public ChariotContext Context { get; }
        protected ADao(ChariotContext _chariotContext)
        {
            Context = _chariotContext;
        }
        public T InsertOrUpdate<T>(T entity) where T : class, IEntity
        {
            try
            {
                var stateRegister = Guid.Empty == entity.Id ? EntityState.Added : EntityState.Modified;

                if (Context.Entry(entity).State == EntityState.Detached && stateRegister == EntityState.Added)
                {
                    Context.Set<T>().Add(entity);
                }

                Context.Entry(entity).State = stateRegister;

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                // throw new ExceptionMardis(ex.Message, ex);
            }

            return entity;
        }

        public List<T> GetPaginatedList<T>(int pageIndex, int pageSize) where T : class, IEntity
        {
            var sortedList = Context.Set<T>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return sortedList;
        }
        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void PhysicalDelete<T>(T entity) where T : class
        {
            try
            {
          

                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Context.Set<T>().Add(entity);
                }

                Context.Set<T>().Remove(entity);

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
