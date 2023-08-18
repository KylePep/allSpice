<template>
  <div class="card-bg d-flex flex-column rounded mb-4 elevation-2">
    <div class="reserved-space d-flex justify-content-between align-items-start">
      <p class="m-1 card-data px-3">{{ recipeProp.category }}</p>
      <div class="card-data favorite m-1">

        <i v-if="!isFavorite" @click="toggleFavorite(1, recipeProp.id)" class="mdi mdi-heart p-1 "></i>
        <i v-else @click="toggleFavorite(-1, recipeProp.id)" class="mdi mdi-heart p-1 text-danger "></i>

      </div>
    </div>
    <div @click="setActiveRecipe()" type="button" data-bs-toggle="modal" data-bs-target="#ModalDetails"
      class="card-data flex-grow-1 px-2 m-2">
      <h5>{{ recipeProp.title }}</h5>
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { favoritesService } from "../services/FavoritesService.js";


export default {
  props: {
    recipeProp: { type: Object, required: true }
  },
  setup(props) {
    return {
      bgImg: computed(() => `url("${props.recipeProp.img}")`),

      isFavorite: computed(() => {
        if (AppState.favorites.find(f => f.id == props.recipeProp.id)) {
          return true
        }
        else return false
      }),

      async toggleFavorite(toggle, recipeId) {
        try {
          if (toggle > 0) {
            await favoritesService.createFavorite(recipeId)
          } else {
            let favorite = AppState.favorites.find(f => f.id == props.recipeProp.id)
            const favoriteId = favorite.favoriteId
            await favoritesService.deleteFavorite(favoriteId)
          }
        } catch (error) {
          Pop.error(error.message, '[TOGGLE FAVORITE]')
        }
      },
      setActiveRecipe() {
        AppState.activeRecipe = props.recipeProp;
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.card-bg {
  background-image: v-bind(bgImg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  min-height: 35vh;
}

.reserved-space {
  min-height: 24vh;
}

.card-data {
  font-family: 'Roboto', sans-serif;
  color: lightgray;
  background: rgba(98, 97, 97, 0.72);
  border-radius: 8px;
  box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(9.3px);
  -webkit-backdrop-filter: blur(9.3px);
  border: 1px solid rgba(98, 97, 97, 0.45);
}

.favorite {}

.favorite:hover {
  cursor: pointer;
}
</style>