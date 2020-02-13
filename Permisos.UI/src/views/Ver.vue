<template>
  <div>
    <PageHeader title="Listado de permisos" />
    <div class="columns is-multiline" v-if="permisos && permisos.length > 0">
      <div
        class="column is-one-quarter"
        v-for="permiso in permisos"
        :key="permiso.id"
      >
        <div
          class="card"
          :class="{ 'has-background-grey-light': permiso.loading }">
          <header class="card-header">
            <p class="card-header-title">
              {{ permiso.nombreEmpleado }} {{ permiso.apellidosEmpleado }}
            </p>
            <a href="#" class="card-header-icon" aria-label="more options">
              <span class="icon">
                <i class="fas fa-angle-down" aria-hidden="true"></i>
              </span>
            </a>
          </header>
          <div class="card-content">
            <div class="content">
              {{ permiso.tipoPermiso.descripcion }}

              <br />
              <span>{{ permiso.fechaPermiso }}</span>
            </div>
          </div>
          <footer class="card-footer">
            <button class="card-footer-item" @click="remove(permiso)">
              Eliminar
            </button>
          </footer>
        </div>
      </div>
    </div>

    <AlertMessage
      message="No hay permisos que mostrar en este momento"
      messageType="warning"
      hide-delete="true"
      v-if="!permisos || permisos.length === 0"
    />
  </div>
</template>

<script>
import { dataService } from '../shared';
import PageHeader from '@/components/common/page-header';
import AlertMessage from '@/components/common/alert-message';

export default {
  components: {
    PageHeader,
    AlertMessage
  },
  methods: {
    async remove(permiso) {
      this.permisos = this.permisos.map(p => {
        if (p.id === permiso.id) {
          p.loading = true;
        }

        return p;
      });

      var response = await dataService.removePermiso(permiso.id);
      if (response === true) {
        this.permisos = this.permisos.filter(p => p.id !== permiso.id);
      }
    }
    //
  },
  data() {
    return {
      permisos: undefined
    };
  },
  async created() {
    this.permisos = await dataService.getPermisos();
  }
};
</script>
