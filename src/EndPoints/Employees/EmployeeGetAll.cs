﻿using Dapper;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using MySqlConnector;
using System.Security.Claims;

namespace IWantApp.EndPoints.Employees {
    public class EmployeeGetAll {
        public static string Template => "/employees";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query) {
            return Results.Ok(query.Execute(page.Value, rows.Value));
        }

    }
}