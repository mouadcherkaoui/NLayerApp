import { TestBed, inject } from '@angular/core/testing';

import { AzureStorageBlobService } from './azure-storage-blob.service';

describe('AzureStorageBlobService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AzureStorageBlobService]
    });
  });

  it('should be created', inject([AzureStorageBlobService], (service: AzureStorageBlobService) => {
    expect(service).toBeTruthy();
  }));
});
