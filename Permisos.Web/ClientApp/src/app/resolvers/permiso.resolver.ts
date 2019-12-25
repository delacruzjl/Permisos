import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { IPermiso } from '../services/permiso.interface';
import { PermisoService } from '../services/permiso.service';

@Injectable({ providedIn: 'root' })
export class PermisoResolver implements Resolve<IPermiso[]> {
  constructor(private permisoService: PermisoService) {}

  resolve(
    route: ActivatedRouteSnapshot
  ): Observable<IPermiso[]> | Promise<IPermiso[]> | IPermiso[] {
    return this.permisoService.getAll();
  }
}
