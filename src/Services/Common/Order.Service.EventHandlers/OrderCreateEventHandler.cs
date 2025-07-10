using MediatR;
using Microsoft.Extensions.Logging;
using Order.Persistence.Database;
using Order.Service.EventHandlers.Commands;
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Commands;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderCreateEventHandler> _logger;
        private readonly ICatalogProxy _catalogProxy;

        public OrderCreateEventHandler(
            ApplicationDbContext context,
            ILogger<OrderCreateEventHandler> logger,
            ICatalogProxy catalogProxy)
        {
            _context = context;
            _logger = logger;
            _catalogProxy = catalogProxy;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("-- New order creation started");
            var entry = new Domain.Order();

            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                //01. Prepare detail
                _logger.LogInformation("-- Preparing detail");
                PrepareDetail(entry, notification);

                //02. Prepare header
                _logger.LogInformation("-- Preparing header");
                PrepareDetail(entry, notification);

                //03. Create order
                _logger.LogInformation("-- Creating order");
                await _context.AddAsync(entry);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"--- Order {entry.OrderId} was created");

                //04. Update Stock
                //pediente
                await _catalogProxy.UpdateStockAsync(new ProductInStockUpdateStockCommand
                {
                    Items = notification.Items.Select(x => new ProductInStockUpdateItem
                    {
                        ProductId = x.ProductId,
                        Stock = x.Quantity,
                        Action = ProductInStockAction.Substract
                    })
                });

                await trx.CommitAsync();
            }
            _logger.LogInformation("-- New order creation ended");
        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand notification)
        {

        }
    }
}
