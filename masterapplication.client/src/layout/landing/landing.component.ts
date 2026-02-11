import {  Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CaptchaRequest } from 'src/shared-library/models/login.model';
import { ApiService } from 'src/shared-library/service/api.service';
import { AuthService } from 'src/shared-library/service/auth.service';
import { RSAService } from 'src/shared-library/service/rsa.service';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';
import { LoginComponent } from '../login/login.component';
import { BISMatDialogService } from 'src/shared-library/service/master-mat-dialog.service';

@Component({
  selector: 'app-landing',
  imports: [SharedLibraryModule],
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css',
})
export class LandingComponent {
  private dialogService = inject(BISMatDialogService)
  constructor(){

  }

  openDialog(){
    this.dialogService.open(LoginComponent, null,'20vw')
  }
}
