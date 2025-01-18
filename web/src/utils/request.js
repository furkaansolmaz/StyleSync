import axios from "axios";
import { MessageBox, Notification } from "element-ui";
import store from "@/store";
import { getToken, setToken } from "../utils/auth";
import i18n from "../lang"; // internationalization

// create an axios instance
const service = axios.create({
    baseURL: "http://localhost:5066" , // process.env.VUE_APP_BASE_API, // url = base url + request url
    // withCredentials: true, // send cookies when cross-domain requests
    timeout: 50000 // request timeout
});

// request interceptor
service.interceptors.request.use(
    config => {
        // do something before request is sent
        if (store.getters.token) {
            config.headers.Authorization = `Bearer ${getToken()}`;
        }
        return config;
        // if (store.getters.token) {
        //     // let each request carry token
        //     // ['X-Token'] is a custom headers key
        //     // please modify it according to the actual situation
        //     // config.headers['BearerToken'] = getToken()
        // }
        // return config;
    },
    error => {
        // do something with request error
        return Promise.reject(error);
    }
);

// response interceptor
service.interceptors.response.use(
    response => {
        setToken(getToken());
        if (response.status === 201) {
            const { location } = response.headers;
            if (location && response.data === "") {
                response.data = {
                    id: location.substr(location.lastIndexOf("/") + 1)
                };
            }
        }
        if (response.status === 200) {
            if (location && response.data.message) {
                // action return
                Notification({
                    title: i18n.t("common.actionResult"),
                    message: i18n.t("enums." + response.data.message),
                    duration: 4000
                });
            }
        }
        return response;
    },
    error => {
        if (
            error.response.data.status >= 400 ||
            error.response.status >= 400 ||
            error.message !== undefined
        ) {
            // eslint-disable-next-line
            console.log("1");
        }
        if (error.response.data.status == 401 || error.response.status == 401) {
            store.dispatch("user/logout");
        }
        let errorMessage = error.message;
        if (
            error.message !== undefined &&
            error.message === "Request failed with status code 500"
        ) {
            errorMessage = i18n.t("errors.500");
            console.log("2");
        // } else if (error.response.data.status === 400) {
        //     MessageBox.confirm(
        //         error.response.data.errors,
        //         i18n.t("please input text"),
        //         {
        //             confirmButtonText: i18n.t("button.ok"),
        //             type: "error"
        //         }
        //     ).then(() => {});
        // } else if (error.response.data.status === 422) {
            var message = generateMessage(error.response.data.errors);
            MessageBox.confirm(
                message,
                i18n.t("common.validationErrorBoxTitle"),
                {
                    confirmButtonText: i18n.t("button.ok"),
                    type: "error",
                    dangerouslyUseHTMLString: true
                }
            ).then(() => {});
            console.log("4");
        } else if (
            error.message !== undefined &&
            error.message === "Request failed with status code 401"
        ) {
            store.dispatch("user/logout");
            //self.$router.push({ path: "/login" });
        } else if (
            error.message !== undefined &&
            error.message === "Network Error"
        ) {
            console.log("6");

            errorMessage = i18n.t("errors.networkerror"); // 'Giriş esnasında bir hata oluştu.(101)';
        } else {
            console.log("7"); 

            Notification({
                type: "error",
                message: i18n.t("Lütfen bilgileri doğru giriniz"),
                duration: 4000
            });
        }
        error.message = errorMessage;
        return Promise.reject(error);
    }
);
function generateMessage(errors) {
    var message = "";
    console.log('Backend Errors:', errors);  // Burada backend'den gelen hataları kontrol edebilirsiniz
    if (Array.isArray(errors)) {
        errors.forEach(element => {
            message +=
                element.errorMessage.replace(
                    element.propertyName,
                    "<b>" +
                        i18n.t("form." + element.propertyName + ".label") +
                        "</b>"
                ) + "<br>";
        });
    } else {
        message = "<b><br>" + errors + "</br><b>";
    }
    return message;
}

export default (method, resource, data, baseUrl, config = undefined) =>
    service({
        method,
        url: resource,
        data,
        baseURL: "http://localhost:5066" ,
        config
    });
