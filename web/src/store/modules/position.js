const position = {
    state: {
        positionId: "",
        positionName: ""
    },

    mutations: {
        SET_POSITION: (state, row) => {
            state.positionId = row.positionId;
            state.positionName = row.positionName;
        }
    },

    actions: {
        SetPosition({ commit }, row) {
            commit("SET_POSITION", row);
        },
        DeletePosition({ commit }) {
            commit("SET_POSITION", "", "");
        }
    }
};

export default position;
