using SearchDocWindow.Interfaces;
using SearchDocWindow.SearchServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace SearchDocWindow.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        #region Services.

        /// <summary>
        /// Менеджер пользовательских сообщений.
        /// </summary>
        private static IDialog DialogManager { get; set; }

        /// <summary>
        /// Сервис для навигатора.
        /// </summary>
        public INavigator NavigatorService { get; set; }

        /// <summary>
        /// Менеджер локализации.
        /// </summary>
        public LocalizationViewModel Localization { get; set; }

        /// <summary>
        /// Менеджер команд раздела "Помощь".
        /// </summary>
        public HelpBaseCommands HelpCommandsManager { get; set; }

        /// <summary>
        /// Сервис для поиска файлов.
        /// </summary>
        public ISearcher SearchFileService { get; set; }

        #endregion

        #region Privates.

        private RelayCommand _openSelectedFile;
        private RelayCommand _openSelectedFileToFolder;
        private RelayCommand _showSelectedFileIntoFolder;
        private RelayCommand _refresh;
        private RelayCommand _showAutofilterRowCommand;

        private string[] _args;
        private List<string> _listOfData;
        private ObservableCollection<Model.File> _findedFiles;
        private Model.File _selectedFile;

        private bool _isErrorArguments = false;

        private bool _showAutofilterRow = false; // Показать строку автофильтра.
        private int _rowsNumber; // Число строк в GridControl.

        #endregion

        #region Properties.

        /// <summary>
        /// Входные аргументы приложения.
        /// </summary>
        public string[] Arguments
        {
            get { return _args; }
            set { _args = value; OnPropertyChanged("Arguments"); }
        }
        
        /// <summary>
        /// Список файлов, найденных по условию Arguments[1].
        /// </summary>
        public List<string> ListOfData
        {
            get { return _listOfData; }
            set { _listOfData = value; OnPropertyChanged("ListOfData"); }
        }
        
        /// <summary>
        /// Список найденных файлов.
        /// </summary>
        public ObservableCollection<Model.File> FindedFiles
        {
            get { return _findedFiles; }
            set
            {
                _findedFiles = value;
                OnPropertyChanged("FindedFiles");
            }
        }
        
        public Model.File SelectedFile
        {
            get { return _selectedFile; }
            set { _selectedFile = value; OnPropertyChanged("SelectedFile"); }
        }

        /// <summary>
        /// Показать строку автофильтра.
        /// </summary>
        public bool ShowAutofilterRow
        {
            get { return _showAutofilterRow; }
            set { _showAutofilterRow = value; OnPropertyChanged("ShowAutofilterRow"); }
        }

        /// <summary>
        /// Число строк в GridControl.
        /// </summary>
        public int RowsNumber
        {
            get { return _rowsNumber; }
            set { _rowsNumber = value; OnPropertyChanged("RowsNumber"); }
        }

        #endregion

        public MainViewModel()
        {
            ListOfData = new List<string>();
            FindedFiles = new ObservableCollection<Model.File>();
            FindedFiles.CollectionChanged += FindedFiles_CollectionChanged; // Изменение коллекции.
            Localization = new LocalizationViewModel();
            HelpCommandsManager = new HelpBaseCommands();
        }

        public MainViewModel(string[] args)
        {
            DialogManager = new Dialogs.Dialogs();
            NavigatorService = new NavigatorViewModel();
            SearchFileService = new SimpleFileSearch();
            HelpCommandsManager = new HelpBaseCommands();
            Localization = new LocalizationViewModel();
            ListOfData = new List<string>();
            FindedFiles = new ObservableCollection<Model.File>();
            FindedFiles.CollectionChanged += FindedFiles_CollectionChanged; // Изменение коллекции.
            Arguments = args;

            if (Arguments.Length > 0)
            {
                GetFiles(Arguments[0], Arguments[1]); // Получаем список файлов.
                SelectedFile = FindedFiles[0]; // Выделяем первый найденный файл.
            }
            else
            {
                DialogManager.ErrorMessage("Число аргументов не соответсвтует формату.\nПрограмма будет запущена по умолчанию.", "Ошибка передачи аргументов");
                _isErrorArguments = true;
            }
        }

        public MainViewModel(string[] args, IDialog dialogManager, INavigator navigatorService, ISearcher searcherService)
        {
            DialogManager = dialogManager;
            NavigatorService = navigatorService;
            SearchFileService = searcherService;
            HelpCommandsManager = new HelpBaseCommands();
            Localization = new LocalizationViewModel();
            ListOfData = new List<string>();
            FindedFiles = new ObservableCollection<Model.File>();
            FindedFiles.CollectionChanged += FindedFiles_CollectionChanged; // Изменение коллекции.
            Arguments = args;

            if (Arguments.Length > 0)
            {
                GetFiles(Arguments[0], Arguments[1]); // Получаем список файлов.
                SelectedFile = FindedFiles[0]; // Выделяем первый найденный файл.
            }
            else
            {
                DialogManager.ErrorMessage("Число аргументов не соответсвтует формату.\nПрограмма будет запущена по умолчанию.", "Ошибка передачи аргументов");
                _isErrorArguments = true;
            }
        }

        #region Events.

        private void FindedFiles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RowsNumber = FindedFiles.Count; // При изменении коллекции пересчитываем число строк.
        }

        #endregion

        #region Commands.

        /// <summary>
        /// Команда для открытия выделенного файла.
        /// </summary>
        public RelayCommand OpenSelectedFile
        {
            get
            {
                return _openSelectedFile ??
                  (_openSelectedFile = new RelayCommand(obj =>
                  {
                      Process.Start(SelectedFile.Path);
                  },
                 (obj) => SelectedFile != null));
            }
        }

        /// <summary>
        /// Команда для открытия папки, где находится выделенный файл.
        /// </summary>
        public RelayCommand OpenSelectedFileToFolder
        {
            get
            {
                return _openSelectedFileToFolder ??
                  (_openSelectedFileToFolder = new RelayCommand(obj =>
                  {
                      Process.Start(SelectedFile.PathToFolder);
                  },
                 (obj) => SelectedFile != null));
            }
        }

        /// <summary>
        /// Команда для открытия папки, где находится выделенный файл, и выделить его.
        /// </summary>
        public RelayCommand ShowSelectedFileIntoFolder
        {
            get
            {
                return _showSelectedFileIntoFolder ??
                  (_showSelectedFileIntoFolder = new RelayCommand(obj =>
                  {
                      Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + SelectedFile.Path));
                  },
                 (obj) => SelectedFile != null));
            }
        }

        /// <summary>
        /// Команда для открытия папки, где находится выделенный файл.
        /// </summary>
        public RelayCommand Refresh
        {
            get
            {
                return _refresh ??
                  (_refresh = new RelayCommand(obj =>
                  {
                      FindedFiles.Clear(); 
                      GetFiles(Arguments[0], Arguments[1]);
                      SelectedFile = FindedFiles[0]; // Выделяем первый найденный файл.
                  },
                  (obj) => _isErrorArguments == false));
            }
        }

        /// <summary>
        /// Команда для открытия/скрытия строки автофильтра.
        /// </summary>
        public RelayCommand ShowAutofilterRowCommand
        {
            get
            {
                return _showAutofilterRowCommand ??
                  (_showAutofilterRowCommand = new RelayCommand(obj =>
                  {
                      ShowAutofilterRow = (ShowAutofilterRow) ? false : true;
                  }));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get interested file.
        /// </summary>
        /// <param name="path">Path to initial directory for search.</param>
        /// <param name="startValue">File name start with value.</param>
        public void GetFiles(string path, string startValue)
        {
            string[] files = SearchFileService.SearchFile(path, string.Format("*{0}*", startValue), SearchOption.AllDirectories);
            foreach (string file in files)
            {
                FindedFiles.Add(new Model.File(file, Path.GetFileName(file)));
            }
        }

        #endregion
    }
}
