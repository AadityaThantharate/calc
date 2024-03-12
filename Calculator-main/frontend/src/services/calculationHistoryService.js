import axios from "axios";

const API_BASE_URL = "https://localhost:7031/api/calculationhistory";

export default {
  async createHistory(history) {
    const response = await axios.post(`${API_BASE_URL}/`, history);
    return response.data;
  },

  async getAllHistory() {
    try {
      const response = await axios.get(`${API_BASE_URL}/`);
      return response.data;
    } catch (error) {
      console.error('Error fetching history:', error);
      return [];
    }
  },
  async deleteHistoryById(id) {
    try {
      await axios.delete(`${API_BASE_URL}/${id}`);
    } catch (error) {
      console.error('Error deleting history:', error);
    }
  },
  async deleteAllHistory() {
    try {
      await axios.delete(`${API_BASE_URL}/`);
    } catch (error) {
      console.error('Error deleting all history:', error);
    }
  },
};
 