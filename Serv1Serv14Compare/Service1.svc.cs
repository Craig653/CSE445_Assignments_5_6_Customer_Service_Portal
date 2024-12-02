/*
    Author: Christopher Angulo
    Description: This file contains the implementation of WSDL service that updates the TicketsDatabase.xml file with the most
        common word in the Text element of each Ticket element. This way the Agent can easily view Tickets by category.
    Class: ASU CSE-445 - Service Oriented Computing - Fall 2024
    Professor: Yinong Chen
    Last Date Modified: 12/1/2024
 */

// libraries
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

// keeping an older name of this project for compatibility
namespace Serv1Serv14Compare
{
    public class Service1 : IService1
    {
        // common stop words to ignore as hashset
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

        // get the most common word in the XML Text element
        public bool MostCommon()
        {
            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/"); // path to folder

            // path to Tickets XML file
            string localFile = Path.Combine(HttpRuntime.AppDomainAppPath, "../CSE445_Assignments_4_5_Customer_Service_Portal/App_Data/TicketsDatabase.xml");

            // ensure the directory exists
            if (!Directory.Exists(localDir))
            {
                Directory.CreateDirectory(localDir);
            }

            // ensure the Tickets XML file exists
            if (!File.Exists(localFile))
            {
                using (var writer = new StreamWriter(localFile))
                {
                    writer.WriteLine("<Tickets></Tickets>"); // initialize with root element
                }
            }

            XDocument xmlDoc = XDocument.Load(localFile); // load the XML tickets document

            foreach (var ticket in xmlDoc.Descendants("Ticket")) // iterate through each Ticket element
            {
                var textElement = ticket.Element("Text"); // get the Text element (this is the issue summary)
                if (textElement != null) // if XML Text element exists
                {
                    string text = textElement.Value; // get the XML Text element value
                    string mostCommonWord = GetMostCommonWord(text); // get the most common word in the Text element value (issue summary)

                    if (mostCommonWord == null) // if no common word found
                    {
                        ticket.SetAttributeValue("Category", "NONE"); // set XML Category attribute to "NONE"
                    }
                    else
                    {
                        ticket.SetAttributeValue("Category", mostCommonWord); // update the Category attribute of the XML Ticket element
                    }
                }
            }

            xmlDoc.Save(localFile); // save the updated XML document
            return true; // no need to return anything but returning bool might be useful for future method success checks
        }



        // helper method to find the most common word in a given string
        private string GetMostCommonWord(string text)
        {
            var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase); // case-insensitive dictionary
            var words = Regex.Split(text, @"\W+"); // split the text into words

            foreach (var word in words) // iterate through each word
            {
                // skip empty strings and common stop words
                if (string.IsNullOrWhiteSpace(word) || CommonStopWords.Contains(word.ToLower())) continue;

                // if the word is already in the dictionary increment this word's count
                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
                // if the word is not in the dictionary add it with a count of 1
                else
                {
                    wordCounts[word] = 1;
                }
            }

            int maxCount = wordCounts.Values.Max(); // find the word with the maximum count

            // get all words with the maximum count
            // (so if there are multiple words with the same max count we can return the first one)
            var mostCommonWords = wordCounts.Where(kvp => kvp.Value == maxCount).Select(kvp => kvp.Key).ToList();

            return mostCommonWords.FirstOrDefault(); // return the first word with the maximum count
        }

        // this is only for the TryIt page to see the XML Tickets file contents to verify it works
        public string GetCurrentXML()
        {
            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/"); // path to folder
            string localFile = Path.Combine(HttpRuntime.AppDomainAppPath);

            // path to XML Tickets file
            localFile = localFile.Replace("CommonCategorySearch", "CSE445_Assignments_4_5_Customer_Service_Portal") + "/App_Data/TicketsDatabase.xml";

            if (!Directory.Exists(localDir)) // ensure the directory exists
            {
                Directory.CreateDirectory(localDir);
            }

            if (!File.Exists(localFile)) // ensure the XML Tickets file exists
            {
                File.Create(localFile).Dispose(); // create the file and dispose to release the handle
            }

            XDocument xmlDoc = XDocument.Load(localFile); // load the XML Tickets file

            return xmlDoc.ToString(); // return the XML file contents as a string
        }

        // this was created by default, I'm just going to leave it alone
        public string GetData(int value)
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
