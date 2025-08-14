import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { ShopModule } from './shop/shop.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from "ngx-spinner";
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { loaderInterceptor } from './core/interceptor/loader.interceptor';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    ToastrModule.forRoot()

  ],
 providers: [
  provideClientHydration(),
  provideHttpClient(
    withInterceptors([
      loaderInterceptor
    ])
  ),
  provideAnimationsAsync()
],
  bootstrap: [AppComponent]
})
export class AppModule { }
