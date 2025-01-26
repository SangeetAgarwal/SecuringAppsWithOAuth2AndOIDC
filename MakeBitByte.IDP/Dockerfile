# ------------------ STAGE 1: Build ------------------
  FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
  WORKDIR /src
  
  # Copy .csproj and restore
  COPY *.csproj ./
  RUN dotnet restore
  
  # Copy the rest of your source
  COPY . ./
  
  RUN dotnet publish -c Release -o /app
  
  # ------------------ STAGE 2: Runtime ------------------
  FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
  WORKDIR /app
  
  # Copy published files from 'build' stage
  COPY --from=build /app ./
  
  # ----------- Dev Certificate Setup (Local Dev) -----------
  # 1) Copy the dev cert (make sure it exists in your project folder!)
  COPY aspnetcore-dev-cert.pfx /app/aspnetcore-dev-cert.pfx
  
  # 2) Set environment variables for Kestrel to use HTTPS
  ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/aspnetcore-dev-cert.pfx
  ENV ASPNETCORE_Kestrel__Certificates__Default__Password=MyPw123
  
  # Expose ports for HTTP (80) and HTTPS (443)
  EXPOSE 80 443
  
  # Make the app listen on both 80 & 443
  ENV ASPNETCORE_URLS=http://+:80;https://+:443
  
  ENTRYPOINT ["dotnet", "MakeBitByte.IDP.dll"]
  