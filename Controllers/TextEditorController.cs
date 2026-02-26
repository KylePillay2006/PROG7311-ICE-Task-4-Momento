using Microsoft.AspNetCore.Mvc;
using TextEditor.Models;

namespace TextEditor.Controllers
{
    public class TextEditorController : Controller
    {
        private static TextEditorOriginator _editor = new TextEditorOriginator();
        private static HistoryManager _history = new HistoryManager();

        public IActionResult Index()
        {
            ViewBag.Content  = _editor.Content;
            ViewBag.History  = _history.GetHistory();
            ViewBag.CanUndo  = _history.CanUndo;
            ViewBag.CanRedo  = _history.CanRedo;
            return View();
        }

        [HttpPost]
        public IActionResult Save(string content)
        {
            _editor.Content = content;
            _history.Save(_editor.Save());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Undo()
        {
            var memento = _history.Undo();
            if (memento != null)
                _editor.Restore(memento);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Redo()
        {
            var memento = _history.Redo();
            if (memento != null)
                _editor.Restore(memento);
            return RedirectToAction("Index");
        }
    }
}