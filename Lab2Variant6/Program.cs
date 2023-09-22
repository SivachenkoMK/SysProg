using Lab2Variant6;

var data = File.ReadAllLines("transitions.txt");

var startState = int.Parse(data[0]);
var finalStates = new List<int> { int.Parse(data[1]) };
var transitions = new List<Transition>();

for (var i = 2; i < data.Length; i++)
{
    var parts = data[i].Split(new[] { ' ', '-', '>', ' ' }, StringSplitOptions.RemoveEmptyEntries);
    var from = int.Parse(parts[0]);
    var transition = parts[1][0];
    var to = int.Parse(parts[2]);

    transitions.Add(new Transition(from, transition, to));
}

var automation = new Automation(startState, finalStates, transitions);

Console.WriteLine(CheckForWords("b", "b"));
return;

bool IsWordAccepted(string word)
{
    var currentState = automation.StartState;
    foreach (char ch in word)
    {
        var transitionExists = automation.Transitions.Exists(t => t.From == currentState && t.TransitionChar == ch);
        if (!transitionExists) return false;

        var transition = automation.Transitions.Find(t => t.From == currentState && t.TransitionChar == ch);
        if (transition == null)
            return false;
        currentState = transition.To;
    }
    return automation.FinalStates.Contains(currentState);
}

bool CheckForWords(string w1, string w2)
{
    for (int len = 1; len <= 8; ++len)
    {
        for (int i = 0; i < 1 << len; ++i)
        {
            string w0 = "";
            for (int j = 0; j < len; ++j)
            {
                if ((i & (1 << j)) != 0) w0 += 'b';
                else w0 += 'a';
            }
            if (IsWordAccepted(w1 + w0 + w2)) return true;
        }
    }
    return false;
}
