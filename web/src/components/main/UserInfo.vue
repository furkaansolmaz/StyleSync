<template>
  <el-container>
    <el-header class="header">
      <p>User Information</p>
    </el-header><br><br><br><br>
    <el-main class="main">
      <el-form :model="user" label-width="120px" class="user-form">
        <el-form-item label="First Name">
          <el-input v-model="user.firstName" size="small" readonly></el-input>
        </el-form-item>
        <el-form-item label="Last Name">
          <el-input v-model="user.lastName" size="small" readonly></el-input>
        </el-form-item>
        <el-form-item label="Birth Date">
          <el-date-picker v-model="user.birthDate" type="date" placeholder="Select a date" size="small" readonly></el-date-picker>
        </el-form-item>
      </el-form>
    </el-main>
  </el-container>
</template>

<script>
export default {
  name: 'UserInfo',
  data() {
    return {
      user: {
        firstName: 'Furkan', // z
        lastName: 'solmaz',   // Örneğin: this.member.firstName = gelenVeri.firstName;
        birthDate: '2000-01-01'
      }
    };
  }
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

.main {
  display: flex;
  justify-content: center;
  align-items: flex-start;
  height: calc(100vh - 80px);
  margin-left: 20px; /* Formu biraz sola kaydırır */
}

.user-form {
  width: 400px;
}

.el-form-item {
  margin-bottom: 20px;
}

.el-input, .el-date-picker, .el-button {
  width: 100%;
}
</style>
