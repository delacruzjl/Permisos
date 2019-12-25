import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { IPermiso } from './permiso.interface';

@Injectable()
export class PermisoService {
  private url = 'api/permisos';

  constructor(private apiService: ApiService) {}

  public add(permiso: IPermiso): Observable<IPermiso> {
    return this.apiService.post(this.url, permiso);
  }

  getAll(): Observable<IPermiso[]> {
    return this.apiService.get(this.url);
  }

  delete(id: number): Observable<void> {
    return this.apiService.delete(`${this.url}/${id}`);
  }
}
