using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using Akavache;
using System.Reactive.Linq;

namespace Marvel
{
	public class InformacoesViewModel : ViewModelBase
	{
		private readonly IMarvelApiService _marvelService;

		private int characterId;

		public InformacoesViewModel(int id)
		{
			_marvelService = new MarvelApiService();
			characterId = id;
		}

		private CharacterItemViewModel character;

		public CharacterItemViewModel Character
		{
			get
			{
				return character;
			}
			set
			{
				character = value;
				OnPropertyChanged("Character");
			}
		}

		private List<Comics> comicsList;

		public List<Comics> ComicsList
		{
			get
			{
				return comicsList;
			}
			set
			{
				comicsList = value;
				OnPropertyChanged("ComicsList");
			}
		}

		public async Task LoadData()
		{
			IsBusy = true;

			var result = await BlobCache.LocalMachine.GetOrFetchObject<MarvelApiData<Characters>>("CH"+ characterId.ToString(),
																						() => _marvelService.GetCharacter(characterId),
																					   DateTimeOffset.Now.AddDays(2));

			if (result != null)
			{
				Character = (from p in result.Results
							 select new CharacterItemViewModel()
							 {
								 Id = p.Id,
								 Name = p.Name,
								 Thumbnail = p.Thumbnail.Path + "." + p.Thumbnail.Extension,
								 Description = p.Description
							 }).SingleOrDefault();
			}

			await LoadComics();

			IsBusy = false;

		}

		private async Task LoadComics()
		{
			var result = await BlobCache.LocalMachine.GetOrFetchObject<MarvelApiData<Comics>>("CM" + characterId.ToString(),
			                                                                                  () => _marvelService.GetComics(characterId),
																						   DateTimeOffset.Now.AddDays(2));

			if (result != null)
			{
				ComicsList = (from p in result.Results
							  select new Comics()
							  {
								  Id = p.Id,
								  Title = p.Title
							  }).ToList();
			}
		}
	}
}
