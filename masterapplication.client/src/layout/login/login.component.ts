import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CaptchaRequest } from 'src/shared-library/models/login.model';
import { ApiService } from 'src/shared-library/service/api.service';
import { AuthService } from 'src/shared-library/service/auth.service';
import { RSAService } from 'src/shared-library/service/rsa.service';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';

@Component({
  selector: 'app-login',
  imports: [SharedLibraryModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
loginform: FormGroup;
  showPassword = false;
  captchaCode:CaptchaRequest = new CaptchaRequest();
  errorMessage:string = '';
  lockImage:string = 'lock.png';
  constructor(private fb: FormBuilder,private rsaService:RSAService,private apiService:ApiService,private toastr:ToastrService,private authService:AuthService,private router: Router,private dialog : MatDialog){
    this.loginform = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', ],
      code:[''],
      token:['']
    });
     this.getCaptcha()
  }

  togglePasswordVisibility() {
    this.lockImage = this.lockImage == 'lock.png' ? 'unlock.png':'lock.png';
    this.showPassword = !this.showPassword;
  }
 getCaptcha() {
  this.apiService.getWithHeaders('auth/generate').subscribe({
    next: (res) => {

      this.captchaCode = res;
      this.loginform.patchValue({ token: res.token });
    },
    error: (err) => {
      this.errorMessage = err;
    }
  });
}

  doLogin() {
  if (this.loginform.invalid) {
    return;
  }

  this.apiService.getWithHeaders('auth/publickey').subscribe({
    next: (xml:any) => {
      try {

        const publicKeyPem = this.rsaService.xmlToPublicKeyPem(xml.key);
        const encryptedUsername = this.rsaService.encryptWithPublicKey(publicKeyPem, this.loginform.get('username')?.value);
        const encryptedPassword = this.rsaService.encryptWithPublicKey(publicKeyPem, this.loginform.get('password')?.value);

        const encryptedPayload = {
          username: encryptedUsername,
          password: encryptedPassword,
          code:this.loginform.get('code')?.value,
          token:this.loginform.get('token')?.value,
        };

        this.apiService.postWithHeader('auth/login', encryptedPayload).subscribe({
          next: (res: any) => {
            if (res) {
              this.authService.setRoleType()
              this.dialog.closeAll();
              this.router.navigate(['/wing']);
              this.errorMessage = '';
            }
            else {
              this.router.navigate(['/landing']);
            }
          },
          error: (err) => {
            this.errorMessage = err.error?.message || 'Login failed';
             this.toastr.error(this.errorMessage, "Login failed");
            this.router.navigate(['/landing']);
          }
        });

      } catch (e) {
        this.errorMessage = 'Encryption error';
        console.error(e);
        this.router.navigate(['/landing']);
      }
    },
    error: (err) => {
      this.errorMessage = err?.message || 'Failed to fetch public key';
      this.router.navigate(['/landing']);
    }
  });
}

  forgetPs(){
    this.apiService.postWithHeader('auth/forget-password',this.loginform.value).subscribe(res => {

      if (res) {
        this.toastr.success("Successfully Forget",'success')
      }
    })
  }
}
