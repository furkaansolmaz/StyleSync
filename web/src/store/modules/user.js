import { login, logout } from "@/api/user";
import { getToken, setToken, removeToken } from "@/utils/auth";
import router, { resetRouter } from "@/router";

const state = {
    token: getToken(),
    name: "",
    avatar: "",
    introduction: "",
    roles: []
};

const mutations = {
    SET_TOKEN: (state, token) => {
        state.token = token;
    },
    SET_INTRODUCTION: (state, introduction) => {
        state.introduction = introduction;
    },
    SET_NAME: (state, name) => {
        state.name = name;
    },
    SET_AVATAR: (state, avatar) => {
        state.avatar = avatar;
    },
    SET_ROLES: (state, roles) => {
        state.roles = roles;
    }
};

const actions = {
    // user login
    login({ commit }, userInfo) {
        const { username, password } = userInfo;

        return new Promise((resolve, reject) => {
            login({ username: username.trim(), password: password })
                .then(response => {
                    const { data } = response;
                    console.log(data);
                    commit("SET_TOKEN", data.access_token);
                    setToken(data.access_token);
                    resolve();
                })
                .catch(error => {
                    reject(error);
                });
        });
    },

    // get user info
    getInfo({ commit }) {
        var token = getToken();
        var tokenData = JSON.parse(
            decodeURIComponent(escape(window.atob(token.split(".")[1])))
        );
        console.log(tokenData);
        var data = {
            roles: [tokenData.role],
            introduction: "",
            avatar:
                "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif",
            name: tokenData.given_name + " " + tokenData.family_name
        };

        const { roles, name, avatar, introduction } = data;
        commit("SET_ROLES", roles);
        commit("SET_NAME", name);
        commit("SET_AVATAR", avatar);
        commit("SET_INTRODUCTION", introduction);
        return data;
    },

    // user logout
    logout({ commit, state, dispatch }) {
        return new Promise((resolve, reject) => {
            logout(state.token)
                .then(() => {
                    commit("SET_TOKEN", "");
                    commit("SET_ROLES", []);
                    removeToken();
                    resetRouter();

                    // reset visited views and cached views
                    // to fixed https://github.com/PanJiaChen/vue-element-admin/issues/2485
                    dispatch("tagsView/delAllViews", null, { root: true });

                    resolve();
                })
                .catch(error => {
                    reject(error);
                });
        });
    },

    // remove token
    resetToken({ commit }) {
        return new Promise(resolve => {
            commit("SET_TOKEN", "");
            commit("SET_ROLES", []);
            removeToken();
            resolve();
        });
    },

    // dynamically modify permissions
    async changeRoles({ commit, dispatch }, role) {
        const token = role + "-token";

        commit("SET_TOKEN", token);
        setToken(token);

        const { roles } = await dispatch("getInfo");

        resetRouter();

        // generate accessible routes map based on roles
        const accessRoutes = await dispatch(
            "permission/generateRoutes",
            roles,
            { root: true }
        );
        // dynamically add accessible routes
        router.addRoutes(accessRoutes);

        // reset visited views and cached views
        dispatch("tagsView/delAllViews", null, { root: true });
    }
};

export default {
    namespaced: true,
    state,
    mutations,
    actions
};
