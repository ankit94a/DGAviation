import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../service/api.service';
import { SharedLibraryModule } from '../../shared-library.module';

@Component({
  selector: 'lib-ecr-token',
  imports: [SharedLibraryModule],
  templateUrl: './ecr-token.component.html',
  styleUrls: ['./ecr-token.component.css'],
})
export class EcrTokenComponent {
  form: FormGroup;
  tokenInfo: boolean = false;
  subject: any;
  tokenErrorMessage: any = '';
  pinErrorMessage: any;
  validationResult: any = null;
  isOfficerGetToken: boolean = false;
  serialNumber: "";
  @Output() isVarified = new EventEmitter();
  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.form = this.fb.group({
      pin: ['', Validators.required],
    });
    this.getEcrTokenDetails();
  }

  verifySerialNumber() {
    const subj = String(this.subject);
    const serialMatch = subj.match(/SERIALNUMBER=([^,]+)/);
    let serialNumber = null;
    if (serialMatch) {
      serialNumber = serialMatch[1].trim();
    }
    this.apiService.postWithHeaderForToken('validatePersID2FA', { inputPersID: serialNumber }).subscribe({
      next: (res) => {
        if (res.ValidatePersID2FAResult) {
          this.isVarified.emit(true);
        } else {
          this.tokenErrorMessage = "Invalid Pin"
        }

      }
    })
  }
  getEcrTokenDetails() {
    this.apiService.getWithHeadersForToken('FetchUniqueTokenDetails').subscribe({
      next: (res) => {
        if (res[0].TokenValid) {
          this.subject = res[0].subject;
          this.tokenErrorMessage = '';
          this.verifySerialNumber();
          this.isOfficerGetToken = true;
        } else {
          this.tokenErrorMessage = res[0].Remarks
        }
      },
      error: (err) => {
        this.tokenErrorMessage = err.error;
      },
    });
  }

  // onSubmit() {
  //   if (this.form.invalid) return;
  //   // const pin = this.form.value.pin;

  // const pin = this.form.value.pin;
  //     if (pin=="12345678") {
  //         this.isOfficerGetToken = true;
  //         this.pinErrorMessage = null;
  //         // this.isVarified.emit(true);
  //       } else {
  //         this.pinErrorMessage = "Invalid PIN.";
  //       }
  //   this.apiService.postWithHeader('auth/validate-pin', { pin }).subscribe({
  //     next: (res) => {
 
  //       if (res.isValid) {
  //         this.isOfficerGetToken = true;
  //         this.pinErrorMessage = null;
  //         this.isVarified.emit(true);
  //       } else {
  //         this.pinErrorMessage = "Invalid PIN.";
  //       }
  //     },
  //     error: (err) => {
  //       this.pinErrorMessage = err.error;
  //     },
  //   });
  // }
}
