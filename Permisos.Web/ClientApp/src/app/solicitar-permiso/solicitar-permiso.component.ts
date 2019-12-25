import { Component, OnInit } from '@angular/core';
import { ITipoPermiso } from '../services/tipo-permiso.interface';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { PermisoService } from '../services/permiso.service';
import { IPermiso } from '../services/permiso.interface';

@Component({
  templateUrl: './solicitar-permiso.component.html'
})
export class SolicitarPermisoComponent implements OnInit {
  tipoPermisos: ITipoPermiso[];
  solicitudForm: FormGroup;
  loading: boolean;

  constructor(
    private permisoService: PermisoService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.tipoPermisos = this.route.snapshot.data['tipoPermisos'];

    this.solicitudForm = this.formBuilder.group({
      nombreEmpleado: [
        '',
        [Validators.required, Validators.minLength(1), Validators.pattern(/\S/)]
      ],
      apellidosEmpleado: [
        '',
        [Validators.required, Validators.minLength(1), Validators.pattern(/\S/)]
      ],
      tipoPermisoId: ['', Validators.required],
      fechaPermiso: [
        new Date(),
        [Validators.required, Validators.minLength(1), Validators.pattern(/\S/)]
      ]
    });
  }

  get f() {
    return this.solicitudForm.controls;
  }

  onSubmit(formValues: any): void {
    const permiso: IPermiso = {
      nombreEmpleado: formValues.nombreEmpleado,
      apellidosEmpleado: formValues.apellidosEmpleado,
      fechaPermiso: formValues.fechaPermiso,
      tipoPermisoId: +formValues.tipoPermisoId
    };

    this.loading = true;
    this.permisoService.add(permiso).subscribe(
      () => {
        this.loading = undefined;
        this.router.navigate(['/ver']);
      },
      reason => {
        this.loading = undefined;
        console.log(reason);
      }
    );
  }
}
