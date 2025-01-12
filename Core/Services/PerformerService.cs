using Core.DTOs.Users;
using Core.Includes;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mapping;
using Core.Models;
using Core.Responses.Performers;
using System.Linq.Expressions;

namespace Core.Services;

public class PerformerService (
    IPerformerRepository performerRepository, 
    IUserRepository userRepository,
    IUserService userService
) : IPerformerService
{
    public Task<Performer> GetPerformer(Expression<Func<Performer, bool>> predicate)
    {
        return performerRepository.GetPerfomer(predicate);
    }

    public async Task<PerformersResponse> GetPerformers(QueryAllUsersDTO queryAllUsersDTO)
    {
        Expression<Func<Performer, bool>> predicate = performer =>
            String.IsNullOrWhiteSpace(queryAllUsersDTO.Search)
            || performer.User.Username.Contains(queryAllUsersDTO.Search, StringComparison.CurrentCultureIgnoreCase);

        var performers = await performerRepository.GetPerformers(predicate);

        return performers.ToSendInvitationPerformersResponse();
    }


    // ------------------  FAVORITE PERFORMERS ENDPOINTS SECTION   ----------------------------

    public async Task<IEnumerable<FavoritePerformerResponse>> GetFavoritePerformers(Guid userId)
    {
        var userExist = await userRepository.Exist(u => u.Id == userId);
        if (!userExist) throw new Exception("User not found");

        var favoritePerformers = await performerRepository.GetfavoritePerformers(userId);
        var performerIds = favoritePerformers.Select(favoritePerformer => favoritePerformer.PerformerId);

        var performersRelatedUsers = await performerRepository.GetPerformersRelatedUsers(
            performer => performerIds.Contains(performer.Id));

        List<FavoritePerformerResponse> response = [];
        foreach (var performerRelatedUser in performersRelatedUsers)
        {
            var rating = await userService.GetUserAverageRating(performerRelatedUser.Id.ToString());

            var favoritePerformerResponse = performerRelatedUser.ToFavoritePerformerResponse();
            favoritePerformerResponse.Rating = rating;
            response.Add(favoritePerformerResponse);
        }

        return response;
    }

    public async Task<bool> IsFavoritePerformer(Guid userId, Guid performerId)
    {
        var userExist = await userRepository.Exist(u => u.Id == userId);
        if (!userExist) throw new Exception("User not found");

        return await performerRepository.IsFavoritePerformer(userId, performerId);
    }

    public async void AddFavoritePerformer(Guid userId, Guid performerId)
    {
        var userExist = await userRepository.Exist(u => u.Id == userId);
        if (!userExist) throw new Exception("User not found");

        var perfomrmerExist = await performerRepository.Exist(performerId);
        if (!perfomrmerExist) throw new Exception("Performer not found");

        var favoritePerformer = new FavoritePerformer
        {
            UserId = userId,
            PerformerId = performerId
        };

        performerRepository.AddFavoritePerformer(favoritePerformer);
    }

    public async void RemoveFavoritePerformer(Guid userId, Guid performerId)
    {
        var userExist = await userRepository.Exist(u => u.Id == userId);
        if (!userExist) throw new Exception("User not found");

        var perfomrmerExist = await performerRepository.Exist(performerId);
        if (!perfomrmerExist) throw new Exception("Performer not found");

        performerRepository.RemoveFavoritePerformer(userId, performerId);
    }
}
