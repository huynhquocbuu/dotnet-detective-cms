cd src/services/ordering
dotnet ef migrations add CmsDbMigrate01 -p Infrastructure --startup-project AppFrontEnd -c CmsDbContext --output-dir Persistence/Migrations
dotnet ef migrations remove -p Ordering.Infrastructure --startup-project Ordering.API
dotnet ef database update -p Infrastructure --startup-project AppFrontEnd -c CmsDbContext


https://github.com/telerik/kendo-ui-demos-service/blob/master/demos-and-odata-v3/KendoCRUDService/Controllers

dotnet ef migrations add CmsDbMigrate01 -p Cms.Infrastructure --startup-project Cms.WebMvc -c CmsDbContext --output-dir Persistence/Migrations


dotnet restore
dotnet build -c Release -o /app/build

RUN dotnet publish -c Release -o /app/publish