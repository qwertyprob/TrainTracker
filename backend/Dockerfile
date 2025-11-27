FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["TrainTracker.Api/TrainTracker.Api.csproj", "TrainTracker.Api/"]
COPY ["TrainTracker.BLL/TrainTracker.BLL.csproj", "TrainTracker.BLL/"]
COPY ["TrainTracker.DAL/TrainTracker.DAL.csproj", "TrainTracker.DAL/"]
COPY ["TrainTracker.DTO/TrainTracker.DTO.csproj", "TrainTracker.DTO/"]

RUN dotnet restore "TrainTracker.Api/TrainTracker.Api.csproj"
COPY . ./
WORKDIR "/src/TrainTracker.Api"
RUN dotnet build "./TrainTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TrainTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrainTracker.Api.dll"]
