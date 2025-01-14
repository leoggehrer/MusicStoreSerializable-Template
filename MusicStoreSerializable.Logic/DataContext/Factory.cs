using MusicStoreSerializable.Logic.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicStoreSerializable.Logic.DataContext
{
    /// <summary>
    /// Factory class to create instances of IMusicStoreContext.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Creates an instance of IMusicStoreContext.
        /// </summary>
        /// <returns>An instance of IMusicStoreContext.</returns>
        public static IMusicStoreContext CreateMusicStoreContext()
        {
            MusicStoreContext? result = null;

            if (File.Exists(MusicStoreContext.DbFile))
            {
                string jsonString = File.ReadAllText(MusicStoreContext.DbFile);

                result = JsonSerializer.Deserialize<MusicStoreContext>(jsonString, MusicStoreContext.JsonOptions);
                result?.CreateRelationships();
            }
            else
            {
                result = new MusicStoreContext();

                result.GenreSet = DataLoader.LoadGenresFromCsv("data/Genres.csv");
                result.AlbumSet = DataLoader.LoadAlbumsFromCsv("data/Albums.csv");
                result.ArtistSet = DataLoader.LoadArtistsFromCsv("data/Artists.csv");
                result.TrackSet = DataLoader.LoadTracksFromCsv("data/Tracks.csv");
            }
            return result!;
        }
    }
}
