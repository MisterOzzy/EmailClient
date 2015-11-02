using System.ComponentModel;

namespace EmailClient.UI.VM.Core
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        private CommandHolder _commands;

        public CommandHolder Commands
        {
            get { return _commands ?? (_commands = new CommandHolder()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            var changedEventHandler = PropertyChanged;
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}