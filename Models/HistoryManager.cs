namespace TextEditor.Models
{
    public class HistoryManager
    {
        // Full timeline of saved states
        private readonly List<TextEditorMemento> _history = new();

        // Points to the "current" position in history (-1 = nothing saved yet)
        private int _cursor = -1;

        /// <summary>
        /// Save a new state. If we're mid-history (after undos), 
        /// discard the future branch — exactly like Word does.
        /// </summary>
        public void Save(TextEditorMemento memento)
        {
            // Drop everything ahead of the cursor (future branch)
            if (_cursor < _history.Count - 1)
                _history.RemoveRange(_cursor + 1, _history.Count - _cursor - 1);

            _history.Add(memento);
            _cursor = _history.Count - 1;
        }

        /// <summary>Move one step back. Returns the previous state, or null if at the start.</summary>
        public TextEditorMemento? Undo()
        {
            if (_cursor <= 0) return null;
            _cursor--;
            return _history[_cursor];
        }

        /// <summary>Move one step forward. Returns the next state, or null if at the end.</summary>
        public TextEditorMemento? Redo()
        {
            if (_cursor >= _history.Count - 1) return null;
            _cursor++;
            return _history[_cursor];
        }

        /// <summary>Everything up to and including the cursor (visible history).</summary>
        public List<TextEditorMemento> GetHistory()
            => _history.Take(_cursor + 1).ToList();

        public bool CanUndo => _cursor > 0;
        public bool CanRedo => _cursor < _history.Count - 1;
    }
}