

namespace SearchDocWindow.Interfaces
{
    /// <summary>
    /// Интерфейс поиска файлов.
    /// </summary>
    interface ISearcher
    {
        string[] SearchFile(string path, string pattern, System.IO.SearchOption searchOption);
    }
}
