<template>
  <!-- Modal -->
  <div class="modal fade" id="ModalDetails" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered ">
      <div class="modal-content rounded container-fluid">
        <form @submit.prevent="editRecipe()">
          <section class="modal-body rounded row p-0">
            <div class="col-12 col-md-4 card-bg d-flex justify-content-end">
              <i v-if="!isFavorite" @click="toggleFavorite(1, recipe.id)"
                class="favoriteButton mdi mdi-heart p-1 text-light fs-1"></i>
              <i v-else @click="toggleFavorite(-1, recipe.id)"
                class="favoriteButton mdi mdi-heart p-1 text-danger fs-1"></i>
            </div>

            <div class="col-12 col-md-8 d-flex flex-column justify-content-between">
              <!--  -->
              <section class="row">
                <div class="col-12 fs-3 py-2 px-4 d-flex justify-content-between">
                  <div v-if="edit == 'edit'">
                    <input v-model="editable.title" class="form-control" type="text" minlength="3" maxlength="255"
                      required>
                  </div>
                  <div v-else>
                    {{ recipe.title }}
                  </div>
                  <div v-if="recipe.creator?.id == AppState.account?.id">
                    <i v-if="edit == ''" @click="edit = 'edit'" class="mdi mdi-pencil add-button" title="Edit"></i>
                    <div v-if="edit == 'edit'">
                      <button type="submit" @click="editRecipe()" class="mdi mdi-plus add-button me-2"
                        title="Confirm"></button>
                      <i @click=" removeRecipe()" class="mdi mdi-delete add-button me-2" title="DELETE"></i>
                      <i @click="edit = ''" class="mdi mdi-close add-button" title="Cancel"></i>
                    </div>
                  </div>
                </div>
                <div class="col-12 d-flex justify-content-start px-4">
                  <div v-if="edit == 'edit'">
                    <label for="category">Category</label>
                    <select v-model="editable.category" class="form-select" name="category" id="category" required>
                      <option v-for="option in optionArr" :key="option" :value="option">{{ option }}</option>
                    </select>
                  </div>
                  <div v-else class="card-data px-2">
                    {{ recipe.category }}
                  </div>
                </div>
              </section>
              <section class="row flex-grow-1 p-3">
                <div class="col-11 col-md-5 mx-auto elevation-3 p-0">
                  <div class="dark-bg text-light ps-2 fs-4">
                    Instructions
                  </div>
                  <div v-if="edit == 'edit'" class="px-3">
                    <textarea v-model="editable.instructions" class="form-control mt-2" style="resize: none;" name="title"
                      id="insturction" cols="30" rows="10" minlength="3" maxlength="1000" required></textarea>
                  </div>
                  <div v-else class="px-3">
                    {{ recipe.instructions }}
                  </div>
                </div>

                <!--  -->
                <div class="col-11 col-md-5 d-flex flex-column  mx-auto elevation-3 p-0 ">
                  <div class="dark-bg text-light ps-2 fs-4">
                    Ingredients
                  </div>
                  <div class="flex-grow-1">
                    <div v-for="i in ingredients" :key="i.id"> <i v-if="edit != 'edit'"
                        class="mdi mdi-circle-medium ps-1"></i>
                      <i @click="removeIngredient(i.id)" v-else class="mdi mdi-close add-button ps-1"></i>
                      {{ i.quantity }} {{ i.name }}
                    </div>
                  </div>
                  <div v-if="edit == 'edit'" class="d-flex flex-row align-items-center ps-2">
                    <form @submit.prevent="createIngredient()" class="d-flex flex-row align-items-center ps-2">
                      <input v-model="ingredientEditable.quantity" class="form-control" type="text" placeholder="Quantity"
                        id="quantity" minlength="1" maxlength="255" required>
                      <input v-model="ingredientEditable.name" class="form-control" type="text" placeholder="Name"
                        id="name" minlength="1" maxlength="255" required>
                      <button type="submit" class="mdi mdi-plus add-button fs-3" title="Confirm"></button>
                    </form>
                  </div>
                </div>
              </section>
              <section class="row justify-content-end">
                <div class="col-12 col-md-6 text-end pb-2 pe-4">published by-{{ recipe.creator?.name }}</div>
              </section>
            </div>
          </section>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { favoritesService } from "../services/FavoritesService.js";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { ingredientService } from "../services/IngredientsService.js";
import { logger } from "../utils/Logger.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const edit = ref('')
    const editable = ref({})
    const ingredientEditable = ref({})
    watchEffect(() => {
      AppState.activeRecipe
      editable.value = { ...AppState.activeRecipe }
      edit.value = ''
    })
    return {
      edit,
      editable,
      ingredientEditable,
      optionArr: ['Cheese',
        'Italian',
        'Soup',
        'Mexican',
        'Specialty Coffee'],
      AppState: computed(() => AppState),
      recipe: computed(() => AppState.activeRecipe),
      recipeImg: computed(() => `url("${AppState.activeRecipe.img}")`),
      ingredients: computed(() => AppState.ingredients),
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
      async editRecipe() {
        try {
          await recipesService.editRecipe(editable.value, AppState.activeRecipe.id)
          edit.value = ''
        } catch (error) {
          Pop.error(error.message, '[]')
        }
      },
      async createIngredient() {
        logger.log('Trying to create ingredient')
        try {
          const ingredientData = ingredientEditable.value
          ingredientData.recipeId = AppState.activeRecipe.id
          await ingredientService.createIngredient(ingredientData)
          ingredientEditable.value = {}
        } catch (error) {
          Pop.error(error.message, '[]')
        }
      },
      async removeIngredient(ingredientId) {
        try {
          await ingredientService.removeIngredient(ingredientId)
        } catch (error) {
          Pop.error(error.message, '[]')
        }
      },
      async removeRecipe() {
        try {
          const confirmRemove = await Pop.confirm('Delete This Recipe?')
          if (!confirmRemove) {
            return
          }
          const recipeId = AppState.activeRecipe.id
          await recipesService.removeRecipe(recipeId)
          edit.value = ''
          Modal.getOrCreateInstance('#ModalDetails').hide()
        } catch (error) {
          Pop.error(error.message, '[]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.favoriteButton {
  cursor: pointer;
  text-shadow: 0px 0px 2px black;
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

.add-button {
  cursor: pointer;
  border: none;
  background-color: rgba(255, 255, 255, 0);
}

.card-bg {
  background-image: v-bind(recipeImg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  border-radius: 5px 0px 0px 5px;
  min-height: 40vh;
}

@media screen and (min-width: 768px) {
  .card-bg {
    min-height: 70vh;
  }
}
</style>