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

        #region Ingredients

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
                    IngredientName = new Models.Dto.IngredientName()
                    {
                        Id = ingredient.IngredientName.Id,
                        Name = ingredient.IngredientName.Name
                    },
                    IngredientQuantity = new Models.Dto.IngredientQuantity()
                    {
                        Id = ingredient.IngredientQuantity.Id,
                        Quantity = ingredient.IngredientQuantity.Quantity
                    },
                    Recipes = ingredient.Recipes.Select(
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
                IngredientName = new Models.Dto.IngredientName()
                {
                    Id = response.IngredientName.Id,
                    Name = response.IngredientName.Name
                },
                IngredientQuantity = new Models.Dto.IngredientQuantity()
                {
                    Id = response.IngredientQuantity.Id,
                    Quantity = response.IngredientQuantity.Quantity
                },
                Recipes = response.Recipes.Select(
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
        public async Task<IActionResult> AddIngredient(Models.Dto.Requests.Ingredient.AddUpdateIngredientRequest addIngredientRequest)
        {
            var request = new Models.Domain.Requests.Ingredient.AddUpdateIngredientRequest()
            {
                IngredientNameId = addIngredientRequest.IngredientNameId,
                IngredientQuantityId = addIngredientRequest.IngredientQuantityId
            };

            var response = await repository.AddIngredient_Repos(request);

            var ingredientDto = new Models.Dto.Ingredient()
            {
                Id = response.Id,
                IngredientName = new Models.Dto.IngredientName()
                {
                    Id = response.IngredientName.Id,
                    Name = response.IngredientName.Name
                },
                IngredientQuantity = new Models.Dto.IngredientQuantity()
                {
                    Id = response.IngredientQuantity.Id,
                    Quantity = response.IngredientQuantity.Quantity
                }
            };

            return Ok(ingredientDto);
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
                IngredientName = new Models.Dto.IngredientName()
                {
                    Id = response.IngredientName.Id,
                    Name = response.IngredientName.Name
                },
                IngredientQuantity = new Models.Dto.IngredientQuantity()
                {
                    Id = response.IngredientQuantity.Id,
                    Quantity = response.IngredientQuantity.Quantity
                },
                Recipes = response.Recipes.Select(
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
        public async Task<IActionResult> UpdateIngredient([FromRoute] Guid id, [FromBody] Models.Dto.Requests.Ingredient.AddUpdateIngredientRequest updateIngredientRequest)
        {
            var request = new Models.Domain.Requests.Ingredient.AddUpdateIngredientRequest()
            {
                IngredientNameId = updateIngredientRequest.IngredientNameId,
                IngredientQuantityId = updateIngredientRequest.IngredientQuantityId
            };

            var response = await repository.UpdateIngredient_Repos(id, request);
            if (response == null)
            {
                return NotFound();
            }

            var ingredientDto = new Models.Dto.Ingredient()
            {
                Id = response.Id,
                IngredientName = new Models.Dto.IngredientName()
                {
                    Id = response.IngredientName.Id,
                    Name = response.IngredientName.Name
                },
                IngredientQuantity = new Models.Dto.IngredientQuantity()
                {
                    Id = response.IngredientQuantity.Id,
                    Quantity = response.IngredientQuantity.Quantity
                },
                Recipes = response.Recipes.Select(
                         q => new Models.Dto.Recipe()
                         {
                             Id = q.Id,
                             Title = q.Title,
                         }).ToList()
            };

            return Ok(ingredientDto);
        }

        #endregion

        #endregion

        #region Ingredients Names

        #region Get All Ingredients Names

        [HttpGet]
        public async Task<IActionResult> GetAllIngredientsNames()
        {
            var ingredientsNamesDto = new List<Models.Dto.IngredientName>();

            var response = await repository.GetAllIngredientsNames_Repos();
            response.ToList().ForEach(ingredientName =>
            {
                var ingredientNameDto = new Models.Dto.IngredientName()
                {
                    Id = ingredientName.Id,
                    Name = ingredientName.Name
                };

                ingredientsNamesDto.Add(ingredientNameDto);
            });

            return Ok(ingredientsNamesDto);
        }

        #endregion

        #region Get Ingredient Name

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetIngredientName(Guid id)
        {
            var response = await repository.GetIngredientName_Repos(id);
            if (response == null) return NotFound();

            var ingredientNameDto = new Models.Dto.IngredientName()
            {
                Id = response.Id,
                Name = response.Name
            };

            return Ok(ingredientNameDto);
        }

        #endregion

        #region Add Ingredient Name

        [HttpPost]
        public async Task<IActionResult> AddIngredientName(Models.Dto.Requests.Ingredient.AddUpdateIngredientNameRequest addIngredientNameRequest)
        {
            var request = new Models.Domain.IngredientName()
            {
                Name = addIngredientNameRequest.Name
            };

            var response = await repository.AddIngredientName_Repos(request);
            var responseDto = new Models.Dto.IngredientName()
            {
                Id = response.Id,
                Name = response.Name
            };

            return Ok(responseDto);
        }

        #endregion

        #region Delete Ingredient Name

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteIngredientName(Guid id)
        {
            var response = await repository.DeleteIngredientName_Repos(id);
            var ingredientDto = new Models.Dto.IngredientName()
            {
                Id = response.Id,
                Name = response.Name
            };

            return Ok(ingredientDto);
        }

        #endregion

        #region Update Ingredient Name

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateIngredientName([FromRoute] Guid id, [FromBody] Models.Dto.Requests.Ingredient.AddUpdateIngredientNameRequest updateIngredientNameRequest)
        {
            var request = new Models.Domain.IngredientName()
            {
                Name = updateIngredientNameRequest.Name,
            };

            var response = await repository.UpdateIngredientName_Repos(id, request);
            if (response == null)
            {
                return NotFound();
            }

            var ingredientNameDto = new Models.Dto.IngredientName()
            {
                Id = response.Id,
                Name = response.Name
            };

            return Ok(ingredientNameDto);
        }

        #endregion

        #endregion

        #region Ingredients Quantities

        #region Get All Ingredients Quantity

        [HttpGet]
        public async Task<IActionResult> GetAllIngredientsQuantities()
        {
            var ingredientsQuantitiesDto = new List<Models.Dto.IngredientQuantity>();

            var response = await repository.GetAllIngredientsQuantities_Repos();
            response.ToList().ForEach(ingredientQuantity =>
            {
                var ingredientQuantityDto = new Models.Dto.IngredientQuantity()
                {
                    Id = ingredientQuantity.Id,
                    Quantity = ingredientQuantity.Quantity
                };

                ingredientsQuantitiesDto.Add(ingredientQuantityDto);
            });

            return Ok(ingredientsQuantitiesDto);
        }

        #endregion

        #region Get Ingredient Quantity

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetIngredientQuantity(Guid id)
        {
            var response = await repository.GetIngredientQuantity_Repos(id);
            if (response == null) return NotFound();

            var ingredientQuantityDto = new Models.Dto.IngredientQuantity()
            {
                Id = response.Id,
                Quantity = response.Quantity
            };

            return Ok(ingredientQuantityDto);
        }

        #endregion

        #region Add Ingredient Quantity

        [HttpPost]
        public async Task<IActionResult> AddIngredientQuantity(Models.Dto.Requests.Ingredient.AddUpdateIngredientQuantityRequest addIngredientQuantityRequest)
        {
            var request = new Models.Domain.IngredientQuantity()
            {
                Quantity = addIngredientQuantityRequest.Quantity
            };

            var response = await repository.AddIngredientQuantity_Repos(request);
            var responseDto = new Models.Dto.IngredientQuantity()
            {
                Id = response.Id,
                Quantity = response.Quantity
            };

            return Ok(responseDto);
        }

        #endregion

        #region Delete Ingredient Quantity

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteIngredientQuantity(Guid id)
        {
            var response = await repository.DeleteIngredientQuantity_Repos(id);
            var ingredientDto = new Models.Dto.IngredientQuantity()
            {
                Id = response.Id,
                Quantity = response.Quantity
            };

            return Ok(ingredientDto);
        }

        #endregion

        #region Update Ingredient Quantity

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateIngredientQuantity([FromRoute] Guid id, [FromBody] Models.Dto.Requests.Ingredient.AddUpdateIngredientQuantityRequest updateIngredientQuantityRequest)
        {
            var request = new Models.Domain.IngredientQuantity()
            {
                Quantity = updateIngredientQuantityRequest.Quantity,
            };

            var response = await repository.UpdateIngredientQuantity_Repos(id, request);
            if (response == null)
            {
                return NotFound();
            }

            var ingredientQuantityDto = new Models.Dto.IngredientQuantity()
            {
                Id = response.Id,
                Quantity = response.Quantity
            };

            return Ok(ingredientQuantityDto);
        }

        #endregion

        #endregion
    }
}
