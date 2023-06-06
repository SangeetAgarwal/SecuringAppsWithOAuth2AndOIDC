using Microsoft.EntityFrameworkCore;
using Notes.API.Datalayer.DbSet;
using System.Reflection;

namespace Notes.API.Datalayer.Extensions
{
    public static class ModelBuilderExtensions
    {
        #region Global Query Filter: https://stackoverflow.com/questions/45096799/filter-all-queries-trying-to-achieve-soft-delete/45097532#45097532

        public static void HasSoftDeleteQueryFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            HasSoftDeleteQueryFilterMethod
                .MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        private static readonly MethodInfo HasSoftDeleteQueryFilterMethod = 
            typeof(ModelBuilderExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static).Single(t => t.IsGenericMethod && t.Name == nameof(HasSoftDeleteQueryFilter));

        public static void HasSoftDeleteQueryFilter<TEntity>(this ModelBuilder modelBuilder) where TEntity : Base
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.DeleteFlag);
        }

        #endregion
    }
}
