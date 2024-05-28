const organization = {
    state: {
        partyId: "",
        partyName: "",
        partyType: ""
    },

    mutations: {
        SET_ORGANIZATION: (state, row) => {
            state.partyId = row.partyId;
            state.partyName = row.partyName;
            state.partyType = row.partyType;
        }
    },

    actions: {
        SetOrganization({ commit }, row) {
            commit("SET_ORGANIZATION", row);
        },
        DeleteOrganization({ commit }) {
            commit("SET_ORGANIZATION", "", "");
        }
    }
};

export default organization;
