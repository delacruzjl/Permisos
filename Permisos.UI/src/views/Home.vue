<template>
  <div>
    <PageHeader title="Solicitud the Permiso" />
    <div class="card-content">
      <PermisoEditor
        :tipo-permisos="tipoPermisos"
        :date-format="dateFormat"
        @submit="ok"
      />

      <AlertMessage
        :message="message"
        :messageType="hasErrors ? 'danger' : 'info'"
        v-if="loading || (!loading && hasErrors)"
      />
    </div>
  </div>
</template>

<script>
/* eslint-disable no-console */

import { dataService } from '../shared';
import PageHeader from '@/components/common/page-header';
import PermisoEditor from '@/components/permiso-editor/permiso-editor';
import AlertMessage from '@/components/common/alert-message';

const DATE_FORMAT = 'YYYY-MM-DD';

export default {
  name: 'home',
  components: {
    PageHeader,
    PermisoEditor,
    AlertMessage
  },
  async created() {
    this.tipoPermisos = await dataService.getTipoPermisos();
  },
  data() {
    return {
      dateFormat: DATE_FORMAT,
      tipoPermisos: [],
      loading: undefined,
      message:
        'Estamos procesando su solicitud, espere un momento por favor...',
      hasErrors: undefined
    };
  },
  methods: {
    async ok(permiso) {
      this.hasErrors = undefined;
      this.message = undefined;
      this.loading = true;
      const result = await dataService
        .addPermiso(permiso)
        .catch(reason => new Error(reason));
      this.loading = false;
      if (result instanceof Error) {
        this.hasErrors = true;
        this.message = result.message;
        return;
      }

      this.$router.push({ name: 'ver' });
    }
  }
};
</script>
