﻿FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/Mov2Gif.Client/Mov2Gif.Client.csproj", "Mov2Gif.Client/"]
COPY ["src/Utilities/Mov2Gif.Utilities.GifConverter/Mov2Gif.Utilities.GifConverter.csproj", "Utilities/Mov2Gif.Utilities.GifConverter/"]
RUN dotnet restore "Mov2Gif.Client/Mov2Gif.Client.csproj"

COPY ["src/Mov2Gif.Client/", "Mov2Gif.Client/"]
COPY ["src/Utilities/Mov2Gif.Utilities.GifConverter/", "Utilities/Mov2Gif.Utilities.GifConverter/"]

RUN apt-get update && apt-get install -y nodejs npm
WORKDIR /src/Mov2Gif.Client
RUN npm install
RUN npm run build:client-css

WORKDIR /src
RUN dotnet publish "Mov2Gif.Client/Mov2Gif.Client.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install FFmpeg. This is also done in the Mov2Gif.IntegrationTests Dockerfile.
# When changing this, also make sure to update the Dockerfile in the integration tests project.
RUN apt-get update && \
    apt-get install -y ffmpeg && \
    rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# Set ASP.NET Core environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

ENTRYPOINT ["dotnet", "Mov2Gif.Client.dll"]
