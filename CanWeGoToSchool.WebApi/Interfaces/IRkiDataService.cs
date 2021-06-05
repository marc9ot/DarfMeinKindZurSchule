using System;
namespace CanWeGoToSchool.WebApi.Services
{
    using System.Threading.Tasks;

    public interface IRkiDataService
    {
        Task<Root> GetRkiCoronaData();

        bool CanWeGoToSchool(int incidence);
    }
}
