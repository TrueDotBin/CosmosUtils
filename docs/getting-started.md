# Getting Started

## Prerequisites

- [.NET SDK 10.0][dotnet-install] or later
- [Cosmos Gen 3][cosmos-install]

# Steps

1. Install .NET and Cosmos (read the [Prerequisites](#prerequisites))
2. Create a new kernel project:
```bash
cosmos new MyKernel
cd MyKernel
```
3. Once the project has been created, add the package to your project:

```bash
dotnet package add TrueDotBin.CosmosUtils
```

1. Add these lines to the top of your Kernel.cs:
```cs
using TrueDotBin.CosmosUtils;
using TrueDotBin.CosmosUtils.FileSystem; // File system utilities
using TrueDotBin.CosmosUtils.Graphics;   // Nine-sliced images and text alignment
using TrueDotBin.CosmosUtils.Logging;    // Logger class
using TrueDotBin.CosmosUtils.Resources;  // Embedded resource loading
```

See the [API docs](../api/) for exact signatures.

[cosmos-install]: https://cosmosos.github.io/articles/user/install.html
[dotnet-install]: https://dotnet.microsoft.com/download
[latest-release]: https://github.com/TrueDotBin/CosmosUtils/releases/latest