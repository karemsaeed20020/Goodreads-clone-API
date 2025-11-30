using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Interfaces.Authorization;
using Goodreads.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Authorization
{
    internal class QuoteAuthorizationService(IUnitOfWork unitOfWork, IUserContext userContext) : IQuoteAuthorizationService
    {
        public async Task<bool> IsOwnerOrAdminAsync(string quoteId)
        {
            var userId = userContext.UserId;
            if (string.IsNullOrEmpty(userId))
                return false;

            // Check if user is admin
            if (userContext.IsInRole(Roles.Admin))
                return true;

            // Check if user is the owner of the quote
            var quote = await unitOfWork.Quotes.GetByIdAsync(quoteId);
            if (quote is null || quote.CreatedByUserId != userId) return false;

            return quote.CreatedByUserId == userId;
        }
    }
}
