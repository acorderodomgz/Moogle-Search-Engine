public class CorpusInfo
{
    public string document;
    /*
    Save a file name
    Guarda el nombre de un documento
    */
    public Dictionary<string, int> WordsAndFreq {get;}
    /* 
    This dictionary save a word from a document with key and value, 
    the value corresponds to the number of times it is repeated in the document
    Este diccionario guarda una palabra de un documento en su key y a su value le corresponde a la cantidad de veces
    que se repite en el documento document
    */

    public CorpusInfo(FileInfo F)
    {
        document = F.Name;
        WordsAndFreq = FillList(File.ReadAllText(F.FullName));
    }
    public void Print()
    {
        foreach (var par in WordsAndFreq)
            Console.WriteLine(par.Key + " " + (par.Value));
    }
    /*
    Print the values from a dictionary
    Imprime todos los valores de un diccionario
    */
    public Dictionary<string, int> FillList(string content)
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
    /*
    assigns the dictionary information form a specific document
    asigna todos la informacion que guarda el diccionario de un documento en específico
    */
}