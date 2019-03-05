using SearchDocWindow.Model;
using System;

namespace SearchDocWindow.ViewModel
{
    public class LocalizationViewModel
    {
        /// <summary>
        /// Локализация приложения.
        /// </summary>
        public Localization MainLocalization { get; set; }

        public LocalizationViewModel()
        {
            //MainLocalization = new Localization();

            string language = LoadUserLanguage();
            switch (language)
            {
                case "RU":
                    MainLocalization = new Localization();
                    break;
                case "EN":
                    MainLocalization = new Localization();
                    SetEngLocalization();
                    break;
            }
        }

        /// <summary>
        /// Получить язык пользователя.
        /// </summary>
        private string LoadUserLanguage()
        {
            string _languageOfDataBase = "RU";
            try
            {
                //_languageOfDataBase = LanguageFromBase.GetLanguage(); // Получаем язык.
                //LanguageFromBase.SetCulture(_languageOfDataBase);
                return _languageOfDataBase;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, _errorLoadLanguage, MessageBoxButton.OK, MessageBoxImage.Error);
                return "RU"; // "RU";
            }
        }

        /// <summary>
        /// Задать английскую локализацию.
        /// </summary>
        private void SetEngLocalization()
        {
            MainLocalization.Title = "Search doc";
            MainLocalization.Exit = "Exit";
            MainLocalization.Autofilter = "Autofilter";
            MainLocalization.Text = "Text";
            MainLocalization.ShowInExplorer = "Show in explorer";

            MainLocalization.FileNameColumn = "File name";
            MainLocalization.FilePathColumn = "Full path";

            MainLocalization.MenuExport = "Export";

            MainLocalization.MenuSettings = "Settings";
            MainLocalization.MenuHelp = "Help";
            MainLocalization.MenuHelpAbout = "About";
            MainLocalization.MenuHelpTowebSite = "To developer`s site";
            MainLocalization.MenuHelpMail = "Message to developer";
            MainLocalization.MenuBugReport = "Report an error";
        }
    }
}