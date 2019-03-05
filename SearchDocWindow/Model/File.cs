

namespace SearchDocWindow.Model
{
    /// <summary>
    /// Модель найденного файла.
    /// </summary>
    class File : ViewModel.ViewModelBase
    {
        private string _path;
        private string _fileName;
        private string _pathToFolder;

        /// <summary>
        /// Полный путь к файлу.
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; OnPropertyChanged("Path"); }
        }

        /// <summary>
        /// Путь к папке у файла.
        /// </summary>
        public string PathToFolder
        {
            get { return _pathToFolder; }
            set { _pathToFolder = value; OnPropertyChanged("PathToFolder"); }
        }

        /// <summary>
        /// Название файла.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged("FileName"); }
        }

        public File()
        {

        }

        public File(string path, string name)
        {
            this.Path = path;
            this.PathToFolder = System.IO.Path.GetDirectoryName(Path);
            this.FileName = name;
        }
    }
}
