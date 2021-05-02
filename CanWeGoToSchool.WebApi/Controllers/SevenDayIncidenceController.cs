namespace CanWeGoToSchool.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using CanWeGoToSchool.WebApi.Queries;


    [Route("api/v1/[controller]")]
    [ApiController]
    public class SevenDayIncidenceController : Controller
    {
        private readonly IRkiDataQueries _rkiDataQueries;

        public SevenDayIncidenceController(IRkiDataQueries rkiDataQueries)
        {
            _rkiDataQueries = rkiDataQueries ?? throw new ArgumentNullException(nameof(_rkiDataQueries));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Incidence>), (int)System.Net.HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Incidence>>> GetOrdersAsync(string district)
        {
            var incidence = await _rkiDataQueries.Get7DayIncidence(district);
            

            return Ok(incidence);
        }
    }
}
