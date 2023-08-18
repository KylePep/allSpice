<template>
  <!-- Modal -->
  <div class="modal fade" id="ModalDetails" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered ">
      <div class="modal-content container-fluid">
        <section class="modal-header row ">

          <div class="col-4 card-bg d-flex justify-content-end">
            <i v-if="!isFavorite" @click="toggleFavorite(1, recipe.id)" class="mdi mdi-heart p-1 "></i>
            <i v-else @click="toggleFavorite(-1, recipe.id)" class="mdi mdi-heart p-1 text-danger "></i>
          </div>

          <div class="col-8">

          </div>
        </section>
      </div>
      <!-- <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          ...
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div> -->
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { favoritesService } from "../services/FavoritesService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    return {
      recipe: computed(() => AppState.activeRecipe),
      recipeImg: computed(() => `url("${AppState.activeRecipe.img}")`),
      isFavorite: computed(() => {
        if (AppState.favorites.find(f => f.id == AppState.activeRecipe.id)) {
          return true
        }
        else return false
      }),
      async toggleFavorite(toggle, recipeId) {
        try {
          if (toggle > 0) {
            await favoritesService.createFavorite(recipeId)
          } else {
            let favorite = AppState.favorites.find(f => f.id == AppState.activeRecipe.id)
            const favoriteId = favorite.favoriteId
            await favoritesService.deleteFavorite(favoriteId)
          }
        } catch (error) {
          Pop.error(error.message, '[TOGGLE FAVORITE]')
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.card-bg {
  background-image: v-bind(recipeImg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  min-height: 70vh;
  min-width: 40vw;

}
</style>