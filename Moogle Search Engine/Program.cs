using System;
using System.IO;

#region PreCharged
static string GiveMeAContent(FileInfo F)
{
    return File.ReadAllText(F.FullName);
}
string contents = "";
DirectoryInfo d = new DirectoryInfo(@"Content");
FileInfo[] Files = d.GetFiles("*.txt");
foreach (FileInfo file in Files)
{
    contents = GiveMeAContent(file);
}
List<CorpusInfo> InfoOFAllDocuments = new List<CorpusInfo>();
for (int i = 0; i < Files.Length; i++)
{
    CorpusInfo data = new CorpusInfo(Files[i]);
    InfoOFAllDocuments.Add(data);
}
int c = 0;
int total = Files.Length;
List<TFIDF> TFIDF_Info = new List<TFIDF>();
for (int i = 0; i < InfoOFAllDocuments.Count; i++)
{
    TFIDF t = new TFIDF(InfoOFAllDocuments, InfoOFAllDocuments[i].document);
    TFIDF_Info.Add(t);
    c++;
    System.Console.WriteLine("Loading documents, please wait..." + c + "/" + total);
}
#endregion

#region Query Work
static int CountingOperator(int pos, string query)
{
    int count = 0;
    for (int i = pos; i < query.Length; i++)
    {
        if (query[i] == '*') count++;
        else break;
    }
    return count;
}

while (true)
{

    string query = "";
    Console.WriteLine("Enter Your Search: ");
    query = Console.ReadLine();
    List<string> Query = new List<string>();
    string str = "";
    string str_temp = "";
    int operators_importance = 0;
    int rare_count = 0;
    for (int i = 0; i < query.Length; i++)
    {
        if ((query[i] == '!') || (query[i] == '^') || (query[i] == '*'))
        {
            for (int j = i + 1; j < query.Length; j++)
            {
                if (query[j] == ' ') break;
                if (char.IsLetterOrDigit(query[j])) str_temp += query[j];
            }
            if ((query[i] == '*') && (rare_count == 0))
            {
                System.Console.WriteLine(str_temp);
                operators_importance = CountingOperator(i, query);
                OperatorQuery('*', str_temp);
                rare_count++;
            }
            switch (query[i])
            {
                case '!': OperatorQuery('!', str_temp); break;
                case '^': OperatorQuery('!', str_temp); break;
            }
            str_temp = "";
        }
        if (query[i] == '~')
        {
            string word2 = AuxiliarMethodsAndTesters.CreateAWord(query, i);
            string word1 = "";
            for (int j = i - 1; j < -1; j--)
            {
                if (!char.IsLetterOrDigit(query[j]) || j == 0)
                {
                    word1 = AuxiliarMethodsAndTesters.CreateAWord(query, j);
                    break;
                }
            }
            DistanceOperator(word1, word2);
        }
        if (char.IsLetterOrDigit(query[i])) str += query[i];
        else
        {
            if ((query[i] == '!') || (query[i] == '^') || (query[i] == '*'))
                Query.Add(str.ToLower());
            str = "";
        }
        if (i == query.Length - 1)
        {
            Query.Add(str.ToLower());
            str = "";
        }
    }

    void OperatorQuery(char symbol, string word)
    {
        switch (symbol)
        {
            case '!': TFIDF_Info = Operators.OperatorNotExist(TFIDF_Info, word); break;
            case '^': TFIDF_Info = Operators.OperatorNeedExist(TFIDF_Info, word); break;
            case '*': TFIDF_Info = Operators.OperatorMoreImportance(TFIDF_Info, word, operators_importance); break;
        }
    }

    bool isDistanceOperator = false;
    string distanceOperatorSnippet = "";
    void DistanceOperator(string word1, string word2)
      {
          TFIDF_Info = Operators.OperatorNear(TFIDF_Info, word1, word2);
          isDistanceOperator = true;
          distanceOperatorSnippet = Operators.DistanceOperatorSnippet(TFIDF_Info, word1, word2);
      }
    #endregion

    #region Suggestions
    bool exist = false;
    for (int j = 0; j < Query.Count; j++)
    {
        for (int i = 0; i < InfoOFAllDocuments.Count; i++)
        {
            foreach (var item in InfoOFAllDocuments[i].WordsAndFreq)
            {
                if (Query[j] == item.Key)
                {
                    exist = true;
                    break;
                }
            }
            if (exist) break;
        }
        if (!exist)
        {
            System.Console.WriteLine("¿Maybe you wanted to say: " + Suggestion.Levenshtein(InfoOFAllDocuments, Query[j]) + "?");
            Query[j] = Suggestion.Levenshtein(InfoOFAllDocuments, Query[j]);
            break;
        }
    }
    #endregion

    #region CosineSimilarity
    SearchResult[] allResults = new SearchResult[5];
    string bestSnippet2 = "";
    string bestDocument2 = "";
    string bestSnippet3 = "";
    if (!isDistanceOperator)
    {
        string BestWord(TFIDF t)
        {
            double max = double.MinValue;
            string word = "";
            foreach (var par in t.wordsAndWeight)
            {
                if (Query.Contains(par.Key))
                    if (par.Value > max)
                    {
                        max = par.Value;
                        word = par.Key;
                    }
            }
            return word;
        }
        double max = double.MinValue;
        double cos = 0;
        string best_document = "";
        string best_word = "";
        for (int i = 0; i < TFIDF_Info.Count; i++)
        {
            cos = TFIDF_Info[i].CosineSimilarity(Query);
            if (cos > max)
            {
                max = cos;
                best_document = TFIDF_Info[i].document_name;
                best_word = BestWord(TFIDF_Info[i]);
                SearchResult[] specificResult = AuxiliarMethodsAndTesters.RunArray(allResults);
                allResults = specificResult;
                Snippet s = new Snippet(best_word, best_document);
                allResults[0] = new SearchResult(best_document, s.MakeASnippet(), max);
            }
        }
        /*Console.WriteLine(best_document);
        System.Console.WriteLine(best_word);
        Snippet s = new Snippet(best_word, best_document);
        System.Console.WriteLine(s.MakeASnippet());*/
        for (int i = 0; i < allResults.Length; i++)
        {
            if (allResults[i] != null)
                SearchResult.PrintSearchResult(allResults[i]);
        }
    }
    else
    {
        System.Console.WriteLine(distanceOperatorSnippet);
    }
    #endregion
}