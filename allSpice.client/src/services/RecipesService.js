import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js"

class RecipesService {
  async getRecipes() {
    const res = await api.get('api/recipes');
    const recipes = res.data.map(r => new Recipe(r));
    AppState.recipes = recipes;
  }

  async createRecipe(recipeData) {
    logger.log(recipeData)
    const res = await api.post('api/recipes', recipeData)
    AppState.recipes.push(new Recipe(res.data))
  }

  async getIngredientsByRecipeId(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    const ingredients = res.data.map(i => new Ingredient(i))
    AppState.ingredients = ingredients
  }

  async editRecipe(recipeData, recipeId) {
    const foundRecipe = AppState.recipes.findIndex(f => f.id == recipeId)
    const res = await api.put(`api/recipes/${recipeId}`, recipeData);
    const recipe = new Recipe(res.data)
    AppState.activeRecipe = recipe
    AppState.recipes.splice(foundRecipe, 1, recipe)
  }

  async getRecipesByQuery(queryObject) {
    const res = await api.get(`api/recipes?query=${queryObject.query}`)
    logger.log(`Did anything comeback?`, res.data)
  }
}
export const recipesService = new RecipesService()
