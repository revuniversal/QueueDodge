using Microsoft.EntityFrameworkCore;

namespace QueueDodge
{
    public static class Extensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
