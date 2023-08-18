<template>
  <header class="sticky-top">
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="text-light">
    <div class="new-recipe text-center mb-3 me-2 d-flex justify-content-center align-items-center">
      <i class="mdi mdi-plus fs-2"></i>
    </div>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
  --light-green: rgba(50, 135, 50);
  --bg-green: rgb(226, 246, 226);

}

body {
  font-family: 'Sahitya', serif;

}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}

.new-recipe {
  position: fixed;
  bottom: 0%;
  right: 0%;
  background-color: rgba(50, 135, 50, 0.844);
  border-radius: 50%;
  height: 6vh;
  width: 6vh;
}

.new-recipe:hover {
  background-color: rgba(50, 135, 50, 0.959);
  cursor: pointer;
}
</style>
