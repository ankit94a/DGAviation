import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from 'src/shared-library/component/header/header.component';
import { SidebarComponent } from 'src/shared-library/component/sidebar/sidebar.component';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';

@Component({
  selector: 'app-layout',
  imports: [SharedLibraryModule,HeaderComponent,SidebarComponent,RouterModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css',
})
export class LayoutComponent {
  sideBarOpen = true;
  isSideBarLoaded: boolean = false;
  isMinimized: boolean = false;

  constructor(){

  }
  changeClass() {
    this.isMinimized = !this.isMinimized;
  }

  isLoaded($event: any) {
    this.isSideBarLoaded = $event
  }
  sideBarToggler() {
    this.sideBarOpen = !this.sideBarOpen;
  }
}
