import { Pipe, PipeTransform } from '@angular/core';
import { UtilService } from '../service/util.service';

@Pipe({
  name: 'enumList',
})
export class EnumListPipe implements PipeTransform {

  constructor(private utils: UtilService) {
  }
  transform(value: any, sortOrder: 'asc' | 'desc'): any {
    return this.utils.stringEnumToKeyValue(value,sortOrder);
  }
}
