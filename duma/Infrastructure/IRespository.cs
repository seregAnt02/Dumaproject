using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using duma.Models;

namespace duma.Infrastructure
{
    public interface IRespository
    {
        void AttacheParameter(Parameter model);
        System.Data.Entity.EntityState ModifiedObj(Parameter model);
        List<Parameter> ParamList();
        IEnumerable<Duma> DumaList();
        IEnumerable<TotalPrice> TotalList();
        Task<Duma> GetAsync(int id);
        Parameter GetAsyncModelParameter(int id);
        List<Parameter> GetAsNoTrackingToList(int id);
        void Save(Parameter model);
        void AddOrUpdateOrSave(Parameter model);
        Parameter FindParameter(int id);
        DumContext Context { get; }
    }
}
