# Cosmos Utilities

**TrueDotBin.CosmosUtils** is a utility package containing QoL features for Cosmos kernels.

## Features

### Logging

| Feature | Description | Usage |
|---------|-------------|-------|
| Logger | Writes leveled, formatted log messages to the console | `Logger.Global` in `TrueDotBin.CosmosUtils.Logging` |

### Graphics

| Feature | Description | Usage |
|---------|-------------|-------|
| Nine-sliced image scaling | Scales an image without distorting its corners and edges | `canvas.DrawNineSliced(...)` in `TrueDotBin.CosmosUtils.Graphics` |

### Resources

| Feature | Description | Usage |
|---------|-------------|-------|
| Embedded resource loading | Loads a file embedded in an assembly | `EmbeddedResourceLoader` in `TrueDotBin.CosmosUtils.Resources` |

### File system

| Feature | Description | Usage |
|---------|-------------|-------|
| Partition utility | Creates disk partitions on a block device | `PartitionUtility` in `TrueDotBin.CosmosUtils.FileSystem` |

## Requirements

- A Gen3 kernel
- [Resource loading](#resources) requires an assembly, use `typeof(Kernel).Assembly` or `Assembly.GetExecutingAssembly()`