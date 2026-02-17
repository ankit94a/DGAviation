import { Component, inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatStepperIntl } from '@angular/material/stepper';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';

@Component({
  selector: 'app-registration-add',
  imports: [SharedLibraryModule],
  templateUrl: './registration-add.component.html',
  styleUrl: './registration-add.component.css',
  standalone: true
})
export class RegistrationAddComponent implements OnInit {
  private _formBuilder = inject(FormBuilder);
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  imagePreview: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;
  checked: boolean = true;

  constructor() {

  }
  ngOnInit(): void {
    this.initializeForm();
    this.secondFormGroup = this._formBuilder.group({
    lastThreeAAAS: this._formBuilder.array([
      this.createLastThreeGroup(),
      this.createLastThreeGroup(),
      this.createLastThreeGroup()
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

createLastThreeGroup(): FormGroup {
  return this._formBuilder.group({
    id: [0],
    personalInfoId: [0],
    type: ['', ],
    from: ['', ],
    to: ['', ],
    io: [''],
    ro: [''],
    sro: [''],
    createdby: [''],
    updatedby: [''],
    createdon: [''],
    updatedon: [''],
    isactive: [true],
    isdeleted: [false]
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

  private initializeForm(): void {
    this.firstFormGroup = this._formBuilder.group({
      isNewRegistration: [true],
      icNumber: ['', ],
      name: ['', ],
      reportingPeriod: [''],
      imgUrl: [''],
      unit: ['', ],
      comd: ['', ],
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
