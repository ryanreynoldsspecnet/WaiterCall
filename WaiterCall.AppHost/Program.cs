var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WaiterCall_Api>("waitercall-api");

builder.AddProject<Projects.WaiterCall_Web>("waitercall-web");

builder.Build().Run();
