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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates relationships between entities.
        /// </summary>
        internal void ValidateRelationships()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion methods
    }
}
