using System;
using System.Collections.Generic;
using _247fandom.DTOs;
using _247fandom.Mappers.Utilities;
using _247fandom.Models;

namespace _247fandom.Mappers
{
    public class UserDataMapper
    {
        public UserDataMapper()
        {
        }

        public User mapModelToState(User requestUser, User dbUser)
        {
            if (requestUser.FirstName != null)
            {
                dbUser.FirstName = requestUser.FirstName;
            }

            if (requestUser.LastName != null)
            {
                dbUser.LastName = requestUser.LastName;
            }

            if (requestUser.AvatarUrl != null)
            {
                dbUser.AvatarUrl = requestUser.AvatarUrl;
            }

            if (requestUser.Email != null)
            {
                dbUser.Email = requestUser.Email;
            }

            if (requestUser.UserName != null)
            {
                dbUser.UserName = requestUser.UserName;
            }

            dbUser.UpdatedAt = DateTime.UtcNow;
            return dbUser;
        }


        public UserResponseDTO MapUserResponse(User user)
        {
            var userData = new UserResponseDTO
            {
                User = UserUtil.BuildUserData(user),
                Posts = user.Posts != null ? MapPosts(user.Posts) : new List<PostResponseDTO>(),
                Memberships = user.Memberships != null ? MapMembership(user.Memberships): new List<CommunityDTO>()

            };
            return userData;
        }

        public List<UserDTO> MapUserResponse(List<User> users)
        {
            List<UserDTO> allUsers = new();
            foreach (var user in users)
            {
                var userData = UserUtil.BuildUserData(user);
                allUsers.Add(userData);
            }
            return allUsers;
        }

        private static List<PostResponseDTO> MapPosts(ICollection<Post> posts)
        {
            List<PostResponseDTO> postList = new();
            foreach(Post post in posts) {
                var newPostDTO = new PostResponseDTO
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt
                };
                postList.Add(newPostDTO);
            }

            return postList;
        }

        private List<CommunityDTO> MapMembership(ICollection<Community> communities)
        {
            List<CommunityDTO> memberships = new();
            foreach(Community community in communities)
            {
                var newCommunityDTO = new CommunityDTO
                {
                    CommunityId = community.CommunityId,
                    Name = community.Name,
                    HandleName = community.HandleName,
                    About = community.About,
                    AvatarUrl = community.AvatarUrl,
                    CreatedAt = community.CreatedAt,
                    UpdatedAt = community.UpdatedAt
                };
                memberships.Add(newCommunityDTO);
            }

            return memberships;
            
        }


    }
}
