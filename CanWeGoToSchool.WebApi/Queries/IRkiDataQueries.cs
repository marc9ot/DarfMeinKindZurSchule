using System;
namespace CanWeGoToSchool.WebApi.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRkiDataQueries
    {
        Task<Incidence> Get7DayIncidence(string Landkreis);

        //a1 new interface registration
    }
}
