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
        // Safely check for error response properties
        const errorResponse = error.response || {};
        const errorStatus = errorResponse.status;
        const errorData = errorResponse.data || {};
        
        let errorMessage = error.message;

        // Handle 401 Unauthorized
        if (errorStatus === 401 || errorData.status === 401) {
            store.dispatch("user/logout");
            return Promise.reject(new Error(i18n.t("errors.unauthorized") || "Unauthorized access"));
        }
        
        // Handle 404 Not Found
        if (errorStatus === 404) {
            errorMessage = i18n.t("errors.404") || "Resource not found";
            Notification({
                type: "error",
                message: errorMessage,
                duration: 4000
            });
            return Promise.reject(new Error(errorMessage));
        }

        // Handle 422 Validation Error
        if (errorStatus === 422) {
            const message = generateMessage(errorData.errors);
            MessageBox.confirm(
                message,
                i18n.t("common.validationErrorBoxTitle"),
                {
                    confirmButtonText: i18n.t("button.ok"),
                    type: "error",
                    dangerouslyUseHTMLString: true
                }
            );
            return Promise.reject(new Error(message));
        }

        // Handle other specific errors
        if (error.message === "Network Error") {
            errorMessage = i18n.t("errors.networkerror");
        } else if (error.message === "Request failed with status code 500") {
            errorMessage = i18n.t("errors.500");
        }

        // Show notification for unhandled errors
        Notification({
            type: "error",
            message: errorMessage || i18n.t("Lütfen bilgileri doğru giriniz"),
            duration: 4000
        });

        return Promise.reject(new Error(errorMessage));
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
