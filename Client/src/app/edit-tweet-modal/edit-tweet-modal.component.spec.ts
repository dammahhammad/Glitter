import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTweetModalComponent } from './edit-tweet-modal.component';

describe('EditTweetModalComponent', () => {
  let component: EditTweetModalComponent;
  let fixture: ComponentFixture<EditTweetModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditTweetModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditTweetModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
