using Microsoft.AspNetCore.Mvc;
using RecipeBook.Repositories.Interface;

namespace RecipeBook.Controllers
{
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
                    Name = ingredient.Name
                };

                ingredientsDto.Add(ingredientDto);
            });

            return Ok(ingredientsDto);
        }

        #endregion

        #region Get Recipe

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
                Quantity = response.Quantity
            };

            return Ok(ingredientDto);
        }

        #endregion

        #region Add Recipe

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

        #region Delete Recipe

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var response = await repository.DeleteIngredient_Repos(id);
            var ingredientDto = new Models.Dto.Ingredient()
            {
                Id = response.Id,
                Quantity = response.Quantity,
                Name = response.Name
            };

            return Ok(ingredientDto);
        }

        #endregion

        #region Update Recipe

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
                Name = response.Name
            };

            return Ok(ingredientDto);
        }

        #endregion
    }
}
