import { Component } from '@angular/core';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { firstValueFrom } from 'rxjs';
import { SharedLibraryModule } from '../../../shared-library/shared-library.module';
import { MasterTableComponent } from '../../../shared-library/component/master-table/Master-table.component';
import { TablePaginationSettingsConfig } from '../../../shared-library/component/master-table/table-settings.model';
import { ApiService } from '../../../shared-library/service/api.service';
import { AuthService } from '../../../shared-library/service/auth.service';
import { PersonalInfo } from '../../../shared-library/models/personalinfo.model';



@Component({
    selector: 'app-registration-list',
    imports: [SharedLibraryModule, MasterTableComponent, NgxSpinnerModule],
    templateUrl: './registration-list.component.html',
    styleUrl: './registration-list.component.css'
})

export class RegistrationListComponent extends TablePaginationSettingsConfig {
  personalInfo: PersonalInfo[] = [];
  constructor(private spinner: NgxSpinnerService,private toastr:ToastrService,private apiService: ApiService,private authService: AuthService) {
    super();
    this.tablePaginationSettings.enableView=true;
    this.tablePaginationSettings.enableColumn=true;
    this.tablePaginationSettings.enableAction=true;
    this.tablePaginationSettings.enableColumn = true;
    this.tablePaginationSettings.pageSizeOptions = [50, 100];
    this.tablePaginationSettings.showFirstLastButtons = false;
    this.tablePaginationSettings.enableEdit = true;
    this.tablePaginationSettings.enableDelete = true;
    
    
  }
 ngOnInit(){

    this.getList();
  }
  getList() {
    this.spinner.show();
    this.apiService.getWithHeaders('registration/personalInfo').subscribe(res => {
        if (res) {
          this.personalInfo = res;
          this.spinner.hide();
        }
    });
  }
view(row: PersonalInfo) {
  console.log('View:', row);
}

edit(row: PersonalInfo) {
  console.log('Edit:', row);
}

del(row: PersonalInfo) {
  console.log('Delete:', row);
}



  columns = [

    {
      name: 'name',
      displayName: 'Full Name',
      isSearchable: true,
      hide: false,
      type: 'text',
    },
    {
      name: 'unit',
      displayName: 'Unit',
      isSearchable: true,
      hide: false,
      type: 'text',
    },
    {
      name: 'parentArm',
      displayName: 'Parent Arm',
      isSearchable: true,
      hide: false,
      type: 'text',
    },
    {
      name: 'dtOfCommission',
      displayName: 'Dt Of Commission',
      isSearchable: true,
      hide: false,
      type: 'text',
    },
    {
      name: 'dtOfSeniority',
      displayName: 'Dt Of Seniority',
      isSearchable: true,
      hide: false,
      type: 'text',
    },
    {
      name: 'awardOfWings',
      displayName: 'Award Of Wings',
      isSearchable: true,
      hide: false,
      type: 'text',
    },
    {
      name: 'apptHeld',
      displayName: 'Appt Held',
      isSearchable: true,
      hide: true,
      type: 'text',
    },
    {
      name: 'medCategory',
      displayName: 'Med Cat',
      isSearchable: true,
      hide: true,
      type: 'text',
    },
    {
      name: 'instrCategory',
      displayName: 'Instr Cat',
      isSearchable: true,
      hide: true,
      type: 'text',
    },
  ];
}
