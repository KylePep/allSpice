<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-11 mt-4 m-auto bg-recipes rounded">
        <section class="row pt-3 px-2">
          <div v-for="recipe in recipes" :key="recipe.id" class="col-3">
            <RecipeCard :recipeProp="recipe" />
          </div>
        </section>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js"
import { favoritesService } from "../services/FavoritesService.js"
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";

export default {
  setup() {
    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      } catch (error) {
        Pop.error(error.message, '[HOME PAGE - GET RECIPES]')
      }
    }

    async function getMyFavorites() {
      try {
        await favoritesService.getMyFavorites();
      } catch (error) {
        Pop.error(error.message, '[HOME PAGE - GET FAVORITES]')
      }
    }


    onMounted(() => {
      getRecipes();
      // getMyFavorites();
    })
    return {
      recipes: computed(() => AppState.recipes)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.bg-recipes {
  background-color: var(--bg-green);
}
</style>
