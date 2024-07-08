public class TFIDF
{
    public string document_name { get; }
    public Dictionary<string, double> wordsAndWeight { get; set; }
    //public List<string> query { get; }
    private int total_documents { get; set; }
    private List<CorpusInfo> InfoOFAllDocuments { get; }
    public TFIDF(List<CorpusInfo> InfoOFAllDocuments, string document_name)
    {
        //this.query = query;
        this.total_documents = InfoOFAllDocuments.Count;
        this.InfoOFAllDocuments = InfoOFAllDocuments;
        this.document_name = document_name;
        this.wordsAndWeight = FillWordsAndWeight();
    }

    private double TF(int freq)
    {
        if (freq == 0) return 0;
        return 1 + Math.Log10(freq);
    }

    private int TotalDocumentsContainingTheWord(string word)
    {
        int count = 0;
        for (int i = 0; i < InfoOFAllDocuments.Count; i++)
        {
            if (InfoOFAllDocuments[i].WordsAndFreq.ContainsKey(word))
                count++;
        }
        return count;
    }

    private double IDF(string word)
    {
        if (TotalDocumentsContainingTheWord(word) == 0) return 0;
        return Math.Log10(InfoOFAllDocuments.Count / TotalDocumentsContainingTheWord(word));
    }

    public double TF_IDF(string word)
    {
        int pos = 0;
        for (int i = 0; i < InfoOFAllDocuments.Count; i++)
        {
            if (InfoOFAllDocuments[i].document == document_name) pos = i;
        }
        if (InfoOFAllDocuments[pos].WordsAndFreq.ContainsKey(word))
            return TF(InfoOFAllDocuments[pos].WordsAndFreq[word]) * IDF(word);
        return 0;
    }

    public Dictionary<string, double> FillWordsAndWeight()
    {
        Dictionary<string, double> dicc = new Dictionary<string, double>();
        /*for (int i = 0; i < query.Count; i++)
        {
            if (!dicc.ContainsKey(query[i]))
                dicc.Add(query[i], TF_IDF(query[i]));
        }*/
        for (int i = 0; i < InfoOFAllDocuments.Count; i++)
        {
            foreach (var par in InfoOFAllDocuments[i].WordsAndFreq)
            {
                if(InfoOFAllDocuments[i].document==document_name)
                dicc.Add(par.Key, TF_IDF(par.Key));
            }
        }
        return dicc;
    }

    private List<double> QueryVector(List<string> query)
    {
        Dictionary<string, int> QueryWordsFrequency = new Dictionary<string, int>();
        for (int i = 0; i < query.Count; i++)
        {
            if (QueryWordsFrequency.ContainsKey(query[i])) QueryWordsFrequency[query[i]]++;
            else
            {
                QueryWordsFrequency.Add(query[i], 1);
            }
        }
        Dictionary<string, double> wordAndTF = new Dictionary<string, double>();
        foreach (var par in QueryWordsFrequency)
        {
            wordAndTF.Add(par.Key, TF(par.Value));
        }
        List<double> query_vector = new List<double>();
        List<string> listOfWords = new List<string>();
        foreach (var par in wordAndTF)
        {
            listOfWords.Add(par.Key);
        }
        for (int i = 0; i < listOfWords.Count; i++)
        {
            query_vector.Add(wordAndTF[listOfWords[i]] * IDF(listOfWords[i]));
        }
        return query_vector;
    }

    private List<double> Vector(List<string> query)
    {
        List<double> vector = new List<double>();
        foreach (var par in wordsAndWeight)
        {
            if (query.Contains(par.Key))
                vector.Add(par.Value);
        }
        return vector;
    }

    public double CosineSimilarity(List<string> query)
    {
        List<double> vector = Vector(query);
        List<double> query_vector = QueryVector(query);
        double two_vectors = 0;
        double distancev1 = 0;
        double distancev2 = 0;
        for (int i = 0; i < vector.Count; i++)
        {
            two_vectors += vector[i] * query_vector[i];
            distancev1 += Math.Pow(vector[i], 2);
            distancev2 += Math.Pow(query_vector[i], 2);
        }
        distancev1 = Math.Sqrt(distancev1);
        distancev2 = Math.Sqrt(distancev2);
        if (distancev1 == 0 || distancev2 == 0) return 0;
        return two_vectors / (distancev1 * distancev2);
    }
}
