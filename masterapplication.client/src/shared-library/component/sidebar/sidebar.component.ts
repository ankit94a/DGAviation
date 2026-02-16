import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SharedLibraryModule } from '../../shared-library.module';
import { AuthService } from '../../service/auth.service';
// import { FilterSidebarPipe } from '../pipes/filter-sidebar.pipe';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  imports: [SharedLibraryModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  public showMenu!: string;
  sideBarMenus: any;
  permissionList: any = [];
  @Output() sidenavClose = new EventEmitter();
  @Output() isloaded = new EventEmitter();
  roleType;
  expandedMenus: Set<string> = new Set();
  activeMenu: string = '';
  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {
    this.http.get<any[]>('/menu.json').subscribe(data => {
      this.sideBarMenus = data;
    });
  }

  async ngOnInit() {
    //  this.roleType = await firstValueFrom(this.authService.roleType$);
  }

  public onSidenavClose = () => {
    this.sidenavClose.emit();
  }

  addExpandClass(menuText: string) {
    this.showMenu = this.showMenu === menuText ? '' : menuText;
  }

  onMenuClick(menu: any, event: Event) {
    if (menu.children?.length) {
      event.preventDefault();
      event.stopPropagation();
      this.addExpandClass(menu.text);
    }
  }

  toggleMenu(menu): void {
    if (menu.children && menu.children.length > 0) {
      // Toggle submenu expansion
      if (this.expandedMenus.has(menu.text)) {
        this.expandedMenus.delete(menu.text);
      } else {
        // Close other expanded menus if needed (optional - for accordion style)
        // this.expandedMenus.clear();
        this.expandedMenus.add(menu.text);
      }
    } else {
      // Set active menu for leaf items
      this.activeMenu = menu.text;
      // Navigate if not already there
      if (menu.routerLink && !this.router.url.includes(menu.routerLink)) {
        this.router.navigate([menu.routerLink]);
      }
    }
  }

  setActiveMenu(menu): void {
    this.activeMenu = menu.text;
  }

  isMenuExpanded(menu): boolean {
    return this.expandedMenus.has(menu.text);
  }

  isMenuActive(menu): boolean {
    // Check if current URL matches menu or any of its children
    const currentUrl = this.router.url;

    // Check main menu link
    if (menu.routerLink && currentUrl.includes(menu.routerLink)) {
      return true;
    }

    // Check children links
    if (menu.children && menu.children.length > 0) {
      return menu.children.some(child =>
        child.routerLink && currentUrl.includes(child.routerLink)
      );
    }

    return this.activeMenu === menu.text;
  }

  checkActiveMenuOnInit(): void {
    const currentUrl = this.router.url;

    // Find which menu item is active based on current URL
    for (const menu of this.sideBarMenus) {
      if (menu.routerLink && currentUrl.includes(menu.routerLink)) {
        this.activeMenu = menu.text;
        return;
      }

      if (menu.children && menu.children.length > 0) {
        const activeChild = menu.children.find(child =>
          child.routerLink && currentUrl.includes(child.routerLink)
        );
        if (activeChild) {
          this.activeMenu = menu.text;
          // Expand the parent menu
          this.expandedMenus.add(menu.text);
          return;
        }
      }
    }
  }

}
