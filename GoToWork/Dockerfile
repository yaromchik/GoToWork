#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GoToWork/GoToWork.csproj", "GoToWork/"]
COPY ["GoToWork_DataAccess/GoToWork_DataAccess.csproj", "GoToWork_DataAccess/"]
COPY ["GoToWork_Models/GoToWork_Models.csproj", "GoToWork_Models/"]
RUN dotnet restore "GoToWork/GoToWork.csproj"
COPY . .
WORKDIR "/src/GoToWork"
RUN dotnet build "GoToWork.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoToWork.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoToWork.dll"]