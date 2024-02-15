import axios from 'axios'

const API_URL = 'http://localhost:5181/Catalog'

export default {
    async getAllItems() {
      return axios.get(API_URL);
    },
  
    async getItemById(id) {
      return axios.get(`${API_URL}/${id}`);
    },

    async createItem(gameItem) {
        return axios.get(`${API_URL}/Create`, gameItem);
      },
  
    // ... other user related API calls
  };