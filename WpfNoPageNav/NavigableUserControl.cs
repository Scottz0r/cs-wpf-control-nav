using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfNoPageNav
{
    /// <summary>
    /// Base class for a UserControl that can use Navigation.
    /// </summary>
    public class NavigableUserControl : UserControl
    {
        /// <summary>
        /// Get or set the navigation service for the control. This is set when the Navigation Service navigates to
        /// the control.
        /// </summary>
        public INavigationService NavigationService { get; set; }
    }
}
