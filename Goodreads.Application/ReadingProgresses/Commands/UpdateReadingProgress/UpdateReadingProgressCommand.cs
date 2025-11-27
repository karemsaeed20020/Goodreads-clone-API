using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.ReadingProgresses.Commands.UpdateReadingProgress
{
    public record UpdateReadingProgressCommand(string BookId, int CurrentPage) : IRequest<Result>;
}
