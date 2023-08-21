<template>
  <div class="container-fluid nav-bg pb-0">
    <section class="row ">
      <div class="col-4">

      </div>

      <div class="col-4 fs-1 pt-3 text-light text-center postion-relative">
        <div class="pb-5">All-Spice
          <p class="fs-4 mb-0">Cherish Your Family</p>
          <p class="fs-4 ">And Their Cooking</p>
        </div>

        <section class="row buttons bg-light fs-4 elevation-3 rounded">
          <div @click="getRecipes(), filterBy = ''" class="col-4 selectable">Home</div>
          <div @click="filterBy = 'creator'" class="col-4 selectable">My Recipes</div>
          <div @click="filterBy = 'favorite'" class="col-4 selectable">Favorites</div>
        </section>

      </div>

      <nav class="col-4  px-3 d-flex justify-content-end">
        <form class="btn-group d-flex align-items-start my-2 me-3">

          <input v-model="editable.query" id="query" class="search-start  ps-2 pb-1" type="search" placeholder="Search">
          <button @submit.prevent="" @click="getRecipesByQuery()" class="search-end mdi mdi-magnify"
            type="submit"></button>
        </form>

        <Login />
      </nav>
    </section>
  </div>
</template>

<script>
import { computed, ref } from "vue";
import Login from './Login.vue';
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
export default {
  setup() {
    const filterBy = ref('')
    const editable = ref({})

    return {
      filterBy,
      editable,
      filter: computed(AppState.filter = filterBy),
      async getRecipesByQuery() {
        try {
          const queryObject = editable.value;
          await recipesService.getRecipesByQuery(queryObject)
          editable.value = {}
        } catch (error) {
          Pop.error(error.message, '[]')
        }
      },
      async getRecipes() {
        try {
          await recipesService.getRecipes();
        } catch (error) {
          Pop.error(error.message, '[HOME PAGE - GET RECIPES]')
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  .nav-bg {
    height: 190px;
  }
}

.nav-bg {
  background-image: url('https://images.unsplash.com/photo-1509358271058-acd22cc93898?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=870&q=80');
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;

}

/* .nav-bg {
  background-image:
    linear-gradient(black, black),
    url('https://images.unsplash.com/photo-1509358271058-acd22cc93898?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=870&q=80');
  background-blend-mode: saturation;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;

} */

/* .nav-bg::before {
  content: "";
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  position: absolute;
  background-image: url('https://images.unsplash.com/photo-1509358271058-acd22cc93898?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=870&q=80');
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  filter: grayscale(1);
} */

.search-start {
  border-radius: 7px 0% 0% 7px;
  background-color: whitesmoke;
  width: 16vw;
  border: none;
  height: 40px;
}

.search-end {
  border-radius: 0% 20% 20% 0%;
  background-color: whitesmoke;
  border: none;
  height: 40px;


}

.buttons {
  color: var(--light-green);
  position: absolute;
  width: 50vw;
  left: 26%;
  top: 88%;
}
</style>
