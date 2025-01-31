﻿using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories;

public class FriendRepository : BaseRepository, IFriendRepository
{
    public IEnumerable<FriendEntity> FindAllByUserId(int userId)
    {
        return Query<FriendEntity>(@"select * from friends where userId = :user_id", new { user_id = userId });
    }

    public int Create(FriendEntity friendEntity)
    {
        return Execute(@"insert into friends (userId,friendId) values (:UserId,:FriendId)", friendEntity);
    }

    public int Delete(int id)
    {
        return Execute(@"delete from friends where id = :id_p", new { id_p = id });
    }
}