namespace CanWeGoToSchool.WebApi.Queries
{
    using Dapper;
    using Npgsql;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public class RkiDataQueries : IRkiDataQueries
    {
        //private string _connectionString = string.Empty;
        private readonly IConfiguration _configuration;

        public RkiDataQueries(IConfiguration configuration)
        {
            _configuration = configuration;

            //_connectionString = !string.IsNullOrEmpty(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<Incidence> Get7DayIncidence(string district)
        {
            var constr = _configuration.GetConnectionString("PostgresConnection");
            using var connection = new NpgsqlConnection(constr);
            connection.Open();

            var result = await connection.QueryAsync<dynamic>(
               @"select objectid, gen as district, cases7_per_100k as casses7per100k
               from public.rki_incidence
               where gen=@district", new { district }
                );

            if (result.AsList().Count == 0)
                throw new KeyNotFoundException();

            return MapToIncidence(result);
        }

        private Incidence MapToIncidence(dynamic result)
        {
            int objId = (int)result[0].objectid;
            string dist = (string)result[0].district;
            string cases = (string)result[0].casses7per100k;

            var incidence = new Incidence
            {
                objectId = objId,
                casses7per100k = cases,
                district = dist
            };

            return incidence;

        }

        //a1 seed table rki incidence


        //get district from zipcode

    }
}
