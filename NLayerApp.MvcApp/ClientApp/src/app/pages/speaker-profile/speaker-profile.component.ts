import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import * as Azure from '@azure/storage-blob';

@Component({
  selector: 'app-speaker-profile',
  templateUrl: './speaker-profile.component.html',
  styleUrls: ['./speaker-profile.component.css']
})
export class SpeakerProfileComponent implements OnInit {
  stream: MediaStream;
  identificationProfileId = '';
  storageSASUri =
    `${'https://datengstore.blob.core.windows.net/'}
      ${'?sv=2018-03-28&ss=b&srt=co&sp=rwlac&se=2019-02-28T21:26:30Z'}
      ${'&st=2019-02-28T13:26:30Z&spr=https&sig=kz42n39Vc9ozi2SetezcOAYw8Mto9oKiShAiM5negIA%3D'}`;

  getEnrollProfileUrl =
    (profileId) => `https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles/${profileId}/enroll`;

  constructor(private $client: HttpClient) { }

  ngOnInit() {
  }

  createProfile() {
    this.$client.post('createProfileUrl', {}, { headers: (new HttpHeaders()).append('Content-Type', 'application/json') })
      .subscribe(re => {
        this.identificationProfileId = (<any>re).identificationProfileId;
      });
  }

  enrollProfile() {
    if (this.hasGetUserMedia()) {
      navigator.mediaDevices.getUserMedia({audio: true}).
      then((audioStream) => {this.stream = audioStream; });
    } else {
      alert('getUserMedia() is not supported by your browser');
    }
    this.$client.post(
      this.getEnrollProfileUrl(this.identificationProfileId),
      this.stream,
      {
        headers: new HttpHeaders()
          .append('Content-Type', 'multipart/form-data')
          .append('x-ms-blob-type', 'BlockBlob')
          .append('x-ms-blob-content-length',  '0')
          .append('x-ms-blob-sequence-number', '0')
      });
  }

  hasGetUserMedia() {
    return !!(navigator.mediaDevices &&
      navigator.mediaDevices.getUserMedia);
  }

  getUserMedia() {
    navigator.mediaDevices.getUserMedia({ audio: true })
      .then(stream => {
        const mediaRecorder = new msr.MediaRecorder(stream);
        mediaRecorder.start();
      });
  }

  onMediaSuccess(stream) {
    const mediaRecorder = new msr.MediaStreamRecorder(stream);
    mediaRecorder.mimeType = 'audio/wav'; // check this line for audio/wav
    mediaRecorder.ondataavailable = function (blob) {
        // POST/PUT "Blob" using FormData/XHR2
        const blobURL = URL.createObjectURL(blob);
        document.write('<a href="' + blobURL + '">' + blobURL + '</a>');
    };
    mediaRecorder.start(3000);
  }

  onMediaError(e) {
      console.error('media error', e);
  }
}
