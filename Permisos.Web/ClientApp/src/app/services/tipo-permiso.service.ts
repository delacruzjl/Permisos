import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ITipoPermiso } from './tipo-permiso.interface';
import { ApiService } from './api.service';

@Injectable()
export class TipoPermisoService {
  constructor(private apiService: ApiService) {}

  getAll(): Observable<ITipoPermiso[]> {
    const url = `api/tipopermisos`;
    return this.apiService.get(url);
  }
}
