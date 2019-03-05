using SearchDocWindow.Interfaces;
using System.IO;

namespace SearchDocWindow.SearchServices
{
    class SimpleFileSearch : ISearcher
    {
        /// <summary>
        /// Поиск файлов в папке со всеми каталоами и подкаталогами.
        /// </summary>
        /// <param name="path">Путь к начальной директории.</param>
        /// <param name="pattern">Шаблон поиска.</param>
        /// <param name="searchOption">Опиця поиска (по умолчанию AllDirectories).</param>
        /// <returns></returns>
        public string[] SearchFile(string path, string pattern, SearchOption searchOption = SearchOption.AllDirectories)
        {
            string[] ResultSearch = Directory.GetFiles(path, pattern, searchOption);
            return ResultSearch;
        }
    }
}
