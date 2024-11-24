<div align="center">

<h1>mov2gif</h1>

<p align="center">
<a href="https://mov2gif.lanedirt.com">Live demo 🔥</a>
</p>

<h3 align="center">
Convert .mov to .gif via simple Web UI
</h3>

[<img src="https://img.shields.io/github/actions/workflow/status/lanedirt/mov2gif/dotnet-integration-tests.yml?label=integration tests">](https://github.com/lanedirt/mov2gif/actions/workflows/dotnet-integration-tests.yml)

</div>

mov2gif is an open-source .mov to .gif converter built with C# ASP.NET technology. It provides a simple web interface to convert your .mov files to .gif format with customizable settings like quality, frame rate, and resolution.

## Live demo
A live demo of the app is available at [mov2gif.lanedirt.com](https://mov2gif.lanedirt.com)

## Installation

The easiest way to run mov2gif is using Docker. The container includes all required dependencies (including FFmpeg).

```bash
docker run -d -p 3100:80 ghcr.io/lanedirt/mov2gif:latest
```

After running the container, the web UI will be available at:
- http://localhost:3100

## Contributing

Contributions are welcome! Feel free to submit a Pull Request.

## Tech stack / credits
The following technologies, frameworks and libraries are used in this project:

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - A simple, modern, object-oriented, and type-safe programming language.
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) - An open-source framework for building modern, cloud-based, internet-connected applications.
- [Blazor Server](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) - A framework for building interactive web UIs using C#.
- [FFmpeg](https://ffmpeg.org/) - A complete, cross-platform solution to record, convert and stream audio and video.
- [Docker](https://www.docker.com/) - A platform for building, sharing, and running containerized applications.
- [Tailwind CSS](https://tailwindcss.com/) - A utility-first CSS framework for rapidly building custom designs.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
