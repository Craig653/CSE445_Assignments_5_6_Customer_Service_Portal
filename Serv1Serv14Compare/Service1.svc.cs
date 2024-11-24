/*
    Author: Christopher Angulo
    Description: This file contains the implementation of WSDL service. It contains the logic to compare the output
    of WSDL service 1 and REST service 14. Therefore, this service is using both service 1 and service 14 to compare
    if both services have completed operations on a given URL.
    Class: ASU CSE-445 - Service Oriented Computing - Fall 2024
    Professor: Yinong Chen
    Last Date Modified: 10/20/2024
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Serv1Serv14Compare
{
    public class Service1 : IService1
    {
        public static readonly HashSet<string> CommonStopWords = new HashSet<string>
        {
            "a", "about", "above", "abroad", "across", "after", "afterwards", "again", "against", "ain't", "all", "almost",
"alone", "along", "already", "also", "although", "always", "am", "amid", "amidst", "among", "amongst", "an",
"and", "another", "any", "anybody", "anyhow", "anyone", "anything", "anywhere", "are", "aren't", "around",
"as", "aside", "at", "away", "b", "back", "backward", "backwards", "be", "became", "because", "become",
"becomes", "becoming", "been", "before", "beforehand", "behind", "being", "below", "beneath", "beside",
"besides", "between", "beyond", "both", "but", "by", "c", "can", "can't", "cannot", "can't", "caption",
"cause", "certain", "certainly", "changes", "clearly", "c'mon", "come", "comes", "concerning", "consequently",
"consider", "considering", "could", "couldn't", "d", "despite", "did", "didn't", "do", "does", "doesn't",
"doing", "don't", "done", "down", "downward", "downwards", "during", "e", "each", "either", "else", "elsewhere",
"enough", "entirely", "especially", "et", "etc", "even", "ever", "every", "everybody", "everyone", "everything",
"everywhere", "except", "f", "fairly", "far", "farther", "few", "fifth", "first", "five", "followed", "following",
"follows", "for", "former", "formerly", "forth", "four", "from", "further", "furthermore", "g", "get", "gets",
"getting", "given", "gives", "go", "goes", "going", "gone", "got", "gotten", "greetings", "h", "had", "hadn't",
"half", "happens", "hardly", "has", "hasn't", "have", "haven't", "having", "he", "he'd", "he'll", "he's",
"hello", "help", "hence", "her", "here", "here's", "hereafter", "hereby", "herein", "hereupon", "hers",
"herself", "hi", "him", "himself", "his", "hither", "hopefully", "how", "how's", "however", "i", "i'd",
"i'll", "i'm", "i've", "if", "ignored", "immediate", "in", "inasmuch", "inc", "indeed", "indicate", "indicated",
"indicates", "inner", "insofar", "instead", "into", "inward", "is", "isn't", "it", "it's", "its", "itself",
"j", "just", "k", "keep", "keeps", "kept", "know", "knows", "known", "l", "last", "lately", "later", "latter",
"latterly", "least", "less", "lest", "let", "let's", "like", "liked", "likely", "likewise", "little", "look",
"looking", "looks", "low", "lower", "ltd", "m", "made", "mainly", "make", "makes", "many", "may", "maybe",
"me", "mean", "meanwhile", "merely", "might", "more", "moreover", "most", "mostly", "much", "must", "mustn't",
"my", "myself", "n", "name", "namely", "nd", "near", "nearly", "necessary", "need", "needs", "neither",
"never", "nevertheless", "new", "next", "nine", "no", "nobody", "non", "none", "nonetheless", "noone",
"nor", "normally", "not", "nothing", "novel", "now", "nowhere", "o", "obviously", "of", "off", "often",
"oh", "ok", "okay", "on", "once", "one", "ones", "only", "onto", "or", "other", "others", "otherwise",
"ought", "our", "ours", "ourselves", "out", "outside", "over", "overall", "own", "p", "particular",
"particularly", "per", "perhaps", "placed", "please", "plus", "possible", "presumably", "probably",
"provides", "q", "que", "quite", "qv", "r", "rather", "rd", "re", "really", "reasonably", "regarding",
"regardless", "regards", "relatively", "respectively", "right", "s", "said", "same", "saw", "say", "saying",
"says", "second", "secondly", "see", "seeing", "seem", "seemed", "seeming", "seems", "seen", "self",
"selves", "sensible", "sent", "serious", "seriously", "seven", "several", "shall", "shan't", "she",
"she'd", "she'll", "she's", "should", "shouldn't", "since", "six", "so", "some", "somebody", "somehow",
"someone", "something", "sometime", "sometimes", "somewhat", "somewhere", "soon", "sorry", "specified",
"specify", "specifying", "still", "sub", "such", "sup", "sure", "t", "take", "taken", "taking", "tell",
"tends", "th", "than", "thank", "thanks", "thanx", "that", "that's", "thats", "the", "their", "theirs",
"them", "themselves", "then", "thence", "there", "there's", "thereafter", "thereby", "therefore",
"therein", "theres", "thereupon", "these", "they", "they'd", "they'll", "they're", "they've", "think",
"third", "this", "thorough", "thoroughly", "those", "though", "three", "through", "throughout",
"thru", "thus", "to", "together", "too", "took", "toward", "towards", "tried", "tries", "truly", "try",
"trying", "twice", "two", "u", "un", "under", "underneath", "undoing", "unfortunately", "unless",
"unlike", "unlikely", "until", "unto", "up", "upon", "us", "use", "used", "useful", "uses", "using",
"usually", "v", "value", "various", "versus", "very", "via", "viz", "vs", "w", "want", "wants",
"was", "wasn't", "way", "we", "we'd", "we'll", "we're", "we've", "welcome", "well", "went", "were",
"weren't", "what", "what's", "whatever", "when", "when's", "whence", "whenever", "where", "where's",
"whereafter", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while",
"whilst", "whither", "who", "who's", "whoever", "whole", "whom", "whomever", "whose", "why", "why's",
"will", "willing", "wish", "with", "within", "without", "wonder", "won't", "would", "wouldn't", "x",
"y", "yes", "yet", "you", "you'd", "you'll", "you're", "you've", "your", "yours", "yourself",
"yourselves", "z", "zero"
        };

        public bool MostCommon() // compare if a given URL has been used by both Service 1 and Service 14
        {
            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/"); // path to folder
            string localFile = Path.Combine(localDir, "TicketsDatabase.xml"); // path to file

            // Ensure the directory exists
            if (!Directory.Exists(localDir))
            {
                Directory.CreateDirectory(localDir);
            }

            // Ensure the file exists
            if (!File.Exists(localFile))
            {
                File.Create(localFile).Dispose(); // Create the file and dispose to release the handle
            }

            XDocument xmlDoc = XDocument.Load(localFile);

            // Iterate through each <Ticket> element
            foreach (var ticket in xmlDoc.Descendants("Ticket"))
            {
                var textElement = ticket.Element("Text");
                if (textElement != null)
                {
                    string text = textElement.Value;
                    string mostCommonWord = GetMostCommonWord(text);

                    if (mostCommonWord == null)
                    {
                        ticket.SetAttributeValue("Category", "NONE");
                    }
                    else
                    {
                        // Update the Category attribute of the parent <Ticket> element
                        ticket.SetAttributeValue("Category", mostCommonWord);
                    }
                }
            }

            // Save the updated XML document
            xmlDoc.Save(localFile);

            return true; // Implement the actual comparison logic here
        }


        // Helper method to find the most common word in a given text
        private string GetMostCommonWord(string text)
        {
            var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var words = Regex.Split(text, @"\W+");

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word) || CommonStopWords.Contains(word.ToLower())) continue;

                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
                else
                {
                    wordCounts[word] = 1;
                }
            }

            // Find the maximum frequency
            int maxCount = wordCounts.Values.Max();

            // Get all words with the maximum frequency
            var mostCommonWords = wordCounts.Where(kvp => kvp.Value == maxCount).Select(kvp => kvp.Key).ToList();

            // Return the first word with the maximum frequency
            return mostCommonWords.FirstOrDefault();
        }

        public string GetCurrentXML()
        {
            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/"); // path to folder
            //string localFile = Path.Combine(localDir, "TicketsDatabase.xml"); // path to file
            string localFile = Path.Combine(HttpRuntime.AppDomainAppPath);
            localFile = localFile.Replace("CommonCategorySearch", "CSE445_Assignments_4_5_Customer_Service_Portal") + "/App_Data/TicketsDatabase.xml";

            // Ensure the directory exists
            if (!Directory.Exists(localDir))
            {
                Directory.CreateDirectory(localDir);
            }

            // Ensure the file exists
            if (!File.Exists(localFile))
            {
                File.Create(localFile).Dispose(); // Create the file and dispose to release the handle
            }

            XDocument xmlDoc = XDocument.Load(localFile);

            return xmlDoc.ToString();
        }

        public string GetData(int value) // this was created by default, I'm just going to leave it alone
        {
            return string.Format("You entered: {0}", value);
        }

        // this was created by default, I'm just going to leave it alone
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

    }
}
