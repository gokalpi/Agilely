using FluentValidation;

namespace Agilely.BoardApi.DTO
{
    public class BoardDTO
    {
        public string TenantId { get; set; }
        public string Tenant { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    }

    public class BoardDTOValidator : AbstractValidator<BoardDTO>
    {
        public BoardDTOValidator()
        {
            RuleFor(o => o.TenantId).NotEmpty();
            RuleFor(o => o.Name).NotEmpty();
        }
    }
}