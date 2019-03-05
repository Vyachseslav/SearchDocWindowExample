using SearchDocWindow.Model;
using System;
using System.IO;
using System.Windows;
using System.Windows.Interactivity;
using System.Xml.Serialization;

namespace SearchDocWindow.Behavior
{
    class WindowBehavior : Behavior<Window>
    {
        /// <summary>
        /// Путь к сохранению пользовательских настроек.
        /// </summary>
        private readonly string UserSettingsPath = GetPersonFolderPath() + "\\WindowSettings.xml";

        protected override void OnAttached()
        {
            AssociatedObject.Closing += AssociatedObject_Closing;
            AssociatedObject.Initialized += AssociatedObject_Initialized;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Closing -= AssociatedObject_Closing;
            AssociatedObject.Initialized -= AssociatedObject_Initialized;
        }

        private void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            LoadWindowSettings();
        }

        private void AssociatedObject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveWindowSettings();
        }

        /// <summary>
        /// Сохранение настроек окна.
        /// </summary>
        private void SaveWindowSettings()
        {
            WindowSettings window = new WindowSettings();
            window.Width = AssociatedObject.Width;
            window.Height = AssociatedObject.Height;
            window.Left = AssociatedObject.Left;
            window.Top = AssociatedObject.Top;
            window.WindowState = AssociatedObject.WindowState;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(WindowSettings));
                WindowSettings saveParameters = window;
                using (TextWriter txtWriter = new StreamWriter(UserSettingsPath))
                {
                    serializer.Serialize(txtWriter, saveParameters);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(MainWindowLocalizations.MainWindowLocalizations.ErrorSavingSettings + ":\n" + ex.Message,
                //    MainWindowLocalizations.MainWindowLocalizations.ErrorSavingSettings,
                //    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Загрузить настройки окна.
        /// </summary>
        public void LoadWindowSettings()
        {
            WindowSettings window = new WindowSettings();
            window = DeserializeFromXml(UserSettingsPath);
            AssociatedObject.Width = window.Width;
            AssociatedObject.Height = window.Height;
            AssociatedObject.Left = window.Left;
            AssociatedObject.Top = window.Top;
            AssociatedObject.WindowState = window.WindowState;
        }

        /// <summary>
        /// Распарсить XML с параметрами окна.
        /// </summary>
        /// <param name="fPath">Путь к файлу с настройками.</param>
        /// <returns></returns>
        public WindowSettings DeserializeFromXml(string fPath)
        {
            WindowSettings local = new WindowSettings();
            XmlSerializer serializer = new XmlSerializer(typeof(WindowSettings));
            try
            {
                using (Stream reader = new FileStream(fPath, FileMode.Open))
                {
                    // Call the Deserialize method to restore the object's state.
                    local = (WindowSettings)serializer.Deserialize(reader);
                    return local;
                }
            }
            catch { }
            return local;
        }

        public static string GetPersonFolderPath()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Путь к документам пользователя.
            if (Directory.Exists(Path.Combine(md, "SearchDocWindow\\Settings")) == false)
            {
                Directory.CreateDirectory(Path.Combine(md, "SearchDocWindow\\Settings"));
            }
            return Path.Combine(md, "SearchDocWindow\\Settings");
        }

        //string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
        //    if (Directory.Exists(md +"\\export") == false)
        //    {
        //        Directory.CreateDirectory(md + "\\export");
        //    }
    }
}
