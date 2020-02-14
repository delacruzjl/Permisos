<template>
  <div>
    <PageHeader title="Listado de permisos" />
    <PermisoList
      :list="permisos"
      @remove-item="remove"
      v-if="permisos && permisos.length > 0"
    />

    <AlertMessage
      :message="message"
      :messageType="hasErrors ? 'danger' : 'warning'"
      hide-delete="true"
      v-if="(doneLoading && (!permisos || permisos.length === 0)) || hasErrors"
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
      var response = await dataService
        .removePermiso(permiso.id)
        .catch(reason => new Error(reason));
      if (response !== true) {
        this.hasErrors = true;
        this.message = response.message;
        return;
      }

      this.permisos = this.permisos.filter(p => p.id !== permiso.id);
    }
  },
  data() {
    return {
      permisos: [],
      doneLoading: undefined,
      message: 'No hay permisos que mostrar en este momento',
      hasErrors: undefined
    };
  },
  async created() {
    const result = await dataService
      .getPermisos()
      .catch(reason => new Error(reason));
    this.doneLoading = true;

    if (result instanceof Error) {
      this.hasErrors = true;
      this.message = result.message;
      return;
    }

    this.permisos = result;
  }
};
</script>
