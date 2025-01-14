using MusicStoreSerializable.Logic.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicStoreSerializable.Logic.DataContext
{
    /// <summary>
    /// Represents the data context for the Music Store application.
    /// </summary>
    [Serializable]
    internal sealed class MusicStoreContext : IMusicStoreContext
    {
        #region fields
        public static readonly string DbFile = "MusicStore.json";
        public static readonly JsonSerializerOptions JsonOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        #endregion fields

        #region Properties
        /// <summary>
        /// Gets or sets the collection of genres.
        /// </summary>
        public DbSet<Models.Genre> GenreSet { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of artists.
        /// </summary>
        public DbSet<Models.Artist> ArtistSet { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of albums.
        /// </summary>
        public DbSet<Models.Album> AlbumSet { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of tracks.
        /// </summary>
        public DbSet<Models.Track> TrackSet { get; set; } = [];
        #endregion Properties

        #region methods
        /// <summary>
        /// Creates relationships between entities.
        /// </summary>
        internal void CreateRelationships()
        {
            AlbumSet.ForEach(a =>
            {
                a.Artist = ArtistSet.FirstOrDefault(e => e.Id == a.ArtistId);
                a.Tracks = TrackSet.Where(t => t.AlbumId == a.Id).ToList();
            });
            GenreSet.ForEach(g =>
            {
                g.Tracks = TrackSet.Where(t => t.GenreId == g.Id).ToList();
            });
            ArtistSet.ForEach(a =>
            {
                a.Albums = AlbumSet.Where(al => al.ArtistId == a.Id).ToList();
            });
            TrackSet.ForEach(t =>
            {
                t.Album = AlbumSet.FirstOrDefault(e => e.Id == t.AlbumId);
                t.Genre = GenreSet.FirstOrDefault(e => e.Id == t.GenreId);
            });
        }

        /// <summary>
        /// Validates relationships between entities.
        /// </summary>
        internal void ValidateObjectModel()
        {
            AlbumSet.ForEach(a =>
            {
                if (a.Artist == default)
                {
                    throw new InvalidOperationException($"Artist not found for album {a.Title}");
                }
            });
            ArtistSet.ForEach(a =>
            {
                a.Albums.ForEach(al =>
                {
                    if (al.Artist == default)
                    {
                        throw new InvalidOperationException($"Artist Id mismatch for album {al.Title}");
                    }
                });
            });
            // Check if Artist.Name is unique.
            if (ArtistSet.GroupBy( a => a.Name.ToLower()).Count() != ArtistSet.Count())
            {
                throw new InvalidOperationException($"Artist.Name is not unique.");
            }

            // Check if Genre.Name is unique.
            if (GenreSet.GroupBy(g => g.Name.ToLower()).Count() != GenreSet.Count())
            {
                throw new InvalidOperationException($"Genre.Name is not unique.");
            }

            TrackSet.ForEach(t =>
            {
                if (t.Genre == default)
                {
                    throw new InvalidOperationException($"Genre not found for track {t.Title}");
                }
                if (t.Album == default)
                {
                    throw new InvalidOperationException($"Album not found for track {t.Title}");
                }
            });
        }

        /// <summary>
        /// Dissolves relationships between entities.
        /// </summary>
        internal void DissolvingRelationships()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves changes to the data context.
        /// </summary>
        public void SaveChanges()
        {
            CreateRelationships();
            ValidateObjectModel();

            var jsonString = JsonSerializer.Serialize<MusicStoreContext>(this, JsonOptions);

            File.WriteAllText(DbFile, jsonString);
        }
        #endregion methods
    }
}
