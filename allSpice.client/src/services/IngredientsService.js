import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService {
  async createIngredient(ingredientData) {
    const res = await api.post('api/ingredients', ingredientData)
    logger.log(res.data)
    AppState.ingredients.push(new Ingredient(res.data))
  }
  async removeIngredient(ingredientId) {
    const ingredientIndex = AppState.ingredients.findIndex(i => i.id == ingredientId)
    const res = await api.delete(`api/ingredients/${ingredientId}`)
    AppState.ingredients.splice(ingredientIndex, 1)
  }
}
export const ingredientService = new IngredientsService()