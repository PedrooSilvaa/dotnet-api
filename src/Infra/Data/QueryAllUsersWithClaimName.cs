using Dapper;
using IWantApp.EndPoints.Employees;
using MySqlConnector;

namespace IWantApp.Infra.Data {
    public class QueryAllUsersWithClaimName {

        public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows) {
            var db = new MySqlConnection("Server=localhost;Database=IWantApp;User=root;Password=root;");

            int offset = (page - 1) * rows;

            var query =
                @"SELECT Email, ClaimValue AS Name
                FROM AspNetUsers u 
                INNER JOIN AspNetUserClaims c
                ON u.id = c.userId AND claimType = 'Name'
                ORDER BY Name
                LIMIT @rows OFFSET @offset";

            return await db.QueryAsync<EmployeeResponse>(
                query,
                new { rows, offset }
            );
        }

    }
}
