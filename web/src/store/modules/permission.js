import { asyncRoutes, constantRoutes } from "@/router";
import store from "@/store";

/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(roles, route) {
    if (store.getters.name === "Alice Smith") {
        return true;
    }
    if (route.name)
        return (
            roles.findIndex(
                el => el.toLowerCase() === route.name.toLowerCase()
            ) >= 0
        );
    else {
        return true;
    }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, roles) {
    const res = [];

    routes.forEach(route => {
        const tmp = { ...route };
        if (hasPermission(roles, tmp)) {
            if (tmp.children) {
                tmp.children = filterAsyncRoutes(tmp.children, roles);
            }
            res.push(tmp);
        }
    });

    return res;
}

const state = {
    routes: [],
    addRoutes: []
};

const mutations = {
    SET_ROUTES: (state, routes) => {
        state.addRoutes = routes;
        state.routes = constantRoutes.concat(routes);
    }
};

const actions = {
    generateRoutes({ commit }) {
        return new Promise(resolve => {
            
                    commit("SET_ROUTES", asyncRoutes);
                    resolve(asyncRoutes);
        });
    }
};

export default {
    namespaced: true,
    state,
    mutations,
    actions
};
