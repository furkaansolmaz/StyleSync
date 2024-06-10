<template>
  <div>
    <el-container>
      <el-header class="header">
        <p>StyleSync</p>
      </el-header>
      <el-main class="main-content">
        <div class="upload-section">
          <el-button type="primary" icon="el-icon-upload" @click="dialogVisible = true">Ekle</el-button>
        </div>
        <el-dialog :visible.sync="dialogVisible" title="Resim Ekle">
          <el-upload
            class="upload-demo"
            ref="upload"
            :show-file-list="false"
            :auto-upload="false"
            :on-change="handleFileChange"
            :before-upload="beforeUpload">
            <el-button type="primary" icon="el-icon-upload" :disabled="imageUrl !== ''">Resim Seç</el-button>
          </el-upload>
          <div v-if="imageUrl" class="preview">
            <img :src="imageUrl" alt="Uploaded Image" />
          </div>
          <span slot="footer" class="dialog-footer">
            <el-button @click="cancelUpload">İptal</el-button>
            <el-button type="primary" @click="uploadImage">Kaydet</el-button>
          </span>
        </el-dialog>

        <el-table ref="imageTable" :data="imageList" @selection-change="handleSelectionChange" style="width: 100%; margin-top: 20px">
          <el-table-column
            type="selection"
            width="55">
          </el-table-column>
          <el-table-column
            prop="url"
            label="Resim"
            width="180">
            <template slot-scope="scope">
              <img :src="scope.row.url" class="table-image" />
            </template>
          </el-table-column>
        </el-table>
        <div class="request-input-section">
          <el-input v-model="city" placeholder="Konum Seçiniz" :disabled="!isImageSelected"></el-input>
        </div>
        <div class="request-input-section">
          <el-input v-model="informationRequest" placeholder="Bilgi talebi giriniz" :disabled="!isImageSelected"></el-input>
          <el-button type="primary" class="chat-button" @click="sendSelectedImages" :disabled="!isImageSelected">Seçili Resimleri Gönder</el-button>
        </div>
        <div v-if="responseChat" class="request-input-section">
          {{ responseChat }}
        </div>
      </el-main>
    </el-container>
  </div>
</template>

<script>
import request from '@/utils/request';

export default {
  name: 'StyleSync',
  data() {
    return {
      imageUrl: '', 
      dialogVisible: false, 
      imageList: [],
      image: null,
      informationRequest: '',
      isImageSelected: false,
      responseChat: null,
      city: null,
    };
  },
  methods: {
    handleFileChange(file) {
      this.image = file.raw;
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
      const base64Image = this.imageUrl.split(',')[1];
      const memberId = localStorage.getItem('memberId');
      const viewModel = {
        memberId: memberId,
        image: base64Image
      };
      request('post', `api/v1/stylesyncprod`, viewModel).then((v) => {
        console.log('furkan', v)
        this.imageList.push({
          id : v.data,
          url: this.imageUrl,
          name: `Resim ${this.imageList.length + 1}`
        });
        this.dialogVisible = false;
        this.imageUrl = '';
        this.image = null;
        this.$message.success('Görüntü başarıyla yüklendi!');
      }).catch(error => {
        this.$message.error('Görüntü yüklenirken bir hata oluştu!');
        console.error('Görüntü yüklenirken hata:', error);
      });
    },
    cancelUpload() {
      this.imageUrl = '';
      this.dialogVisible = false;
      this.image = null; 
    },
    listImage(){
      request('get', `api/v1/stylesyncprod/getAll?memberId=` + localStorage.getItem('memberId')).then((v) => {
        this.$message.success('Resimler listelendi!');
        this.imageList = v.data.map(item => ({
          id: item.styleSyncProdId,
          url: `data:image/png;base64,${item.image}`,
          name: item.name
        }));
      }).catch(error => {
        this.$message.error('Resimleri yüklerken bir hata oluştu!');
        console.error('Resimleri yüklerken hata:', error);
      });
    },
    handleSelectionChange(selection) {
      this.isImageSelected = selection.length > 0;
    },
    sendSelectedImages() {
      const styleSyncProdId = this.$refs.imageTable.selection.map(image => image.id);
      console.log(styleSyncProdId)
      const viewModel = {
        styleSyncProdId: styleSyncProdId,
        informationRequest: this.informationRequest,
        city : this.city
      };

      request('post', `api/v1/chatgpt`, viewModel).then(response => {
        this.$message.success('Seçili resimler başarıyla gönderildi!');
        console.log('ChatGPT yanıtı:', response.data.informationResponse);
        this.responseChat = response.data.informationResponse
      }).catch(error => {
        this.$message.error('Resimler gönderilirken bir hata oluştu!');
        console.error('Resimler gönderilirken hata:', error);
      });
    }
  },
  created() {
    this.listImage();
  },
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

.upload-section {
  text-align: right; /* Butonu sağa hizalamak için */
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

.table-image {
  max-width: 100px;
  max-height: 100px;
}

.request-input-section {
  margin-top: 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.chat-button {
  margin-left: 10px;
}
</style>
