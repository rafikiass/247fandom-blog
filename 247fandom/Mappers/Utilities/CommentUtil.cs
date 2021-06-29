using System;
using System.Collections.Generic;
using _247fandom.DTOs;
using _247fandom.Models;

namespace _247fandom.Mappers.Utilities
{
    public class CommentUtil
    {
        public static List<CommentResponseDTO> MapCommentData(ICollection<Comment> comments)
        {
            List<CommentResponseDTO> commentList = new();
            foreach (Comment comment in comments)
            {
                var commentResponseData = new CommentResponseDTO
                {
                    Comment = GetCommentData(comment),
                    Author = UserUtil.BuildUserData(comment.User)
                };
                commentList.Add(commentResponseData);
            }
            return commentList;
        }

        public static CommentDTO GetCommentData(Comment comment)
        {
            var commentData = new CommentDTO
            {
                CommentId = comment.CommentId,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt
            };
            return commentData;
        }
    }
}
