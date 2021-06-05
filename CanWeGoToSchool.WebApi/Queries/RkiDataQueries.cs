namespace CanWeGoToSchool.WebApi.Queries
{
    using Dapper;
    using Npgsql;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using CanWeGoToSchool.WebApi.Services;


    public class RkiDataQueries : IRkiDataQueries
    {
        private readonly IConfiguration _configuration;

        public RkiDataQueries(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public async Task<Incidence> Get7DayIncidence(string district)
        {
            var constr = _configuration.GetConnectionString("PostgresConnection");
            using var connection = new NpgsqlConnection(constr);
            connection.Open();

            var result = await connection.QueryAsync<dynamic>(
               @"select id, landkreis, cases7_per_100k_txt
               from public.rki_corona_information
               where landkreis=@district", new { district }
                );

            if (result.AsList().Count == 0)
                throw new KeyNotFoundException();
            connection.Close();

            return MapToIncidence(result);
        }

        private bool IsNullOrEmpty()
        {
            throw new System.NotImplementedException();
        }

        private Incidence MapToIncidence(dynamic result)
        {
            int objId = (int)result[0].id;
            string dist = (string)result[0].landkreis;
            string cases = (string)result[0].cases7_per_100k_txt;

            var incidence = new Incidence
            {
                objectId = objId,
                casses7per100k = cases,
                district = dist
            };

            return incidence;

        }


        public async Task SeedCoronaInformationTable(Root CoronaData)
        {
            var constr = _configuration.GetConnectionString("PostgresConnection");

            using var connection = new NpgsqlConnection(constr);
            connection.Open();

            var deleteStatement = @"DELETE FROM public.rki_corona_information";
            var insertStatement = @"INSERT INTO public.rki_corona_information (id, landkreis, bl, bl_id, county, last_update, cases7_per_100k, cases7_bl_per_100k, cases7_per_100k_txt, BEZ)
                                   Values (@id, @landkreis, @bl, @bl_id, @county, @last_update, @cases7_per_100k, @cases7_bl_per_100k, @cases7_per_100k_txt, @BEZ)";

            connection.Execute(deleteStatement);

            int index = 1;
            foreach (var feature in CoronaData.features)
            {
                var affectedRows = await connection.ExecuteAsync(insertStatement, new
                {
                    id = index,
                    landkreis = feature.attributes.GEN,
                    bl = feature.attributes.BL,
                    bl_id = feature.attributes.BL_ID,
                    feature.attributes.county,
                    feature.attributes.last_update,
                    feature.attributes.cases7_per_100k,
                    feature.attributes.cases7_bl_per_100k,
                    feature.attributes.cases7_per_100k_txt,
                    feature.attributes.BEZ
                }); ;
                index++;
            }

            connection.Close();
        }

        public async Task<string> GetLandkreisFromPlz(int plz)
        {
            var constr = _configuration.GetConnectionString("PostgresConnection");

            using var connection = new NpgsqlConnection(constr);
            connection.Open();

            dynamic result = await connection.QueryAsync<dynamic>(
               @"select plz, kreis, bundesland
               from public.plz_to_landkreis
               where plz=@plz", new { plz }
                );

            if (result.Count == 0)
                throw new KeyNotFoundException();
            connection.Close();

            return result[0].kreis.ToString();
        }

    }
}
