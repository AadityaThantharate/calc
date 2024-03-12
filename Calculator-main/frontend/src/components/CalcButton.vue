<template>
  <button class="calc-button">{{ label }}</button>
</template>
  
<script>
export default {
  name: 'CalcButton',
  props: {
    label: {
      type: String,
      required: true,
    },
  },
};
</script>
  
<style scoped>
button {
  padding: 10px;
  margin: 5px;
  border: none;
  border-radius: 5px;
  background-color: #f2f2f2;
  color: #333;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background-color: #ddd;
}
</style>
  