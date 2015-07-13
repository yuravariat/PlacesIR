using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.YouTube
{
    // Search
    // Response <List<Currency>>
    // Route /search
    [Route("/search")]
    public class ReqSearch : GoogleApiRequest, IReturn<YoutubeSearchResponse>
    {
        public string part { get; set; }
        public bool forContentOwner { get; set; }
        public bool forDeveloper { get; set; }
        public bool forMine { get; set; }
        public string relatedToVideoId { get; set; }
        public string channelId { get; set; }
        public ChanelType? channelType { get; set; }
        public EventType? eventType { get; set; }
        public string location { get; set; } //The parameter value is a string that specifies latitude/longitude coordinates e.g. (37.42307,-122.08427).
        public string locationRadius { get; set; }
        public int? maxResults { get; set; }
        public string onBehalfOfContentOwner { get; set; }
        public OrderType? order { get; set; }
        public string pageToken { get; set; }
        public DateTime? publishedAfter { get; set; }
        public string publishedBefore { get; set; }
        public string q { get; set; }
        public string regionCode { get; set; }
        public string relevanceLanguage { get; set; }
        public SafeSearch? safeSearch { get; set; }
        public string topicId { get; set; }
        public string type { get; set; }
        public VideoCuptionType? videoCaption { get; set; }
        public string videoCategoryId { get; set; }
        public VideoDefinitionType? videoDefinition { get; set; }
        public VideoDimensionType? videoDimension { get; set; }
        public VideoDurationType? videoDuration { get; set; }
        public string videoEmbeddable { get; set; }
        public string videoLicense { get; set; }
        public string videoSyndicated { get; set; }
        public VideoType? videoType { get; set; }
    }
    public enum ChanelType
    {
        [StringValue("any")]
        any,
        [StringValue("show")]
        show
    }
    public enum VideoType
    {
        [StringValue("any")]
        any,
        [StringValue("episode")]
        episode,
        [StringValue("movie")]
        movie
    }
    public enum VideoDurationType
    {
        [StringValue("any")]
        any,
        [StringValue("long")]
        Long,
        [StringValue("medium")]
        medium,
        [StringValue("short")]
        Short
    }
    public enum VideoDimensionType
    {
        [StringValue("2d")]
        d2,
        [StringValue("3d")]
        d3,
        [StringValue("any")]
        any
    }
    public enum VideoDefinitionType
    {
        [StringValue("any")]
        any,
        [StringValue("high")]
        high,
        [StringValue("standard")]
        standard
    }
    public enum SearchType
    {
        [StringValue("channel")]
        channel,
        [StringValue("playlist")]
        playlist,
        [StringValue("video")]
        upcoming
    }
    public enum VideoCuptionType
    {
        [StringValue("any")]
        any,
        [StringValue("closedCaption")]
        closedCaption,
        [StringValue("none")]
        none
    }
    public enum EventType
    {
        [StringValue("completed")]
        completed,
        [StringValue("live")]
        live ,
        [StringValue("upcoming")]
        upcoming 
    }
    public enum OrderType
    {
        [StringValue("date")]
        date,
        [StringValue("rating")]
        rating,
        [StringValue("relevance")]
        relevance,
        [StringValue("title")]
        title,
        [StringValue("videoCount")]
        videoCount,
        [StringValue("viewCount")]
        viewCount  
    }
    public enum SafeSearch
    {
        [StringValue("moderate")]
        moderate,
        [StringValue("none")]
        none,
        [StringValue("strict")]
        strict
    }
}