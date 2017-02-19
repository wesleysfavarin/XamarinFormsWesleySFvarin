using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Akavache;
using System.Reactive.Linq;
using Marvel.View;

namespace Marvel
{
	public class PrincipalViewModel : ViewModelBase
	{
		private readonly IMarvelApiService _marvelService;
        public static INavigation Navigation;
        public PrincipalViewModel()
		{
			//_marvelService = DependencyService.Get<IMarvelApiService>();
			_marvelService = new MarvelApiService();
           
           
        }


		private string _SearchText;

		public string SearchText
		{
			get
			{
				return _SearchText;
			}
			set
			{
				_SearchText = value;
				OnPropertyChanged("SearchText");
			}
		}

		private ICommand _SearchByName;

		public ICommand SearchByName
		{
			get
			{
				return _SearchByName ?? (_SearchByName = new Command(
					async () => await ExecuteSearchByNameCommand()));
			}
		}

		private async Task ExecuteSearchByNameCommand()
		{
			await LoadData(SearchText);
		}

		private List<CharacterItemViewModel> _CharacterList;

		public List<CharacterItemViewModel> CharacterList
		{
			get
			{
				return _CharacterList;
			}
			set
			{
				_CharacterList = value;
				OnPropertyChanged("CharacterList");
			}
		}

		public async Task LoadData(string filter = null, int limit = 0, int offset = 0)
		{
			IsBusy = true;

			var cache = Akavache.BlobCache.LocalMachine;
			var cachedCharacters = cache.GetAndFetchLatest("CharacterList" + filter, () => _marvelService.GetCharacters(filter, limit, offset),
				ofset =>
				{
					TimeSpan elapsed = DateTimeOffset.Now - ofset;
					return elapsed > new TimeSpan(hours: 0, minutes: 10, seconds: 0);
				});

			var result = await cachedCharacters.FirstOrDefaultAsync();

			//var result =  await _marvelService.GetCharacters(filter, limit, offset);


			if (result != null)
			{
				CharacterList = (from p in result.Results
								 select new CharacterItemViewModel()
								 {
									 Id = p.Id,
									 Name = p.Name,
									 Description = p.Description
								 }).ToList();
			}

			IsBusy = false;
           
        }


		private Command refresh;
		public Command Refresh
		{
			get
			{
				return refresh ??
					(refresh = new Command(ExecuteRefreshCommand));
			}
		}

		async void ExecuteRefreshCommand()
		{
			await LoadData();
		}
	}
}
