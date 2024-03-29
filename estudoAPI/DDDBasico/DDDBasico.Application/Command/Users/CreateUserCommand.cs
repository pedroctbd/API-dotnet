﻿using DDDBasico.Application.DTO;
using DDDBasico.Application.Extras;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace DDDBasico.Application.Users.Command
{
    public record CreateUserCommand(String UserName, String Password, String email) : IRequest<Response>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
    {

        private readonly IRepositoryUser _repository;


        public CreateUserCommandHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using var hmac = new HMACSHA512();
            var user = new User
            {
                UserName = request.UserName.ToLower(),
                email = request.email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key
            };
            _repository.Add(user);

            return new Response(new UserDTO {Id=user.Id,UserName=user.UserName,email=user.email,drink_counter=user.drink_counter });

        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        public CreateUserCommandValidator(IRepositoryUser repository)
        {
            RuleFor(newUser => newUser.UserName).NotEmpty().MaximumLength(100);
            RuleFor(newUser => newUser.email).NotEmpty().MaximumLength(100).EmailAddress();
            RuleFor(newUser => newUser.Password).NotEmpty().MaximumLength(12).MinimumLength(4);
            RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetUserByEmail(newUser.email) != null).WithMessage("Email taken");

        }
  
    }


}


