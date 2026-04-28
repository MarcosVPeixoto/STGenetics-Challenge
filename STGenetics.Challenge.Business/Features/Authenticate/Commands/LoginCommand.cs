using STGenetics.Challenge.Business.Responses;
using STGenetics.Challenge.Business.Responses.Commands;
using MediatR;

namespace STGenetics.Challenge.Business.Commands
{
    public record LoginCommand : IRequest<RequestHandlerResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; } 
    }
}
