using HtmlAgilityPack;
using NUglify;
using SimpleSEOAnalyser.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace SimpleSEOAnalyser
{
    //public class Constant
    //{
    //    public const string StopWordsPath = "/JSON/StopWord.json";
    //}

    public class FilterFormat
    {
        public const string GetAllLinks = @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";
        public const string _StopWords = "[ \"a\", \"about\", \"above\", \"after\", \"again\", \"against\", \"all\", \"am\", \"an\", \"and\", \"any\", \"are\", \"as\", \"at\", \"be\", \"because\", \"been\", \"before\", \"being\", \"below\", \"between\", \"both\", \"but\", \"by\", \"could\", \"did\", \"do\", \"does\", \"doing\", \"down\", \"during\", \"each\", \"few\", \"for\", \"from\", \"further\", \"had\", \"has\", \"have\", \"having\", \"he\", \"he'd\", \"he'll\", \"he's\", \"her\", \"here\", \"here's\", \"hers\", \"herself\", \"him\", \"himself\", \"his\", \"how\", \"how's\", \"i\", \"i'd\", \"i'll\", \"i'm\", \"i've\", \"if\", \"in\", \"into\", \"is\", \"it\", \"it's\", \"its\", \"itself\", \"let's\", \"me\", \"more\", \"most\", \"my\", \"myself\", \"nor\", \"of\", \"on\", \"once\", \"only\", \"or\", \"other\", \"ought\", \"our\", \"ours\", \"ourselves\", \"out\", \"over\", \"own\", \"same\", \"she\", \"she'd\", \"she'll\", \"she's\", \"should\", \"so\", \"some\", \"such\", \"than\", \"that\", \"that's\", \"the\", \"their\", \"theirs\", \"them\", \"themselves\", \"then\", \"there\", \"there's\", \"these\", \"they\", \"they'd\", \"they'll\", \"they're\", \"they've\", \"this\", \"those\", \"through\", \"to\", \"too\", \"under\", \"until\", \"up\", \"very\", \"was\", \"we\", \"we'd\", \"we'll\", \"we're\", \"we've\", \"were\", \"what\", \"what's\", \"when\", \"when's\", \"where\", \"where's\", \"which\", \"while\", \"who\", \"who's\", \"whom\", \"why\", \"why's\", \"with\", \"would\", \"you\", \"you'd\", \"you'll\", \"you're\", \"you've\", \"your\", \"yours\", \"yourself\", \"yourselves\" ]";
    }

    public class Common
    {
        public static Dictionary<string, int> GetAllWordsInfo(string _KeyText, bool _isFilterStopWords)
        {
            try
            {
                var listWords = new List<string>();

                var web = new HtmlWeb();
                var doc = web.Load(_KeyText);
                var root = doc.DocumentNode.SelectSingleNode("//body");
                var allText = Uglify.HtmlToText(root.OuterHtml);
                listWords = Util.GetAllWords(allText.Code);

                if (_isFilterStopWords)
                {

                    listWords = Util.FilterStopWords(listWords, FilterFormat._StopWords);
                }

                return Util.GroupListOfString(listWords);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public static List<MetaDetails> GetAllMetaTagsInfo(string _KeyText, bool _isFilterStopWords)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(_KeyText);
            var metas = document.DocumentNode.SelectNodes("//meta");
            var listofMetas = new List<MetaDetails>();

            foreach (var tag in metas.ToList())
            {
                string content = tag.Attributes["content"] != null ? tag.Attributes["content"].Value : "";
                string name = tag.Attributes["name"] != null ? tag.Attributes["name"].Value : "";
                var hrefList = Regex.Replace(content, FilterFormat.GetAllLinks, "$1");
                var words = Util.SplitSentenceIntoWords(hrefList.ToLower(), 1);
                List<string> listWords = new List<string>();
                listWords.AddRange(words);
                //List<string> listURLs = new List<string>();

                if (_isFilterStopWords)
                {
                    listWords = Util.FilterStopWords(listWords, FilterFormat._StopWords);
                }

                Dictionary<string, int> WordsInfoList = Util.GroupListOfString(listWords);

                foreach (var key in WordsInfoList.Keys)
                {
                    var metaTagInfo = new MetaDetails();
                    metaTagInfo.Content = content;
                    metaTagInfo.Name = name;
                    metaTagInfo.Words = key;
                    metaTagInfo.TotalWordCount = WordsInfoList[key];

                    if (!string.IsNullOrWhiteSpace(metaTagInfo.Content))
                    {
                        listofMetas.Add(metaTagInfo);
                    }
                }

                //if (hrefList.ToString().ToUpper().Contains("HTTP") || hrefList.ToString().ToUpper().Contains("://"))
                //{
                //    listURLs.Add(hrefList);
                //}
                //else
                //{
                //    var words = Util.SplitSentenceIntoWords(hrefList.ToLower(), 1);
                //    listWords.AddRange(words);
                //}
                //metaTagInfo.TotalWordCount = listWords.Count();
            }

            return listofMetas;
        }

        public static Dictionary<string, int> GetAllExternalLinks(string _KeyText)
        {
            var web = new HtmlWeb();
            var doc = web.Load(_KeyText);

            var listURLs = new List<String>();
            var nodeSingle = doc.DocumentNode.SelectSingleNode("//html");
            listURLs = Util.GetAllExternalLinksFromText(nodeSingle.OuterHtml);

            return Util.GroupListOfString(listURLs);
        }

        public static DataTable GridGetSource(string _KeyText, bool _isFilterStopWords, Util.eOptions _eOptions)
        {
            List<Dictionary<string, int>> listDict = new List<Dictionary<string, int>>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            DataTable _table = new DataTable();
            //List<string> listNumber = new List<string>();
            switch (_eOptions)
            {
                case Util.eOptions.GetAllWords:
                    dict = GetAllWordsInfo(_KeyText, _isFilterStopWords);
                    listDict.Add(dict);

                    //listNumber = dict.Keys.ToList();

                    _table  = Util.ListofDicToDataTable(listDict);
                    break;
                case Util.eOptions.GetAllMetas:
                    _table = Util.ToDataTable(GetAllMetaTagsInfo( _KeyText, _isFilterStopWords));
                    break;
                case Util.eOptions.GetAllExternalLink:
                    dict = GetAllExternalLinks(_KeyText);
                    listDict.Add(dict);
                    _table = Util.ListofDicToDataTable(listDict);
                    break;
            }
            
            
            return _table;
        }

        public static string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "DESC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "ASC";
                    break;
            }

            return newSortDirection;
        }


        public static bool CheckURLValid(string s, out Uri resultURI)
        {
            if (!Regex.IsMatch(s, @"^https?:\/\/", RegexOptions.IgnoreCase))
                s = "https://" + s;

            if (Uri.TryCreate(s, UriKind.Absolute, out resultURI))
                return (resultURI.Scheme == Uri.UriSchemeHttp ||
                        resultURI.Scheme == Uri.UriSchemeHttps);

            return false;
        }

        public static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                request.Timeout = 5000;
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

    }
}