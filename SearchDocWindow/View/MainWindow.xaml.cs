using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchDocWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Запускать с указанными параметрами.
        /// </summary>
        public bool IsStartWithMyArguments { get; set; } = false;

        /// <summary>
        /// Путь к каталогу.
        /// </summary>
        public string PathToFolder { get; set; }

        /// <summary>
        /// Номер гос. реестра.
        /// </summary>
        public string Number { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.ContentRendered += MainWindow_ContentRendered;
        }

        public MainWindow(string[] args)
        {
            InitializeComponent();
            this.ContentRendered += MainWindow_ContentRendered;

            if (IsStartWithMyArguments)
            {
                this.DataContext = new ViewModel.MainViewModel(new string[] { PathToFolder, Number });
            }
            else
            {
                this.DataContext = new ViewModel.MainViewModel(
                    args,
                    new Dialogs.Dialogs(),
                    new ViewModel.NavigatorViewModel(),
                    new SearchServices.SimpleFileSearch()
                    );
            }

        }

        private void MainWindow_ContentRendered(object sender, System.EventArgs e)
        {
            grid.Focus();
        }
    }
}
