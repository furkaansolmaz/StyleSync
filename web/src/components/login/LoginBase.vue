<template>
    <div class="app-container">
      <keep-alive>
        <user-login v-if="currentComponent=='UserLogin'" :user-login-visible="userLoginVisible" />
      </keep-alive>
      <keep-alive>
        <sign-up v-if="currentComponent=='SignUp'" :sign-up-visible="signUpVisible"/>
      </keep-alive>
    </div>
  </template>
  
  <script>
  import UserLogin from './components/UserLogin'
  import SignUp from './components/SignUp'
  
  export default {
    name: 'LoginBase',
    components: {
        UserLogin,
        SignUp
    },
    props: {
        userLoginVisible: {
        type: Boolean,
        default: true
    },
        signUpVisible: {
        type: Number,
        default: null
    }
    },
    data() {
      return {
        activeNamePositionKeyword: 'userLoginVisible',
        contactKeyword: 'userLoginVisible',
        currentComponent: 'signUpVisible'
      }
    },
    watch: {
      model: {
        handler() {
        },
        deep: true
      }
    },
    methods: {
  
    }
  }
  
  </script>
  