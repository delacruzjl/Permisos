import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SolicitarPermisoComponent } from './solicitar-permiso/solicitar-permiso.component';
import { appRoutes } from './app.routes';
import { VerPermisosComponent } from './ver-permisos/ver-permisos.component';
import { TipoPermisoService } from './services/tipo-permiso.service';
import { ApiService } from './services/api.service';
import { PermisoService } from './services/permiso.service';
import { TipoPermisoResolver } from './resolvers/tipo-permiso.resolver';
import { PermisoResolver } from './resolvers/permiso.resolver';
import { NotFoundComponent } from './app/not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SolicitarPermisoComponent,
    VerPermisosComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    TipoPermisoService,
    ApiService,
    PermisoService,
    TipoPermisoResolver,
    PermisoResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
