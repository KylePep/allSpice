import { AppState } from "../AppState.js";
import { Favorite } from "../models/Favorite.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { api } from "./AxiosService.js";

class FavoritesService {
  async getMyFavorites() {
    try {
      const res = await api.get('account/favorites');
      const favorites = res.data.map(f => new Favorite(f));
      AppState.favorites = favorites;
    } catch (error) {
      Pop.error(error.message, '[FAVORITES-SERVICE]')
    }
  }
  async createFavorite(recipeId) {
    const res = await api.post('api/favorites', { recipeId })
    this.getMyFavorites()
    // logger.log(res.data)
    // let favoriteData = res.data
    // favoriteData.id = res.data.recipeId
    // favoriteData.favoriteId = res.data.id
    // AppState.favorites.push(new Favorite(favoriteData))
  }

  async deleteFavorite(favoriteId) {
    await api.delete(`api/favorites/${favoriteId}`)
    const favIndex = AppState.favorites.findIndex(f => f.favoriteId == favoriteId)
    AppState.favorites.splice(favIndex, 1)
  }

}
export const favoritesService = new FavoritesService();