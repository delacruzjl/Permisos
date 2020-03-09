<template>
  <div class="columns is-multiline">
    <div
      class="column is-one-quarter"
      v-for="permiso in list"
      :key="permiso.id"
    >
      <PermisoDetail :item="permiso" @remove-item="removeItem" />
    </div>
  </div>
</template>

<script>
import PermisoDetail from './permiso-detail';

export default {
  name: 'PermisoList',
  props: { list: Array },
  components: {
    PermisoDetail
  },
  methods: {
    removeItem(permiso) {
      this.$emit('remove-item', permiso);
    }
    //
  }
};
</script>
