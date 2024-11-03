using System.Linq.Expressions;
using Tracks.Core.Models;

namespace Tracks.Core.Interfaces
{
    public interface ITrackRepository
    {
        public Task<Track?> GetTrack(
            Expression<Func<Track, bool>> predicate,
            params Expression<Func<Track, object>>[] includes);
        public Task<List<Track>> GetTracks(
            Expression<Func<Track, bool>> predicate,
            params Expression<Func<Track, object>>[] includes);
        public Task<List<Track>> GetPaginationTracks(
            Expression<Func<Track, bool>> predicate,
            IEnumerable<Expression<Func<Track, object>>> includes = null,
            string sortBy = null,
            bool sortDescending = false);

        public Task<IEnumerable<Genre>> GetTrackGenres(string trackId);
        public Task<Track> CreateTrack(Track track);
        public Task RemoveTrack(Track track);
        public Task<List<Genre>> GetGenres(IEnumerable<string> trackIds);
    }
}
