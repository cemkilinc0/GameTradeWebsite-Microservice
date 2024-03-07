<template>
  <img alt="Vue logo" src="./assets/left-leg.png" class="logo">
  <HomePage msg="Welcome to Your Vue.js App"/>
</template>

<script>
import HomePage from './components/HomePage.vue';

export default {
  name: 'App',
  components: {
    HomePage
}
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

.logo {
  max-width: 500px;
  max-height: 750px;
  margin: auto;
  padding: 20px;
}

</style>
