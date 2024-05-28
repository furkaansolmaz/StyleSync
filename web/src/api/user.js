import request from "@/utils/request";

export function login(data) {
    const data2 = {
        client_id: "SPA",
        grant_type: "password",
        username: data.username,
        password: data.password
    };
    const config = {
        headers: {
            "Access-Control-Allow-Headers":
                "Origin, X-Requested-With, Content-Type, Accept",
            "Content-Type": "application/x-www-form-urlencoded",
            "Access-Control-Allow-Origin": "*",
            "Cache-Control": "no-cache",
            "Access-Control-Allow-Credentials": true
        }
    };
    const dataString = Object.keys(data2)
        .map(k => `${encodeURIComponent(k)}=${encodeURIComponent(data2[k])}`)
        .join("&");

    return request(
        "post",
        "connect/token",
        dataString,
        "http://localhost:5066",
        config
    );
}

export function getUserInfo() {
    return request("get", "connect/userinfo", undefined, "http://localhost:5066" );
}

export function getInfo(token) {
    const data2 = {
        client_id: "SPA",
        token: token
    };
    const dataString = Object.keys(data2)
        .map(k => `${encodeURIComponent(k)}=${encodeURIComponent(data2[k])}`)
        .join("&");

    return request(
        "get",
        "connect/userinfo",
        dataString,
        "http://localhost:5066"
    );
}

export function logout() {
    return new Promise(resolve => {
        request("get", "/connect/endsession", undefined, "http://localhost:5066")
            .then(() => resolve())
            .catch(() => resolve());
    });
}
