using MusicStoreSerializable.Logic.Models;

namespace MusicStoreSerializable.Logic.DataContext
{
    public sealed class DbSet<T> : List<T> where T : IdentityObject
    {
        new public void Add(T entity)
        {
            int maxId = 1;

            if (this.Count > 0)
            {
                maxId = this.Max(e => e.Id) + 1;
            }
            entity.Id = maxId;
            base.Add(entity);
        }
    }
}
