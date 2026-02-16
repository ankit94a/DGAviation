import { Component, ElementRef, ViewChild } from '@angular/core';
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
  standalone:true
})
export class LoginComponent {
loginform: FormGroup;
  showPassword = false;
  captchaCode:CaptchaRequest = new CaptchaRequest();
  errorMessage:string = '';
  lockImage:string = 'lock.png';
    @ViewChild('captchaCanvas', { static: true })
captchaCanvas!: ElementRef<HTMLCanvasElement>;
  constructor(private fb: FormBuilder,private rsaService:RSAService,private apiService:ApiService,private toastr:ToastrService,private authService:AuthService,private router: Router,private dialog : MatDialog){
    this.loginform = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', ],
      code:[''],
      token:['']
    });
     this.getCaptcha()
  }


ngAfterViewInit() {
  this.drawCaptcha(this.captchaCode.code);
}

drawCaptcha(text: string) {
  const canvas = this.captchaCanvas.nativeElement;
  const ctx = canvas.getContext('2d')!;

  ctx.clearRect(0, 0, canvas.width, canvas.height);

  // background
  ctx.fillStyle = '#2f3b45';
  ctx.fillRect(0, 0, canvas.width, canvas.height);

  // noise lines
  for (let i = 0; i < 5; i++) {
    ctx.strokeStyle = '#555';
    ctx.beginPath();
    ctx.moveTo(Math.random() * canvas.width, Math.random() * canvas.height);
    ctx.lineTo(Math.random() * canvas.width, Math.random() * canvas.height);
    ctx.stroke();
  }

  // text
  ctx.font = '22px Arial';
  ctx.fillStyle = '#ffd24c';
  ctx.setTransform(1, 0.1, -0.1, 1, 0, 0);
  ctx.fillText(text, 25, 30);
}

  togglePasswordVisibility() {
    this.lockImage = this.lockImage == 'lock.png' ? 'unlock.png':'lock.png';
    this.showPassword = !this.showPassword;
  }
 getCaptcha() {
  this.apiService.getWithHeaders('auth/generate').subscribe({
    next: (res) => {
      this.captchaCode = res;
      this.drawCaptcha(this.captchaCode.code);
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

        this.apiService.auth(encryptedPayload).subscribe({
          next: (res: any) => {
            if (res) {
              // this.authService.setRoleType()
              this.dialog.closeAll();
              this.router.navigate(['/dashboard']);
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
