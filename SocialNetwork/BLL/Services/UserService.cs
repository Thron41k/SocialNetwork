using System.ComponentModel.DataAnnotations;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.BLL.Services;

public class UserService(IUserRepository userRepository, IFriendRepository friendRepository)
{
    private readonly MessageService _messageService = new();

    public UserService() : this(new UserRepository(), new FriendRepository())
    {
    }

    public void Register(UserRegistrationData userRegistrationData)
    {
        if (string.IsNullOrEmpty(userRegistrationData.FirstName))
            throw new ArgumentNullException();

        if (string.IsNullOrEmpty(userRegistrationData.LastName))
            throw new ArgumentNullException();

        if (string.IsNullOrEmpty(userRegistrationData.Password))
            throw new ArgumentNullException();

        if (string.IsNullOrEmpty(userRegistrationData.Email))
            throw new ArgumentNullException();

        if (userRegistrationData.Password.Length < 8)
            throw new ArgumentNullException();

        if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
            throw new ArgumentNullException();

        if (userRepository.FindByEmail(userRegistrationData.Email) != null)
            throw new ArgumentNullException();

        var userEntity = new UserEntity
        {
            Firstname = userRegistrationData.FirstName,
            Lastname = userRegistrationData.LastName,
            Password = userRegistrationData.Password,
            Email = userRegistrationData.Email
        };

        if (userRepository.Create(userEntity) == 0)
            throw new Exception();

    }

    public User Authenticate(UserAuthenticationData userAuthenticationData)
    {
        var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
        if (findUserEntity is null) throw new UserNotFoundException();

        if (findUserEntity.Password != userAuthenticationData.Password)
            throw new WrongPasswordException();

        return ConstructUserModel(findUserEntity);
    }

    public User FindByEmail(string? email)
    {
        var findUserEntity = userRepository.FindByEmail(email);
        if (findUserEntity is null) throw new UserNotFoundException();

        return ConstructUserModel(findUserEntity);
    }

    public User FindById(int id)
    {
        var findUserEntity = userRepository.FindById(id);
        if (findUserEntity is null) throw new UserNotFoundException();

        return ConstructUserModel(findUserEntity);
    }

    public void Update(User? user)
    {
        var updatableUserEntity = new UserEntity
        {
            Id = user!.Id,
            Firstname = user.FirstName,
            Lastname = user.LastName,
            Password = user.Password,
            Email = user.Email,
            Photo = user.Photo,
            FavoriteMovie = user.FavoriteMovie,
            FavoriteBook = user.FavoriteBook
        };

        if (userRepository.Update(updatableUserEntity) == 0)
            throw new Exception();
    }

    private IEnumerable<User?> GetFriendsByUserId(int userId)
    {
        return friendRepository.FindAllByUserId(userId)
            .Select(friendsEntity => FindById(friendsEntity.FriendId));
    }

    public void AddFriend(UserAddingFriendData userAddingFriendData)
    {
        var findUserEntity = userRepository.FindByEmail(userAddingFriendData.FriendEmail);
        if (findUserEntity is null) throw new UserNotFoundException();

        var friendEntity = new FriendEntity
        {
            UserId = userAddingFriendData.UserId,
            FriendId = findUserEntity.Id
        };

        if (friendRepository.Create(friendEntity) == 0)
            throw new Exception();
    }

    private User ConstructUserModel(UserEntity userEntity)
    {
        var incomingMessages = _messageService.GetIncomingMessagesByUserId(userEntity.Id);
        var outgoingMessages = _messageService.GetOutcomingMessagesByUserId(userEntity.Id);
        var friends = GetFriendsByUserId(userEntity.Id);

        return new User(userEntity.Id,
            userEntity.Firstname,
            userEntity.Lastname,
            userEntity.Password,
            userEntity.Email,
            userEntity.Photo,
            userEntity.FavoriteMovie,
            userEntity.FavoriteBook,
            incomingMessages,
            outgoingMessages,
            friends);
    }
}