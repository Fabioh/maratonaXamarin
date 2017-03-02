using Cats.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cats.ViewModels
{
	public class CatsViewModel : INotifyPropertyChanged
	{
		#region fields
		private bool _isBusy;
		#endregion

		#region propriedades
		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				OnPropertyChanged();
				GetCatsCommand.ChangeCanExecute();
			}
		}

		public ObservableCollection<Cat> Cats { get; set; }
		public Command GetCatsCommand { get; set; } 
		#endregion

		public CatsViewModel()
		{
			Cats = new ObservableCollection<Cat>();
			GetCatsCommand = new Command(async () => await GetCats(),
				() => !IsBusy);
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private async Task GetCats()
		{
			if (!_isBusy)
			{
				Exception Error = null;
				try
				{
					IsBusy = true;
					var Repository = new Repository();
					var Items = await Repository.GetCats();
					Cats.Clear();
					foreach (var Cat in Items)
					{
						Cats.Add(Cat);
					}
				}
				catch (Exception ex)
				{
					Error = ex;
				}
				finally
				{
					IsBusy = false;
					if (Error != null)
					{
						await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error!", Error.Message, "OK");
					}
				}
			}
			return;
		}
	}
}
