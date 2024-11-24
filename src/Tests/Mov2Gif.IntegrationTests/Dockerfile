FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files first for better layer caching
COPY ["src/Tests/Mov2Gif.IntegrationTests/Mov2Gif.IntegrationTests.csproj", "Tests/Mov2Gif.IntegrationTests/"]
COPY ["src/Utilities/Mov2Gif.Utilities.GifConverter/Mov2Gif.Utilities.GifConverter.csproj", "Utilities/Mov2Gif.Utilities.GifConverter/"]
RUN dotnet restore "Tests/Mov2Gif.IntegrationTests/Mov2Gif.IntegrationTests.csproj"

# Copy the rest of the source code
COPY ["src/Tests/Mov2Gif.IntegrationTests/", "Tests/Mov2Gif.IntegrationTests/"]
COPY ["src/Utilities/Mov2Gif.Utilities.GifConverter/", "Utilities/Mov2Gif.Utilities.GifConverter/"]

# Install FFmpeg
RUN apt-get update && \
    apt-get install -y ffmpeg && \
    rm -rf /var/lib/apt/lists/*

# Set the entry point to run tests
ENTRYPOINT ["dotnet", "test", "Tests/Mov2Gif.IntegrationTests/Mov2Gif.IntegrationTests.csproj", "--verbosity", "normal"]
