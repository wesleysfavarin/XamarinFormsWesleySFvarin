using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Marvel
{
	public partial class InformacoesPage : ContentPage
	{
		public InformacoesPage(InformacoesViewModel vm)
		{
			InitializeComponent();


			this.Title = "Informações";

			vm.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
			{
				if (e.PropertyName == "Character")
				{
					imgPhoto.Source = UriImageSource.FromUri(new Uri((BindingContext as InformacoesViewModel).Character.Thumbnail));
				}
			};

			BindingContext = vm;
			vm.LoadData();
		}
	}
}
