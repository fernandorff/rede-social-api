using RedeSocial.Contracts.Repositories;
using RedeSocial.Entities;
using RedeSocial.Enums;
using RedeSocial.Implementations.FriendshipEntity.Contracts.Mappers;
using RedeSocial.Implementations.FriendshipEntity.Contracts.Responses;
using RedeSocial.Implementations.FriendshipEntity.Messages;
using RedeSocial.Implementations.FriendshipEntity.Services.Interfaces;
using RedeSocial.Implementations.UserEntity.Contracts.Mappers;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Messages;

namespace RedeSocial.Implementations.FriendshipEntity.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUserRepository _userRepository;

        public FriendshipService(IFriendshipRepository friendshipRepository, IUserRepository userRepository)
        {
            _friendshipRepository = friendshipRepository;
            _userRepository = userRepository;
        }

        public async Task<FriendshipDto> SendFriendRequest(Guid authenticatedUserId, Guid requestedToUserId)
        {
            var response = new FriendshipDto();

            if (authenticatedUserId == requestedToUserId)
            {
                response.AddErrorMessage(FriendshipServiceErrorMessages.SameUserFriendRequest);
                return response;
            }

            var friendshipCheck = await _friendshipRepository.GetFriendshipByUsers(authenticatedUserId, requestedToUserId);
            if (friendshipCheck != null)
            {
                response.AddErrorMessage(FriendshipServiceErrorMessages.AlreadySentRequest);
                return response;
            }

            var requestedByUser = await _userRepository.GetUserById(authenticatedUserId);
            if (requestedByUser == null)
            {
                response.AddErrorMessage(UserServiceErrorMessages.UserNotFound(authenticatedUserId));
                return response;
            }

            var requestedToUser = await _userRepository.GetUserById(requestedToUserId);
            if (requestedToUser == null)
            {
                response.AddErrorMessage(UserServiceErrorMessages.UserNotFound(requestedToUserId));
                return response;
            }

            var existingFriendship = await _friendshipRepository.GetFriendshipByUsers(requestedToUserId, requestedToUserId);
            if (existingFriendship != null && existingFriendship.FriendRequestStatus == FriendRequestStatus.Pending)
            {
                existingFriendship.SetFriendRequestFlag(FriendRequestStatus.Accepted);
                await _friendshipRepository.UpdateFriendship(existingFriendship);
                response.AddSuccessMessage(FriendshipServiceSuccessMessages.FriendRequestAccepted);
                return response;
            }

            var friendship = new Friendship(requestedByUser, requestedToUser, FriendRequestStatus.Pending);
            await _friendshipRepository.AddFriendship(friendship);

            response = friendship.ToDto();
            response.AddSuccessMessage(FriendshipServiceSuccessMessages.FriendRequestSent);
            return response;
        }

        public async Task<FriendshipDto> AcceptFriendRequest(Guid requestedByUserId, Guid authenticatedUserId)
        {
            var response = new FriendshipDto();

            var friendship = await _friendshipRepository.GetFriendshipByUsers(requestedByUserId, authenticatedUserId);
            if (friendship == null)
            {
                response.AddErrorMessage(FriendshipServiceErrorMessages.FriendshipNotFound);
                return response;
            }

            if (friendship.RequestedToUserId != authenticatedUserId)
            {
                response.AddErrorMessage(FriendshipServiceErrorMessages.NotRequestedToUser);
                return response;
            }

            friendship.SetFriendRequestFlag(FriendRequestStatus.Accepted);
            await _friendshipRepository.UpdateFriendship(friendship);

            response = friendship.ToDto();
            response.AddSuccessMessage(FriendshipServiceSuccessMessages.FriendRequestAccepted);
            return response;
        }

        public async Task<FriendshipDto> DeclineFriendRequest(Guid requestedByUserId, Guid authenticatedUserId)
        {
            var response = new FriendshipDto();

            var friendship = await _friendshipRepository.GetFriendshipByUsers(requestedByUserId, authenticatedUserId);
            if (friendship == null)
            {
                response.AddErrorMessage(FriendshipServiceErrorMessages.FriendshipNotFound);
                return response;
            }

            if (friendship.RequestedToUserId != authenticatedUserId)
            {
                response.AddErrorMessage(FriendshipServiceErrorMessages.NotRequestedToUser);
                return response;
            }

            response = friendship.ToDto();
            response.AddSuccessMessage(FriendshipServiceSuccessMessages.FriendRequestDeclined);
            await _friendshipRepository.DeleteFriendship(friendship);

            return response;
        }

        public async Task<FriendshipDto> Unfriend(Guid targetUserId, Guid authenticatedUserId)
        {
            var response = new FriendshipDto();

            if (authenticatedUserId == targetUserId)
            {
                response.AddErrorMessage(FriendshipServiceErrorMessages.SameUserUnfriend);
                return response;
            }

            var friendship = await _friendshipRepository.GetFriendshipByUsers(targetUserId, authenticatedUserId);
            if (friendship == null)
            {
                response.AddErrorMessage(FriendshipServiceErrorMessages.FriendshipNotFound);
                return response;
            }

            response = friendship.ToDto();
            response.AddSuccessMessage(FriendshipServiceSuccessMessages.Unfriend);
            await _friendshipRepository.DeleteFriendship(friendship);

            return response;
        }

        public async Task<List<UserDto>> GetFriendListByUserId(Guid userId)
        {
            var friends = await _friendshipRepository.GetFriendListByUserId(userId);
            return friends.Select(u => u.ToDto()).ToList();
        }

        public async Task<List<UserDto>> GetSentFriendRequestsByUserId(Guid userId)
        {
            var sentRequests = await _friendshipRepository.GetSentFriendRequestsByUserId(userId);
            return sentRequests.Select(u => u.ToDto()).ToList();
        }

        public async Task<List<UserDto>> GetReceivedFriendRequestsByUserId(Guid userId)
        {
            var receivedRequests = await _friendshipRepository.GetReceivedFriendRequestsByUserId(userId);
            return receivedRequests.Select(u => u.ToDto()).ToList();
        }

        public async Task<List<UserDto>> GetNonFriendUsers(Guid userId)
        {
            var nonFriendUsers = await _friendshipRepository.GetNonFriendUsers(userId);
            return nonFriendUsers.Select(u => u.ToDto()).ToList();
        }

        public async Task<List<UserDto>> SearchNonFriendUsersByPartialMatchOfFirstNameNicknameOrEmail(Guid userId, string searchQuery)
        {
            var nonFriendUsers = await _friendshipRepository.SearchNonFriendUsersByPartialMatchOfFirstNameOrNicknameOrEmail(userId, searchQuery);
            return nonFriendUsers.Select(u => u.ToDto()).ToList();
        }

        public async Task<List<UserDto>> SearchFriendsByPartialMatchOfFirstNameNicknameOrEmail(Guid userId, string searchQuery)
        {
            var nonFriendUsers = await _friendshipRepository.SearchFriendsByPartialMatchOfFirstNameOrNicknameOrEmail(userId, searchQuery);
            return nonFriendUsers.Select(u => u.ToDto()).ToList();
        }
    }
}

