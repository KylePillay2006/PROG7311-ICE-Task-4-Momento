namespace TextEditor.Models
{
    public class TextEditorMemento
    {
        public string Content { get; }

        public TextEditorMemento(string content)
        {
            Content = content;
        }
    }
}