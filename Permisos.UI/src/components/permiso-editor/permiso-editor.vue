<template>
  <div class="card">
    <div class="columns">
      <div class="column">
        <div class="field">
          <label class="label">Nombres</label>
          <div class="control">
            <input
              class="input"
              type="text"
              placeholder="Requerido"
              name="nombreEmpleado"
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
              name="apellidosEmpleado"
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
              <select
                class="is-fullwidth"
                id="tipoPermisoId"
                v-model="permiso.tipoPermisoId"
              >
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
      <div class="column is-one-fifth">
        <div class="field">
          <label class="label">Fecha</label>
          <div class="control">
            <input
              class="input"
              type="date"
              name="fechaPermiso"
              v-model="permiso.fechaPermiso"
            />
          </div>
          <p class="help">This is a help text</p>
        </div>
      </div>
    </div>
    <div class="columns">
      <div class="column is-full">
        <button
          type="submit"
          class="button is-primary is-fullwidth"
          :disabled="!formIsValid"
          @click="ok"
        >
          Solicitar Permiso
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import moment from 'moment';
import * as shared from '../../shared';

export default {
  name: 'PermisoEditor',
  props: ['tipoPermisos', 'dateFormat'],
  data() {
    return {
      permiso: {
        id: undefined,
        nombreEmpleado: undefined,
        apellidosEmpleado: undefined,
        tipoPermisoId: 0,
        fechaPermiso: moment().format(this.dateFormat)
      }
    };
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
  },
  methods: {
    nombreValidate() {
      return shared.valueValidator.stringValidate(this.permiso.nombreEmpleado);
    },
    apellidoValidate() {
      return shared.valueValidator.stringValidate(
        this.permiso.apellidosEmpleado
      );
    },
    tipoPermisoValidate() {
      return shared.valueValidator.intValidate(this.permiso.tipoPermisoId);
    },
    fechaValidate() {
      var hasData = shared.valueValidator.stringValidate(
        this.permiso.fechaPermiso
      );
      var dt = moment(this.permiso.fechaPermiso, this.dateFormat, true);
      var isDate = dt.isValid();
      return hasData && isDate;
    },
    ok() {
      this.$emit('submit', this.permiso);
    }
  }
};
</script>
