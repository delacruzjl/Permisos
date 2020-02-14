<template>
  <div>
    <PageHeader title="Listado de permisos" />
    <PermisoList
      :list="permisos"
      @remove-item="remove"
      v-if="permisos && permisos.length > 0"
    />

    <AlertMessage
      message="No hay permisos que mostrar en este momento"
      messageType="warning"
      hide-delete="true"
      v-if="doneLoading && (!permisos || permisos.length === 0)"
    />
  </div>
</template>

<script>
import { dataService } from '../shared';
import PageHeader from '@/components/common/page-header';
import AlertMessage from '@/components/common/alert-message';
import PermisoList from '@/components/permiso-viewer/permiso-list';

export default {
  components: {
    PageHeader,
    AlertMessage,
    PermisoList
  },
  methods: {
    async remove(permiso) {
      var response = await dataService.removePermiso(permiso.id);
      if (response === true) {
        this.permisos = this.permisos.filter(p => p.id !== permiso.id);
      }
    }
  },
  data() {
    return {
      permisos: undefined,
      doneLoading: undefined
    };
  },
  async created() {
    this.permisos = await dataService.getPermisos();
    this.doneLoading = true;
  }
};
</script>
