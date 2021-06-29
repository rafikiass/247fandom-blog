using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _247fandom.Models;
using _247fandom.DTOs;
using _247fandom.Mappers.Utilities;

namespace _247fandom.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Votes/5
        [HttpGet("post/{postId}/votes")]
        public async Task<ActionResult<VoteResponseDTO>> GetPostVote(long postId)
        {
            var post = await _context.Post
                .Include(p => p.Votes)
                .ThenInclude(v => v.User)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                return NotFound();
            }
                
            return BuildVoteResponse<Post>(post);
        }


        [HttpPost("voteForPost")]
        public async Task<ActionResult<object>> PostVote(VoteRequestDTO voteRequest)
        {
            if(voteRequest.Votable.Equals("PostVote"))
            {
                var postVote = BuildPostVote(voteRequest);
                _context.PostVotes.Add(postVote);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPostVote", new { id = postVote.Id }, postVote);
            }
            return NotFound();
        }

        [HttpPost("voteForComment")]
        public async Task<ActionResult<CommentVote>> PostCommentVote(VoteRequestDTO voteRequest)
        {
            if (voteRequest.Votable.Equals("CommentVote"))
            {
                var commentVote = BuildCommentVote(voteRequest);
                _context.CommentVotes.Add(commentVote);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCommentVote", new { id = commentVote.Id }, commentVote);
            }
            return NotFound();
        }

        // DELETE: api/Votes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePostVote(long id)
        //{
        //    var postVote = await _context.PostVotes.FindAsync(id);
        //    if (postVote == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PostVotes.Remove(postVote);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool PostVoteExists(long id)
        {
            return _context.PostVotes.Any(e => e.Id == id);
        }


        private static PostVote BuildPostVote(VoteRequestDTO voteRequest)
        {
            return new PostVote
            {
                PostId = voteRequest.VotableId,
                UserId = voteRequest.UserId,
                VoteType = voteRequest.VoteType,
            };
        }


        private static CommentVote BuildCommentVote(VoteRequestDTO voteRequest)
        {
            return new CommentVote
            {
                CommentId = voteRequest.VotableId,
                UserId = voteRequest.UserId,
                VoteType = voteRequest.VoteType,
            };
        }

        private  VoteResponseDTO BuildVoteResponse<T>(T votable)
        {
            Type type = votable.GetType();
            if(type == typeof(Post))
            {
                Post post = votable as Post;
                return BuildPostWithVote(post);

            } else if(type == typeof(Comment))
            {
                Comment comment = votable as Comment;
                return BuildCommentWithVote(comment);
            }
            return null;
        }

        private  VoteResponseDTO BuildPostWithVote(Post post)
        {
            return new VoteResponseDTO
            {
                Votable = new PostResponseDTO
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt,
                    Author = UserUtil.BuildUserData(post.User)
                },
                Votes = MapVotes<PostVote>(post.Votes)
            };
        }


        private  VoteResponseDTO BuildCommentWithVote(Comment comment)
        {
            return new VoteResponseDTO
            {
                Votable = new CommentResponseDTO
                {
                    Comment = CommentUtil.GetCommentData(comment),
                    Author = UserUtil.BuildUserData(comment.User)
                },
                Votes = MapVotes<CommentVote>(comment.Votes)
            };
        }

        public  List<VoteDTO> MapVotes<T>(ICollection<T> tParam)
        {
            Type type = tParam.GetType();
            var votesList = new List<VoteDTO>();
            if (typeof(ICollection<PostVote>).IsAssignableFrom(type))
            {
                var votes = tParam as ICollection<PostVote>;
                foreach (PostVote pv in votes)
                {
                    var vdto = BuildVoteData(pv.Id, (int) pv.VoteType, pv.User);
                    votesList.Add(vdto);
                }
            } else if (typeof(ICollection<CommentVote>).IsAssignableFrom(type))
            {
                var votes = tParam as ICollection<CommentVote>;
                foreach (CommentVote cv in votes)
                {
                    var vdto = BuildVoteData(cv.CommentId, (int) cv.VoteType, cv.User);
                    votesList.Add(vdto);
                }
            }

            return votesList;
        }

        private  VoteDTO BuildVoteData(long objectId, int voteType, User user)
        {
            return new VoteDTO
            {
                VoteId = objectId,
                VoteType = voteType,
                User = UserUtil.BuildUserData(user)
            };
        }
    }
}
