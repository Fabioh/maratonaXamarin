using Cats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cats.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsPage : ContentPage
	{
		private Cat selectedCat;

		public DetailsPage(Cat selectedCat)
		{
			InitializeComponent();
			this.selectedCat = selectedCat;
			BindingContext	= this.selectedCat;
			ButtonWebSite.Clicked += ButtonWebSite_Clicked;
		}

		private void ButtonWebSite_Clicked(object sender, EventArgs e)
		{
			if (selectedCat.WebSite.StartsWith("http"))
			{
				Device.OpenUri(new Uri(selectedCat.WebSite));
			}
		}
	}
}
