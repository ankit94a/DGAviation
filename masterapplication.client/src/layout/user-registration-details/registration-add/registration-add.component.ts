import { Component, inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatStepperIntl } from '@angular/material/stepper';
import { EnumBase } from 'src/shared-library/pipes/enum';
import { EnumListPipe } from 'src/shared-library/pipes/enum.pipe';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { ApiService } from 'src/shared-library/service/api.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration-add',
  imports: [SharedLibraryModule, NgSelectModule, EnumListPipe],
  templateUrl: './registration-add.component.html',
  styleUrl: './registration-add.component.css',
  standalone: true
})

export class RegistrationAddComponent extends EnumBase implements OnInit {
  private _formBuilder = inject(FormBuilder);
  private apiService = inject(ApiService);
  private toastr = inject(ToastrService);
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  thirdFormGroup!: FormGroup;
  imagePreview: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;
  checked: boolean = true;
  apiUrl: string = 'registration/personalinfo'
  apiUrl2: string = 'registration/SaveAAAAndAccident'
  constructor() {
    super();
  }

  savePersonalDetails() {
    if (this.firstFormGroup.invalid) {
      this.firstFormGroup.markAllAsTouched();
      this.toastr.error('Please fill all required fields', 'Validation Error');
      return;
    }

    const formData = new FormData();
    const formValue = this.firstFormGroup.value;
    Object.keys(formValue).forEach(key => {
      let value = formValue[key];
      if (value instanceof Date) {
        value = value.toISOString();
      }
      if (value === null || value === undefined) {
        value = '';
      }
      formData.append(key, value);
    });
    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }
    this.apiService.postWithHeader(this.apiUrl, formData).subscribe(res => {
      if (res) {
        this.toastr.success("Detail Successfully Saved", 'success');
      }
    })
  }

  saveStepSecond() {

  if (this.secondFormGroup.invalid) {
    this.secondFormGroup.markAllAsTouched();
    return;
  }

  const payload = {
    lastThreeAAAS: this.secondFormGroup.value.lastThreeAAAS.map(x => ({
      ...x,
      from: x.from ? new Date(x.from).toISOString() : null,
      to: x.to ? new Date(x.to).toISOString() : null
    })),
    accidentDetails: this.secondFormGroup.value.accidentDetails.map(x => ({
      ...x,
      date: x.date ? new Date(x.date).toISOString() : null
    }))
  };

   this.apiService.postWithHeader(this.apiUrl2, payload)
    .subscribe(res => {
      this.toastr.success("Detail Successfully Saved", "Success");
    });
}

  // saveStepSecond(){
  //   debugger
  //    if (this.secondFormGroup.invalid) {
  //     this.secondFormGroup.markAllAsTouched();
  //     this.toastr.error('Please fill all required fields', 'Validation Error');
  //     return;
  //    }
  // const formDataSecond = new FormData();
  // const formValue = this.secondFormGroup.value;

  //   Object.keys(this.secondFormGroup.value).forEach(key => {
  //     let value = formValue[key];

  // // Convert specific date fields
  // if (
  //    (key === 'From' ||
  //      key === 'To' ||
  //      key === 'dt') 
  //     && value
  //    ) {
  //   value = new Date(value).toISOString();
  // }

  // formDataSecond.append(key, value);
  //     formDataSecond.append(key, this.secondFormGroup.value[key]);
  //   });
  //    this.apiService.postWithHeader(this.apiUrl, formDataSecond).subscribe(res => {
  //     if (res) {
  //       this.toastr.success("Detail Successfully Saved", 'success');
  //     }
  //   })
  // }
  ngOnInit(): void {
    this.initializeForm();
    this.secondFormGroup = this._formBuilder.group({
      lastThreeAAAS: this._formBuilder.array([
        this.createLastThreeGroup(),
        this.createLastThreeGroup(),
        this.createLastThreeGroup()
      ]),
      accidentDetails: this._formBuilder.array([
        this.createAccidentDetail(),
        this.createAccidentDetail(),
        this.createAccidentDetail()
      ])
    });
    this.thirdFormGroup = this._formBuilder.group({
      advExecRptRaisedList: this._formBuilder.array([
        this.createAdvExecGroup()
      ]),
       foreignVisitList: this._formBuilder.array([
      this.createForeignVisitGroup(),
      this.createForeignVisitGroup(),
      this.createForeignVisitGroup()
    ]),

    dvDetailsList: this._formBuilder.array([
      this.createDvDetailsGroup(),
      this.createDvDetailsGroup(),
      this.createDvDetailsGroup()
    ])
    });
  }

  toggleICField() {
    const control = this.firstFormGroup.get('isNewRegistration');
    const currentValue = control?.value;
    control?.setValue(!currentValue);
  }

  get lastThreeAAAS(): FormArray {
    return this.secondFormGroup.get('lastThreeAAAS') as FormArray;
  }

  get accidentDetails(): FormArray {
    return this.secondFormGroup.get('accidentDetails') as FormArray;
  }

  get advExecRptRaisedList(): FormArray {
    return this.thirdFormGroup.get('advExecRptRaisedList') as FormArray;
  }
  get foreignVisitList(): FormArray {
  return this.thirdFormGroup.get('foreignVisitList') as FormArray;
}

