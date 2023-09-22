namespace Lab2Variant6;

class Transition
{
    public int From { get; set; }

    public char TransitionChar { get; set; }
    
    public int To { get; set; }

    public Transition(int from, char transition, int to)
    {
        From = from;
        TransitionChar = transition;
        To = to;
    }
}