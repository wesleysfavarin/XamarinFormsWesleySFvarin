using Marvel.View;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Marvel
{
	public partial class PrincipalPage : ContentPage
	{
		public PrincipalPage()
		{

            //Menu direito
            ToolbarItems.Clear();
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "about.png",
                Text = "Sobre",
                Order = ToolbarItemOrder.Primary,
                
                Command = new Command(() => Navigation.PushModalAsync(new RootPage()))
            });

            InitializeComponent();

            //MainPage = new RootPage();    
            var vm = new PrincipalViewModel();

			listCharacters.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
		   {
			   var character = (CharacterItemViewModel)e.SelectedItem;
			   var infoVm = new InformacoesViewModel(character.Id);
				var infoView = new InformacoesPage(infoVm);
			   App.Navigation.PushAsync(infoView);
		   };

			listCharacters.IsPullToRefreshEnabled = true;

			BindingContext = vm;
			vm.LoadData(null,100,0);
		}
	}
}
