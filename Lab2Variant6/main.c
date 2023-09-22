#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

typedef struct Transition {
    int from;
    char transitionChar;
    int to;
} Transition;

typedef struct Automation {
    int startState;
    int* finalStates;
    int numFinalStates;
    Transition* transitions;
    int numTransitions;
} Automation;

bool isWordAccepted(Automation* automation, char* word) {
    int currentState = automation->startState;
    for (int i = 0; word[i] != '\0'; i++) {
        bool transitionExists = false;
        Transition* transition = NULL;
        for (int j = 0; j < automation->numTransitions; j++) {
            if (automation->transitions[j].from == currentState && automation->transitions[j].transitionChar == word[i]) {
                transitionExists = true;
                transition = &automation->transitions[j];
                break;
            }
        }
        if (!transitionExists) return false;
        if (transition == NULL) return false;
        currentState = transition->to;
    }
    for (int i = 0; i < automation->numFinalStates; i++) {
        if (automation->finalStates[i] == currentState) return true;
    }
    return false;
}

bool checkForWords(Automation* automation, char* w1, char* w2) {
    for (int len = 1; len <= 8; ++len) {
        for (int i = 0; i < 1 << len; ++i) {
            char w0[9] = "";
            for (int j = 0; j < len; ++j) {
                if ((i & (1 << j)) != 0) strcat(w0, "b");
                else strcat(w0, "a");
            }
            char word[20] = "";
            strcat(word, w1);
            strcat(word, w0);
            strcat(word, w2);
            if (isWordAccepted(automation, word)) return true;
        }
    }
    return false;
}

int main() {
    FILE *file = fopen("transitions.txt", "r");
    if (file == NULL){
        printf("Could not open file transitions.txt");
        return 1;
    }

    int startState;
    fscanf(file, "%d\n", &startState);

    int numFinalStates = 1;
    int* finalStates = malloc(numFinalStates * sizeof(int));
    fscanf(file, "%d\n", &finalStates[0]);

    int numTransitions = 10;
    Transition* transitions = malloc(numTransitions * sizeof(Transition));

    for(int i=0; i<numTransitions; i++){
        fscanf(file, "%d -%c> %d\n", &transitions[i].from, &transitions[i].transitionChar, &transitions[i].to);
    }

    Automation automation = {startState, finalStates, numFinalStates, transitions, numTransitions};

    printf("%s\n", checkForWords(&automation, "a", "c") ? "true" : "false");

    free(finalStates);
    free(transitions);

    fclose(file);

    return 0;
}