import { HttpInterceptorFn } from '@angular/common/http';
import { delay, finalize } from 'rxjs';
import { LoadingService } from '../services/loading.service';
import { inject } from '@angular/core';

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
  // Get the LoadingService instance (you'll need to provide it in the functional interceptor)
  const loadingService = inject(LoadingService);
  
  loadingService.loading();
  
  return next(req).pipe(
    delay(1000),
    finalize(() => {
      loadingService.hideloader();
    })
  );
};

// import { HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { delay, finalize, Observable } from 'rxjs';
// import { LoadingService } from '../services/loading.service';

 
 
 
// // export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
// //   return next(req);
// // };
// @Injectable()
// export class loaderInterceptor implements HttpInterceptor {
//   constructor(private _service: LoadingService) { }
//   intercept(req: HttpRequest<any>, next: HttpHandler):
 
//     Observable<HttpEvent<any>> {
//     this._service.loading()
//     return next.handle(req).pipe(
//       delay(1000),
//       finalize(() => {
//         this._service.hideloader();
//       })
//     );
 
//   }
// }