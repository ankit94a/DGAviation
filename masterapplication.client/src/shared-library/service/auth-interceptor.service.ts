import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
 return next(req).pipe(
      catchError((err:any) =>{
        if(err.status == 101 || err.status == 400 || err.status== 500){
          window.location.href = "https://iam4.army.mil/IAM/User";
        }
        return throwError(() => err);
      }))
};

