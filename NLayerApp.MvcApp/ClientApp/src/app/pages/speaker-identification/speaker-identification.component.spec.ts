import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpeakerIdentificationComponent } from './speaker-identification.component';

describe('SpeakerIdentificationComponent', () => {
  let component: SpeakerIdentificationComponent;
  let fixture: ComponentFixture<SpeakerIdentificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpeakerIdentificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpeakerIdentificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
