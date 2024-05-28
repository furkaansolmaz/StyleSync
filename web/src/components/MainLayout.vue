<template>
  <div class="main-layout">
    <MainPanel class="side-panel" />
    <div class="content">
      <router-view />
    </div>
  </div>
</template>

<script>
import MainPanel from '@/components/main/MainPanel.vue';

export default {
  name: 'MainLayout',
  components: {
    MainPanel
  }
};
</script>

<style scoped>
.main-layout {
  display: flex;
  height: 100vh;
}

.side-panel {
  width: 250px;
  background-color: #f5f5f5;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.content {
  flex-grow: 1;
  padding: 20px;
  background-color: #ffffff;
}
</style>
