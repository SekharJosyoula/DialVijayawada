using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DV.DataProvider;
using System.Data.Entity.Validation;

namespace DV.Manager.Interfaces
{
    public abstract class AbstractCommitManager<TEntity> : IDisposable where TEntity : class
    {
        protected dvradiusEntities dvradiusEntities = new dvradiusEntities();

        public DbSet<TEntity> Entities
        {
            get { return GetEntites(); }
        }

        protected virtual void BeforeInsert(TEntity entity)
        {

        }

        protected virtual void BeforeUpdate(TEntity entity)
        {

        }

        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                BeforeInsert(entity);
                Entities.Add(entity);
            }

            try
            {
                dvradiusEntities.SaveChanges();
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

            }

            return entities;
        }

        //TODO: where is delete?
        public TEntity Insert(TEntity t)
        {
            Insert(new[] {t});
            return t;
        }

        public TEntity InsertIfNew(TEntity t)
        {
            if (!IsExists(t))
            {
                return Insert(t);
            }

            //dont try to throw exception here this was intentionally done. contact Vital for more details
            return t;
        }
        public TEntity Update(TEntity t)
        {
            BeforeUpdate(t);
            dvradiusEntities.Entry(t).State = EntityState.Modified;
            dvradiusEntities.SaveChanges();
            return t;
        }

        public TEntity InsertOrUpdate(TEntity t)
        {
            if (IsExists(t))
            {
                Update(t);
            }
            else
            {
                Insert(t);
            }
            return t;
        }

        public abstract bool IsExists(TEntity t);
        public abstract DbSet<TEntity> GetEntites();

        public void Dispose()
        {
            dvradiusEntities.Dispose();
        }
    }
}