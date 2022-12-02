﻿using Microsoft.AspNetCore.Mvc;
using RecipeBook.Repositories.Interface;
using System;

namespace RecipeBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository repository;

        #region Constructor

        public RecipeController(IRecipeRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Get All Recipies

        [HttpGet]
        public async Task<IActionResult> GetAllRecipies()
        {
            var recipiesDto = new List<Models.Dto.Recipe>();

            var response = await repository.GetAllRecipies_Repos();
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
                    Difficulty = recipe.Difficulty
                };

                recipiesDto.Add(recipeDto);
            });

            return Ok(recipiesDto);
        }

        #endregion

        #region Get Recipe

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRecipe(Guid id)
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
                Difficulty = response.Difficulty
            };

            return Ok(recipeDto);
        }

        #endregion

        #region Add Recipe

        [HttpPost]
        public async Task<IActionResult> AddRecipe(Models.Dto.Requests.Recipe.AddIngredientRequest addRecipeRequest)
        {
            var request = new Models.Domain.Recipe()
            {
                Title = addRecipeRequest.Title,
                Description = addRecipeRequest.Description,
                Steps = addRecipeRequest.Steps,
                Portions = addRecipeRequest.Portions,
                Duration = addRecipeRequest.Duration,
                Difficulty = addRecipeRequest.Difficulty
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
                Difficulty = response.Difficulty
            };

            return Ok(responseDto);
        }

        #endregion

        #region Delete Recipe

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
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
                Difficulty = response.Difficulty
            };

            return Ok(recipeDto);
        }

        #endregion

        #region Update Recipe

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRecipe([FromRoute] Guid id, [FromBody] Models.Dto.Requests.Recipe.UpdateIngredientRequest updateRecipeRequest)
        {
            var request = new Models.Domain.Recipe()
            {
                Title = updateRecipeRequest.Title,
                Description = updateRecipeRequest.Description,
                Steps = updateRecipeRequest.Steps,
                Portions = updateRecipeRequest.Portions,
                Duration = updateRecipeRequest.Duration,
                Difficulty = updateRecipeRequest.Difficulty
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
                Difficulty = response.Difficulty
            };

            return Ok(recipeDto);
        }

        #endregion
    }
}