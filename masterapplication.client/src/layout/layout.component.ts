import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from 'src/shared-library/component/header/header.component';
import { SidebarComponent } from 'src/shared-library/component/sidebar/sidebar.component';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';

@Component({
  selector: 'app-layout',
  imports: [SharedLibraryModule, HeaderComponent, SidebarComponent, RouterModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css',
})
export class LayoutComponent {
  sideBarOpen = true;
  isSideBarLoaded: boolean = false;
  isMinimized: boolean = false;
 alertMessage = 'DG Aviation Wing Command Interface — Real-time oversight of fleet operations, mission authorizations, and regulatory compliance. Operational insights and critical alerts are surfaced automatically for command review.';
 constructor() {
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
