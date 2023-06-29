using RedeSocial.Enums;
using RedeSocial.Implementations.PostEntity.Messages;
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Implementations.PostEntity.Contracts.Requests;

public class PostCreateRequest
{
    [Required(ErrorMessage = PostRequestErrorMessages.BlankTitle)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = PostRequestErrorMessages.BlankText)]
    public string Text { get; set; } = string.Empty;

    public string PictureUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = PostRequestErrorMessages.BlankVisibility)]
    [EnumDataType(typeof(PostVisibility), ErrorMessage = PostRequestErrorMessages.InvalidVisibility)]
    public PostVisibility Visibility { get; set; }
}