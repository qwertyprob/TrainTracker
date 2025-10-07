using FluentValidation;
using TrainTracker.DTO;

namespace TrainTracker.Validators;

public class IncidentValidator :AbstractValidator<IncidentDto>
{

    public IncidentValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required!")
            .MustAsync(async (username, cancellation) => 
            {
                return username != "admin";
            }).WithMessage("Username 'admin' is not allowed");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Reason is required!");
        
        
    }
    
    
}