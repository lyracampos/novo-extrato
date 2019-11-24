using System;
using GL.NovoExtrato.Shared.Entities;
using MediatR;

namespace GL.NovoExtrato.Shared.Commands
{ 
    public interface ICommand : IRequest<Resultado>
    { }
}
