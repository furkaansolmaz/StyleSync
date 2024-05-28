<template>
  <el-container>
    <el-header class="header">
      <p>StyleSync</p>
    </el-header>
    <el-main class="main-content">
      <el-upload
        class="upload-demo"
        action="https://jsonplaceholder.typicode.com/posts/"
        :on-preview="handlePreview"
        :on-remove="handleRemove"
        :file-list="fileList"
        list-type="picture">
        <el-button size="small" type="primary">Click to upload</el-button>
        <div slot="tip" class="el-upload__tip">jpg/png files with a size less than 500kb</div>
      </el-upload>
      <div v-if="imageUrl" class="preview">
        <img :src="imageUrl" alt="Uploaded Image" />
        <div class="buttons">
          <el-button type="success" @click="uploadImage">Ekle</el-button>
          <el-button type="danger" @click="cancelUpload">İptal</el-button>
        </div>
      </div>
    </el-main>
  </el-container>
</template>

<script>
export default {
  name: 'StyleSync',
  data() {
    return {
      imageUrl: '' // Yüklenen görüntünün base64 URL'i
    };
  },
  methods: {
    handleFileChange(file) {
      const reader = new FileReader();
      reader.onload = (event) => {
        this.imageUrl = event.target.result; // Base64 URL'ini sakla
      };
      reader.readAsDataURL(file.raw);
    },
    beforeUpload(file) {
      // İsteğe bağlı: Dosya boyutu veya türü gibi kontrolleri burada yapabilirsiniz
      const isImage = file.type.startsWith('image/');
      const isLt2M = file.size / 1024 / 1024 < 2;

      if (!isImage) {
        this.$message.error('Yalnızca resim dosyaları yüklenebilir!');
      }
      if (!isLt2M) {
        this.$message.error('Resim boyutu 2MB dan küçük olmalıdır!');
      }
      return isImage && isLt2M;
    },
    uploadImage() {
      // Ekleme işlemi için gereken adımları burada gerçekleştirin
      console.log('Görüntü eklendi:', this.imageUrl);
      // Örneğin, bu aşamada görüntüyü bir API'ye gönderebilir ve veritabanına kaydedebilirsiniz.
    },
    cancelUpload() {
      this.imageUrl = ''; // Görüntüyü temizle
    }
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

.main-content {
  padding: 20px;
  text-align: center;
}

.upload-demo {
  display: inline-block;
}

.preview {
  margin-top: 20px;
}

.preview img {
  max-width: 200px;
  max-height: 200px;
  margin-bottom: 10px;
}

.buttons {
  display: flex;
  justify-content: center;
  gap: 10px;
}
</style>
