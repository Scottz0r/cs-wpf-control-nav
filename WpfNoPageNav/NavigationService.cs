using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfNoPageNav
{
    public class NavigatingEventArgs
    {
        public NavigableUserControl Control { get; }

        public NavigatingEventArgs(NavigableUserControl control)
        {
            Control = control;
        }
    }

    public class NavigationService : INavigationService, INotifyPropertyChanged
    {
        private DropOutStack<NavigableUserControl> _backStack;
        private DropOutStack<NavigableUserControl> _fontStack;
        private NavigableUserControl _currentControl;

        public event EventHandler<NavigatingEventArgs> Navigated;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanGoForward => _fontStack.Count > 0;

        public bool CanGoBack => _backStack.Count > 0;

        public NavigationService(int maxNavigateHistory)
        {
            _backStack = new DropOutStack<NavigableUserControl>(maxNavigateHistory);
            _fontStack = new DropOutStack<NavigableUserControl>(maxNavigateHistory);
        }

        public void Navigate(NavigableUserControl control)
        {
            // Add the current control to the back stack for going backwards in history.
            if (_currentControl != null)
            {
                _backStack.Push(_currentControl);
                OnPropertyChanged(nameof(CanGoBack));
            }

            // Clear out forward stack because "normal" navigation blows away existing forward history (new "branch").
            _fontStack.Clear();
            OnPropertyChanged(nameof(CanGoForward));

            DoNavigation(control);
        }

        public void GoForward()
        {
            if(!CanGoForward)
            {
                throw new InvalidOperationException();
            }

            if(_currentControl != null)
            {
                _backStack.Push(_currentControl);
                OnPropertyChanged(nameof(CanGoBack));
            }

            var control = _fontStack.Pop();
            OnPropertyChanged(nameof(CanGoForward));

            DoNavigation(control);
        }

        public void GoBack()
        {
            if(!CanGoBack)
            {
                throw new InvalidOperationException();
            }

            if (_currentControl != null)
            {
                _fontStack.Push(_currentControl);
                OnPropertyChanged(nameof(CanGoForward));
            }

            var control = _backStack.Pop();
            OnPropertyChanged(nameof(CanGoBack));

            DoNavigation(control);
        }

        public void SetCurrent(NavigableUserControl control)
        {
            _currentControl = control;
        }

        private void DoNavigation(NavigableUserControl control)
        {
            _currentControl = control;
            _currentControl.NavigationService = this;

            var args = new NavigatingEventArgs(_currentControl);
            Navigated?.Invoke(this, args);
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
