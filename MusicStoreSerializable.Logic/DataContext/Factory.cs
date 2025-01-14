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
            throw new NotImplementedException();
        }
    }
}
