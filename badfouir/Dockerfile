# Load development image and set working directory in image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development
# Copy relevant files to image and download needed dependencies
COPY ./bad4/*.csproj .
RUN dotnet restore bad4.csproj

# Copy everything else to the image and compile the application
COPY ./bad4/ .
RUN dotnet publish bad4.csproj --os linux --arch x64 -c release -o /app

# Set deployment image and copy build to the final app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "bad4.dll", "--enveironment=Development"]


