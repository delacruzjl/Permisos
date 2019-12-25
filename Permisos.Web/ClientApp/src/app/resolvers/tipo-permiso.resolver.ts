import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ITipoPermiso } from '../services/tipo-permiso.interface';
import { TipoPermisoService } from '../services/tipo-permiso.service';

@Injectable({ providedIn: 'root' })
export class TipoPermisoResolver implements Resolve<ITipoPermiso[]> {
  constructor(private tipoPermisoService: TipoPermisoService) {}

  resolve(
    route: ActivatedRouteSnapshot
  ): Observable<ITipoPermiso[]> | Promise<ITipoPermiso[]> | ITipoPermiso[] {
    return this.tipoPermisoService.getAll();
  }
}
