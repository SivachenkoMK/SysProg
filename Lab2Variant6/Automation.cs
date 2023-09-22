namespace Lab2Variant6;

class Automation
{
    public int StartState { get; set; }

    public List<int> FinalStates { get; set; }

    public List<Transition> Transitions { get; set; }
    
    public Automation(int startState, List<int> finalStates, List<Transition> transitions)
    {
        StartState = startState;
        FinalStates = finalStates;
        Transitions = transitions;
    }
}