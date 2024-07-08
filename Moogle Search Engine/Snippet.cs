using System;
using System.IO;
public class Snippet
{
    private string best_word { get; }
    private string best_document { get; }
    public Snippet(string best_word, string best_document)
    {
        this.best_word = best_word;
        this.best_document = best_document;
    }
    private string GiveAContent()
    {
        string s = "";
        foreach (string file in Directory.EnumerateFiles("Content", best_document))//this for each save the document in string, este for each guarda el documento en un string
        {
            s = File.ReadAllText(file);
        }
        return s;
    }
    public string MakeASnippet()
    {
        int count = 0;
        string content = GiveAContent();
        string snippet = "";
        string s = "";
        string ss = "";
        string semi_snippet1 = " ";
        string semi_snippet2 = "";
        int n = 15;
        int m = 10;
        for (int i = 0; i < content.Length; i++)
        {
            if (i > 0)
                if (content[i - 1] == ' ')
                    if (content[i] == best_word[0])
                    {
                        s = AuxiliarMethodsAndTesters.CreateAWord(content, i);
                        s = s.ToLower();
                    }
            if (s == best_word)
            {
                for (int j = i; j < content.Length; j++)
                {
                    if (content[j] == ' ')
                    {
                        if (AuxiliarMethodsAndTesters.IsValidPosition(content, j + 1))
                        {
                            semi_snippet1 += AuxiliarMethodsAndTesters.CreateAWord(content, j + 1) + " ";
                            n--;
                        }
                    }
                    if (n == 0) break;
                }
                for (int j = i; j > 0; j--)
                {
                    if ((content[j] == ' ') && (count > 0))
                    {
                        ss = AuxiliarMethodsAndTesters.CreateAWord(content, j + 1) + " ";
                        semi_snippet2 = ss + semi_snippet2;
                        m--;
                    }
                    else
                    {
                        count++;
                    }
                    if (m == 0) break;
                }
                snippet = "... " + semi_snippet2;
                snippet += semi_snippet1 + " ...";
                break;
            }
        }
        return snippet;
    }
}