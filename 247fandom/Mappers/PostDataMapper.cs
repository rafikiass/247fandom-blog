using System;
using System.Collections.Generic;
using _247fandom.DTOs;
using _247fandom.Mappers.Utilities;
using _247fandom.Models;

namespace _247fandom.Mappers
{
    public class PostDataMapper
    {
        public PostDataMapper()
        {
        }

        public PostResponseDTO MapPostData(Post post)
        {
            return new PostResponseDTO
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                Author = UserUtil.BuildUserData(post.User),
                Comments = post.Comments != null? CommentUtil.MapCommentData(post.Comments) : new List<CommentResponseDTO>(),

            };
        }   
    }
}
