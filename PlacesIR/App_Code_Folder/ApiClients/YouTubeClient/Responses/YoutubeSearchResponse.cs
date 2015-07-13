using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PlacesIR.YouTube
{
    [Serializable]
    public class YoutubeSearchResponse
    {
        public YoutubeSearchResponse() { }

        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string prevPageToken { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<VideoItem> items { get; set; }
    }
    [Serializable]
    public class PageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }
    [Serializable]
    public class VideoItem
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public VideoId id { get; set; }
        public VideoSnipet snippet { get; set; }
    }
    [Serializable]
    public class VideoId
    {
        public string kind { get; set; }
        public string videoId { get; set; }
    }
    [Serializable]
    public class VideoSnipet
    {
        public DateTime publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string channelTitle { get; set; }
        public string liveBroadcastContent { get; set; }
        public Thumbnail thumbnails { get; set; }
    }
    [Serializable]
    public class Thumbnail
    {
        public ThumbnailItem Default { get; set; }
        public ThumbnailItem medium { get; set; }
        public ThumbnailItem high { get; set; }
    }
    [Serializable]
    public class ThumbnailItem
    {
        public string url { get; set; }
    }
}