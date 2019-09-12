using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.CustomVision
{


    public class ImageDetails
    {
        public string id { get; set; }
        public string created { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string resizedImageUri { get; set; }
        public string thumbnailUri { get; set; }
        public string originalImageUri { get; set; }
        public List<Tag> tags { get; set; }
        public List<Region> regions { get; set; }
    }

    public class Image
    {
        public string sourceUrl { get; set; }
        public string status { get; set; }
        public ImageDetails image { get; set; }
    }

    public class CreateImagesResult
    {
        public bool isBatchSuccessful { get; set; }
        public List<Image> images { get; set; }
    }
    public class CreateImagesRequest
    {
        public List<ImageUrl> images { get; set; }
        public List<region> regions { get; set; }
        public List<string> tagIds { get; set; }
    }

    public class region
    {
        public string tagId { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    public class ImageUrl
    {
        public string url { get; set; }
        public List<string> tagIds { get; set; }
    }
}
