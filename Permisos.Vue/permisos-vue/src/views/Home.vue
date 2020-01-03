<template>
  <div class="card">
    <header class="card-header">
      <p class="card-header-title">Solicitud the Permiso</p>
    </header>
    <div class="card-content">
      <div class="columns">
        <div class="column">
          <div class="field">
            <label class="label">Nombres</label>
            <div class="control">
              <input
                class="input"
                type="text"
                placeholder="Requerido"
                v-model="permiso.nombreEmpleado"
              />
            </div>
            <p class="help">El nombre es necesario para la solicitud</p>
          </div>
        </div>
        <div class="column">
          <div class="field">
            <label class="label">Apellidos</label>
            <div class="control">
              <input
                class="input"
                type="text"
                placeholder="Requerido"
                v-model="permiso.apellidosEmpleado"
              />
            </div>
            <p class="help">El apellido es necesario para la solicitud</p>
          </div>
        </div>
      </div>
      <div class="columns">
        <div class="column is-half">
          <div class="field">
            <label class="label">Tipo Permiso</label>
            <div class="control">
              <div class="select is-rounded is-fullwidth">
                <select class="is-fullwidth" v-model="permiso.tipoPermisoId">
                  <option value="0">Select dropdown</option>
                  <option
                    v-for="item in tipoPermisos"
                    :key="item.id"
                    :value="item.id"
                  >
                    {{ item.descripcion }}
                  </option>
                </select>
              </div>
            </div>
          </div>
        </div>
        <div class="column is-half">
          <div class="field">
            <label class="label">Fecha</label>
            <div class="control">
              <input class="input" type="date" v-model="permiso.fechaPermiso" />
            </div>
            <p class="help">This is a help text</p>
          </div>
        </div>
      </div>
      <div class="columns">
        <div class="column is-full">
          <button
            class="button is-primary is-fullwidth"
            :disabled="!formIsValid"
            @click="ok"
          >
            Solicitar Permiso
          </button>
        </div>
      </div>

      <div class="notification is-info" v-if="loading">
        <button class="delete"></button>
        Estamos procesando su solicitud, espere un momento por favor...
      </div>
    </div>
  </div>
</template>

<script>
import { format } from 'date-fns';
import { dataService } from '../shared';

const DATE_FORMAT = 'yyyy-MM-dd';

export default {
  name: 'home',
  components: {},
  async created() {
    this.tipoPermisos = await dataService.getTipoPermisos();
  },
  data() {
    return {
      tipoPermisos: [],
      permiso: {
        id: undefined,
        nombreEmpleado: undefined,
        apellidosEmpleado: undefined,
        tipoPermisoId: 0,
        fechaPermiso: format(new Date(), DATE_FORMAT)
      },
      loading: undefined
    };
  },
  methods: {
    nombreValidate() {
      var validate =
        this.permiso.nombreEmpleado && this.permiso.nombreEmpleado.length > 0;
      return validate;
    },
    apellidoValidate() {
      var validate =
        this.permiso.apellidosEmpleado &&
        this.permiso.apellidosEmpleado.length > 0;
      return validate;
    },
    tipoPermisoValidate() {
      var validate =
        this.permiso.tipoPermisoId && +this.permiso.tipoPermisoId > 0;
      return validate;
    },
    fechaValidate() {
      var validate =
        this.permiso.fechaPermiso && this.permiso.fechaPermiso.length > 0;
      return validate;
    },
    async ok() {
      this.loading = true;
      await dataService.addPermiso(this.permiso);
      this.loading = false;
      this.$router.push({ name: 'ver' });
    }
  },
  computed: {
    formIsValid() {
      return (
        this.nombreValidate() &&
        this.apellidoValidate() &&
        this.tipoPermisoValidate() &&
        this.fechaValidate()
      );
    }
  }
};
</script>
