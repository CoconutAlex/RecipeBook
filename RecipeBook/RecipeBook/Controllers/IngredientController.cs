using Microsoft.AspNetCore.Mvc;
using RecipeBook.Repositories.Interface;

namespace RecipeBook.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository repository;

        #region Constructor

        public IngredientController(IIngredientRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Get All Ingredients

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredientsDto = new List<Models.Dto.Ingredient>();

            var response = await repository.GetAllIngredients_Repos();
            response.ToList().ForEach(ingredient =>
            {
                var ingredientDto = new Models.Dto.Ingredient()
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    Recipies = ingredient.Recipies.Select(
                         q => new Models.Dto.Recipe()
                         {
                             Id = q.Id,
                             Title = q.Title,
                         }).ToList()
                };

                ingredientsDto.Add(ingredientDto);
            });

            return Ok(ingredientsDto);
        }

        #endregion

        #region Get Ingredient

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetIngredient(Guid id)
        {
            var response = await repository.GetIngredient_Repos(id);
            if (response == null) return NotFound();

            var ingredientDto = new Models.Dto.Ingredient()
            {
                Id = response.Id,
                Name = response.Name,
                Quantity = response.Quantity,
                Recipies = response.Recipies.Select(
                         q => new Models.Dto.Recipe()
                         {
                             Id = q.Id,
                             Title = q.Title,
                         }).ToList()
            };

            return Ok(ingredientDto);
        }

        #endregion

        #region Add Ingredient

        [HttpPost]
        public async Task<IActionResult> AddIngredient(Models.Dto.Requests.Ingredient.AddIngredientRequest addIngredientRequest)
        {
            var request = new Models.Domain.Ingredient()
            {
                Quantity = addIngredientRequest.Quantity,
                Name = addIngredientRequest.Name
            };

            var response = await repository.AddIngredient_Repos(request);
            var responseDto = new Models.Dto.Ingredient()
            {
                Id = response.Id,
                Quantity = response.Quantity,
                Name = response.Name
            };

            return Ok(responseDto);
        }

        #endregion

        #region Delete Ingredient

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var response = await repository.DeleteIngredient_Repos(id);
            var ingredientDto = new Models.Dto.Ingredient()
            {
                Id = response.Id,
                Quantity = response.Quantity,
                Name = response.Name,
                Recipies = response.Recipies.Select(
                         q => new Models.Dto.Recipe()
                         {
                             Id = q.Id,
                             Title = q.Title,
                         }).ToList()
            };

            return Ok(ingredientDto);
        }

        #endregion

        #region Update Ingredient

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateIngredient([FromRoute] Guid id, [FromBody] Models.Dto.Requests.Ingredient.UpdateIngredientRequest updateIngredientRequest)
        {
            var request = new Models.Domain.Ingredient()
            {
                Quantity = updateIngredientRequest.Quantity,
                Name = updateIngredientRequest.Name
            };

            var response = await repository.UpdateIngredient_Repos(id, request);
            if (response == null)
            {
                return NotFound();
            }

            var ingredientDto = new Models.Dto.Ingredient()
            {
                Id = response.Id,
                Quantity = response.Quantity,
                Name = response.Name,
                Recipies = response.Recipies.Select(
                         q => new Models.Dto.Recipe()
                         {
                             Id = q.Id,
                             Title = q.Title,
                         }).ToList()
            };

            return Ok(ingredientDto);
        }

        #endregion
    }
}
