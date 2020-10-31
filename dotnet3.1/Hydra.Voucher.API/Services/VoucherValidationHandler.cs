using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hydra.Core.Integration.Messages.VoucherMessages;
using Hydra.Core.MessageBus;
using Hydra.Voucher.API.Data;
using Hydra.Voucher.API.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hydra.Voucher.API.Services
{
    public class VoucherValidationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
         private readonly IServiceProvider _serviceProvider; //Use this to inject the context scope in a singleton instance.

        public VoucherValidationHandler(IMessageBus messageBus, IServiceProvider serviceProvider)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetRespond();
            _messageBus.AdvancedBus.Connected += OnConnect;

            return Task.CompletedTask;
        }
        
        private async Task<VoucherResponseMessage> GetVoucher(VoucherIntegrationEvent message)
        {
            var voucher = new Vouchers();
            using(var scope = _serviceProvider.CreateScope()) // Create scope inside the singleton (Lifecicle scope)
            {
                //Basically service locator is used When it is outside the context or when the class cannot pass arguments through the constructor
                var voucherRepository = scope.ServiceProvider.GetRequiredService<IVoucherRepository>();
                voucher = await voucherRepository.FindByCode(message.Code);
            }


            ValidationResult result = new ValidationResult();

            if(voucher == null)
            {
                result.Errors.Add(new ValidationFailure("Voucher", "Voucher not found"));
                return new VoucherResponseMessage(null, 0, -1, DateTime.Now, false, false, result); 
            }

            if(!voucher.Active)
                result.Errors.Add(new ValidationFailure("Active", "Voucher is not active"));
            
            if(voucher.ExpirationDate < DateTime.UtcNow)
                result.Errors.Add(new ValidationFailure("Active", "Voucher is expired"));

            return new VoucherResponseMessage(voucher.Code, 
            voucher.VoucherType == Models.VoucherType.Value ? voucher.DiscountPercentage : voucher.DiscountAmount,
            (int)voucher.VoucherType, voucher.ExpirationDate, voucher.Active, false, result);
        }

         /// <summary>
        /// It will renew the subscription when the application will be abble to connect with RabbitMQ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnConnect(object sender, EventArgs e) => SetRespond();

        private void SetRespond()
        {
              _messageBus.RespondAsync<VoucherIntegrationEvent, VoucherResponseMessage>(async request =>
                await GetVoucher(request));
        }
    }
}