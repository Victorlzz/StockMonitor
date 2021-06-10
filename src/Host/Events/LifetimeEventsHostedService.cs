using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stock.Contract.GatewayContract;
using Stock.Contract.StockContract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHost.Events
{
    public class LifetimeEventsHostedService : IHostedService, IDisposable
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ILogger _logger;
        private readonly IGatewayServiceProvider _gatewayServiceProvider;
        private Timer _timerProcess;
        private bool _process = true;
        private bool disposedValue;

        public LifetimeEventsHostedService(IHostApplicationLifetime appLifetime, ILogger<LifetimeEventsHostedService> logger, IGatewayServiceProvider gatewayServiceProvider)
        {
            _appLifetime = appLifetime;
            _logger = logger;
            _gatewayServiceProvider = gatewayServiceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            _timerProcess = new Timer(CaptureStockData, null, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private void CaptureStockData(object state)
        {
            if (!_process)
                return;
            _process = false;
            Task capture = Task.Run(async () =>
            {
                await this._gatewayServiceProvider.Get<IStockMonitorService>().Monitor();
            });
            capture.Wait();
            _process = true;
        }


        public Task StopAsync(CancellationToken cancellationToken)

        {
            _timerProcess?.Change(Timeout.Infinite, Timeout.Infinite);


            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");

            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");

            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");

            // Perform post-stopped activities here
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _timerProcess.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LifetimeEventsHostedService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
