namespace TextEditor.Models
{
    public class TextEditorOriginator
    {
        public string Content { get; set; } = string.Empty;

        public TextEditorMemento Save() => new TextEditorMemento(Content);

        public void Restore(TextEditorMemento memento) => Content = memento.Content;
    }
}