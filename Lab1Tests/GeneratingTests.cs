using System.Text.RegularExpressions;
using Lab1Variant6.AbstractionsExample;
using Lab1Variant6.AbstractionsExample.Implementations;
using Lab1Variant6.AbstractionsExample.Implementations.Models;
using Moq;

namespace Lab1Tests;

[Parallelizable]
public class GeneratingTests
{
    [Test]
    public void OneWordSentenceGeneratingServiceTest()
    {
        const string capital = "Capital";
        const string small = "small";
        
        // Mocks
        var separatorGeneratingService = new Mock<ISeparatorGeneratingService>();
        var wordGeneratingService = new Mock<IWordGeneratingService>();

        wordGeneratingService.Setup(x => x.GenerateWord(true)).Returns(capital);
        wordGeneratingService.Setup(x => x.GenerateWord(false)).Returns(small);

        var separatorEndsSentence = true;
        separatorGeneratingService.Setup(x => x.GenerateSeparator(out separatorEndsSentence)).Returns(". ");
        
        // Arrange
        var sentenceGeneratingService = new SentenceGeneratingService(wordGeneratingService.Object, separatorGeneratingService.Object);
        
        // Act
        var sentence = sentenceGeneratingService.GenerateSentence();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(sentence, Is.Not.Empty);
            Assert.That(sentence, Is.EqualTo($"{capital}. "));
        });
    }

    [Test]
    public void SeparatorGeneratingServiceTest()
    {
        // Arrange
        var separatorConfiguration = new SeparatorConfiguration();
        var separatorService = new SeparatorGeneratingService(separatorConfiguration);
        
        // Act
        var separator = separatorService.GenerateSeparator(out _);

        // Assert
        Assert.That(separator.ToCharArray().Last(), Is.EqualTo(' '));
    }

    [Test]
    public void CapitalizedWordGeneratingServiceTest()
    {
        // Arrange
        var wordService = new WordGeneratingService();

        // Act
        var word = wordService.GenerateWord(true);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(word, Is.Not.Empty);

            var firstLetter = word.First()!.ToString();
            Assert.That(firstLetter.ToUpper(), Is.EqualTo(firstLetter));
        });
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    [TestCase(-1)]
    public void TextGeneratingServiceTest(int sentencesAmount)
    {
        // Arrange
        const string sameSentenceText = "The same sentence every time. ";
        var sentenceGeneratingService = new Mock<ISentenceGeneratingService>();

        sentenceGeneratingService.Setup(x => x.GenerateSentence()).Returns(sameSentenceText);

        var textGeneratingService = new TextGeneratingService(sentenceGeneratingService.Object);
        
        // Act
        var text = textGeneratingService.GenerateText(sentencesAmount);
        var actualAmount = 0 > sentencesAmount ? 0 : sentencesAmount;
        
        // Assert
        sentenceGeneratingService.Verify(x => x.GenerateSentence(), Times.Exactly(actualAmount));
        Assert.That(actualAmount, Is.EqualTo(Regex.Matches(text, sameSentenceText).Count));
    }
}