const employee = {
    state: {
        partyId: "",
        partyName: "",
        partyType: ""
    },

    mutations: {
        SET_EMPLOYEE: (state, row) => {
            state.partyId = row.partyId;
            state.partyName = row.partyName;
            state.partyType = row.partyType;
        }
    },

    actions: {
        SetEmployee({ commit }, row) {
            commit("SET_EMPLOYEE", row);
        },
        DeleteEmployee({ commit }) {
            commit("SET_EMPLOYEE", "", "");
        }
    }
};

export default employee;
