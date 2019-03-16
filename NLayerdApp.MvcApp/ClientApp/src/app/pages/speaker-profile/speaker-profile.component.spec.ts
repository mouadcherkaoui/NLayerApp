import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpeakerProfileComponent } from './speaker-profile.component';

describe('SpeakerProfileComponent', () => {
  let component: SpeakerProfileComponent;
  let fixture: ComponentFixture<SpeakerProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpeakerProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpeakerProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
