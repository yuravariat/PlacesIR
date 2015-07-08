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
        public string c2coff { get; set; }
        //
        // Summary:
        //     Country restrict(s).
        public string cr { get; set; }
        //
        // Summary:
        //     The URL of a linked custom search engine
        public string cref { get; set; }
        //
        // Summary:
        //     The custom search engine ID to scope this search query
        public string cx { get; set; }
        //
        // Summary:
        //     Specifies all search results are from a time period
        public string dateRestrict { get; set; }
        //
        // Summary:
        //     Identifies a phrase that all documents in the search results must contain
        public string exactTerms { get; set; }
        //
        // Summary:
        //     Identifies a word or phrase that should not appear in any documents in the
        //     search results
        public string excludeTerms { get; set; }
        //
        // Summary:
        //     Returns images of a specified type. Some of the allowed values are: bmp,
        //     gif, png, jpg, svg, pdf, ...
        public string fileType { get; set; }
        //
        // Summary:
        //     Controls turning on or off the duplicate content filter.
        public FilterEnum? filter { get; set; }
        //
        // Summary:
        //     Geolocation of end user.
        public string gl { get; set; }
        //
        // Summary:
        //     The local Google domain to use to perform the search.
        public string googlehost { get; set; }
        //
        // Summary:
        //     Creates a range in form as_nlo value..as_nhi value and attempts to append
        //     it to query
        public string highRange { get; set; }
        //
        // Summary:
        //     Sets the user interface language.
        public string hl { get; set; }
        //
        // Summary:
        //     Appends the extra query terms to the query.
        public string hq { get; set; }
        //
        // Summary:
        //     Gets the HTTP method.
        public string httpMethod { get; set; }
        //
        // Summary:
        //     Returns black and white, grayscale, or color images: mono, gray, and color.
        public ImgColorTypeEnum? imgColorType { get; set; }
        //
        // Summary:
        //     Returns images of a specific dominant color: yellow, green, teal, blue, purple,
        //     pink, white, gray, black and brown.
        public ImgDominantColorEnum? imgDominantColor { get; set; }
        //
        // Summary:
        //     Returns images of a specified size, where size can be one of: icon, small,
        //     medium, large, xlarge, xxlarge, and huge.
        public ImgSizeEnum? imgSize { get; set; }
        //
        // Summary:
        //     Returns images of a type, which can be one of: clipart, face, lineart, news,
        //     and photo.
        public ImgTypeEnum? imgType { get; set; }
        //
        // Summary:
        //     Specifies that all search results should contain a link to a particular URL
        public string linkSite { get; set; }
        //
        // Summary:
        //     Creates a range in form as_nlo value..as_nhi value and attempts to append
        //     it to query
        public string lowRange { get; set; }
        //
        // Summary:
        //     The language restriction for the search results
        public LrEnum? lr { get; set; }
        //
        // Summary:
        //     Gets the method name.
        public string methodName { get; set; }
        //
        // Summary:
        //     Number of search results to return
        public long? num { get; set; }
        //
        // Summary:
        //     Provides additional search terms to check for in a document, where each document
        //     in the search results must contain at least one of the additional search
        //     terms
        public string orTerms { get; set; }
        //
        // Summary:
        //     Query
        public string q { get; set; }
        //
        // Summary:
        //     Specifies that all search results should be pages that are related to the
        //     specified URL
        public string relatedSite { get; set; }
        //
        // Summary:
        //     Gets the REST path.
        public string restPath { get; set; }
        //
        // Summary:
        //     Filters based on licensing. Supported values include: cc_publicdomain, cc_attribute,
        //     cc_sharealike, cc_noncommercial, cc_nonderived and combinations of these.
        public string rights { get; set; }
        //
        // Summary:
        //     Search safety level
        public SafeEnum? safe { get; set; }
        //
        // Summary:
        //     Specifies the search type: image.
        public SearchTypeEnum? searchType { get; set; }
        //
        // Summary:
        //     Specifies all search results should be pages from a given site
        public string siteSearch { get; set; }
        //
        // Summary:
        //     Controls whether to include or exclude results from the site named in the
        //     as_sitesearch parameter
        public SiteSearchFilterEnum? siteSearchFilter { get; set; }
        //
        // Summary:
        //     The sort expression to apply to the results
        public string sort { get; set; }
        //
        // Summary:
        //     The index of the first result to return
        public long? start { get; set; }


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
            color = 0,
            //
            // Summary:
            //     gray
            [StringValue("gray")]
            gray = 1,
            //
            // Summary:
            //     mono
            [StringValue("mono")]
            mono = 2,
        }

        // Summary:
        //     Returns images of a specific dominant color: yellow, green, teal, blue, purple,
        //     pink, white, gray, black and brown.
        public enum ImgDominantColorEnum
        {
            // Summary:
            //     black
            [StringValue("black")]
            black = 0,
            //
            // Summary:
            //     blue
            [StringValue("blue")]
            blue = 1,
            //
            // Summary:
            //     brown
            [StringValue("brown")]
            brown = 2,
            //
            // Summary:
            //     gray
            [StringValue("gray")]
            gray = 3,
            //
            // Summary:
            //     green
            [StringValue("green")]
            green = 4,
            //
            // Summary:
            //     pink
            [StringValue("pink")]
            pink = 5,
            //
            // Summary:
            //     purple
            [StringValue("purple")]
            purple = 6,
            //
            // Summary:
            //     teal
            [StringValue("teal")]
            teal = 7,
            //
            // Summary:
            //     white
            [StringValue("white")]
            white = 8,
            //
            // Summary:
            //     yellow
            [StringValue("yellow")]
            yellow = 9,
        }

        // Summary:
        //     Returns images of a specified size, where size can be one of: icon, small,
        //     medium, large, xlarge, xxlarge, and huge.
        public enum ImgSizeEnum
        {
            // Summary:
            //     huge
            [StringValue("huge")]
            huge = 0,
            //
            // Summary:
            //     icon
            [StringValue("icon")]
            icon = 1,
            //
            // Summary:
            //     large
            [StringValue("large")]
            large = 2,
            //
            // Summary:
            //     medium
            [StringValue("medium")]
            medium = 3,
            //
            // Summary:
            //     small
            [StringValue("small")]
            small = 4,
            //
            // Summary:
            //     xlarge
            [StringValue("xlarge")]
            xlarge = 5,
            //
            // Summary:
            //     xxlarge
            [StringValue("xxlarge")]
            xxlarge = 6,
        }

        // Summary:
        //     Returns images of a type, which can be one of: clipart, face, lineart, news,
        //     and photo.
        public enum ImgTypeEnum
        {
            // Summary:
            //     clipart
            [StringValue("clipart")]
            clipart = 0,
            //
            // Summary:
            //     face
            [StringValue("face")]
            face = 1,
            //
            // Summary:
            //     lineart
            [StringValue("lineart")]
            lineart = 2,
            //
            // Summary:
            //     news
            [StringValue("news")]
            news = 3,
            //
            // Summary:
            //     photo
            [StringValue("photo")]
            photo = 4,
        }

        // Summary:
        //     The language restriction for the search results
        public enum LrEnum
        {
            // Summary:
            //     Arabic
            [StringValue("lang_ar")]
            lang_ar = 0,
            //
            // Summary:
            //     Bulgarian
            [StringValue("lang_bg")]
            lang_bg = 1,
            //
            // Summary:
            //     Catalan
            [StringValue("lang_ca")]
            lang_ca = 2,
            //
            // Summary:
            //     Czech
            [StringValue("lang_cs")]
            lang_cs = 3,
            //
            // Summary:
            //     Danish
            [StringValue("lang_da")]
            lang_da = 4,
            //
            // Summary:
            //     German
            [StringValue("lang_de")]
            lang_de = 5,
            //
            // Summary:
            //     Greek
            [StringValue("lang_el")]
            lang_el = 6,
            //
            // Summary:
            //     English
            [StringValue("lang_en")]
            lang_en = 7,
            //
            // Summary:
            //     Spanish
            [StringValue("lang_es")]
            lang_es = 8,
            //
            // Summary:
            //     Estonian
            [StringValue("lang_et")]
            lang_et = 9,
            //
            // Summary:
            //     Finnish
            [StringValue("lang_fi")]
            lang_fi = 10,
            //
            // Summary:
            //     French
            [StringValue("lang_fr")]
            lang_fr = 11,
            //
            // Summary:
            //     Croatian
            [StringValue("lang_hr")]
            lang_hr = 12,
            //
            // Summary:
            //     Hungarian
            [StringValue("lang_hu")]
            lang_hu = 13,
            //
            // Summary:
            //     Indonesian
            [StringValue("lang_id")]
            lang_id = 14,
            //
            // Summary:
            //     Icelandic
            [StringValue("lang_is")]
            lang_is = 15,
            //
            // Summary:
            //     Italian
            [StringValue("lang_it")]
            lang_it = 16,
            //
            // Summary:
            //     Hebrew
            [StringValue("lang_iw")]
            lang_iw = 17,
            //
            // Summary:
            //     Japanese
            [StringValue("lang_ja")]
            lang_ja = 18,
            //
            // Summary:
            //     Korean
            [StringValue("lang_ko")]
            lang_ko = 19,
            //
            // Summary:
            //     Lithuanian
            [StringValue("lang_lt")]
            lang_lt = 20,
            //
            // Summary:
            //     Latvian
            [StringValue("lang_lv")]
            lang_lv = 21,
            //
            // Summary:
            //     Dutch
            [StringValue("lang_nl")]
            lang_nl = 22,
            //
            // Summary:
            //     Norwegian
            [StringValue("lang_no")]
            lang_no = 23,
            //
            // Summary:
            //     Polish
            [StringValue("lang_pl")]
            lang_pl = 24,
            //
            // Summary:
            //     Portuguese
            [StringValue("lang_pt")]
            lang_pt = 25,
            //
            // Summary:
            //     Romanian
            [StringValue("lang_ro")]
            lang_ro = 26,
            //
            // Summary:
            //     Russian
            [StringValue("lang_ru")]
            lang_ru = 27,
            //
            // Summary:
            //     Slovak
            [StringValue("lang_sk")]
            lang_sk = 28,
            //
            // Summary:
            //     Slovenian
            [StringValue("lang_sl")]
            lang_sl = 29,
            //
            // Summary:
            //     Serbian
            [StringValue("lang_sr")]
            lang_sr = 30,
            //
            // Summary:
            //     Swedish
            [StringValue("lang_sv")]
            lang_sv = 31,
            //
            // Summary:
            //     Turkish
            [StringValue("lang_tr")]
            lang_tr = 32,
            //
            // Summary:
            //     Chinese (Simplified)
            [StringValue("lang_zh-CN")]
            lang_zh_CN = 33,
            //
            // Summary:
            //     Chinese (Traditional)
            [StringValue("lang_zh-TW")]
            lang_zh_TW = 34,
        }

        // Summary:
        //     Search safety level
        public enum SafeEnum
        {
            // Summary:
            //     Enables highest level of safe search filtering.
            [StringValue("high")]
            high = 0,
            //
            // Summary:
            //     Enables moderate safe search filtering.
            [StringValue("medium")]
            medium = 1,
            //
            // Summary:
            //     Disables safe search filtering.
            [StringValue("off")]
            off = 2,
        }

        // Summary:
        //     Specifies the search type: image.
        public enum SearchTypeEnum
        {
            // Summary:
            //     custom image search
            [StringValue("image")]
            image = 0,
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