using System.Text;

namespace SeleniumRecipesCSharp.ch20_programming;

public static class CSVReader
{
    const bool ignoreFirstLineDefault = false;

    public static IEnumerable<List<string>> FromFile(string fileName, bool ignoreFirstLine = ignoreFirstLineDefault)
    {
        using StreamReader rdr = new(fileName);
        return FromReader(rdr, ignoreFirstLine);
    }

    public static IEnumerable<List<string>> FromReader(StreamReader csv, bool ignoreFirstLine)
    {
        if (ignoreFirstLine) { csv.ReadLine(); }

        List<string> result = new();
        StringBuilder curValue = new();
        char c = (char)csv.Read();
        while (csv.Peek() != -1)
        {
            switch (c)
            {
                case ',': //empty field
                    result.Add("");
                    c = (char)csv.Read();
                    break;
                case '"': //qualified text
                case '\'':
                    char q = c;
                    c = (char)csv.Read();
                    bool inQuotes = true;
                    while (inQuotes && csv.Peek() != -1)
                    {
                        if (c == q)
                        {
                            c = (char)csv.Read();
                            if (c != q)
                            {
                                inQuotes = false;
                            }
                        }

                        if (inQuotes)
                        {
                            curValue.Append(c);
                            c = (char)csv.Read();
                        }
                    }
                    result.Add(curValue.ToString());
                    curValue = new StringBuilder();
                    if (c == ',')
                    {
                        c = (char)csv.Read(); // either ',', newline, or endofstream
                    }

                    break;
                case '\n': //end of the record
                case '\r':
                    //potential bug here depending on what your line breaks look like
                    if (result.Count > 0) // don't return empty records
                    {
                        yield return result;
                        result = new List<string>();
                    }
                    c = (char)csv.Read();
                    break;
                default: //normal unqualified text
                    while (c != ',' && c != '\r' && c != '\n' && csv.Peek() != -1)
                    {
                        curValue.Append(c);
                        c = (char)csv.Read();
                    }
                    result.Add(curValue.ToString());
                    curValue = new StringBuilder();
                    if (c == ',')
                    {
                        c = (char)csv.Read(); //either ',', newline, or endofstream
                    }

                    break;
            }
        }
        if (curValue.Length > 0) //potential bug: I don't want to skip on a empty column in the last record if a caller really expects it to be there
        {
            result.Add(curValue.ToString());
        }

        if (result.Count > 0)
        {
            yield return result;
        }
    }
}