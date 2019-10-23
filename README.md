OpenData.Core
=====================

OpenData.Core is a C# library which parses an OData query uri into an object model which can be used to query custom data sources which are not IQueryable.

## Installation

To use it in your own Web API you need to install the nuget package `Install-Package OpenData.Core`

## Configuration

Somewhere in your application startup/webapi configuration, specify the types which will be used for OData queries:

```csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Wire-up OData and define the Entity Data Model
        config.UseOData(entityDataModelBuilder =>
        {
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name);
            entityDataModelBuilder.RegisterEntitySet<Employee>("Employees", x => x.EmailAddress);
            entityDataModelBuilder.RegisterEntitySet<Order>("Orders", x => x.OrderId);
            entityDataModelBuilder.RegisterEntitySet<Product>("Products", x => x.Name);
        });

        // Use Attribute Mapping for the OData controllers
        config.MapHttpAttributeRoutes();
    }
}
```

Note that when you register an Entity Set, you also specify the name of the Entity Set. The name needs to match the URL you intend to use so if you use `http://myservice/odata/Products` then register the Entity Set using `.RegisterEntitySet<Product>("Products", x => x.Name);`, if you use `http://myservice/odata/Product` then register the Entity Set using `.RegisterEntitySet<Product>("Product", x => x.Name);`.

## Usage

In your controller(s), define a Get method which accepts a single parameter of ODataQueryOptions:

```csharp
public IEnumerable<Category> Get(ODataQueryOptions queryOptions)
{
    // Implement query logic.
}
```

### Supported OData Versions

The library supports OData 4.0