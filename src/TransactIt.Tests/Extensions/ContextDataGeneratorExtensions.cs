using System;
using System.Linq;
using System.Text;
using TransactIt.Data.Contexts;

namespace TransactIt.Tests.Extensions
{
    public static class ContextDataGeneratorExtensions
    {
        public static Random Random = new Random();
        public static (bool, int[]) AddTestData<TEntity>(this NoTrackingContext context, int quantity) where TEntity : class
        {
            for (int i = 0; i < quantity; i++)
            {
                var entity = Activator.CreateInstance<TEntity>();
                var properties = entity.GetType().GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name == "Id")
                    {
                        continue;
                    }

                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(entity, Random.GenerateString(10));
                    }
                }
                context.Set<TEntity>().Add(entity);
            }

            context.SaveChanges();

            var ids = context.Set<TEntity>()
                .Select(x => x)
                .ToArray()
                .Select(x => (int)x.GetType().GetProperty("Id").GetValue(x))
                .ToArray();

            return (true, ids);
        }

        public static (bool, int[]) AddTestData<TEntity>(this TrackingContext context, int quantity) where TEntity : class
        {
            for (int i = 0; i < quantity; i++)
            {
                var entity = Activator.CreateInstance<TEntity>();
                var properties = entity.GetType().GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name == "Id")
                    {
                        continue;
                    }

                    if (property.Name.EndsWith("Id"))
                    {
                        property.SetValue(entity, i+1);
                    }

                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(entity, Random.GenerateString(10));
                    }
                }
                context.Set<TEntity>().Add(entity);
            }

            context.SaveChanges();

            var ids = context.Set<TEntity>()
                .Select(x => x)
                .ToArray()
                .Select(x => (int)x.GetType().GetProperty("Id").GetValue(x))
                .ToArray();

            return (true, ids);
        }

        public static string GenerateString(this Random random, int length)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}
