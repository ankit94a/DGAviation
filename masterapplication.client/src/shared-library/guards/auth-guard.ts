import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { firstValueFrom } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    const isValid = await firstValueFrom(this.authService.checkIsValid());
    if (isValid.isAuthenticated) {
      return true; 
    } else {
      // this.router.navigate(['/landing'], { queryParams: { returnUrl: state.url } });
      // this.router.navigate(['https://iam4.army.mil/IAM/username.html?resid']);
      window.location.href = 'https://iam4.army.mil/';
      return false;
    }
  }
}
