using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNoPageNav
{
    /// <summary>
    /// Interface for a service the manages navigation using <see cref="NavigableUserControl"/> objects.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Returns <c>true</c> if forward history exists.
        /// </summary>
        bool CanGoForward { get; }

        /// <summary>
        /// Returns <c>true</c> if backwards history exists.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Navigate to the given control.
        /// </summary>
        /// <param name="control">Control to navigate to.</param>
        void Navigate(NavigableUserControl control);

        /// <summary>
        /// Goes forward in history.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when forward history does not exist.</exception>
        void GoBack();

        /// <summary>
        /// Goes forward in history.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when back history does not exist.</exception>
        void GoForward();

        /// <summary>
        /// Set the current control the service is on, but does not navigate. Useful for program initialization, but
        /// <see cref="Navigate(NavigableUserControl)"/> should be used for normal navigation.
        /// </summary>
        /// <param name="control">Control to set as the current item.</param>
        void SetCurrent(NavigableUserControl control);
    }
}
