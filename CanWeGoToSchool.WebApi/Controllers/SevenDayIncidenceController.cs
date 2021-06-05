namespace CanWeGoToSchool.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using CanWeGoToSchool.WebApi.Queries;
    using CanWeGoToSchool.WebApi.Services;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class SevenDayIncidenceController : Controller
    {
        private readonly IRkiDataQueries _rkiDataQueries;
        private readonly IRkiDataService _rkiDataService;


        public SevenDayIncidenceController(IRkiDataQueries rkiDataQueries, IRkiDataService rkiDataService)
        {
            _rkiDataQueries = rkiDataQueries ?? throw new ArgumentNullException(nameof(_rkiDataQueries));
            _rkiDataService = rkiDataService ?? throw new ArgumentNullException(nameof(_rkiDataService));

        }

        [Route("/by-district")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Incidence>), (int)System.Net.HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Incidence>>> GetIncidenceByLandkreisAsync(string district)
        {
            var incidence = await _rkiDataQueries.Get7DayIncidence(district);
            

            return Ok(incidence);
        }

        [Route("/by-plz")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Incidence>), (int)System.Net.HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Incidence>>> GetIncidenceByPlzAsync(int plz)
        {
            var district = await _rkiDataQueries.GetLandkreisFromPlz(plz);
            var incidence = await _rkiDataQueries.Get7DayIncidence(district);


            return Ok(incidence);
        }

        [Route("/seed-database")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Root>), (int)System.Net.HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Root>>> GetRkiCoronaData()
        {

            var Root = await _rkiDataService.GetRkiCoronaData();
            await _rkiDataQueries.SeedCoronaInformationTable(Root);
            return Ok(Root);
        }
    }
}
