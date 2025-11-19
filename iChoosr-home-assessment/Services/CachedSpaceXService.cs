using iChoosr_home_assessment.PayloadModels;
using Microsoft.Extensions.Caching.Memory;

namespace iChoosr_home_assessment.Services
{
    public class CachedSpaceXService : ISpaceXService
    {
        private readonly SpaceXService _spaceXService;
        private readonly IMemoryCache _cache;
        private const int CacheExpirationMinutes = 5;

        public CachedSpaceXService(
            SpaceXService spaceXService,
            IMemoryCache cache)
        {
            _spaceXService = spaceXService;
            _cache = cache;
        }

        public async Task<List<Payload>> GetAllPayloadsAsync()
        {
            const string cacheKey = "all_payloads";

            if (_cache.TryGetValue(cacheKey, out List<Payload>? cachedPayloads))
            {
                return cachedPayloads!;
            }

            var payloads = await _spaceXService.GetAllPayloadsAsync();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

            _cache.Set(cacheKey, payloads, cacheOptions);

            return payloads;
        }

        public async Task<Payload?> GetPayloadByIdAsync(string id)
        {
            var cacheKey = $"payload_{id}";

            if (_cache.TryGetValue(cacheKey, out Payload? cachedPayload))
            {
                return cachedPayload;
            }

            var payload = await _spaceXService.GetPayloadByIdAsync(id);

            if (payload != null)
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

                _cache.Set(cacheKey, payload, cacheOptions);
            }

            return payload;
        }
    }
}
