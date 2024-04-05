namespace Lab1Variant6;

public class AlphabetService
{
    public char[] GetConsonants()
    {
        return "BCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz".ToCharArray();
    }

    public char[] GetSeparators()
    {
        return "!@#$%^&*()_+-=`~?.,';][/\\ \r\n\t\f\a\b".ToCharArray();
    }
}