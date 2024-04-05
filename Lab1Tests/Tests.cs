using Lab1Variant6;

namespace Lab1Tests;

public class Tests
{
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
}