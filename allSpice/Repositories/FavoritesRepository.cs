
namespace allSpice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
      string sql = @"
      INSERT INTO favorites(recipeId, accountId)
      VALUES
      (@recipeId, @accountId);
      SELECT LAST_INSERT_ID()
      ;";
      int favoriteId = _db.ExecuteScalar<int>(sql, favoriteData);
      favoriteData.Id = favoriteId;
      return favoriteData;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
      string sql = @"
      SELECT
      *
      FROM favorites
      WHERE id = @favoriteId
      ;";
      Favorite favorite = _db.QueryFirstOrDefault<Favorite>(sql,new{favoriteId});
      return favorite;
    }

    internal List<RecipeFavorite> GetProfileFavorites(string userId)
    {
      string sql = @"
      SELECT
      fav.*,
      rec.*,
      acc.*
      FROM favorites fav
      JOIN recipes rec ON rec.id = fav.RecipeId
      JOIN accounts acc ON acc.id = fav.AccountId
      Where fav.AccountId = @userId
      ;";

      List<RecipeFavorite> recipeFavorites = _db.Query<Favorite, RecipeFavorite, Profile, RecipeFavorite>(
        sql,
        (favorite, recipe, profile)=>
        {
          recipe.FavoriteId = favorite.Id;
          recipe.Creator = profile;
          return recipe;
        },
      new {userId}
      ).ToList();

      return recipeFavorites;
    }

    internal void RemoveFavorite(int favoriteId)
    {
      string sql = @"
      DELETE
      FROM favorites 
      WHERE id = @favoriteId LIMIT 1
      ;";

      _db.Execute(sql, new{favoriteId});
    }
}
