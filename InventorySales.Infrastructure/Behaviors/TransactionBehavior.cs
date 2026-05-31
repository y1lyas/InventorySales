using FluentValidation;
using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Features;
using InventorySales.Infrastructure.Exceptions;
using InventorySales.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.Behaviours
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly InventoryDbContext _context;
        private readonly ICacheService _cache;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        public TransactionBehavior(InventoryDbContext context, ILogger<TransactionBehavior<TRequest, TResponse>> logger, ICacheService cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (request is not ICommand)
                return await next();

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                var hasActiveTransaction = _context.Database.CurrentTransaction != null;
                if (hasActiveTransaction)
                {
                    return await next();
                }

                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var response = await next();

                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return response;
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    throw new ConcurrencyException();
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        } 
    }
}
