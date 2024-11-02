using Dapper;
using Microsoft.AspNetCore.Identity;
using MySqlConnector;
using System.Security.Claims;

namespace IWantApp.EndPoints.Employees {
    public class EmployeeGetAll {
        public static string Template => "/employees";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(int page, int rows, IConfiguration configuration) {
            var db = new MySqlConnection("Server=localhost;Database=IWantApp;User=root;Password=root;");

            // Calcule o valor de OFFSET
            int offset = (page - 1) * rows;

            var query =
                @"SELECT Email, ClaimValue AS Name
                FROM AspNetUsers u 
                INNER JOIN AspNetUserClaims c
                ON u.id = c.userId AND claimType = 'Name'
                ORDER BY Name
                LIMIT @rows OFFSET @offset";

            var employees = db.Query<EmployeeResponse>(
                query,
                new { rows, offset } // Passe `offset` calculado aqui
            );
            return Results.Ok(employees);
        }

    }
}
