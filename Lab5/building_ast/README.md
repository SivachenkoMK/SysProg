bison -dy arithmetics.y

flex arithmetics.l

gcc lex.yy.c y.tab.c -o calculator.exe

.\calculator.exe