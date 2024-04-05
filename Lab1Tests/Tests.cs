using Lab1Variant6;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Lab1Tests;

[Parallelizable]
public class Tests
{
    private const string Word = nameof(Word);
    private const string InputReader = nameof(InputReader);
    private const string AlphabetService = nameof(AlphabetService);
    
    private AlphabetService _alphabetService = default!;
    private InputReader _inputReader = default!;
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _alphabetService = new AlphabetService();
    }

    [SetUp]
    public void Setup()
    {
        _inputReader = new InputReader();
    }
    
    [Test]
    [Category(InputReader)]
    public void InputReaderTestOnFail()
    {
        // Arrange
        const string fileName = "no_such_file.txt";
        // Act
        var file = _inputReader.TryGetInput(fileName);
        // Assert
        Assert.That(file.IsFailed, Is.True);
    }

    [Test]
    [Category(InputReader)]
    public void InputReaderTestOnEmpty()
    {
        // Arrange
        const string fileName = "emptyInput.txt";
        // Act
        var file = _inputReader.TryGetInput(fileName);
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(file.IsSuccess, Is.True);
            Assert.That(file.Value, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    [Category(InputReader)]
    public void InputReaderTestOnValid()
    {
        // Arrange
        const string fileName = "simpleTest.txt";
        const string expectedInput = "This is a simple test file";
        // Act
        var file = _inputReader.TryGetInput(fileName);
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(file.IsSuccess, Is.True);
            Assert.That(file.Value, Is.EqualTo(expectedInput));
        });
    }
    
    
    [Category(InputReader)]
    [TestCase(null, ExpectedResult = new string[0])]
    [TestCase("", ExpectedResult = new string[0])]
    [TestCase("This is a simple test file", ExpectedResult = new[] { "This", "is", "a", "simple", "test", "file" })]
    [TestCase("This is a... Simple test file", ExpectedResult = new[] { "This", "is", "a", "Simple", "test", "file" })]
    public string[] InputReaderWordsTestWithTestCase(string input)
    {
        // Arrange
        var separators = _alphabetService.GetSeparators();
        
        // Act
        var words = _inputReader.GetWords(input, separators);
        
        // TestCase Assert
        return words;
    }

    [Test]
    [Category(InputReader)]
    public void InputReaderWordsTestWithCollectionAssertions()
    {
        // Arrange
        const string input = "";
        var expectedResult = Array.Empty<string>();
        var separators = _alphabetService.GetSeparators();
        // Act
        var words = _inputReader.GetWords(input, separators);
        // Assert
        CollectionAssert.AreEqual(words, expectedResult);
    }
    
    [Test]
    [Category(InputReader)]
    public void InputReaderWordsTest()
    {
        // Arrange
        const string input = "This is a simple test file";
        string[] expectedResult = { "This", "is", "a", "simple", "test", "file" };
        var separators = _alphabetService.GetSeparators();
        // Act
        var words = _inputReader.GetWords(input, separators);
        // Assert
        CollectionAssert.AreEqual(words, expectedResult);
    }

    [Test]
    [Category(AlphabetService)]
    public void AlphabetServiceTest()
    {
        // Arrange
        var consonants = _alphabetService.GetConsonants();
        var separators = _alphabetService.GetSeparators();
        
        // Act
        var intersection = consonants.Intersect(separators);
        
        // Assert
        Assert.That(intersection, Is.EqualTo(Array.Empty<char>()));
    }

    [Category(Word)]
    [TestCase(null, ExpectedResult = "")]
    [TestCase("", ExpectedResult = "")]
    [TestCase("     ", ExpectedResult = "")]
    [TestCase("Word", ExpectedResult = "Word")]
    public string WordCreationTest(string input)
    {
        // Arrange
        var word = new Word(input);
        
        return word.Value;
    }

    [Category(Word)]
    [TestCase("Chloe", 3, 5, 5, true)]
    [TestCase("Zzzzz", 5, 3, 5, false)]
    [TestCase("Spray", 3, 3, 3, false)]
    public void CloseWordTest(string text, int amount, int localMax, int expectedAmount, bool isToReset)
    {
        // Arrange
        var word = new Word(text)
        {
            AmountOfConsonantsInCurrentWord = amount,
            LocalMaxAmountOfConsonants = localMax
        };
        
        // Act
        word.CloseWord();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(word.AmountOfConsonantsInCurrentWord, Is.EqualTo(expectedAmount));
            Assert.That(word.ResetOnNextIteration, Is.EqualTo(isToReset));
        });
    }

    [Test]
    [Category(Word)]
    public void ResetLocalMaxTest()
    {
        // Arrange
        var word = new Word("text")
        {
            AmountOfConsonantsInCurrentWord = 2,
            LocalMaxAmountOfConsonants = 4
        };
        
        // Act
        word.ResetLocalMaxAmountOfConsonants();

        // Assert
        Assert.That(word.LocalMaxAmountOfConsonants, Is.EqualTo(0));
    }

    [Test]
    [Category(Word)]
    public void ResetWordIfNewTest()
    {
        // Arrange
        var word = new Word("text")
        {
            AmountOfConsonantsInCurrentWord = 2,
            LocalMaxAmountOfConsonants = 4,
            ResetOnNextIteration = true
        };

        // Act
        word.ResetWordIfNew();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(word.LocalMaxAmountOfConsonants, Is.EqualTo(0));
            Assert.That(word.ResetOnNextIteration, Is.EqualTo(false));
        });
    }

    [Test]
    [Category(Word)]
    public void RegisterConsonantTest()
    {
        // Arrange
        var word = new Word("text")
        {
            AmountOfConsonantsInCurrentWord = 4,
            LocalMaxAmountOfConsonants = 1,
            ResetOnNextIteration = true
        };
        
        // Act
        word.RegisterConsonant();
        
        // Assert
        Assert.That(word.LocalMaxAmountOfConsonants, Is.EqualTo(2));
    }

    [Category(Word)]
    [TestCase(1, 1, 1)]
    [TestCase(1, 2, 2)]
    [TestCase(2, 1, 2)]
    public void EqualizeMaxAmountIfCurrentIsLowerTest(int local, int amount, int expectedAmount)
    {
        // Arrange
        var word = new Word("text")
        {
            AmountOfConsonantsInCurrentWord = amount,
            LocalMaxAmountOfConsonants = local
        };
        
        // Act
        word.EqualizeMaxAmountIfCurrentIsLower();
        
        // Assert
        Assert.That(word.AmountOfConsonantsInCurrentWord, Is.EqualTo(expectedAmount));
    }

    [TestCase(new string[0], "Solo")]
    [TestCase(new[] { "Hello" }, "world")]
    public void UpdateResultsWithNewWordTest(string[] startWords, string addedWord)
    {
        // Arrange
        var result = new Result
        {
            WordsWithMostConsonants = startWords.ToList()
        };
        var word = new Word(addedWord);
        
        // Act
        result.UpdateResultWithNewWord(word);
        
        // Assert
        CollectionAssert.AreEqual(result.WordsWithMostConsonants, startWords.Append(addedWord).ToList());
    }
}