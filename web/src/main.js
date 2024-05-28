import Vue from 'vue';
import App from './App.vue';
import router from './router';
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';

Vue.config.productionTip = false;
Vue.use(ElementUI);
Vue.directive('uppercase',
  {
    inserted: function(el, _, vnode) {
      el.addEventListener('keyup', function(e) {
        e.target.value = e.target.value.toLocaleUpperCase('tr-TR')
        vnode.componentInstance.$emit('input', e.target.value.toLocaleUpperCase('tr-TR'))
      })
    }
  })

Vue.directive('lowercase',
  {
    bind: function(el, binding) {
      const { value } = binding
      el.addEventListener('input', function(e) {
        e.target.value = e.target.value.toLocaleLowerCase(value || 'tr-TR')
      })
    }
  })
  
new Vue({
  router,
  render: h => h(App),
}).$mount('#app');

