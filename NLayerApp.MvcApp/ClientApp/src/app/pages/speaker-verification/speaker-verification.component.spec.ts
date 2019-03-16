import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpeakerVerificationComponent } from './speaker-verification.component';

describe('SpeakerVerificationComponent', () => {
  let component: SpeakerVerificationComponent;
  let fixture: ComponentFixture<SpeakerVerificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpeakerVerificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpeakerVerificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
