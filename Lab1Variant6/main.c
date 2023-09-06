#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main() {
    // Basis alphabet definition
    char consonants[] = "BCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz";
    char separators[] = "!@#$%^&*()_+-=`~?.,';][/\\ \r\n\t\f\a\b";

    FILE *file = fopen("input.txt", "r");
    if (file == NULL) {
        printf("Error during file read occurred.\n");
        return -1;
    }

    fseek(file, 0, SEEK_END);
    long file_size = ftell(file);
    fseek(file, 0, SEEK_SET);

    char *input = (char *)malloc(file_size);
    if (input == NULL) {
        fclose(file);
        printf("Memory allocation failed.\n");
        return -1;
    }

    fread(input, 1, file_size, file);
    fclose(file);

    int biggestAmountOfConsonantsInTheLeftSide = 0;

    char *word = strtok(input, separators);
    char *wordsWithMostConsonants[100];
    int wordCount = 0;

    while (word != NULL) {
        int localMaxAmountOfConsonants = 0;
        int amountOfConsonantsInCurrentWord = 0;
        int resetOnNextIteration = 0;

        for (int i = 0; word[i] != '\0'; i++) {
            char character = word[i];
            if (strchr(consonants, character) == NULL) {
                if (amountOfConsonantsInCurrentWord < localMaxAmountOfConsonants) {
                    amountOfConsonantsInCurrentWord = localMaxAmountOfConsonants;
                    resetOnNextIteration = 1;
                }
                continue;
            }

            if (resetOnNextIteration) {
                localMaxAmountOfConsonants = 0;
                resetOnNextIteration = 0;
            }

            localMaxAmountOfConsonants++;
        }

        if (biggestAmountOfConsonantsInTheLeftSide == amountOfConsonantsInCurrentWord) {
            wordsWithMostConsonants[wordCount] = strdup(word);
            wordCount++;
        } else if (biggestAmountOfConsonantsInTheLeftSide < amountOfConsonantsInCurrentWord) {
            biggestAmountOfConsonantsInTheLeftSide = amountOfConsonantsInCurrentWord;
            for (int i = 0; i < wordCount; i++) {
                free(wordsWithMostConsonants[i]);
            }
            wordCount = 0;
            wordsWithMostConsonants[wordCount] = strdup(word);
            wordCount++;
        }

        word = strtok(NULL, separators);
    }

    for (int i = 0; i < wordCount; i++) {
        printf("%s\n", wordsWithMostConsonants[i]);
        free(wordsWithMostConsonants[i]);
    }

    free(input);

    return 0;
}