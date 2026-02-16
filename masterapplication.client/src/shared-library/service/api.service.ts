import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, EMPTY, map, Observable, throwError } from 'rxjs';
import { environment } from '../enviroments/environments.development';
import { AbstractControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router, private dailog: MatDialog) { }

  getWithHeaders(url: string): Observable<any> {
    return this.http.get(`${this.baseUrl}` + url, { withCredentials: true }).pipe(
      map((res: any) => { if (res) { return res; } }),
      catchError((error) => {
        return this.showError(error);
      })
    );
  }

  postWithHeaderToDownload(url: string, data: any,): Observable<Blob> {
    const options = { responseType: 'blob' as 'json', withCredentials: true };
    return this.http.post(`${this.baseUrl}${url}`, data, options).pipe(
      catchError((error: any) => {
        return this.showError(error);;
      })
    );
  }

  postWithHeader(url: string, Data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}` + url, Data, { withCredentials: true }).pipe(map(
      (res: any) => { if (res) { return res; } }),
      catchError((error: any) => { return this.showError(error); }))
  }

  auth(data: any) {
    return this.http.post<any>(`${this.baseUrl}` + 'auth/login', data).pipe(
      catchError(
        (error: any) => {
          return this.showError(error);
        })
    );
  }

  onLoggedout() {
    this.postWithHeader('auth/logout',null).subscribe(res => {
      // const url = `https://iam4.army.mil/IAM/singleAppLogout/?SAMLRequest=${res.isSuccesLoggout}`;
      // window.location.href = url;
      // if (res.isSuccesLoggout) {
      //   this.router.navigate(['/log-out'])
      // }
    })
  }

  public showError(error: any): Observable<any> {
    let message = 'An unknown error occurred';
    if (error.error.status === 455) {
      message =
        error.error?.message || error.error?.title || 'Unauthorized access';
      this.onLoggedout();
      return throwError(() => ({
        status: 455,
        error: {
          message
        }
      }));
    } else if (error.status == 403 || error.status == 401) {

      message =
        error.error?.message || error.error?.title || error.error.errorMessage;
      this.onLoggedout();
      return throwError(() => ({
        status: 403,
        error: {
          message
        }
      }));
    } else if (error.status == 400) {
      return throwError(() => error)
    }
    else if (error.error != null && (typeof error.error === 'object' || error.constructor == Object)) {
      this.toastr.error(error.error.title.toString(), "error");
      return throwError(() => error);
    }
    return EMPTY;
  }

  checkRequiredFieldsExceptFile(form, fileType): boolean {
    const controls = form.controls;
    for (const key in controls) {
      if (key === fileType) continue;
      const control = controls[key];
      const validator = control.validator ? control.validator({} as AbstractControl) : null;
      const hasRequired = validator && validator['required'];
      if (hasRequired && (control.invalid || control.value === '' || control.value === null)) {
        return false;
      }
    }
    return true;
  }

}
