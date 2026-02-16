import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuthService } from '../service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService,private router: Router){}

  canActivate(): Observable<boolean> {
    if (this.authService.isAuthenticated) {
      return of(true);
    }
    return this.authService.validate().pipe(
      switchMap(isValid => {
        if (!isValid) {
          this.router.navigate(['/landing']);
          return of(false);
        }
        return of(true);
      })
    );
  }
}
