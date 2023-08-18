import { AppState } from "../AppState.js";
import { Favorite } from "../models/Favorite.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class FavoritesService {
  async getMyFavorites() {
    const res = await api.get('account/favorites');
    logger.log('[Favorites-MINE]', res.data)
    const favorites = res.data.map(f => new Favorite(f));
    AppState.favorites = favorites;
  }
}
export const favoritesService = new FavoritesService();