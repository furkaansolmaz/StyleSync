<template>
  <div class="login-container">
    <div class="login">
      <h2>Login</h2>
      <el-form :model="loginForm" :rules="rules" ref="loginForm">
        <el-form-item label="Username" prop="username">
          <el-input v-model="loginForm.username"></el-input>
        </el-form-item>
        <el-form-item label="Password" prop="password">
          <el-input type="password" v-model="loginForm.password"></el-input>
        </el-form-item>
        <div class="button-container">
          <el-button type="primary" class="login-button" @click="login">Login</el-button>
          <el-button type="success" class="signup-button" @click="goToSignUp">Sign Up</el-button>
        </div>
      </el-form>
      <sign-up :dialog-form-visible.sync="dialogFormVisible" />
    </div>
  </div>
</template>

<script>
import request from '@/utils/request';
import SignUp from './SignUp.vue';

export default {
  name: 'UserLogin',
  components: { 
    SignUp 
  },
  data() {
    return {
      dialogFormVisible: false,
      loginForm: {
        username: '',
        password: ''
      },
      rules: {
        username: [
          { required: true, message: 'Please input username', trigger: 'blur' }
        ],
        password: [
          { required: true, message: 'Please input password', trigger: 'blur' }
        ]
      }
    };
  },
  methods: {
    login() {
      request('get', `api/v1/login?username=${this.loginForm.username}&password=${this.loginForm.password}`)
        .then(() => {
          console.log('loginned : ')
          this.$router.push({ path: '/home' });
          this.$notify.success('Login Olundu.')
        })
        .catch(() => {
          console.log('error submit!!');
        });
    },
    goToSignUp() {
      this.dialogFormVisible = true;
    }
  }
};
</script>

<style>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f0f0f0;
  background-image: url('@/assets/clothing-pattern.png');
  background-repeat: repeat;
}

.login {
  background-color: #f9f9f9;
  padding: 40px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  max-width: 400px;
  width: 100%;
}

.login h2 {
  margin-bottom: 20px;
}

.button-container {
  display: flex;
  justify-content: space-between;
}

.login-button {
  width: 65%;
  padding: 10px;
  background-color: #007bff;
  color: #fff;
  border: none;
  border-radius: 4px;
}

.login-button:hover {
  background-color: #0056b3;
}

.signup-button {
  width: 30%;
  padding: 10px;
  background-color: #29b94b;
  color: #fff;
  border: none;
  border-radius: 4px;
}

.signup-button:hover {
  background-color: #218838;
}
</style>
