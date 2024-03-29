﻿using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models.Domain;
using RecipeBook.Models.Dto;
using RecipeBook.Repositories.Interface;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace RecipeBook.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository repository;

        #region Constructor

        public RecipeController(IRecipeRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Get All Recipes

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            try
            {
                var recipesDto = new List<Models.Dto.Recipe>();

                var response = await repository.GetAllRecipes_Repos();
                response.ToList().ForEach(recipe =>
                {
                    var recipeDto = new Models.Dto.Recipe()
                    {
                        Id = recipe.Id,
                        Title = recipe.Title,
                        Description = recipe.Description,
                        Steps = recipe.Steps,
                        Portions = recipe.Portions,
                        Duration = recipe.Duration,
                        Difficulty = recipe.Difficulty,
                        ImageName = recipe.ImageName,
                        Ingredients = recipe.Ingredients.Select(
                             q => new Models.Dto.Ingredient()
                             {
                                 Id = q.Id,
                                 IngredientName = new Models.Dto.IngredientName()
                                 {
                                     Id = q.IngredientName.Id,
                                     Name = q.IngredientName.Name
                                 },
                                 IngredientQuantity = new Models.Dto.IngredientQuantity()
                                 {
                                     Id = q.IngredientQuantity.Id,
                                     Quantity = q.IngredientQuantity.Quantity
                                 }
                             }).ToList()
                    };

                    recipesDto.Add(recipeDto);
                });

                return Ok(recipesDto);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        #endregion

        #region Get Recipe

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRecipe(Guid id)
        {
            try
            {
                var response = await repository.GetRecipe_Repos(id);
                if (response == null) return NotFound();

                var recipeDto = new Models.Dto.Recipe()
                {
                    Id = response.Id,
                    Title = response.Title,
                    Description = response.Description,
                    Steps = response.Steps,
                    Portions = response.Portions,
                    Duration = response.Duration,
                    Difficulty = response.Difficulty,
                    ImageName = response.ImageName,
                    Ingredients = response.Ingredients.Select(
                             q => new Models.Dto.Ingredient()
                             {
                                 Id = q.Id,
                                 IngredientName = new Models.Dto.IngredientName()
                                 {
                                     Id = q.IngredientName.Id,
                                     Name = q.IngredientName.Name
                                 },
                                 IngredientQuantity = new Models.Dto.IngredientQuantity()
                                 {
                                     Id = q.IngredientQuantity.Id,
                                     Quantity = q.IngredientQuantity.Quantity
                                 }
                             }).ToList()
                };

                return Ok(recipeDto);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        #endregion

        #region Add Recipe

        [HttpPost]
        public async Task<IActionResult> AddRecipe(Models.Dto.Requests.Recipe.AddUpdateRecipeRequest addRecipeRequest)
        {
            try
            {
                var request = new Models.Domain.Requests.Recipe.AddUpdateRecipeRequest()
                {
                    Title = addRecipeRequest.Title,
                    Description = addRecipeRequest.Description,
                    Steps = addRecipeRequest.Steps,
                    Portions = addRecipeRequest.Portions,
                    Duration = addRecipeRequest.Duration,
                    Difficulty = addRecipeRequest.Difficulty,
                    ImageName = addRecipeRequest.ImageName,
                    Ingredients = addRecipeRequest.Ingredients.Select(
                         q => new Models.Domain.Requests.Recipe.AddIngredientForRecipeRequest()
                         {
                             IngredientQuantityId = q.IngredientQuantityId,
                             IngredientNameId = q.IngredientNameId
                         }).ToList()
                };

                var response = await repository.AddRecipe_Repos(request);
                var responseDto = new Models.Dto.Recipe()
                {
                    Id = response.Id,
                    Title = response.Title,
                    Description = response.Description,
                    Steps = response.Steps,
                    Portions = response.Portions,
                    Duration = response.Duration,
                    Difficulty = response.Difficulty,
                    ImageName = response.ImageName
                };

                return Ok(responseDto);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        #endregion

        #region Delete Recipe

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            try
            {
                var response = await repository.DeleteRecipe_Repos(id);
                var recipeDto = new Models.Dto.Recipe()
                {
                    Id = response.Id,
                    Title = response.Title,
                    Description = response.Description,
                    Steps = response.Steps,
                    Portions = response.Portions,
                    Duration = response.Duration,
                    Difficulty = response.Difficulty,
                    ImageName = response.ImageName,
                    Ingredients = response.Ingredients.Select(
                             q => new Models.Dto.Ingredient()
                             {
                                 Id = q.Id,
                                 IngredientName = new Models.Dto.IngredientName()
                                 {
                                     Id = q.IngredientName.Id,
                                     Name = q.IngredientName.Name
                                 },
                                 IngredientQuantity = new Models.Dto.IngredientQuantity()
                                 {
                                     Id = q.IngredientQuantity.Id,
                                     Quantity = q.IngredientQuantity.Quantity
                                 }
                             }).ToList()
                };

                return Ok(recipeDto);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        #endregion

        #region Update Recipe

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRecipe([FromRoute] Guid id, [FromBody] Models.Dto.Requests.Recipe.AddUpdateRecipeRequest updateRecipeRequest)
        {
            try
            {
                var request = new Models.Domain.Requests.Recipe.AddUpdateRecipeRequest()
                {
                    Title = updateRecipeRequest.Title,
                    Description = updateRecipeRequest.Description,
                    Steps = updateRecipeRequest.Steps,
                    Portions = updateRecipeRequest.Portions,
                    Duration = updateRecipeRequest.Duration,
                    Difficulty = updateRecipeRequest.Difficulty,
                    ImageName = updateRecipeRequest.ImageName,
                    Ingredients = updateRecipeRequest.Ingredients.Select(
                         q => new Models.Domain.Requests.Recipe.AddIngredientForRecipeRequest()
                         {
                             IngredientQuantityId = q.IngredientQuantityId,
                             IngredientNameId = q.IngredientNameId
                         }).ToList()
                };

                var response = await repository.UpdateRecipe_Repos(id, request);
                if (response == null)
                {
                    return NotFound();
                }

                var recipeDto = new Models.Dto.Recipe()
                {
                    Id = response.Id,
                    Title = response.Title,
                    Description = response.Description,
                    Steps = response.Steps,
                    Portions = response.Portions,
                    Duration = response.Duration,
                    Difficulty = response.Difficulty,
                    ImageName = response.ImageName,
                    Ingredients = response.Ingredients.Select(
                             q => new Models.Dto.Ingredient()
                             {
                                 Id = q.Id,
                                 IngredientName = new Models.Dto.IngredientName()
                                 {
                                     Id = q.IngredientName.Id,
                                     Name = q.IngredientName.Name
                                 },
                                 IngredientQuantity = new Models.Dto.IngredientQuantity()
                                 {
                                     Id = q.IngredientQuantity.Id,
                                     Quantity = q.IngredientQuantity.Quantity
                                 }
                             }).ToList()
                };

                return Ok(recipeDto);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        #endregion
    }
}
