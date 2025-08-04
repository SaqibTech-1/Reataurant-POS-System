import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { LayoutModule } from './layout/layout.module';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    AuthModule,
    LayoutModule,
    SharedModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-center', // 👈 Top center
      preventDuplicates: true,
      timeOut: 3000,
      toastClass: 'ngx-toastr modern-toast',
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
