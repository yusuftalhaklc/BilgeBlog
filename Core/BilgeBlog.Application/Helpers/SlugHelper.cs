    namespace BilgeBlog.Application.Helpers
    {
        public static class SlugHelper
        {
            private static readonly Slugify.SlugHelper _slugifier = new();

            public static string GenerateSlug(string title)
            {
                if (string.IsNullOrWhiteSpace(title))
                    return Guid.NewGuid().ToString();

                return _slugifier.GenerateSlug(title);
            }

            public static string GenerateUniqueSlug(string baseSlug, Func<string, Task<bool>> slugExistsAsync)
            {
                return GenerateUniqueSlugAsync(baseSlug, slugExistsAsync).GetAwaiter().GetResult();
            }

            public static async Task<string> GenerateUniqueSlugAsync(string baseSlug, Func<string, Task<bool>> slugExistsAsync)
            {
                var slug = GenerateSlug(baseSlug);
                if (string.IsNullOrEmpty(slug))
                    slug = "post";

                var uniqueSlug = slug;
                var counter = 1;

                while (await slugExistsAsync(uniqueSlug))
                {
                    uniqueSlug = $"{slug}-{counter}";
                    counter++;
                }

                return uniqueSlug;
            }
        }
    }

