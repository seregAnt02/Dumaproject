using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using duma.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace duma.Infrastructure
{
    public class MySqlRepository: IRespository
    {
       private DumContext db;        
        public MySqlRepository()
        {
            //Context = DumContext;
            //context.Set<Parameter>().AsNoTracking();
        }
        //--------------------------------------------        
        //--------------------------------------------        
        public DumContext Context
        {
            get { return db; }
            set { db = value; }
        }
        //--------------------------------------------
        public void AttacheParameter(Parameter model)
        {
            Context.Parameters.Attach(model);
        }
        //--------------------------------------------
        public EntityState ModifiedObj(Parameter model)
        {
            //db.Context.Entry(model).State = System.Data.Entity.EntityState.Modified;            
            return Context.Entry(model).State = System.Data.Entity.EntityState.Modified;
        }       
        //--------------------------------------------
        public IEnumerable<Duma> DumaList()
        {
            return Context.Dumas.ToList();
        }
        //--------------------------------------------
        public IEnumerable<TotalPrice> TotalList()
        {
            return Context.TotalPrices;
        }
        //--------------------------------------------
        public async Task<Duma> GetAsync(int id)
        {
            return await Context.Dumas.FindAsync(id);
        }
        //--------------------------------------------
        public Parameter GetAsyncModelParameter(int id)
        {
            //Context.Set<Parameter>().AsNoTracking(); нет результотов ?
            return Context.Parameters.Include("Duma").ToList().Find(x => x.id == id);
        }
        //--------------------------------------------
        public List<Parameter> GetAsNoTrackingToList(int id)
        {
            // Возвращает новый запрос, в котором возвращенные сущности не будут кэшироваться в System.Data.Entity.DbContext                     
            return Context.Parameters.AsNoTracking().Include("Duma").ToList();
        }
        //--------------------------------------------
        public void AddOrUpdateOrSave(Parameter model)
        {
            if(model.Duma.id > 0)
            {
                Context.Parameters.AddOrUpdate(model);
                Context.SaveChanges();
            }            
        }
        //--------------------------------------------
        public void Save(Parameter model)
        {
            //Context.Parameters.Attach(model);
            Context.Entry(model).State = EntityState.Modified;
            Context.SaveChanges();
        }
        //--------------------------------------------
        public List<Parameter> ParamList()
        {
            return Context.Parameters.ToList();
            //return db.Parameters.AsNoTracking().ToList();
        }
        //--------------------------------------------
        public Parameter FindParameter(int id)
        {
            return Context.Parameters.Find(id);
        }
    }
}