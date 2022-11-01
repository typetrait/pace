namespace Pace.Server.ViewModel;

public class MainViewModel : ViewModelBase
{
	private ViewModelBase _currentPageViewModel;
    public ViewModelBase CurrentPageViewModel
	{
		get { return _currentPageViewModel; }
		set
		{
			_currentPageViewModel = value;
			OnPropertyChanged(nameof(CurrentPageViewModel));
		}
	}

	public MainViewModel()
	{
		CurrentPageViewModel = new ManageClientsViewModel(this);
	}
}
