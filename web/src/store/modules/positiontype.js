const positiontype = {
    state: {
        positionTypeId: "",
        positionTypeName: ""
    },

    mutations: {
        SET_POSITIONTYPE: (state, row) => {
            state.positionTypeId = row.positionTypeId;
            state.positionTypeName = row.positionTypeName;
        }
    },

    actions: {
        SetPositionType({ commit }, row) {
            commit("SET_POSITIONTYPE", row);
        },
        DeletePositionType({ commit }) {
            commit("SET_POSITIONTYPE", "", "");
        }
    }
};

export default positiontype;
