import axios from 'axios'

const API_URL = 'http://localhost:5181/Catalog'

export default {
    async getAllItems() {
      console.log("---------- getAllItems is called!");
      return axios.get(API_URL);
    },
  
    async getItemById(id) {
      return axios.get(`${API_URL}/${id}`);
    },

    // async createItem(id) {
    //     return axios.get(`${API_URL}/${id}`);
    //   },
  
    // ... other user related API calls
  };