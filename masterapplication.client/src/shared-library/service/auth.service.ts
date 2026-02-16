import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, firstValueFrom, map, Observable, tap } from 'rxjs';
import { ApiService } from './api.service';
import { LoginModel } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private wingSubject = new BehaviorSubject<string | null>(null);
  private roleTypeSubject = new BehaviorSubject<string | null>(null);
  roleType$ = this.roleTypeSubject.asObservable();

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();
  constructor(private router: Router, private apiService: ApiService) {
    const storedWing = sessionStorage.getItem('wing');
    this.wingSubject = new BehaviorSubject<string | null>(storedWing);
  }

  setAuthenticated(value: boolean) {
    this.isAuthenticatedSubject.next(value);
  }
  get isAuthenticated(): boolean {
    return this.isAuthenticatedSubject.value;
  }
  validate(): Observable<boolean> {
    return this.apiService.getWithHeaders('auth/isAuthenticated').pipe(
      map((res: any) => res.isAuthenticated === true),
      tap(val => this.setAuthenticated(val))
    );
  }

  checkIsValid() {
    return this.apiService.postWithHeader('status/isvalid', null)
  }
  clear() {
    sessionStorage.clear();
    this.navigateToLogin(this.router.routerState.snapshot.url);

  }
  public navigateToLogin(stateUrl) {
    this.router.navigate(['/login'], { queryParams: { 1: { returnUrl: stateUrl } } });
  }
  public setWingDetails(wing: { id: string; name: string }) {
    sessionStorage.setItem('wingId', wing.id);
    sessionStorage.setItem('wing', wing.name);

    this.wingSubject.next(wing.name);
  }

  setRoleType(roleType) {
    this.roleTypeSubject.next(roleType);
  }
  async getRoleType(): Promise<void> {
    try {
      const user = new LoginModel();
      const res: any = await firstValueFrom(this.apiService.postWithHeader('status/currentuser', user));
      if (res.role == "Admin") {
        this.roleTypeSubject.next('1');
      }
      //  else if(res.role == "User") {
      //   this.roleTypeSubject.next('0');
      // }
      else {
        this.roleTypeSubject.next('0');
      }

    } catch (err) {
      this.roleTypeSubject.next('0');
    }
  }

  get currentRoleType(): string | null {
    return this.roleTypeSubject.value;
  }
  public get wing$(): Observable<string | null> {
    return this.wingSubject.asObservable();
  }

  public getWingName(): Observable<string | null> {
    return this.wing$;
  }

  public getWingId() {
    return sessionStorage.getItem("wingId")
  }
  public clearWingDetails() {
    sessionStorage.removeItem('wingId');
    sessionStorage.removeItem('wing');
    this.wingSubject.next(null);
  }

}
