using Microsoft.AspNetCore.SignalR.Client;
using System.Net;

namespace testWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("*Worker running at: {time}", DateTimeOffset.Now);

            //using (var context = new Models.test_dbContext())
            //{

            Models.Model m = new Models.Model();

            var dte = m.GetTestSp();
            _logger.LogInformation($"Date: {dte}");

            //}

            if (true)
            {
                string host = Environment.GetEnvironmentVariable("WS_SITE") ?? "localhost";
                string port = Environment.GetEnvironmentVariable("WS_PORT") ?? "5114";



                _logger.LogInformation($"Host {host}");

                var entry = Dns.GetHostEntry(host);
                foreach (var a in entry.AddressList)
                {
                    _logger.LogInformation($"Address {a}");
                    string finalHost = $"http://{a}:{port}/chatHub";


                    //Set connection
                    var connection = new HubConnectionBuilder()
                    .WithUrl(finalHost)
                    .Build();

                    //  new HubConnection("http://localhost:5114/");
                    //Make proxy to hub based on hub name on server
                    // var myHub = connection.CreateHubProxy("chatHub");
                    //Start connection

                    try
                    {
                        await connection.StartAsync();
                        _logger.LogInformation("Connection started");
                        //connectButton.IsEnabled = false;
                        //sendButton.IsEnabled = true;

                        await connection.InvokeAsync("SendMessage",
                        "userrr", $"messageee {DateTime.Now}");
                        await connection.StopAsync();

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                    }

                }




            }

            await Task.Delay(3000, stoppingToken);
        }
    }
}
