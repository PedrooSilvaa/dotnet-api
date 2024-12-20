﻿namespace IWantApp.EndPoints.Categories {
    public class CategoryPut {


        public static string Template => "/categories/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static IResult Action([FromRoute] Guid Id, CategoryRequest categoryRequest, HttpContext http, ApplicationDbContext context) {
            var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var category = context.Categories.Where(c => c.Id == Id).FirstOrDefault();
            if (category == null)  
                return Results.NotFound();

            category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

            if (!category.IsValid)
                return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
                
            context.SaveChanges();
            return Results.Ok();
        }

    }
}
