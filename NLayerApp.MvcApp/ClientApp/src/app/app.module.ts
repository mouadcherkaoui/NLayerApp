import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HttpClient } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { SpeakerProfileComponent } from './pages/speaker-profile/speaker-profile.component';
import { SpeakerIdentificationComponent } from './pages/speaker-identification/speaker-identification.component';
import { SpeakerVerificationComponent } from './pages/speaker-verification/speaker-verification.component';
import { TextToSpeechComponent } from './pages/text-to-speech/text-to-speech.component';
import { SpeechToTextComponent } from './pages/speech-to-text/speech-to-text.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    SpeakerProfileComponent,
    SpeakerIdentificationComponent,
    SpeakerVerificationComponent,
    TextToSpeechComponent,
    SpeechToTextComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'speaker-profile', component: SpeakerProfileComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
