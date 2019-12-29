import { Route } from '@angular/router';
import { SolicitarPermisoComponent } from './solicitar-permiso/solicitar-permiso.component';
import { VerPermisosComponent } from './ver-permisos/ver-permisos.component';
import { TipoPermisoResolver } from './resolvers/tipo-permiso.resolver';
import { PermisoResolver } from './resolvers/permiso.resolver';
import { NotFoundComponent } from './app/not-found/not-found.component';

export const appRoutes: Route[] = [
  {
    path: 'ver',
    component: VerPermisosComponent,
    pathMatch: 'full',
    resolve: { permisos: PermisoResolver }
  },
  {
    path: '',
    component: SolicitarPermisoComponent,
    pathMatch: 'full',
    resolve: { tipoPermisos: TipoPermisoResolver }
  },
  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '/404', pathMatch: 'full' }
];
