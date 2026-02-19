import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
@Component({
  selector: 'image-thumbnail-column',
  template: `
   <div>
     <img border="0" width="50" height="50" [src]="imgsrc" />
   </div>`,
  standalone:true,
})
export class ImageThumbnailComponent implements OnInit {
  @Input() value: string;
  imgsrc: SafeResourceUrl;
  constructor(private _sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    if (this.value != undefined && this.value.length > 0 && this.value !="" && this.value !=null ) {

      this.imgsrc = this._sanitizer.bypassSecurityTrustResourceUrl(this.value);
    }
    else {
      this.imgsrc = "dummy-user.jpg";
    }
  }

}
