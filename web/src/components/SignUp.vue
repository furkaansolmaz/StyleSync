<template>
  <div>
    <el-dialog :visible.sync="localDialogFormVisible" width="30%" @close="closeDialog">
      <div class="signup">
        <h2>Sign Up</h2>
        <el-form :model="signupForm" :rules="rules" ref="signupForm">
          <el-form-item label="Name" prop="name">
            <el-input v-model="signupForm.name"></el-input>
          </el-form-item>
          <el-form-item label="Surname" prop="lastName">
            <el-input v-model="signupForm.lastname"></el-input>
          </el-form-item>
          <el-form-item label="User Name" prop="username">
            <el-input v-model="signupForm.username"></el-input>
          </el-form-item>
          <el-form-item label="Password" prop="password">
            <el-input type="password" v-model="signupForm.password"></el-input>
          </el-form-item>
          <el-form-item label="Date Of Birth" prop="dateOfBirth">
            <el-date-picker v-model="signupForm.dateOfBirth" type="date" placeholder="Bir tarih seçin"></el-date-picker>
          </el-form-item>
          <el-form-item label="Gender" prop="gender">
            <el-select v-model="signupForm.gender" placeholder="Cinsiyet Seçin">
              <el-option label="Erkek" value=1></el-option>
              <el-option label="Kadın" value=2></el-option>
              <el-option label="Diğer" value=3></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSignUp">Sign Up</el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import request from '@/utils/request';

export default {
  name: 'SignUp',
  props: {
    dialogFormVisible: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      signupForm: {
        name: '',
        lastname: '',
        username: '',
        password: '',
        dateOfBirth: '',
        gender: '',
      },
      rules: {
        name: [
          { required: true, message: 'Lütfen isminizi girin', trigger: 'blur' },
        ],
        lastname: [
          { required: true, message: 'Lütfen soyisminizi girin', trigger: 'blur' },
        ],
        username: [
          { required: true, message: 'Lütfen kullanıcı adınızı girin', trigger: 'blur' },
        ],
        password: [
          { required: true, message: 'Lütfen şifrenizi girin', trigger: 'blur' },
        ],
        dateOfBirth: [
          { required: true, message: 'Lütfen bir tarih seçin', trigger: 'change' },
        ],
        gender: [
          { required: true, message: 'Lütfen cinsiyetinizi seçin', trigger: 'change' },
        ],
      },
      localDialogFormVisible: this.dialogFormVisible,
    };
  },
  watch: {
    dialogFormVisible(val) {
      this.localDialogFormVisible = val;
      if (!val) {
        this.clearForm(); // Dialog kapatıldığında formu temizle
      }
    },
  },
  methods: {
    handleSignUp() {
      this.$refs.signupForm.validate((valid) => {
        if (valid) {
          const date = new Date(this.signupForm.dateOfBirth);

          const requestData = {
              name: this.signupForm.name,
              lastname: this.signupForm.lastname,
              username: this.signupForm.username,
              password: this.signupForm.password,
              dateOfBirth: date,
              gender: this.signupForm.gender,
          };
          request('post','api/v1/member', requestData)
            .then(() => {
              console.log('signup successful');
              this.$notify.success('Başarılı şekilde kaydınız oluşmuştur.')
              this.closeDialog()
            })
            .catch((error) => {
              console.error('Error during signup:', error);
            });
        } else {
          console.log('error submit!!');
          return false;
        }
      });
    },
    closeDialog() {
      this.$emit('update:dialogFormVisible', false);
    },
    clearForm() {
      this.signupForm = {
        name: '',
        lastname: '',
        username: '',
        password: '',
        dateOfBirth: '',
        gender: '',
      };
    },
  },
};
</script>


<style>
.signup {
  padding: 20px;
}

.signup h2 {
  margin-bottom: 20px;
}

.signup .el-form-item {
  margin-bottom: 20px;
}

.signup .el-form-item:last-child {
  margin-bottom: 0;
}

.signup .el-button {
  width: 100%;
  padding: 10px;
  background-color: #28a745;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.signup .el-button:hover {
  background-color: #218838;
}
</style>
