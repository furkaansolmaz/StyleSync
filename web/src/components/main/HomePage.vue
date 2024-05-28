<template>
  <el-container>
    <el-header class="header">
      <p>Home Page</p>
      <div class="welcome-message">
        <p>Sayfamıza hoş geldiniz.</p>
        <p>
          StyleSync, sizlere daha şık ve güzel kombinler yapmak için hazırlanan bir uygulamadır. Tarzınızı yansıtmak ve en iyi kombinleri oluşturmak için ihtiyacınız olan her şey burada.
        </p>
        <p>
          Bu platform, size en yeni moda trendlerini sunarken, aynı zamanda kişisel zevklerinize uygun önerilerde bulunur. Gardırobunuzu düzenlemek, yeni kıyafetler keşfetmek ve modayı yakından takip etmek hiç bu kadar kolay olmamıştı!
        </p>
        <p>
          StyleSync ile tarzınızı yeniden keşfedin ve en iyi versiyonunuzu ortaya çıkarın.
        </p>
      </div>
    </el-header>
    <el-main class="main-content">
      <!-- Sayfa içeriği buraya gelecek -->
    </el-main>
  </el-container>
</template>

<script>
export default {
  name: 'HomePage'
};
</script>

<style scoped>
.header {
  background-color: #6200ea;
  color: white;
  padding: 20px;
  text-align: center;
  font-family: 'Arial', sans-serif;
}

.welcome-message {
  font-size: 16px;
  margin-top: 10px;
  line-height: 1.5;
  color: #333; /* Yazı rengi koyu gri olarak değiştirildi */
  text-align: left;
  max-width: 600px;
  margin: 10px auto;
}

.main-content {
  padding: 20px;
}
</style>
