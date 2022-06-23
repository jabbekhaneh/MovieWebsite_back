#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Prsentation/Portal.WebApi/Portal.WebApi.csproj", "src/Prsentation/Portal.WebApi/"]
COPY ["src/Library/Portal.EF/Portal.EF.csproj", "src/Library/Portal.EF/"]
COPY ["src/Library/Portal.Application/Portal.Application.csproj", "src/Library/Portal.Application/"]
COPY ["src/Library/Portal.Domain/Portal.Domain.csproj", "src/Library/Portal.Domain/"]
COPY ["src/Library/Portal.Extentions/Portal.Extentions.csproj", "src/Library/Portal.Extentions/"]
RUN dotnet restore "src/Prsentation/Portal.WebApi/Portal.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Prsentation/Portal.WebApi"
RUN dotnet build "Portal.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Portal.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portal.WebApi.dll"]