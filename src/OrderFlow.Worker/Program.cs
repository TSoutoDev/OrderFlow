using OrderFlow.Worker;
using OrderFlow.Worker.Configuration;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(RabbitMqOptions.SectionName));
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run(); 
