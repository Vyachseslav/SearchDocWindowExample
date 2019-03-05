using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SearchDocWindow.Behavior
{
    /// <summary>
    /// Поведение для контестного меню у GridControl.TableView
    /// </summary>
    public class ContextMenuBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseRightButtonDown += AssociatedObject_PreviewMouseRightButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseRightButtonDown -= AssociatedObject_PreviewMouseRightButtonDown;
        }

        private void AssociatedObject_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();

            MenuItem menuOpenFile = new MenuItem();
            menuOpenFile.Header = (AssociatedObject.DataContext as ViewModel.MainViewModel).Localization.MainLocalization.Text; //"Открыть файл";
            menuOpenFile.Command = (AssociatedObject.DataContext as ViewModel.MainViewModel).OpenSelectedFile;
            contextMenu.Items.Add(menuOpenFile);

            MenuItem menuOpenFolder = new MenuItem();
            menuOpenFolder.Header = "Открыть папку в проводнике";
            menuOpenFolder.Command = (AssociatedObject.DataContext as ViewModel.MainViewModel).OpenSelectedFileToFolder;
            //contextMenu.Items.Add(menuOpenFolder);

            MenuItem menuShowIntoFolder = new MenuItem();
            menuShowIntoFolder.Header = (AssociatedObject.DataContext as ViewModel.MainViewModel).Localization.MainLocalization.ShowInExplorer; //"Показать в проводнике";
            menuShowIntoFolder.Command = (AssociatedObject.DataContext as ViewModel.MainViewModel).ShowSelectedFileIntoFolder;
            contextMenu.Items.Add(menuShowIntoFolder);

            MenuItem menuAutofilter = new MenuItem();
            menuAutofilter.Header = (AssociatedObject.DataContext as ViewModel.MainViewModel).Localization.MainLocalization.Autofilter; //"Автофильтр";
            menuAutofilter.IsChecked = (AssociatedObject.DataContext as ViewModel.MainViewModel).ShowAutofilterRow;
            menuAutofilter.Command = (AssociatedObject.DataContext as ViewModel.MainViewModel).ShowAutofilterRowCommand;
            contextMenu.Items.Add(menuAutofilter);


            contextMenu.IsOpen = true;
        }
    }
}
