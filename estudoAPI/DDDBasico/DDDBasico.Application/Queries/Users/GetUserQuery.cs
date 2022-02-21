﻿using DDDBasico.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Queries.Users
{
    public record GetUserQuery(int Id) : IRequest<string>;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, string>
    {

        private readonly IRepositoryUser _repository;

        public GetUserQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public Task<string> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = _repository.GetById(request.Id);
            Console.WriteLine(user);
            return Task.FromResult("Sucesso");

        }
    }
}