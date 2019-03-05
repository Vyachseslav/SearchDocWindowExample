using SearchDocWindow;
using SearchDocWindow.Interfaces;
using System.Windows.Controls;

namespace SearchDocWindow.ViewModel
{
    class NavigatorViewModel : INavigator
    {
        #region Navigator command.

        private RelayCommand _firstRowCommand;
        private RelayCommand _previewRowCommand;
        private RelayCommand _nextRowCommand;
        private RelayCommand _lastRowCommand;

        /// <summary>
        /// Команда для перехода на первую строку.
        /// </summary>
        public RelayCommand FirstRowCommand
        {
            get
            {
                return _firstRowCommand ??
                  (_firstRowCommand = new RelayCommand(obj =>
                  {
                      (obj as DataGrid).SelectedIndex = 0;
                  },
                  (obj) => (obj as DataGrid)?.SelectedIndex != 0));
            }
        }

        /// <summary>
        /// Команда для перехода на предыдущую строку.
        /// </summary>
        public RelayCommand PreviewRowCommand
        {
            get
            {
                return _previewRowCommand ??
                  (_previewRowCommand = new RelayCommand(obj =>
                  {
                      (obj as DataGrid).SelectedIndex--;
                  },
                  (obj) => (obj as DataGrid)?.SelectedIndex != 0));
            }
        }

        /// <summary>
        /// Команда для перехода на следующую строку.
        /// </summary>
        public RelayCommand NextRowCommand
        {
            get
            {
                return _nextRowCommand ??
                  (_nextRowCommand = new RelayCommand(obj =>
                  {
                      (obj as DataGrid).SelectedIndex++;
                  },
                  (obj) => (obj as DataGrid)?.SelectedIndex != (obj as DataGrid)?.Items.Count - 1));
            }
        }

        /// <summary>
        /// Команда для перехода на последнюю строку.
        /// </summary>
        public RelayCommand LastRowCommand
        {
            get
            {
                return _lastRowCommand ??
                  (_lastRowCommand = new RelayCommand(obj =>
                  {
                      (obj as DataGrid).SelectedIndex = (obj as DataGrid).Items.Count - 1;
                  },
                  (obj) => (obj as DataGrid)?.SelectedIndex != (obj as DataGrid)?.Items.Count - 1));
            }
        }

        #endregion
    }
}
