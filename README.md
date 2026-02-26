# 📝 Smart Text Editor

An ASP.NET Core MVC text editor with undo and redo, built using the **Memento design pattern**.

---

## 🚀 How to Run

```bash
dotnet run
```
Then open `https://localhost:5001/TextEditor` in your browser.

---

## ✨ Features

- Save versions of your text
- Undo ↩️ and Redo ↪️ like Microsoft Word
- Version history panel

---

## 🏛️ Memento Pattern

The **Memento Pattern** is a behavioural design pattern that captures snapshots of an object's state so it can be restored later — without breaking encapsulation. Think of it as a **time machine** ⏳ for your data.

### How This App Uses It

Every time you click **Save Version**, the editor captures the current text as a `TextEditorMemento` — a frozen, immutable snapshot. These snapshots are stored in a history list managed by the `HistoryManager`. When you **Undo**, the app walks back through the snapshots and restores a previous one. **Redo** walks forward again. If you save while mid-history, all future snapshots are discarded — exactly like Word.

```
Save "Hello"         →  ["Hello"]                 cursor: 0
Save "Hello World"   →  ["Hello", "Hello World"]   cursor: 1
Undo                 →  cursor: 0  (content = "Hello")
Redo                 →  cursor: 1  (content = "Hello World")
Undo → Save "Hi"     →  ["Hello", "Hi"]            cursor: 1  ← future gone
```

### The Three Roles

| Role | Class | Job |
|---|---|---|
| Originator | `TextEditorOriginator` | Owns the live text state; creates and restores snapshots |
| Memento | `TextEditorMemento` | Immutable frozen snapshot of the text at a point in time |
| Caretaker | `HistoryManager` | Stores the snapshot list and moves the cursor — never reads inside a snapshot |

### The Classes

**`TextEditorMemento`** — the snapshot itself. Once created it cannot be changed.
```csharp
public class TextEditorMemento
{
    public string Content { get; }
    public TextEditorMemento(string content) => Content = content;
}
```

**`TextEditorOriginator`** — owns the text and knows how to save/restore it.
```csharp
public TextEditorMemento Save()              => new TextEditorMemento(Content);
public void Restore(TextEditorMemento m)     => Content = m.Content;
```

**`HistoryManager`** — manages the timeline. Trims the future branch on save.
```csharp
public void Save(TextEditorMemento memento)  // append + trim future
public TextEditorMemento? Undo()             // cursor--
public TextEditorMemento? Redo()             // cursor++
public bool CanUndo => _cursor > 0;
public bool CanRedo => _cursor < _history.Count - 1;
```

---

## 🛠️ Built With

- ASP.NET Core MVC
- C#
- Razor Views

---

## 📚 References

[1] Refactoring Guru. (n.d.). *Memento Design Pattern*.  
Available at: https://refactoring.guru/design-patterns/memento  
Accessed: 26 February 2026.

[2] GeeksforGeeks. (n.d.). *Memento Design Pattern (System Design)*.  
Available at: https://www.geeksforgeeks.org/system-design/memento-design-pattern/  
Accessed: 26 February 2026.

## 👤 Author

### **Kyle Pillay**
Final-year Application Development Student | Emeris

📧 Email: [kylepillay017@gmail.com](mailto:kylepillay017@gmail.com)

🐙 GitHub: [KylePillay2006](https://github.com/KylePillay2006)

📺 YouTube: [@ByteSizedCode123](https://www.youtube.com/@ByteSizedCode123)

🌐 Portfolio: [kylepillay2006.github.io](https://kylepillay2006.github.io/PersonalPortfolioWebsite/)

💻 Username: @ByteSizedCode123

---

© 2026 Kyle Pillay
