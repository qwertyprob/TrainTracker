FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TrainTracker.Api/TrainTracker.Api.csproj", "TrainTracker.Api/"]
COPY ["TrainTracker.BLL/TrainTracker.BLL.csproj", "TrainTracker.BLL/"]
COPY ["TrainTracker.DAL/TrainTracker.DAL.csproj", "TrainTracker.DAL/"]
COPY ["TrainTracker.DTO/TrainTracker.DTO.csproj", "TrainTracker.DTO/"]

RUN dotnet restore "TrainTracker.Api/TrainTracker.Api.csproj"

COPY . .

WORKDIR "/src/TrainTracker.Api"
RUN dotnet publish "TrainTracker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TrainTracker.Api.dll"]
