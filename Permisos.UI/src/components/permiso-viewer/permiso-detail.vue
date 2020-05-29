<template>
  <div class="card" :class="{ 'has-background-grey-light': deleting }">
    <header class="card-header">
      <p class="card-header-title">
        {{ item.nombreEmpleado }} {{ item.apellidosEmpleado }}
      </p>
      <a href="#" class="card-header-icon" aria-label="more options">
        <span class="icon">
          <i class="fas fa-file-medical-alt" aria-hidden="true"></i>
        </span>
      </a>
    </header>
    <div class="card-content">
      <div class="content">
        {{ item.tipoPermiso.descripcion }}

        <br />
        <span>{{ item.fechaPermiso | moment('YYYY-MM-DD') }}</span>
      </div>
    </div>
    <footer class="card-footer">
      <button class="card-footer-item" @click="remove()">
        Eliminar
      </button>
    </footer>
  </div>
</template>

<script>
export default {
  name: 'PermisoDetail',
  props: ['item'],
  data() {
    return {
      deleting: undefined
    };
  },
  methods: {
    remove() {
      this.deleting = true;
      this.$emit('remove-item', this.item);
    }
  }
};
</script>

<style scoped>
button {
  cursor: pointer;
}
</style>
