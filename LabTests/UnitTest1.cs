namespace LabTests;

public class Tests
{
    [Test]
    public void Test1()
    {
        // Arrange
        var jsCode = "var a = 5;";
        
        // Act
        var result = Lab3Variant6.Program.LexemAnalyzer(jsCode);
        
        // Assert
        Assert.That(result, Is.EqualTo("<var ; KEYWORD>\n" +
                        "<a ; IDENTIFIER>\n" +
                        "<= ; OPERATOR>\n" +
                        "<5 ; NUMERIC>\n" +
                        "<; ; PUNCTUATION>\n"));
    }

    [Test]
    public void Test2()
    {
        // Arrange
        var jsCode = 
@"class DoublyLinkedList {
  constructor() {
    this.nodes = [];
  }";
        
        // Act
        var result = Lab3Variant6.Program.LexemAnalyzer(jsCode).TrimEnd('\n');
        
        // Assert
        Assert.That(result, Is.EqualTo(
@"<class ; IDENTIFIER>
<DoublyLinkedList ; IDENTIFIER>
<{ ; PUNCTUATION>
<constructor ; IDENTIFIER>
<( ; PUNCTUATION>
<) ; PUNCTUATION>
<{ ; PUNCTUATION>
<this ; IDENTIFIER>
<. ; DOT>
<nodes ; IDENTIFIER>
<= ; OPERATOR>
<[ ; PUNCTUATION>
<] ; PUNCTUATION>
<; ; PUNCTUATION>
<} ; PUNCTUATION>".Replace("\r", "")));
    }
}