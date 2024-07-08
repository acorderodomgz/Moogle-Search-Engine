using System;
using System.IO;
public static class Operators
{
    public static List<TFIDF> OperatorNotExist(List<TFIDF> TFIDF_Info, string word)
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        List<TFIDF> Definitive_List = new List<TFIDF>();
        for (int i = 0; i < TFIDF_Info.Count; i++)
        {
            if (!TFIDF_Info[i].wordsAndWeight.ContainsKey(word))
            {
                Definitive_List.Add(TFIDF_Info[i]);
            }
        }
        return Definitive_List;
    }

    public static List<TFIDF> OperatorNeedExist(List<TFIDF> TFIDF_Info, string word)
    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    {
        List<TFIDF> Definitive_List = new List<TFIDF>();
        for (int i = 0; i < TFIDF_Info.Count; i++)
        {
            if (TFIDF_Info[i].wordsAndWeight.ContainsKey(word))
            {
                Definitive_List.Add(TFIDF_Info[i]);
            }
        }
        return Definitive_List;
    }

    public static List<TFIDF> OperatorMoreImportance(List<TFIDF> TFIDF_Info, string word, int cant_operators)
    //******************************************************************************************************
    {
        double expression = cant_operators / 10;
        double final_weight = 0.0;
        for (int i = 0; i < TFIDF_Info.Count; i++)
        {
            if (TFIDF_Info[i].wordsAndWeight.ContainsKey(word))
            {
                final_weight = TFIDF_Info[i].wordsAndWeight[word] + expression;
                TFIDF_Info[i].wordsAndWeight[word] += final_weight;

            }
        }
        return TFIDF_Info;
    }
    #region OperatorNear
    public static List<TFIDF> OperatorNear(List<TFIDF> TFIDF_Info, string word1, string word2)
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Alt+¿
    {
        List<TFIDF> Definitive_List = new List<TFIDF>();
        for (int i = 0; i < TFIDF_Info.Count; i++)
        {
            if ((TFIDF_Info[i].wordsAndWeight.ContainsKey(word1)) && (TFIDF_Info[i].wordsAndWeight.ContainsKey(word2)))
            {
                Definitive_List.Add(TFIDF_Info[i]);
            }
        }
        return Definitive_List;
    }

    

    private static string GiveAContent(string bestDocument)
    {
        string s = "";
        foreach (string file in Directory.EnumerateFiles("Content", bestDocument))//este for each guarda el documento en un string
        {
            s = File.ReadAllText(file);
        }
        return s;
    }

    private static string MakeDistanceOperatorSnippet(string content, int posw1, int posw2)
    {
        string snippet = "";
        int start = Math.Min(posw1, posw2);
        int finish = Math.Max(posw1, posw2);
        for (int i = start; i < finish - 1; i++)
        {
            snippet += content[i];
        }
        snippet += AuxiliarMethodsAndTesters.CreateAWord(content, finish);
        return snippet;
    }
    public static string DistanceOperatorSnippet(List<TFIDF> TFIDF_Info, string word1, string word2)
    {
        string snippet = "";
        int distancew1 = 0;
        int distancew2 = 0;
        string temp_word = "";
        bool forCreateWord = false;
        string document_name = "";
        string content = "";
        int minorDistance = int.MaxValue;
        for (int i = 0; i < TFIDF_Info.Count; i++)
        {
            content = GiveAContent(TFIDF_Info[i].document_name);
            for (int j = 0; j < content.Length; j++)
            {
                if (char.IsLetterOrDigit(content[j]) && (forCreateWord))
                {
                    forCreateWord = false;
                    temp_word = AuxiliarMethodsAndTesters.CreateAWord(content, j);
                }
                else
                {
                    if (temp_word == word1)
                    {
                        distancew1 = j;
                    }
                    if (temp_word == word2)
                    {
                        distancew2 = j;
                    }
                }
                if ((distancew1 != 0) && (distancew2 != 0))
                {
                    if (minorDistance > (Math.Abs(distancew1 - distancew2)))
                    {
                        minorDistance = Math.Abs(distancew1 - distancew2);
                        document_name = TFIDF_Info[i].document_name;
                        snippet = MakeDistanceOperatorSnippet(content, distancew1, distancew2);
                    }
                }
            }
        }
        System.Console.WriteLine(document_name);
        return snippet;
    }
    #endregion
}
