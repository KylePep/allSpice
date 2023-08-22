<template>
  <!-- Modal -->
  <div class="modal fade" id="ModalNew" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content container-fluid">
        <section class="row dark-bg">
          <div class="modal-header">
            <h1 class="modal-title text-light fs-1" id="exampleModalLabel">New Recipe</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
        </section>
        <form @submit.prevent="createRecipe()" action="">
          <section class="row modal-body ">
            <div class="col-8">
              <label class="form-label" for="title">Title</label>
              <input v-model="editable.title" class="form-control" type="text" id="title" minlength="3" maxlength="255"
                required>
            </div>
            <div class="col-4">
              <label for="category">Category</label>
              <select v-model="editable.category" class="form-select" name="category" id="category" required>
                <option v-for="option in optionArr" :key="option" :value="option">{{ option }}</option>
              </select>
            </div>
            <div class="col-12">
              <label class="form-label" for="img">Image</label>
              <input v-model="editable.img" class="form-control" type="text" id="img" minlength="3" maxlength="700"
                required>
            </div>
            <div class="col-12">
              <label class="form-label" for="insturctions">Instruction</label>
              <textarea v-model="editable.instructions" class="form-control" style="resize: none;" name="title"
                id="insturction" cols="30" rows="5" minlength="3" maxlength="1000" required></textarea>
            </div>
          </section>
          <section class="row">
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
              <button type="submit" class="btn btn-success">Create Recipe</button>
            </div>
          </section>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      optionArr: ['Cheese',
        'Italian',
        'Soup',
        'Mexican',
        'Specialty Coffee'],

      async createRecipe() {
        try {
          await recipesService.createRecipe(editable.value);
          Modal.getOrCreateInstance('#ModalNew').hide()
          editable.value = {};
        } catch (error) {
          Pop.error(error.message, '[CREATE RECIPE MODAL]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>