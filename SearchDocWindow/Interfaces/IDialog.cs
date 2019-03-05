

namespace SearchDocWindow.Interfaces
{
    public interface IDialog
    {
        void InfoMessage(string message, string title);
        void ErrorMessage(string message, string title);
    }
}
