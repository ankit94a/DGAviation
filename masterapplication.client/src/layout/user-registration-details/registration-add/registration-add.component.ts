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
  imagePreview: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;
  checked: boolean = true;
  apiUrl: string = 'registration/personalinfo'
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

    Object.keys(this.firstFormGroup.value).forEach(key => {
      formData.append(key, this.firstFormGroup.value[key]);
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
  displayedColumns: string[] = [
    'index',
    'type',
    'from',
    'to',
    'io',
    'ro',
    'sro'
  ];
  displayedColumns1: string[] = [
    'index',
    'date',
    'acNoAndType',
    'unitOrLOC',
    'blamworthy',
    'cause',
    'statusPunish',
  ];
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
    // this.toggleICField();
  }
  onImageUpload(event: any): void {
    const file = event.target.files[0];
    if (file) {
      // Validate file type
      const validTypes = ['image/jpeg', 'image/png', 'image/jpg'];
      if (!validTypes.includes(file.type)) {
        alert('Please upload only JPG or PNG images');
        return;
      }

      // Validate file size (max 5MB)
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


}
