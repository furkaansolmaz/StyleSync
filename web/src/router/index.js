import Vue from 'vue';
import Router from 'vue-router';
import UserLogin from '@/components/login/UserLogin.vue';
import HomePage from '@/components/main/HomePage.vue';
import StyleSync from '@/components/main/StyleSync.vue';
import UserInfo from '@/components/main/UserInfo.vue';
import MainLayout from '@/components/MainLayout.vue';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/login',
      name: 'Login',
      component: UserLogin
    },
    {
      path: '/',
      component: MainLayout,
      children: [
        {
          path: 'home',
          name: 'Home',
          component: HomePage
        },
        {
          path: 'style-sync',
          name: 'StyleSync',
          component: StyleSync
        },
        {
          path: 'user-info',
          name: 'UserInfo',
          component: UserInfo
        }
      ]
    },
    {
      path: '*',
      redirect: '/login'
    }
  ]
});
