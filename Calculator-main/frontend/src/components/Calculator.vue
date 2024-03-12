<template>
  <div class="calculator">
    <input type="text" class="calculator-input" v-model="expression" readonly>
    <div class="calculator-buttons">
      <CalcButton v-for="button in buttons" :key="button.label" :label="button.label"
        @click="handleButtonClick(button.action)"></CalcButton>
      <CalcButton label="History" @click="handleButtonClick('History')" class="double-width"></CalcButton>
      <CalcButton label="Clear History" @click="handleButtonClick('Clear History')" class="double-width"></CalcButton>
    </div>
    <HistoryModal :showModal="showModal" @update:showModal="showModal = $event" />
  </div>
</template>

  
<script>
import CalcButton from './CalcButton.vue';
import HistoryModal from './HistoryModal.vue';
import calculationHistoryService from '../services/calculationHistoryService';
import * as math from 'mathjs';
import Button from 'primevue/button';
import Dialog from 'primevue/dialog';

export default {
  name: 'CalculatorButtons',
  components: {
    CalcButton,
    HistoryModal,
    'p-button': Button,
    'p-dialog': Dialog,
  },
  data() {
    return {
      expression: '',
      showModal: false,
      buttons: [
        { label: '7', action: '7' },
        { label: '8', action: '8' },
        { label: '9', action: '9' },
        { label: '+', action: '+' },
        { label: '4', action: '4' },
        { label: '5', action: '5' },
        { label: '6', action: '6' },
        { label: '-', action: '-' },
        { label: '1', action: '1' },
        { label: '2', action: '2' },
        { label: '3', action: '3' },
        { label: '*', action: '*' },
        { label: '0', action: '0' },
        { label: 'C', action: 'C' },
        { label: '=', action: '=' },
        { label: '/', action: '/' },
      ],
    };
  },
  methods: {
    async handleEquals() {
      try {
        const result = this.evaluateExpression(this.expression);
        if (result !== null) {
          const history = {
            expression: this.expression,
            result: result,
          };
          this.expression = result.toString();

          await calculationHistoryService.createHistory(history);
        } else {
          console.error('Invalid expression');
        }
      } catch (error) {
        console.error('Error performing calculation:', error);
      }
    },
    handleClear() {
      this.expression = '';
    },
    async handleHistory() {
      this.showModal = true;
    },
    async handleClearHistory() {
      await calculationHistoryService.deleteAllHistory();
    },
    isOperator(char) {
      return ['+', '-', '*', '/'].includes(char);
    },
    isLastCharOperator() {
      const lastChar = this.expression.slice(-1);
      return this.isOperator(lastChar);
    },
    handleInput(value) {
      if (this.isOperator(value)) {
        const isDisallowedFirstChar = this.expression.length === 0 && (value === '*' || value === '/');
        if (isDisallowedFirstChar || (this.isLastCharOperator() && (this.expression.length === 1 && (value === '*' || value === '/')))) {
          return;
        }

        if (this.isLastCharOperator()) {
          this.expression = this.expression.slice(0, -1);
        }
      }
      this.expression += value;
    },
    async handleButtonClick(value) {
      switch (value) {
        case '=':
          await this.handleEquals();
          break;
        case 'C':
          this.handleClear();
          break;
        case 'History':
          await this.handleHistory();
          break;
        case 'Clear History':
          await this.handleClearHistory();
          break;
        default:
          this.handleInput(value);
          break;
      }
    },
    evaluateExpression(expression) {
      try {
        return math.evaluate(expression);
      } catch (error) {
        console.error('Error evaluating expression:', error);
        return null;
      }
    },
  },
};
</script>
<style scoped>
.calculator {
  max-width: 300px;
  margin: 0 auto;
  text-align: center;
}

.calculator-input {
  padding: 10px;
  margin-bottom: 10px;
  font-size: 24px;
  text-align: right;
  border: none;
  background-color: #f2f2f2;
}

.calculator-buttons {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  grid-gap: 10px;
}

.double-width {
  grid-column-end: span 2;
}

.table {
  font-size: 0.9rem;
  margin: auto;
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