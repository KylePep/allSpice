<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-11 mt-5 mt-md-4 m-auto bg-recipes rounded">
        <section class="row pt-3 px-2">
          <div v-for="recipe in recipes" :key="recipe.id" class="col-12 col-md-3">
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
import { accountService } from "../services/AccountService.js";

export default {
  setup() {
    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      } catch (error) {
        Pop.error(error.message, '[HOME PAGE - GET RECIPES]')
      }
    }
    watchEffect(() => {
      AppState.filter
      getRecipes();
    })

    onMounted(() => {
      getRecipes();
    })
    return {
      recipes: computed(() => {
        if (AppState.filter == '') {
          return AppState.recipes
        }
        if (AppState.filter == 'creator') {
          return AppState.recipes.filter(r => r.creator.id == AppState.account.id)
        }
        else {
          return AppState.favorites
        }
      })
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
