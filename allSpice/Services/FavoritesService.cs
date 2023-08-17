using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _favoritesRepository;

    public FavoritesService(FavoritesRepository favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
      Favorite favorite = _favoritesRepository.CreateFavorite(favoriteData);
      return favorite;
    }

    internal List<RecipeFavorite> GetProfileFavorites(string userId)
    {
      List<RecipeFavorite> recipeFavorites = _favoritesRepository.GetProfileFavorites(userId);
      return recipeFavorites;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
      Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
      if (favorite == null)
      {
        throw new Exception($"BAD ID: {favoriteId}");
      }
      return favorite;
    }

    internal void RemoveFavorite(int favoriteId, string userId)
    {
      Favorite favorite = GetFavoriteById(favoriteId);
      if (favorite.AccountId != userId)
      {
          throw new Exception($"YOU DO NOT OWN THIS DATA");
      }
      _favoritesRepository.RemoveFavorite(favoriteId);
    }
}
