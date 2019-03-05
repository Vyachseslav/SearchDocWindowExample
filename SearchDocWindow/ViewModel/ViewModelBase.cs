using System.ComponentModel;

namespace SearchDocWindow.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
