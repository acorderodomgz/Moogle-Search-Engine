public class SearchResult
{
    public string document_name { get; set;}
    public string snippet { get; set;}
    public double score { get; set;}
    public SearchResult(string document_name, string snippet, double score)
    {
        this.document_name = document_name;
        this.snippet = snippet;
        this.score = score;
    }
    public static void PrintSearchResult(SearchResult sr)
    {
        System.Console.WriteLine(sr.document_name);
        System.Console.WriteLine(sr.snippet);
    }


}