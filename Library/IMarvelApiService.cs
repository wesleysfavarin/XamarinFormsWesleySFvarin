using System;
using System.Threading.Tasks;

namespace Marvel
{
	public interface IMarvelApiService
	{
		Task<MarvelApiData<Characters>> GetCharacters(string filter = null, int limit = 0, int offset = 0);
		Task<MarvelApiData<Characters>> GetCharacter(int id);
		Task<MarvelApiData<Comics>> GetComics(int id);
	}
}
