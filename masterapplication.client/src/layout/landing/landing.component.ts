import {  Component, inject } from '@angular/core';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';
import { LoginComponent } from '../login/login.component';
import { BISMatDialogService } from 'src/shared-library/service/master-mat-dialog.service';
import { NgOptimizedImage } from "@angular/common";

@Component({
  selector: 'app-landing',
  imports: [SharedLibraryModule, NgOptimizedImage],
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
