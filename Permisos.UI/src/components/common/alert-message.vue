<template>
  <div class="notification" :class="'is-' + messageType" >
    <button class="delete" v-if="!hideDelete"></button>
    {{ message }}
  </div>
</template>

<script>
export default {
  name: 'AlertMessage',
  props: ['message', 'messageType', 'hideDelete']
};
</script>
