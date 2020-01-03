import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: () => import(/* webpackChunkName: "home-bundle" */ '../views/Home.vue')
  },
  {
    path: '/ver',
    name: 'ver',
    component: () => import(/* webpackChunkName: "home-bundle" */ '../views/Ver.vue')
  },
  {
    path: '*',
    redirect: 'home'
  }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
