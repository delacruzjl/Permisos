<template>
    <div class="card">
        <header class="card-header">
            <h2 class="card-header-title">Solicitud the Permiso</h2>
        </header>
        <div class="card-content">
            <div class="columns">
                <div class="column">
                    <div class="field">
                        <label class="label">Nombres</label>
                        <div class="control">
                            <input class="input"
                                   type="text"
                                   placeholder="Requerido"
                                   name="nombreEmpleado"
                                   v-model="permiso.nombreEmpleado" />
                        </div>
                        <p class="help">El nombre es necesario para la solicitud</p>
                    </div>
                </div>
                <div class="column">
                    <div class="field">
                        <label class="label">Apellidos</label>
                        <div class="control">
                            <input class="input"
                                   type="text"
                                   placeholder="Requerido"
                                   name="apellidosEmpleado"
                                   v-model="permiso.apellidosEmpleado" />
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
                                <select class="is-fullwidth"
                                        id="tipoPermisoId"
                                        v-model="permiso.tipoPermisoId">
                                    <option value="0">Select dropdown</option>
                                    <option v-for="item in tipoPermisos"
                                            :key="item.id"
                                            :value="item.id">
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
                            <input class="input"
                                   type="date"
                                   name="fechaPermiso"
                                   v-model="permiso.fechaPermiso" />
                        </div>
                        <p class="help">This is a help text</p>
                    </div>
                </div>
            </div>
            <div class="columns">
                <div class="column is-full">
                    <button type="submit"
                            class="button is-primary is-fullwidth"
                            :disabled="!formIsValid"
                            @click="ok">
                        Solicitar Permiso
                    </button>
                </div>
            </div>

            <div class="notification is-info" v-if="loading">
                <button class="delete"></button>
                Estamos procesando su solicitud, espere un momento por favor...
            </div>
            <div class="notification is-danger" v-if="!loading && hasErrors">
                <button class="delete"></button>
                {{ message }}
            </div>
        </div>
    </div>
</template>

<script>
    /* eslint-disable no-console */

    import { dataService, valueValidator } from '../shared';
    import moment from 'moment';

    const DATE_FORMAT = 'YYYY-MM-DD';

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
                    fechaPermiso: moment().format(DATE_FORMAT)
                },
                loading: undefined,
                message: undefined,
                hasErrors: undefined
            };
        },
        methods: {
            nombreValidate() {
                return valueValidator.stringValidate(this.permiso.nombreEmpleado);
            },
            apellidoValidate() {
                return valueValidator.stringValidate(this.permiso.apellidosEmpleado);
            },
            tipoPermisoValidate() {
                return valueValidator.intValidate(this.permiso.tipoPermisoId);
            },
            fechaValidate() {
                var hasData = valueValidator.stringValidate(this.permiso.fechaPermiso);
                var dt = moment(this.permiso.fechaPermiso, DATE_FORMAT, true);
                var isDate = dt.isValid();
                return hasData && isDate;
            },
            async ok() {
                this.hasErrors = undefined;
                this.message = undefined;
                this.loading = true;
                try {
                    await dataService.addPermiso(this.permiso);
                    this.$router.push({ name: 'ver' });
                } catch (error) {
                    this.hasErrors = true;
                    this.message = error;
                }

                this.loading = false;
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
