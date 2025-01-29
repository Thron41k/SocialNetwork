using SocialNetwork.DAL.Repositories.Interfaces;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.BLL.Exceptions;

namespace SocialNetwork.BLL.Services;

public class UserService
{
    private readonly IUserRepository _userRepository = new UserRepository();

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

        if (_userRepository.FindByEmail(userRegistrationData.Email) != null)
            throw new ArgumentNullException();

        var userEntity = new UserEntity
        {
            Firstname = userRegistrationData.FirstName,
            Lastname = userRegistrationData.LastName,
            Password = userRegistrationData.Password,
            Email = userRegistrationData.Email
        };

        if (_userRepository.Create(userEntity) == 0)
            throw new Exception();

    }

    public User Authenticate(UserAuthenticationData userAuthenticationData)
    {
        var findUserEntity = _userRepository.FindByEmail(userAuthenticationData.Email);
        if (findUserEntity is null) throw new UserNotFoundException(userAuthenticationData.Email);

        if (findUserEntity.Password != userAuthenticationData.Password)
            throw new WrongPasswordException();

        return ConstructUserModel(findUserEntity);
    }

    public User FindByEmail(string? email)
    {
        var findUserEntity = _userRepository.FindByEmail(email);
        if (findUserEntity is null) throw new UserNotFoundException(email);

        return ConstructUserModel(findUserEntity);
    }

    public void Update(User user)
    {
        var updatableUserEntity = new UserEntity
        {
            Id = user.Id,
            Firstname = user.FirstName,
            Lastname = user.LastName,
            Password = user.Password,
            Email = user.Email,
            Photo = user.Photo,
            FavoriteMovie = user.FavoriteMovie,
            FavoriteBook = user.FavoriteBook
        };

        if (_userRepository.Update(updatableUserEntity) == 0)
            throw new Exception();
    }

    private User ConstructUserModel(UserEntity userEntity)
    {
        return new User(userEntity.Id,
            userEntity.Firstname,
            userEntity.Lastname,
            userEntity.Password,
            userEntity.Email,
            userEntity.Photo,
            userEntity.FavoriteMovie,
            userEntity.FavoriteBook);
    }
}