using DV.TeleCallerHelper.Common.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel;

namespace DV.TeleCallerHelper.Common
{
    public abstract class ViewModelBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Administrative Properties

        /// <summary>
        /// Whether the view model should ignore property-change events.
        /// </summary>
        public virtual bool IgnorePropertyChangeEvents { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        public virtual void RaisePropertyChanged(string propertyName)
        {
            // Exit if changes ignored
            if (IgnorePropertyChangeEvents) return;

            // Exit if no subscribers
            if (PropertyChanged == null) return;

            // Raise event
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }

        /// <summary>
        /// Raises the PropertyChanging event.
        /// </summary>
        /// <param name="propertyName">The name of the changing property.</param>
        public virtual void RaisePropertyChanging(string propertyName)
        {
            // Exit if changes ignored
            if (IgnorePropertyChangeEvents) return;

            // Exit if no subscribers
            if (PropertyChanging == null) return;

            // Raise event
            var e = new PropertyChangingEventArgs(propertyName);
            PropertyChanging(this, e);
        }

        #endregion

        protected IEventAggregator EventAggregator
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<IEventAggregator>();
            }
        }

        protected void SetStatusbarMessage(string message, StatusMessageType msgType)
        {
            var statusBarEvent = EventAggregator.GetEvent<StatusbarEvent>();
            statusBarEvent.Publish(new StatusbarEventArgs(message, msgType));
        }

        protected void ShowBusyCursor(bool show)
        {
            ShowBusyCursor(show, string.Empty);
        }

        protected void ShowBusyCursor(bool show, string message)
        {
            var statusBarEvent = EventAggregator.GetEvent<ShowBusyCursorEvent>();
            statusBarEvent.Publish(new ShowBusyCursorEventArg(show, message));
        }
    }
}