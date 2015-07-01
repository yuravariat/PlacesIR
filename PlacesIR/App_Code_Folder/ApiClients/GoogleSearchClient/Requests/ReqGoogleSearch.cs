using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GoogleSearch
{
    //GoogleSearch
    //Response <List<Currency>>
    //Route /customsearch/v1
    [Route("/customsearch/v1")]
    public class ReqGoogleSearch : GoogleApiRequest, IReturn<GoogleSearchApiResponse>
    {
        // Summary:
        //     Turns off the translation between zh-CN and zh-TW.
        public string C2coff { get; set; }
        //
        // Summary:
        //     Country restrict(s).
        public string Cr { get; set; }
        //
        // Summary:
        //     The URL of a linked custom search engine
        public string Cref { get; set; }
        //
        // Summary:
        //     The custom search engine ID to scope this search query
        public string Cx { get; set; }
        //
        // Summary:
        //     Specifies all search results are from a time period
        public string DateRestrict { get; set; }
        //
        // Summary:
        //     Identifies a phrase that all documents in the search results must contain
        public string ExactTerms { get; set; }
        //
        // Summary:
        //     Identifies a word or phrase that should not appear in any documents in the
        //     search results
        public string ExcludeTerms { get; set; }
        //
        // Summary:
        //     Returns images of a specified type. Some of the allowed values are: bmp,
        //     gif, png, jpg, svg, pdf, ...
        public string FileType { get; set; }
        //
        // Summary:
        //     Controls turning on or off the duplicate content filter.
        public FilterEnum? Filter { get; set; }
        //
        // Summary:
        //     Geolocation of end user.
        public string Gl { get; set; }
        //
        // Summary:
        //     The local Google domain to use to perform the search.
        public string Googlehost { get; set; }
        //
        // Summary:
        //     Creates a range in form as_nlo value..as_nhi value and attempts to append
        //     it to query
        public string HighRange { get; set; }
        //
        // Summary:
        //     Sets the user interface language.
        public string Hl { get; set; }
        //
        // Summary:
        //     Appends the extra query terms to the query.
        public string Hq { get; set; }
        //
        // Summary:
        //     Gets the HTTP method.
        public string HttpMethod { get; set; }
        //
        // Summary:
        //     Returns black and white, grayscale, or color images: mono, gray, and color.
        public ImgColorTypeEnum? ImgColorType { get; set; }
        //
        // Summary:
        //     Returns images of a specific dominant color: yellow, green, teal, blue, purple,
        //     pink, white, gray, black and brown.
        public ImgDominantColorEnum? ImgDominantColor { get; set; }
        //
        // Summary:
        //     Returns images of a specified size, where size can be one of: icon, small,
        //     medium, large, xlarge, xxlarge, and huge.
        public ImgSizeEnum? ImgSize { get; set; }
        //
        // Summary:
        //     Returns images of a type, which can be one of: clipart, face, lineart, news,
        //     and photo.
        public ImgTypeEnum? ImgType { get; set; }
        //
        // Summary:
        //     Specifies that all search results should contain a link to a particular URL
        public string LinkSite { get; set; }
        //
        // Summary:
        //     Creates a range in form as_nlo value..as_nhi value and attempts to append
        //     it to query
        public string LowRange { get; set; }
        //
        // Summary:
        //     The language restriction for the search results
        public LrEnum? Lr { get; set; }
        //
        // Summary:
        //     Gets the method name.
        public string MethodName { get; set; }
        //
        // Summary:
        //     Number of search results to return
        public long? Num { get; set; }
        //
        // Summary:
        //     Provides additional search terms to check for in a document, where each document
        //     in the search results must contain at least one of the additional search
        //     terms
        public string OrTerms { get; set; }
        //
        // Summary:
        //     Query
        public string Q { get; set; }
        //
        // Summary:
        //     Specifies that all search results should be pages that are related to the
        //     specified URL
        public string RelatedSite { get; set; }
        //
        // Summary:
        //     Gets the REST path.
        public string RestPath { get; set; }
        //
        // Summary:
        //     Filters based on licensing. Supported values include: cc_publicdomain, cc_attribute,
        //     cc_sharealike, cc_noncommercial, cc_nonderived and combinations of these.
        public string Rights { get; set; }
        //
        // Summary:
        //     Search safety level
        public SafeEnum? Safe { get; set; }
        //
        // Summary:
        //     Specifies the search type: image.
        public SearchTypeEnum? SearchType { get; set; }
        //
        // Summary:
        //     Specifies all search results should be pages from a given site
        public string SiteSearch { get; set; }
        //
        // Summary:
        //     Controls whether to include or exclude results from the site named in the
        //     as_sitesearch parameter
        public SiteSearchFilterEnum? SiteSearchFilter { get; set; }
        //
        // Summary:
        //     The sort expression to apply to the results
        public string Sort { get; set; }
        //
        // Summary:
        //     The index of the first result to return
        public long? Start { get; set; }


        // Summary:
        //     Controls turning on or off the duplicate content filter.
        public enum FilterEnum
        {
            // Summary:
            //     Turns off duplicate content filter.
            [StringValue("0")]
            Value0 = 0,
            //
            // Summary:
            //     Turns on duplicate content filter.
            [StringValue("1")]
            Value1 = 1,
        }

        // Summary:
        //     Returns black and white, grayscale, or color images: mono, gray, and color.
        public enum ImgColorTypeEnum
        {
            // Summary:
            //     color
            [StringValue("color")]
            Color = 0,
            //
            // Summary:
            //     gray
            [StringValue("gray")]
            Gray = 1,
            //
            // Summary:
            //     mono
            [StringValue("mono")]
            Mono = 2,
        }

        // Summary:
        //     Returns images of a specific dominant color: yellow, green, teal, blue, purple,
        //     pink, white, gray, black and brown.
        public enum ImgDominantColorEnum
        {
            // Summary:
            //     black
            [StringValue("black")]
            Black = 0,
            //
            // Summary:
            //     blue
            [StringValue("blue")]
            Blue = 1,
            //
            // Summary:
            //     brown
            [StringValue("brown")]
            Brown = 2,
            //
            // Summary:
            //     gray
            [StringValue("gray")]
            Gray = 3,
            //
            // Summary:
            //     green
            [StringValue("green")]
            Green = 4,
            //
            // Summary:
            //     pink
            [StringValue("pink")]
            Pink = 5,
            //
            // Summary:
            //     purple
            [StringValue("purple")]
            Purple = 6,
            //
            // Summary:
            //     teal
            [StringValue("teal")]
            Teal = 7,
            //
            // Summary:
            //     white
            [StringValue("white")]
            White = 8,
            //
            // Summary:
            //     yellow
            [StringValue("yellow")]
            Yellow = 9,
        }

        // Summary:
        //     Returns images of a specified size, where size can be one of: icon, small,
        //     medium, large, xlarge, xxlarge, and huge.
        public enum ImgSizeEnum
        {
            // Summary:
            //     huge
            [StringValue("huge")]
            Huge = 0,
            //
            // Summary:
            //     icon
            [StringValue("icon")]
            Icon = 1,
            //
            // Summary:
            //     large
            [StringValue("large")]
            Large = 2,
            //
            // Summary:
            //     medium
            [StringValue("medium")]
            Medium = 3,
            //
            // Summary:
            //     small
            [StringValue("small")]
            Small = 4,
            //
            // Summary:
            //     xlarge
            [StringValue("xlarge")]
            Xlarge = 5,
            //
            // Summary:
            //     xxlarge
            [StringValue("xxlarge")]
            Xxlarge = 6,
        }

        // Summary:
        //     Returns images of a type, which can be one of: clipart, face, lineart, news,
        //     and photo.
        public enum ImgTypeEnum
        {
            // Summary:
            //     clipart
            [StringValue("clipart")]
            Clipart = 0,
            //
            // Summary:
            //     face
            [StringValue("face")]
            Face = 1,
            //
            // Summary:
            //     lineart
            [StringValue("lineart")]
            Lineart = 2,
            //
            // Summary:
            //     news
            [StringValue("news")]
            News = 3,
            //
            // Summary:
            //     photo
            [StringValue("photo")]
            Photo = 4,
        }

        // Summary:
        //     The language restriction for the search results
        public enum LrEnum
        {
            // Summary:
            //     Arabic
            [StringValue("lang_ar")]
            LangAr = 0,
            //
            // Summary:
            //     Bulgarian
            [StringValue("lang_bg")]
            LangBg = 1,
            //
            // Summary:
            //     Catalan
            [StringValue("lang_ca")]
            LangCa = 2,
            //
            // Summary:
            //     Czech
            [StringValue("lang_cs")]
            LangCs = 3,
            //
            // Summary:
            //     Danish
            [StringValue("lang_da")]
            LangDa = 4,
            //
            // Summary:
            //     German
            [StringValue("lang_de")]
            LangDe = 5,
            //
            // Summary:
            //     Greek
            [StringValue("lang_el")]
            LangEl = 6,
            //
            // Summary:
            //     English
            [StringValue("lang_en")]
            LangEn = 7,
            //
            // Summary:
            //     Spanish
            [StringValue("lang_es")]
            LangEs = 8,
            //
            // Summary:
            //     Estonian
            [StringValue("lang_et")]
            LangEt = 9,
            //
            // Summary:
            //     Finnish
            [StringValue("lang_fi")]
            LangFi = 10,
            //
            // Summary:
            //     French
            [StringValue("lang_fr")]
            LangFr = 11,
            //
            // Summary:
            //     Croatian
            [StringValue("lang_hr")]
            LangHr = 12,
            //
            // Summary:
            //     Hungarian
            [StringValue("lang_hu")]
            LangHu = 13,
            //
            // Summary:
            //     Indonesian
            [StringValue("lang_id")]
            LangId = 14,
            //
            // Summary:
            //     Icelandic
            [StringValue("lang_is")]
            LangIs = 15,
            //
            // Summary:
            //     Italian
            [StringValue("lang_it")]
            LangIt = 16,
            //
            // Summary:
            //     Hebrew
            [StringValue("lang_iw")]
            LangIw = 17,
            //
            // Summary:
            //     Japanese
            [StringValue("lang_ja")]
            LangJa = 18,
            //
            // Summary:
            //     Korean
            [StringValue("lang_ko")]
            LangKo = 19,
            //
            // Summary:
            //     Lithuanian
            [StringValue("lang_lt")]
            LangLt = 20,
            //
            // Summary:
            //     Latvian
            [StringValue("lang_lv")]
            LangLv = 21,
            //
            // Summary:
            //     Dutch
            [StringValue("lang_nl")]
            LangNl = 22,
            //
            // Summary:
            //     Norwegian
            [StringValue("lang_no")]
            LangNo = 23,
            //
            // Summary:
            //     Polish
            [StringValue("lang_pl")]
            LangPl = 24,
            //
            // Summary:
            //     Portuguese
            [StringValue("lang_pt")]
            LangPt = 25,
            //
            // Summary:
            //     Romanian
            [StringValue("lang_ro")]
            LangRo = 26,
            //
            // Summary:
            //     Russian
            [StringValue("lang_ru")]
            LangRu = 27,
            //
            // Summary:
            //     Slovak
            [StringValue("lang_sk")]
            LangSk = 28,
            //
            // Summary:
            //     Slovenian
            [StringValue("lang_sl")]
            LangSl = 29,
            //
            // Summary:
            //     Serbian
            [StringValue("lang_sr")]
            LangSr = 30,
            //
            // Summary:
            //     Swedish
            [StringValue("lang_sv")]
            LangSv = 31,
            //
            // Summary:
            //     Turkish
            [StringValue("lang_tr")]
            LangTr = 32,
            //
            // Summary:
            //     Chinese (Simplified)
            [StringValue("lang_zh-CN")]
            LangZhCN = 33,
            //
            // Summary:
            //     Chinese (Traditional)
            [StringValue("lang_zh-TW")]
            LangZhTW = 34,
        }

        // Summary:
        //     Search safety level
        public enum SafeEnum
        {
            // Summary:
            //     Enables highest level of safe search filtering.
            [StringValue("high")]
            High = 0,
            //
            // Summary:
            //     Enables moderate safe search filtering.
            [StringValue("medium")]
            Medium = 1,
            //
            // Summary:
            //     Disables safe search filtering.
            [StringValue("off")]
            Off = 2,
        }

        // Summary:
        //     Specifies the search type: image.
        public enum SearchTypeEnum
        {
            // Summary:
            //     custom image search
            [StringValue("image")]
            Image = 0,
        }

        // Summary:
        //     Controls whether to include or exclude results from the site named in the
        //     as_sitesearch parameter
        public enum SiteSearchFilterEnum
        {
            // Summary:
            //     exclude
            [StringValue("e")]
            E = 0,
            //
            // Summary:
            //     include
            [StringValue("i")]
            I = 1,
        }
    }
}