get dvDetailsList(): FormArray {
  return this.thirdFormGroup.get('dvDetailsList') as FormArray;
}

  createLastThreeGroup(): FormGroup {
    return this._formBuilder.group({
      id: [0],
      personalInfoId: [0],
      type: ['',],
      from: ['',],
      to: ['',],
      io: [''],
      ro: [''],
      sro: ['']
    });
  }

  createAccidentDetail(): FormGroup {
    return this._formBuilder.group({
      id: [0],
      personalInfoId: [0],
      date: ['',],
      acNoAndType: ['',],
      unitOrLOC: ['',],
      blamworthy: [''],
      cause: [''],
      statusPunish: ['']
    });
  }
  createAdvExecGroup(): FormGroup {
    return this._formBuilder.group({
      personalInfoId: [0],
      dt: [null],
      unit: [''],
      io: [''],
      ro: [''],
      sro: [''],
      decisionByDG: ['']
    });
  }
  createForeignVisitGroup(): FormGroup {
  return this._formBuilder.group({
    appt: [''],
    fromDate: [''],
    toDate: [''],
    country: [''],
    remark: ['']
  });
}
createDvDetailsGroup(): FormGroup {
  return this._formBuilder.group({
    details: ['']
  });
}

  private initializeForm(): void {
    this.firstFormGroup = this._formBuilder.group({
      isNewRegistration: [true],
      icNumber: ['',],
      name: ['',],
      reportingPeriod: [''],
      imgUrl: [''],
      unit: ['',],
      comd: ['',],
      typeOfReport: [''],
      parentArm: [''],
      apptHeld: [''],
      dtOfCommission: [''],
      medCategory: [''],
      dtOfSeniority: [''],
      category: [''],
      awardOfWings: [''],
      instrCategory: [''],
      tosDate: [''],
      lastAppearedWithGebCeb: ['']
    });
  }

  onImageUpload(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const validTypes = ['image/jpeg', 'image/png', 'image/jpg'];
      if (!validTypes.includes(file.type)) {
        alert('Please upload only JPG or PNG images');
        return;
      }
      if (file.size > 5 * 1024 * 1024) {
        alert('File size should not exceed 5MB');
        return;
      }
      this.selectedFile = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result;
        this.firstFormGroup.patchValue({
          imgUrl: file.name
        });
      };
      reader.readAsDataURL(file);
    }
  }

  removeImage(): void {
    this.imagePreview = null;
    this.selectedFile = null;
    this.firstFormGroup.patchValue({
      imgUrl: ''
    });
  }

  displayedColumns: string[] = ['index', 'type', 'from', 'to', 'io', 'ro', 'sro'];
  displayedColumns1: string[] = ['index', 'date', 'acNoAndType', 'unitOrLOC', 'blamworthy', 'cause', 'statusPunish',];
  displayedColumns2: string[] = ['index', 'dt', 'unit', 'io', 'ro', 'sro', 'decisionByDG'];
  foreignColumns: string[] = ['index', 'appt', 'fromDate', 'toDate', 'country'];
dvColumns: string[] = ['index', 'details'];

}
