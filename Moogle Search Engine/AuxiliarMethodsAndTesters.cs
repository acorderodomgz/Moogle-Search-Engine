public static class AuxiliarMethodsAndTesters
{
    public static void PrintList(List<string> list)
    {
        Console.WriteLine();
        Console.Write("{ ");
        for (int i = 0; i < list.Count; i++)
        {
            Console.Write(" " + list[i] + " ,");
        }
        Console.Write(" }");
    }
    public static SearchResult[] RunArray(SearchResult[] sr)
    {
        SearchResult[] newSr = new SearchResult[sr.Length];
        for (int i = sr.Length - 1; i > 0; i--)
        {
            newSr[i] = sr[i - 1];
        }
        return newSr;
    }
    public static void PrintDictionary(Dictionary<string, double> dicc)
    {
        foreach (var par in dicc)
            Console.WriteLine(par.Key + " " + par.Value);
    }
    public static Dictionary<string, int> FillList(string content)
    {
        Dictionary<string, int> D = new Dictionary<string, int>();
        string s = "";
        for (int i = 0; i < content.Length; i++)
            if (char.IsLetterOrDigit(content[i])) s += content[i];
            else
            {
                s = s.ToLower();
                if (!D.TryAdd(s, 1)) D[s]++;
                s = "";
            }
        return D;
    }
    public static bool IsValidPosition(string s, int i)
    {
        return (i < 0 || i >= s.Length) ? false : true;
    }
    public static string CreateAWord(string content, int pos)
    {
        string s = "";
        for (int i = pos; i < content.Length; i++)
        {
            if (char.IsLetterOrDigit(content[i])) s += content[i];
            else
                break;
        }
        return s;
    }
    public static string TextFragment(string content, int posi, int posf)
    {
        string s = "";
        if (posf >= posi)
        {
            for (int i = posi; i <= posf; i++)
            {
                s += content[i];
            }
        }
        else
        {
            for (int i = posf; i < posi; i++)
            {
                s += content[i];
            }
        }
        return s;
    }
}