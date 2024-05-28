const customer = {
    state: {
        partyId: "",
        partyName: "",
        partyType: ""
    },

    mutations: {
        SET_CUSTOMER: (state, row) => {
            state.partyId = row.partyId;
            state.partyName = row.partyName;
            state.partyType = row.partyType;
        }
    },

    actions: {
        SetCustomer({ commit }, row) {
            commit("SET_CUSTOMER", row);
        },
        DeleteCustomer({ commit }) {
            commit("SET_CUSTOMER", "", "");
        }
    }
};

export default customer;
