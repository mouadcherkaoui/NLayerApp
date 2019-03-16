import { Injectable, Inject } from '@angular/core';
import { IAzureStorage, IBlobService, IBlobStorage, ISasToken, ISpeedSummary, BLOB_STORAGE_TOKEN} from '../../models/azurestorage';
@Injectable()
export class AzureStorageBlobService {
  // svcUrl = new BlobService()
  
  constructor(@Inject(BLOB_STORAGE_TOKEN) private blobStorage: IBlobStorage) { }

}
