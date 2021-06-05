using System;
namespace CanWeGoToSchool.WebApi.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CanWeGoToSchool.WebApi.Services;

    public interface IRkiDataQueries
    {
        Task<Incidence> Get7DayIncidence(string Landkreis);

        Task SeedCoronaInformationTable(Root CoronaData);

        Task<string> GetLandkreisFromPlz(int plz);

    }
}
