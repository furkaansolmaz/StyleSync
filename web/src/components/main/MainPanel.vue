<template>
  <div class="main-panel">
    <header class="main-panel-header">
      <h1>Menu</h1>
    </header>
    <nav class="main-panel-nav">
      <router-link to="/home">Home</router-link>
      <router-link to="/style-sync">StyleSync</router-link>
      <router-link to="/member-info">Member Information</router-link>
    </nav>
    <div class="logout-container">
      <button @click="logout" class="logout-button">
        <i class="fas fa-sign-out-alt">Logout</i>
      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MainPanel',
  methods: {
    logout() {
      this.$router.push('/login');
    }
  }
};
</script>

<style scoped>
.main-panel {
  display: flex;
  flex-direction: column;
  align-items: stretch;
  height: 100vh;
  background-color: #ffffff; /* Beyaz arka plan rengi */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 10px; /* Köşeleri yuvarlat */
  overflow: hidden;
}

.main-panel-header {
  background-color: #6200ea;
  color: white;
  padding: 20px;
  text-align: center;
  font-family: 'Arial', sans-serif;
  font-size: 24px;
  letter-spacing: 1px;
}

.main-panel-nav {
  display: flex;
  flex-direction: column;
  width: 100%;
  margin-top: 20px; 
}

.main-panel-nav a {
  text-decoration: none;
  color: #6200ea;
  font-weight: bold;
  padding: 15px;
  font-size: 18px;
  text-align: left;
  padding-left: 20px;
  font-family: 'Arial', sans-serif;
  transition: background-color 0.3s, color 0.3s; /* Hover efektine geçiş animasyonu ekle */
}

.main-panel-nav a:hover {
  background-color: #eeeeee;
  color: #4e00b2;
  border-left: 4px solid #4e00b2; /* Sol kenara renkli bir sınır ekle */
}

.logout-container {
  width: 100%;
  padding: 20px;
  text-align: center;
  margin-top: auto;
  background-color: #f5f5f5; /* Ayrı bir arka plan rengi */
}

.logout-button {
  background-color: #6200ea;
  border: none;
  cursor: pointer;
  font-size: 24px;
  color: white;
  padding: 10px 20px;
  border-radius: 5px; /* Butonu yuvarlat */
  transition: background-color 0.3s, transform 0.3s; /* Hover efektine geçiş animasyonu ekle */
}

.logout-button:hover {
  background-color: #4e00b2;
  transform: scale(1.05); /* Hover sırasında butonu büyüt */
}
</style>
