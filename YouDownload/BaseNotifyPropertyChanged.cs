using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YouTubePlaylistAPI
{
    [Serializable]
    public class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]
                                             string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}