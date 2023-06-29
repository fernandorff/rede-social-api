using RedeSocial.Implementations.CommentEntity.Messages;
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Implementations.CommentEntity.Contracts.Requests;

public class CommentEditRequest
{
    [Required(ErrorMessage = CommentRequestErrorMessages.BlankText)]
    public string Text { get; set; } = string.Empty;
}