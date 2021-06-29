using System;
using System.Collections.Generic;
using _247fandom.DTOs;
using _247fandom.Mappers.Utilities;
using _247fandom.Models;

namespace _247fandom.Mappers
{
    public class CommunityDataMapper
    {
        public CommunityDataMapper()
        {
        }

        public CommunityResposeDTO MapCommunityRespose(Community community)
        {
            var communityDTO = new CommunityDTO
            {
                CommunityId = community.CommunityId,
                Name = community.Name,
                HandleName = community.HandleName,
                AvatarUrl = community.AvatarUrl,
                CreatedAt = community.CreatedAt,
                UpdatedAt = community.UpdatedAt

            };
            List<PostResponseDTO> posts = new();
            foreach(Post post in community.Posts)
            {
                var newPostData = new PostResponseDTO
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt,
                    Author = post.User != null ? UserUtil.BuildUserData(post.User) : null,
                    Comments = post.Comments != null ? CommentUtil.MapCommentData(post.Comments) : new List<CommentResponseDTO>()
                };
                posts.Add(newPostData);
            }

            List<UserDTO> users = new();
            foreach (User user in community.Members)
            {
                var userData = UserUtil.BuildUserData(user);
                users.Add(userData);
            }

            return new CommunityResposeDTO
            {
                Community = communityDTO,
                Posts = posts,
                Members = users
            };

        }

    }
}
