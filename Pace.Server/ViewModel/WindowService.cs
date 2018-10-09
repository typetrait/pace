using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pace.Server.ViewModel
{
    public class IWindowService<TWindow> where TWindow : Window, new()
    {
        public void ShowWindow(object viewModel)
        {
            var window = new TWindow()
            {
                DataContext = viewModel
            };

            window.Show();
        }
    }
}