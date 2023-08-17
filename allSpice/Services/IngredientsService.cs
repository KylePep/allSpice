using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _ingredientsRepository;

    public IngredientsService(IngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        int ingredientId = _ingredientsRepository.CreateIngredient(ingredientData);
        Ingredient ingredient = GetIngredientById(ingredientId);
        return ingredient;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
      Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
      if (ingredient == null)
      {
        throw new Exception($"BAD ID: {ingredientId}");
      }
      return ingredient;
    }

    internal List<Ingredient> GetIngredientByRecipeId(int recipeId)
    {
      List<Ingredient> ingredients = _ingredientsRepository.GetIngredientByRecipeId(recipeId);
      return ingredients;
    }

    internal void RemoveIngredient(int ingredientId, string userId)
    {
      Ingredient ingredient = GetIngredientById(ingredientId);
      if (ingredient.CreatorId != userId)
      {
                throw new Exception($"YOU DO NOT OWN THIS DATA");
      }
      _ingredientsRepository.RemoveIngredient(ingredientId);
    }
}
