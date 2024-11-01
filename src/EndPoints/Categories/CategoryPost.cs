﻿using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IWantApp.EndPoints.Categories {
    public class CategoryPost {

        public static string Template => "/categories";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context) {
            var category = new Category(categoryRequest.Name, "Test", "Test")
            {
                CreatedOn = DateTime.Now,
                EditedOn = DateTime.Now
            };
            if (!category.IsValid) 
                return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
            

            context.Categories.Add(category);
            context.SaveChanges();
            return Results.Created($"/categories/{category.Id}", category.Id);
        }
            
     }
}
