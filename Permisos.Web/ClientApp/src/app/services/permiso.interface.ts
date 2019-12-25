import { ITipoPermiso } from './tipo-permiso.interface';

export interface IPermiso {
  id?: number;
  nombreEmpleado: string;
  apellidosEmpleado: string;
  tipoPermisoId?: number;
  tipoPermiso?: ITipoPermiso;
  fechaPermiso: Date;
  loading?: boolean;
}
