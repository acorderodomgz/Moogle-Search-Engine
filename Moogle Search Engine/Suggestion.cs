public class Suggestion
{
    private static double LevenshteinDistance(string s, string t)
    {
        double percent = 0.0;
        int cost = 0;
        int m = s.Length;
        int n = t.Length;
        int[,] d = new int[m + 1, n + 1];
        if (n == 0) return m;
        if (m == 0) return n;
        for (int i = 0; i <= m; d[i, 0] = i++) ;
        for (int j = 0; j <= n; d[0, j] = j++) ;
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1,  
                              d[i, j - 1] + 1),                             
                              d[i - 1, j - 1] + cost);                    
            }
        }
        if (s.Length > t.Length)
            percent = ((double)d[m, n] / (double)s.Length);
        else
            percent = ((double)d[m, n] / (double)t.Length);
        return d[m, n];
    }

    public static string Levenshtein(List<CorpusInfo> ci, string word)
    {
        string best_word = "";
        double min_percent = double.MaxValue;
        for (int i = 0; i < ci.Count; i++)
        {
            foreach (var par in ci[i].WordsAndFreq)
            {
                double percent = LevenshteinDistance(word, par.Key);
                if (percent < min_percent)
                {
                    min_percent = percent;
                    best_word = par.Key;
                }
            }
        }
        return best_word;
    }

}