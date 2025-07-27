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