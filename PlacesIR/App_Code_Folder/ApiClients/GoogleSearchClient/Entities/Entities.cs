using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PlacesIR.GoogleSearch
{
    public class SearchInformationData
    {
        public SearchInformationData() { }

        [DataMember(Name = "formattedSearchTime")]
        public string FormattedSearchTime { get; set; }
        [DataMember(Name = "formattedTotalResults")]
        public string FormattedTotalResults { get; set; }
        [DataMember(Name = "searchTime")]
        public double? SearchTime { get; set; }
        [DataMember(Name = "totalResults")]
        public long? TotalResults { get; set; }
    }

    public class SpellingData
    {
        public SpellingData() { }

        [DataMember(Name = "correctedQuery")]
        public string CorrectedQuery { get; set; }
        [DataMember(Name = "htmlCorrectedQuery")]
        public string HtmlCorrectedQuery { get; set; }
    }

    public class UrlData
    {
        public UrlData() { }

        [DataMember(Name = "template")]
        public string Template { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
    public class Context
    {
        public Context() { }

        // Summary:
        //     The ETag of the item.
        public string ETag { get; set; }
        [DataMember(Name = "facets")]
        public IList<IList<Context.FacetsData>> Facets { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }

        public class FacetsData
        {
            public FacetsData() { }

            [DataMember(Name = "anchor")]
            public string Anchor { get; set; }
            [DataMember(Name = "label")]
            public string Label { get; set; }
            [DataMember(Name = "label_with_op")]
            public string LabelWithOp { get; set; }
        }
    }
    public class Result
    {
        public Result() { }

        [DataMember(Name = "cacheId")]
        public string CacheId { get; set; }
        [DataMember(Name = "displayLink")]
        public string DisplayLink { get; set; }
        //
        // Summary:
        //     The ETag of the item.
        public string ETag { get; set; }
        [DataMember(Name = "fileFormat")]
        public string FileFormat { get; set; }
        [DataMember(Name = "formattedUrl")]
        public string FormattedUrl { get; set; }
        [DataMember(Name = "htmlFormattedUrl")]
        public string HtmlFormattedUrl { get; set; }
        [DataMember(Name = "htmlSnippet")]
        public string HtmlSnippet { get; set; }
        [DataMember(Name = "htmlTitle")]
        public string HtmlTitle { get; set; }
        [DataMember(Name = "image")]
        public Result.ImageData Image { get; set; }
        [DataMember(Name = "kind")]
        public string Kind { get; set; }
        [DataMember(Name = "labels")]
        public IList<Result.LabelsData> Labels { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "mime")]
        public string Mime { get; set; }
        [DataMember(Name = "pagemap")]
        public IDictionary<string, IList<IDictionary<string, object>>> Pagemap { get; set; }
        [DataMember(Name = "snippet")]
        public string Snippet { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }

        public class ImageData
        {
            public ImageData() { }

            [DataMember(Name = "byteSize")]
            public int? ByteSize { get; set; }
            [DataMember(Name = "contextLink")]
            public string ContextLink { get; set; }
            [DataMember(Name = "height")]
            public int? Height { get; set; }
            [DataMember(Name = "thumbnailHeight")]
            public int? ThumbnailHeight { get; set; }
            [DataMember(Name = "thumbnailLink")]
            public string ThumbnailLink { get; set; }
            [DataMember(Name = "thumbnailWidth")]
            public int? ThumbnailWidth { get; set; }
            [DataMember(Name = "width")]
            public int? Width { get; set; }
        }

        public class LabelsData
        {
            public LabelsData() { }

            [DataMember(Name = "displayName")]
            public string DisplayName { get; set; }
            [DataMember(Name = "label_with_op")]
            public string LabelWithOp { get; set; }
            [DataMember(Name = "name")]
            public string Name { get; set; }
        }
    }
    public class Promotion
    {
        public Promotion() { }

        [DataMember(Name = "bodyLines")]
        public IList<Promotion.BodyLinesData> BodyLines { get; set; }
        [DataMember(Name = "displayLink")]
        public string DisplayLink { get; set; }
        //
        // Summary:
        //     The ETag of the item.
        public string ETag { get; set; }
        [DataMember(Name = "htmlTitle")]
        public string HtmlTitle { get; set; }
        [DataMember(Name = "image")]
        public Promotion.ImageData Image { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }

        public class BodyLinesData
        {
            public BodyLinesData() { }

            [DataMember(Name = "htmlTitle")]
            public string HtmlTitle { get; set; }
            [DataMember(Name = "link")]
            public string Link { get; set; }
            [DataMember(Name = "title")]
            public string Title { get; set; }
            [DataMember(Name = "url")]
            public string Url { get; set; }
        }

        public class ImageData
        {
            public ImageData() { }

            [DataMember(Name = "height")]
            public int? Height { get; set; }
            [DataMember(Name = "source")]
            public string Source { get; set; }
            [DataMember(Name = "width")]
            public int? Width { get; set; }
        }
    }
    public class Query
    {
        public Query() { }

        [DataMember(Name = "count")]
        public int? Count { get; set; }
        [DataMember(Name = "cr")]
        public string Cr { get; set; }
        [DataMember(Name = "cref")]
        public string Cref { get; set; }
        [DataMember(Name = "cx")]
        public string Cx { get; set; }
        [DataMember(Name = "dateRestrict")]
        public string DateRestrict { get; set; }
        [DataMember(Name = "disableCnTwTranslation")]
        public string DisableCnTwTranslation { get; set; }
        //
        // Summary:
        //     The ETag of the item.
        public string ETag { get; set; }
        [DataMember(Name = "exactTerms")]
        public string ExactTerms { get; set; }
        [DataMember(Name = "excludeTerms")]
        public string ExcludeTerms { get; set; }
        [DataMember(Name = "fileType")]
        public string FileType { get; set; }
        [DataMember(Name = "filter")]
        public string Filter { get; set; }
        [DataMember(Name = "gl")]
        public string Gl { get; set; }
        [DataMember(Name = "googleHost")]
        public string GoogleHost { get; set; }
        [DataMember(Name = "highRange")]
        public string HighRange { get; set; }
        [DataMember(Name = "hl")]
        public string Hl { get; set; }
        [DataMember(Name = "hq")]
        public string Hq { get; set; }
        [DataMember(Name = "imgColorType")]
        public string ImgColorType { get; set; }
        [DataMember(Name = "imgDominantColor")]
        public string ImgDominantColor { get; set; }
        [DataMember(Name = "imgSize")]
        public string ImgSize { get; set; }
        [DataMember(Name = "imgType")]
        public string ImgType { get; set; }
        [DataMember(Name = "inputEncoding")]
        public string InputEncoding { get; set; }
        [DataMember(Name = "language")]
        public string Language { get; set; }
        [DataMember(Name = "linkSite")]
        public string LinkSite { get; set; }
        [DataMember(Name = "lowRange")]
        public string LowRange { get; set; }
        [DataMember(Name = "orTerms")]
        public string OrTerms { get; set; }
        [DataMember(Name = "outputEncoding")]
        public string OutputEncoding { get; set; }
        [DataMember(Name = "relatedSite")]
        public string RelatedSite { get; set; }
        [DataMember(Name = "rights")]
        public string Rights { get; set; }
        [DataMember(Name = "safe")]
        public string Safe { get; set; }
        [DataMember(Name = "searchTerms")]
        public string SearchTerms { get; set; }
        [DataMember(Name = "searchType")]
        public string SearchType { get; set; }
        [DataMember(Name = "siteSearch")]
        public string SiteSearch { get; set; }
        [DataMember(Name = "siteSearchFilter")]
        public string SiteSearchFilter { get; set; }
        [DataMember(Name = "sort")]
        public string Sort { get; set; }
        [DataMember(Name = "startIndex")]
        public int? StartIndex { get; set; }
        [DataMember(Name = "startPage")]
        public int? StartPage { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "totalResults")]
        public long? TotalResults { get; set; }
    }
}