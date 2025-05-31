# INNOCEL ASPNET CORE ASSESMENT

## Tech Stack

- **ASP.NET Core** 9.0.0
- **C#** 13
- **Docker** 28.0.4

## API ACCESS

- Run in postman desktop application not in web for localhost to work
- [Visit API or clone to your postman](https://www.postman.com/payload-specialist-8137764/workspace/aimanafiq-work-s/collection/33511040-fcea6fdb-9452-4276-a365-8098e9dbef8c?action=share&creator=33511040&active-environment=33511040-9d7a82ea-a89b-4855-9953-f9d97a444f8f)

## Postman ENV

- Pick Innocel Assesment environment
- Sometimes, the {{endpoint}} environment not loaded correctly, but for using docker and MSSQL database both is at localhost:9260

## Important Step

- There are two ways this dotnet app could be run which is docker and using MSSQL database desktop

## Docker [RECOMMENDED]

### [IMPORTANT]

- go to appsettings.Development.json
- for DefaultConnection replace this ONLY if you are using Docker

```bash
"DefaultConnection": "Server=sqlserver,1433;Database=innocel_asessment;User Id=sa;Password=DockerSQL2022_;Encrypt=True;TrustServerCertificate=True;"
```

1. Windows setup

- Intall WSL
- Install Docker desktop and run the software

2. Mac setup

- Install Docker desktop and run the software

3. Linux

- Install Docker
- Install Docker Compose V2

You might be able to use the command below for Linux docker compose v2:

```bash
sudo apt install docker-compose-v2
```

- Run docker build command

```bash
docker compose up --build
```

- You should already got the 2 containers and 1 volume

- Check with command

```bash
docker volume ps -a
docker volume ls
```

1. innocelasessment-api-1 and innocelasessment-sqlserver-1
2. innocelasessment_sqlserver_data

- Your containers should already started, you can check with below command

```bash
docker ps
```

## MSSQL Database localhost

### [IMPORTANT]

- go to appsettings.Development.json
- for DefaultConnection replace this ONLY if you are using MSSQL Database

- Windows

```bash
"DefaultConnection": "Data Source={replace this with what your OS called in MSSQL};Initial Catalog=innocel_asessment;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
```

- For example my Data Source is LAPTOP-NCFJ1P2M\\SQLEXPRESS, also don't forget to remove {}

- Linux / possibility with Mac also ? unsure.

```bash
"DefaultConnection": "Server=localhost,1433;Database=innocel_asessment;User Id=sa;Password=DockerSQL2022_;Encrypt=True;TrustServerCertificate=True;"
```

1. Install MSSQL database desktop and make a database called

```bash
innocel_asessment
```

- Tips
- If you are using Linux and Mac, you need to make container for MSSQL Database
- If you are using Windows, the MSSQL Database is available

2. Add Migrate and run the dotnet application

```bash
cd api
dotnet ef migrations add Init
dotnet watch run
```

3. Update the database

```bash
dotnet ef database update
```

## LOGS

- For logs, if you use Docker or MSSQL Database it's in different place

1. MSSQL database localhost

- The path in api/Logs

2. Docker

- rootPath/Logs
- Also in docker to remove the Logs folder, you could not just delete it. You need to use sudo.

Linux / Mac. Windows unsure, maybe in WSL you could use sudo command?

```bash
sudo rm -rf ./Logs
```
