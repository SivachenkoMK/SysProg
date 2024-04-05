Instead of creating XML configurations, how it's done in NUnit we can use code to define the parallel execution options and use dotnet CLI to adjust the running options.

To simply run tests we can use: ``dotnet test``.

If we want to enable parallel execution, we can use -m flag: ``dotnet test -m:10``. This option runs it with 10 parallel thread.

If we want to run it using categories, we can use --filter flag: ``dotnet test --filter "TestCategory=Word"``.