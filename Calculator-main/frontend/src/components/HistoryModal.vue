<template>
  <p-dialog :visible="showModal" :modal="true" :style="{ width: '30vw' }" @hide="closeDialog"
    @update:visible="$emit('update:showModal', $event)">
    <template #header>
      <h2>History</h2>
    </template>
    <table class="table">
      <thead>
        <tr>
          <th>Exp</th>
          <th>Result</th>
          <th>Delete</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(record, index) in historyRecords" :key="index">
          <td>{{ record.expression }}</td>
          <td>{{ record.result }}</td>
          <td>
            <i class="pi pi-trash delete-history" @click="deleteHistoryRecord(record.id)"></i>
          </td>
        </tr>
      </tbody>
    </table>
    <template #footer>
      <p-button @click="closeDialog" label="Close"></p-button>
    </template>
  </p-dialog>
</template>

<script>
import Button from 'primevue/button';
import Dialog from 'primevue/dialog';
import calculationHistoryService from '../services/calculationHistoryService';

export default {
  name: 'HistoryDialog',
  components: {
    'p-button': Button,
    'p-dialog': Dialog,
  },
  props: {
    showModal: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      historyRecords: [],
    };
  },
  watch: {
    showModal: {
      handler: async function (newVal) {
        if (newVal) {
          await this.fetchHistory();
        }
      },
      immediate: true,
    },
  },
  methods: {
    closeDialog() {
      this.$emit('update:showModal', false);
    },
    async deleteHistoryRecord(id) {
      await calculationHistoryService.deleteHistoryById(id);
      this.historyRecords = await calculationHistoryService.getAllHistory();
    },
    async fetchHistory() {
      this.historyRecords = await calculationHistoryService.getAllHistory();
    },
  },
};
</script>

<style scoped>
.table {
  font-size: 0.9rem;
  margin: auto;
  text-align: center;
}

.table thead {
  background-color: #f5f5f5;
}

.table tbody tr:hover {
  background-color: #f5f5f5;
}

.delete-history {
  cursor: pointer;
  margin-left: 8px;
  color: rgb(179, 179, 179);
}

.delete-history:hover {
  color: #2e2e2e;
}

th {
  width: 4rem;
}
</style>
