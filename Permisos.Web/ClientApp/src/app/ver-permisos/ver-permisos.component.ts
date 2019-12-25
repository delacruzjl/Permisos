import { Component, OnInit } from '@angular/core';
import { IPermiso } from '../services/permiso.interface';
import { ActivatedRoute } from '@angular/router';
import { PermisoService } from '../services/permiso.service';

@Component({
  templateUrl: './ver-permisos.component.html',
  styles: [
    `
      .col-md-3 {
        padding-bottom: 20px;
      }
    `
  ]
})
export class VerPermisosComponent implements OnInit {
  permisos: IPermiso[];

  constructor(
    private route: ActivatedRoute,
    private permisoService: PermisoService
  ) {}

  ngOnInit(): void {
    this.permisos = this.route.snapshot.data['permisos'];
  }

  deleteClick(permiso: IPermiso): void {
    permiso.loading = true;
    this.permisoService.delete(permiso.id).subscribe(
      () => {
        this.permisos = this.permisos.filter(p => p.id !== permiso.id);
      },
      reason => {
        console.log(reason);
      }
    );
  }
}
