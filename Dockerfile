FROM mcr.microsoft.com/dotnet/sdk:8.0 as sdk
WORKDIR /src
COPY . /src

RUN curl -sL https://deb.nodesource.com/setup_18.x | bash - 
RUN apt-get install -y nodejs
RUN npm install -g npm@latest

RUN dotnet publish -c Release -o api src/Infrastructure
RUN npm install --prefix ./src/Presentation
RUN npm run build --prefix ./src/Presentation

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=sdk src/api /app
COPY --from=sdk src/spa /app/wwwroot
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "CloudHotel.Infrastructure.dll"]