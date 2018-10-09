using System.Windows;

namespace Pace.Server.ViewModel
{
    public class WindowService<TWindow> where TWindow : Window, new()
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