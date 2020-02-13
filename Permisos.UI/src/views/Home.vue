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
        message="Estamos procesando su solicitud, espere un momento por favor..."
        messageType="info"
        v-if="loading"
      />
      <AlertMessage
        :message="message"
        messageType="danger"
        v-if="!loading && hasErrors"
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
      message: undefined,
      hasErrors: undefined
    };
  },
  methods: {
    async ok(permiso) {
      this.hasErrors = undefined;
      this.message = undefined;
      this.loading = true;
      try {
        await dataService.addPermiso(permiso);
        this.$router.push({ name: 'ver' });
      } catch (error) {
        this.hasErrors = true;
        this.message = error;
      }

      this.loading = false;
    }
  }
};
</script>
