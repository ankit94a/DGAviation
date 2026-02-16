import { Component, OnInit } from '@angular/core';
import { RouterModule } from "@angular/router";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css','../bootstrap.css'],
    imports: [ RouterModule],
    standalone: true
})
export class AppComponent implements OnInit  {

  title = 'DG Aviation';
  constructor() {

  }

  ngOnInit() {
  }
  toggleTheme() {
  document.documentElement.classList.toggle('dark');
}


}
