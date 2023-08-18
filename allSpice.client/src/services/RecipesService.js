import { AppState } from "../AppState.js";
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
}
export const recipesService = new RecipesService()