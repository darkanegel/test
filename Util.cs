using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SimpleSEOAnalyser
{
    public class Util
    {
        public enum eOptions
        {
            GetAllWords,
            GetAllMetas,
            GetAllExternalLink
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable ListofDicToDataTable(List<Dictionary<string, int>> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            //var columnNames = list.SelectMany(dict => dict.Keys).Distinct();
            //result.Columns.AddRange(columnNames.Select(c => new DataColumn(c)).ToArray());
            result.Columns.Add("Keys");
            result.Columns.Add("Value");

            foreach (Dictionary<string, int> item in list)
            {
                foreach (var key in item.Keys)
                {
                    var row = result.NewRow();
                    row["Keys"] = key;
                    row["Value"] = item[key];
                    result.Rows.Add(row);
                }
            }

            return result;
        }


        public static List<string> GetAllExternalLinksFromText(string text)
        {
            List<string> listURLs = new List<string>();
            MatchCollection mc = Regex.Matches(text, FilterFormat.GetAllLinks);
            foreach (Match match in mc)
            {
                listURLs.Add(match.Value);
            }

            return listURLs;
        }

        public static List<string> FilterStopWords(List<string> Words, string stopWordsPath)
        {
            //var jsonText = File.ReadAllText(stopWordsPath);
            var stopWords = JsonConvert.DeserializeObject<IList<string>>(stopWordsPath);

            var matches = Words.Where(word => !stopWords.Contains(word));

            return matches.ToList<string>();
        }

        public static Dictionary<string, int> GroupListOfString(List<string> listofString)
        {
            return listofString.GroupBy(word => word)
               .ToDictionary(group => group.Key, group => group.Count());
        }

        public static List<string> GetAllWords(string text)
        {
            var words = SplitSentenceIntoWords(text.ToLower(), 1);

            List<string> modifiedWords = new List<string>();

            foreach (var word in words)
            {
                var stripedWords = word;

                if (!string.IsNullOrWhiteSpace(stripedWords) &&
                    Regex.IsMatch(stripedWords, "^[a-z\u00c0-\u00f6]+$", RegexOptions.IgnoreCase) &&
                    stripedWords.Length > 1)
                {
                    modifiedWords.Add(stripedWords);
                }

            }

            return modifiedWords.ToList<string>();

        }

        public static IEnumerable<string> SplitSentenceIntoWords(string sentence, int wordMinLength)
        {
            var word = new StringBuilder();
            foreach (var chr in sentence)
            {
                if (Char.IsPunctuation(chr) || Char.IsSeparator(chr) || Char.IsWhiteSpace(chr))
                {
                    if (word.Length > wordMinLength)
                    {
                        yield return word.ToString();
                        word.Clear();
                    }
                }
                else
                {
                    word.Append(chr);
                }
            }

            if (word.Length > wordMinLength)
            {
                yield return word.ToString();
            }
        }
    }
}