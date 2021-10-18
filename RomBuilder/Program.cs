using System;
using Microsoft.Extensions.DependencyInjection;
using RomBuilder;
using RomBuilder.Commands;

await ConfigureServices()
    .GetRequiredService<IApplication>()
    .Run(args);

static IServiceProvider ConfigureServices() =>
    new ServiceCollection()
        .AddTransient<IApplication, Application>()
        .AddTransient<ICommandBuilder, BuildRomCommand>()
        .AddTransient<ICommandBuilder, ExtractRomCommand>()
        .BuildServiceProvider();
