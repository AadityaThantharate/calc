<template>
  <div class="history-list">
    <h3>Calculation History</h3>
    <ul>
      <li v-for="history in calculationHistories" :key="history.id">
        {{ history.expression }} = {{ history.result }}
      </li>
    </ul>
    <add-history></add-history>
  </div>
</template>


<script>
import calculationHistoryService from '@/services/calculationHistoryService';
import AddHistory from '@/components/AddHistory.vue';

export default {
  name: 'HistoryList',
  components: {
    AddHistory,
  },
  data() {
    return {
      calculationHistories: [],
    };
  },
  async mounted() {
    try {
      this.calculationHistories = await calculationHistoryService.getAllHistory();
      console.log(this.calculationHistories)
    } catch (error) {
      console.error('Error fetching calculation histories:', error);
    }
  },
}; 
</script>


<style scoped>
h3 {
  margin: 40px 0 0;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  margin: 10px 0;
}
</style>
