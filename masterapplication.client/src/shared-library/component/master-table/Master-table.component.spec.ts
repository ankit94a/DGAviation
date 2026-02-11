import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MasterTableComponent } from './Master-table.component';

describe('TableComponent', () => {
  let component: MasterTableComponent;
  let fixture: ComponentFixture<MasterTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MasterTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MasterTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
