using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Commands.DeleteAuhtor
{
    public record DeleteAuthorCommand(string Id) : IRequest<Result>;
}
