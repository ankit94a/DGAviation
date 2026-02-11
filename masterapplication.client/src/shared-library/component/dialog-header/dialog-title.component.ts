import { Component, Input } from '@angular/core';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';

@Component({
  selector: 'app-dialog-header',
  imports: [SharedLibraryModule],
  templateUrl: './dialog-title.component.html',
  styleUrl: './dialog-title.component.css',
})
export class DialogTitleComponent {
  @Input() title;
}
