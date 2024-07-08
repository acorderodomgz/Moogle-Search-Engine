This project consists of a search engine based on vector model Information Retrieval Systems. Given a Corpus (a set of texts where the search is going to be conducted) and a query (user inputted data), the algorithm will search for the best possible result using TF-IDF and vector similarity.

The Corpus must be files in .txt format and saved in the Content folder, which will initially be empty.

The project also includes a suggestion when misspelling a word. For example, if we type "doro" in English, the algorithm may suggest that the desired word is "door" since "doro" does not exist in English, but "door" can be found in the Corpus. This suggestion is made based on Levenshtein distance.

The project also includes several operators that will be described below:

(!) Non-existence operator: If the user's inputted query contains this operator in front of a word, then it will indicate that the user does not want this word to appear in the results.

Example: If the user inputs "!making doors", the word "making" will not appear in the search results if possible.

(^) Existence necessity operator: If the user's inputted query contains this operator in front of a word, then it will indicate that the user wants this word to appear in the results.

Example: If the user inputs "^making doors", the word "making" will appear in the search results if possible. In this case, the word "making" will have higher priority than the word "door".

(*) Weight amplifying operator: If the user inputs "*making doors", the word "making" will gain weight in the search ranking, meaning it will have a higher possibility of appearing in the results. This operator can be overloaded to further increase the ranking of a term.

Example: "**making" is more relevant for the search engine than "*making", and in turn, "*making" is more relevant than "making".

(~) Proximity operator: If the user inputs this operator between two words, then the search engine will prioritize the result where both words are as close together as possible.

Example: "making~doors".

Multiple operators can be used in a single search.

This project only returns one result, the best result, but it could be modified to return several results.
