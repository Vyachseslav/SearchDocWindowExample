using SearchDocWindow.Interfaces;
using System;

namespace SearchDocWindow.ViewModel
{
    public class HelpBaseCommands : ViewModelBase
    {
        #region Fields.

        private RelayCommand _aboutCommand;
        private RelayCommand _toWebsiteCommand;
        private RelayCommand _helpMailCommand;
        private RelayCommand _bagReportCommand;

        #endregion


        /// <summary>
        /// Менеджер пользовательских сообщений.
        /// </summary>
        private static IDialog DialogManager = new Dialogs.Dialogs();

        #region Commands.

        /// <summary>
        /// Команда "О программе".
        /// </summary>
        public RelayCommand AboutCommand
        {
            get
            {
                return _aboutCommand ??
                  (_aboutCommand = new RelayCommand(obj =>
                  {
                      ShowAboutDialog();
                  }));
            }
        }

        /// <summary>
        /// Команда "На сайт разработчика".
        /// </summary>
        public RelayCommand ToWebsiteCommand
        {
            get
            {
                return _toWebsiteCommand ??
                  (_toWebsiteCommand = new RelayCommand(obj =>
                  {
                      StartProcess("http://palitra-system.ru/");
                  }));
            }
        }

        /// <summary>
        /// Команда "Письмо разработчику".
        /// </summary>
        public RelayCommand HelpMailCommand
        {
            get
            {
                return _helpMailCommand ??
                  (_helpMailCommand = new RelayCommand(obj =>
                  {
                      WriteEmailToDeveloper();
                  }));
            }
        }

        /// <summary>
        /// Команда "Отчет об ошибке".
        /// </summary>
        public RelayCommand BagReportCommand
        {
            get
            {
                return _bagReportCommand ??
                  (_bagReportCommand = new RelayCommand(obj =>
                  {
                      //BugSenderNamespace.BugSender bugSender = new BugSenderNamespace.BugSender();
                      //bugSender.ShowDialog();
                  }));
            }
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Открыть диалоговое окно "О программе".
        /// </summary>
        public void ShowAboutDialog()
        {
            string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //AboutWindow about = new AboutWindow(name, version);
            //about.ShowDialog();
        }

        /// <summary>
        /// Запустить процесс.
        /// </summary>
        /// <param name="path">Путь или что-нибудь еще.</param>
        public void StartProcess(string path)
        {
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (System.Exception ex)
            {
                DialogManager.ErrorMessage(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Написать сообщение разработчику.
        /// </summary>
        /// <param name="mailTitle">Заголовок сообщения.</param>
        /// <param name="mailText">Текст сообщения.</param>
        public void WriteEmailToDeveloper(string mailTitle = "", string mailText = "")
        {
            if (mailTitle == "") { mailTitle = "Ошибка в приложении Поиск по номеру Гос. реестра"; }
            if (mailText == "") { mailText = "Возникла ошибка в приложении Поиск по номеру Гос. реестра"; }

            System.Diagnostics.Process.Start(String.Format("mailto:{0}?subject={1}&cc={2}&bcc={3}&body={4}",
               "support@palitra-system.ru", mailTitle, null, null, mailText));
        }

        #endregion
    }
}
