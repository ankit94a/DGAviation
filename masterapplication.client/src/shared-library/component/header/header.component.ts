import { ChangeDetectorRef, Component, effect, ElementRef, EventEmitter, inject, OnInit, Output, signal, ViewChild } from '@angular/core';
import { SharedLibraryModule } from '../../shared-library.module';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { BISMatDialogService } from '../../service/master-mat-dialog.service';
import { Observable, of } from 'rxjs';
import { UserIdleService } from '../../service/user-idol.service';
import { ApiService } from '../../service/api.service';
import { FormControl } from '@angular/forms';
import { DashboardComponent } from 'src/layout/dashboard/dashboard.component';

@Component({
  selector: 'app-header',
  imports: [SharedLibraryModule, RouterModule, ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  wing$: Observable<string | null>;
  private userIdleService = inject(UserIdleService);
  timer$ = signal(15 * 60);
  @Output() toggleSideBarForMe: EventEmitter<any> = new EventEmitter();



  entityList = [];
  filteredOptions: Observable<any>;
  // subscription: Subscription;
  searchValue: string;
  pageNumber: number = 1;
  myControl = new FormControl();
  @ViewChild('searchInput') searchInput: ElementRef;
  searchDataFlag: boolean;
  scrolled = false;
  noMoreDataMessage
  private cdr = inject(ChangeDetectorRef);
  constructor(private authService: AuthService, private dialogService: BISMatDialogService, private apiService: ApiService, private router: Router) {
    // this.setupUserIdleTracking();
  }

  ngOnInit(): void {
    // this.myControl.valueChanges.subscribe(value => {
    //   if (!value) return;

    //   const cleaned = value.replace(/[^a-zA-Z0-9.\s]/g, '');

    //   if (value !== cleaned) {
    //     this.myControl.setValue(cleaned, { emitEvent: false });
    //   }
    // });
  }

  setupUserIdleTracking() {
    this.userIdleService.onUserActivity(() => {
      this.timer$.set(15 * 60);
    });


    effect(() => {
      const interval = setInterval(() => {
        const current = this.timer$();
        if (current > 0) {
          this.timer$.set(current - 1);
        } else {
          clearInterval(interval);
          this.onLoggedout();
        }
      }, 1000);
    });
  }


  toggleSideBar() {
    this.toggleSideBarForMe.emit();
    setTimeout(() => {
      window.dispatchEvent(
        new Event('resize')
      );
    }, 300);
  }
  removeWing() {
    this.authService.clearWingDetails();
  }

  onLoggedout() {
    this.apiService.postWithHeader('auth/logout', null).subscribe(res => {
      if (res) {
        this.authService.clear();
      }
    })

  }
  // Search Funtions

  resetSelectedOption() {

  }
  selectOption() {
    if (this.myControl.value && this.myControl.value.name) {
      this.searchInput.nativeElement.value = this.myControl.value.name;
    }
    this.dialogService.open(DashboardComponent, this.myControl.value, '70vw', '95vh')
  }

  trackByFn(index: number, option): any {
    return option.id;
  }
  displayFn(option: any): string {
    return option ? option.name : '';
  }

  getList(event, loadMore = false) {
    if (event.code === 'Enter') {
      this.selectOption();
      return;
    }
    if (loadMore) {
      this.pageNumber++;
    } else {
      this.searchValue = event.target.value;
      this.pageNumber = 1;
    }
    if (this.searchValue.length >= 3) {
      this.apiService.getWithHeaders('itemmaster/search/' + this.searchValue + "/" + this.pageNumber).subscribe((data) => {
          if (data && !loadMore) {
            this.entityList = data;
            this.searchDataFlag = false;
          } else {
            data.length === 0 ? this.searchDataFlag = true : this.entityList = [...this.entityList, ...data];
          }
          this.filteredOptions = of(this.entityList);
          this.cdr.detectChanges();
        });
    } else {
      this.filteredOptions = of([]);
    }
  }

  profile(){
    this.router.navigateByUrl('/profile')
  }

}
