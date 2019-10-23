﻿namespace OpenData.Core.Tests.Model
{
    using System;
    using NorthwindModel;
    using OData.Model;
    using Xunit;

    public class EntityDataModelBuilderTests
    {
        [Fact]
        public void CollectionNamesCaseInsensitiveByDefault_RegisterCollectionThrowsArgumentExceptionForDuplicateKeyVaryingOnlyOnCasing()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder(StringComparer.OrdinalIgnoreCase);
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name);

            var exception = Assert.Throws<ArgumentException>(() => entityDataModelBuilder.RegisterEntitySet<Category>("categories", x => x.Name));
            Assert.Equal("An item with the same key has already been added.", exception.Message);
        }

        [Fact]
        public void CollectionNamesCaseInsensitiveByDefault_ResolveCollectionVaryingCollectionNameCasing()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder(StringComparer.OrdinalIgnoreCase);
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name);

            var entityDataModel = entityDataModelBuilder.BuildModel();

            Assert.Same(entityDataModel.EntitySets["categories"], entityDataModel.EntitySets["Categories"]);
        }

        public class WhenCalling_BuildModelWith_Models_AndCustomEntitySetName
        {
            private readonly EntityDataModel entityDataModel;

            public WhenCalling_BuildModelWith_Models_AndCustomEntitySetName()
            {
                var entityDataModelBuilder = new EntityDataModelBuilder(StringComparer.OrdinalIgnoreCase);
                entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name, Capabilities.Insertable | Capabilities.Updatable | Capabilities.Deletable);
                entityDataModelBuilder.RegisterEntitySet<Customer>("Customers", x => x.CompanyName, Capabilities.Updatable);
                entityDataModelBuilder.RegisterEntitySet<Employee>("Employees", x => x.Id);
                entityDataModelBuilder.RegisterEntitySet<Manager>("Managers", x => x.Id);
                entityDataModelBuilder.RegisterEntitySet<Order>("Orders", x => x.OrderId, Capabilities.Insertable | Capabilities.Updatable);
                entityDataModelBuilder.RegisterEntitySet<Product>("Products", x => x.ProductId, Capabilities.Insertable | Capabilities.Updatable);

                this.entityDataModel = entityDataModelBuilder.BuildModel();
            }

            [Fact]
            public void EntityDataModel_Current_IsSetToTheReturnedModel()
            {
                Assert.Same(this.entityDataModel, EntityDataModel.Current);
            }

            [Fact]
            public void The_Categories_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Categories"));

                var entitySet = this.entityDataModel.EntitySets["Categories"];

                Assert.Equal("Categories", entitySet.Name);
                Assert.Equal(Capabilities.Insertable | Capabilities.Updatable | Capabilities.Deletable, entitySet.Capabilities);

                var edmComplexType = entitySet.EdmType;

                Assert.Null(edmComplexType.BaseType);
                Assert.Equal(typeof(Category), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Category", edmComplexType.FullName);
                Assert.Equal("Category", edmComplexType.Name);
                Assert.Equal(1, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Name", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType.Properties[0], entitySet.EntityKey);
            }

            [Fact]
            public void The_Customers_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Customers"));

                var entitySet = this.entityDataModel.EntitySets["Customers"];

                Assert.Equal("Customers", entitySet.Name);
                Assert.Equal(Capabilities.Updatable, entitySet.Capabilities);

                var edmComplexType = entitySet.EdmType;

                Assert.Null(edmComplexType.BaseType);
                Assert.Equal(typeof(Customer), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Customer", edmComplexType.FullName);
                Assert.Equal("Customer", edmComplexType.Name);
                Assert.Equal(4, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("City", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("CompanyName", edmComplexType.Properties[1].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[1].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("Country", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("LegacyId", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.Int32, edmComplexType.Properties[3].PropertyType);

                Assert.Same(edmComplexType.Properties[1], entitySet.EntityKey);
            }

            [Fact]
            public void The_Employees_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Employees"));

                var entitySet = this.entityDataModel.EntitySets["Employees"];

                Assert.Equal("Employees", entitySet.Name);
                Assert.Equal(Capabilities.None, entitySet.Capabilities);

                var edmComplexType = entitySet.EdmType;

                Assert.Null(edmComplexType.BaseType);
                Assert.Equal(typeof(Employee), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Employee", edmComplexType.FullName);
                Assert.Equal("Employee", edmComplexType.Name);
                Assert.Equal(8, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("AccessLevel", edmComplexType.Properties[0].Name);
                Assert.Same(EdmType.GetEdmType(typeof(AccessLevel)), edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("BirthDate", edmComplexType.Properties[1].Name);
                Assert.Same(EdmPrimitiveType.Date, edmComplexType.Properties[1].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("EmailAddress", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("Forename", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[3].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[4].DeclaringType);
                Assert.Equal("Id", edmComplexType.Properties[4].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[4].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[5].DeclaringType);
                Assert.Equal("ImageData", edmComplexType.Properties[5].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[5].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[6].DeclaringType);
                Assert.Equal("Surname", edmComplexType.Properties[6].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[6].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[7].DeclaringType);
                Assert.Equal("Title", edmComplexType.Properties[7].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[7].PropertyType);

                Assert.Same(edmComplexType.Properties[4], entitySet.EntityKey);
            }

            [Fact]
            public void The_Managers_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Managers"));

                var entitySet = this.entityDataModel.EntitySets["Managers"];

                Assert.Equal("Managers", entitySet.Name);
                Assert.Equal(Capabilities.None, entitySet.Capabilities);

                var edmComplexType = entitySet.EdmType;

                Assert.Equal(EdmType.GetEdmType(typeof(Employee)), edmComplexType.BaseType);
                Assert.Equal(typeof(Manager), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Manager", edmComplexType.FullName);
                Assert.Equal("Manager", edmComplexType.Name);
                Assert.Equal(1, edmComplexType.Properties.Count); // Does not include inherited properties

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("AnnualBudget", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.Decimal, edmComplexType.Properties[0].PropertyType);

                Assert.Null(entitySet.EntityKey);
            }

            [Fact]
            public void The_Orders_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Orders"));

                var entitySet = this.entityDataModel.EntitySets["Orders"];

                Assert.Equal("Orders", entitySet.Name);
                Assert.Equal(Capabilities.Insertable | Capabilities.Updatable, entitySet.Capabilities);

                var edmComplexType = entitySet.EdmType;

                Assert.Null(edmComplexType.BaseType);
                Assert.Equal(typeof(Order), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Order", edmComplexType.FullName);
                Assert.Equal("Order", edmComplexType.Name);
                Assert.Equal(5, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Freight", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.Decimal, edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("OrderDetails", edmComplexType.Properties[1].Name);
                Assert.IsType<EdmCollectionType>(edmComplexType.Properties[1].PropertyType);
                Assert.Equal(EdmType.GetEdmType(typeof(OrderDetail)), ((EdmCollectionType)edmComplexType.Properties[1].PropertyType).ContainedType);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("OrderId", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.Int64, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("ShipCountry", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[3].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[4].DeclaringType);
                Assert.Equal("TransactionId", edmComplexType.Properties[4].Name);
                Assert.Same(EdmPrimitiveType.Guid, edmComplexType.Properties[4].PropertyType);

                Assert.Same(edmComplexType.Properties[2], entitySet.EntityKey);
            }

            [Fact]
            public void The_Products_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Products"));

                var entitySet = this.entityDataModel.EntitySets["Products"];

                Assert.Equal("Products", entitySet.Name);
                Assert.Equal(Capabilities.Insertable | Capabilities.Updatable, entitySet.Capabilities);

                var edmComplexType = entitySet.EdmType;

                Assert.Null(edmComplexType.BaseType);
                Assert.Equal(typeof(Product), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Product", edmComplexType.FullName);
                Assert.Equal("Product", edmComplexType.Name);
                Assert.Equal(9, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Category", edmComplexType.Properties[0].Name);
                Assert.Same(EdmType.GetEdmType(typeof(Category)), edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("Colour", edmComplexType.Properties[1].Name);
                Assert.Same(EdmType.GetEdmType(typeof(Colour)), edmComplexType.Properties[1].PropertyType);

                var edmEnumType = (EdmEnumType)EdmType.GetEdmType(typeof(Colour));
                Assert.Equal(typeof(Colour), edmEnumType.ClrType);
                Assert.Equal("NorthwindModel.Colour", edmEnumType.FullName);
                Assert.Equal("Colour", edmEnumType.Name);
                Assert.Equal(3, edmEnumType.Members.Count);
                Assert.Equal("Green", edmEnumType.Members[0].Name);
                Assert.Equal(1, edmEnumType.Members[0].Value);
                Assert.Equal("Blue", edmEnumType.Members[1].Name);
                Assert.Equal(2, edmEnumType.Members[1].Value);
                Assert.Equal("Red", edmEnumType.Members[2].Name);
                Assert.Equal(3, edmEnumType.Members[2].Value);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("Deleted", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.Boolean, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("Description", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[3].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[4].DeclaringType);
                Assert.Equal("Name", edmComplexType.Properties[4].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[4].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[5].DeclaringType);
                Assert.Equal("Price", edmComplexType.Properties[5].Name);
                Assert.Same(EdmPrimitiveType.Decimal, edmComplexType.Properties[5].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[6].DeclaringType);
                Assert.Equal("ProductId", edmComplexType.Properties[6].Name);
                Assert.Same(EdmPrimitiveType.Int32, edmComplexType.Properties[6].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[7].DeclaringType);
                Assert.Equal("Rating", edmComplexType.Properties[7].Name);
                Assert.Same(EdmPrimitiveType.Int32, edmComplexType.Properties[7].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[8].DeclaringType);
                Assert.Equal("ReleaseDate", edmComplexType.Properties[8].Name);
                Assert.Same(EdmPrimitiveType.Date, edmComplexType.Properties[8].PropertyType);

                Assert.Same(edmComplexType.Properties[6], entitySet.EntityKey);
            }

            [Fact]
            public void ThereAre_6_RegisteredCollections()
            {
                Assert.Equal(6, this.entityDataModel.EntitySets.Count);
            }
        }

        public class WhenCalling_BuildModelWith_Models_AndTypeNameForEntitySetName
        {
            private readonly EntityDataModel entityDataModel;

            public WhenCalling_BuildModelWith_Models_AndTypeNameForEntitySetName()
            {
                var entityDataModelBuilder = new EntityDataModelBuilder(StringComparer.OrdinalIgnoreCase);
                entityDataModelBuilder.RegisterEntitySet<Category>(x => x.Name);
                entityDataModelBuilder.RegisterEntitySet<Customer>(x => x.CompanyName);
                entityDataModelBuilder.RegisterEntitySet<Employee>(x => x.Id);
                entityDataModelBuilder.RegisterEntitySet<Manager>(x => x.Id);
                entityDataModelBuilder.RegisterEntitySet<Order>(x => x.OrderId);
                entityDataModelBuilder.RegisterEntitySet<Product>(x => x.ProductId);

                this.entityDataModel = entityDataModelBuilder.BuildModel();
            }

            [Fact]
            public void The_Categories_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Category"));

                var entitySet = this.entityDataModel.EntitySets["Category"];

                Assert.Equal("Category", entitySet.Name);

                var edmComplexType = entitySet.EdmType;

                Assert.Equal(typeof(Category), edmComplexType.ClrType);
            }

            [Fact]
            public void The_Customers_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Customer"));

                var entitySet = this.entityDataModel.EntitySets["Customer"];

                Assert.Equal("Customer", entitySet.Name);

                var edmComplexType = entitySet.EdmType;

                Assert.Equal(typeof(Customer), edmComplexType.ClrType);
            }

            [Fact]
            public void The_Employees_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Employee"));

                var entitySet = this.entityDataModel.EntitySets["Employee"];

                Assert.Equal("Employee", entitySet.Name);

                var edmComplexType = entitySet.EdmType;

                Assert.Equal(typeof(Employee), edmComplexType.ClrType);
            }

            [Fact]
            public void The_Managers_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Manager"));

                var entitySet = this.entityDataModel.EntitySets["Manager"];

                Assert.Equal("Manager", entitySet.Name);

                var edmComplexType = entitySet.EdmType;

                Assert.Equal(typeof(Manager), edmComplexType.ClrType);
            }

            [Fact]
            public void The_Orders_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Order"));

                var entitySet = this.entityDataModel.EntitySets["Order"];

                Assert.Equal("Order", entitySet.Name);

                var edmComplexType = entitySet.EdmType;

                Assert.Equal(typeof(Order), edmComplexType.ClrType);
            }

            [Fact]
            public void The_Products_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Product"));

                var entitySet = this.entityDataModel.EntitySets["Product"];

                Assert.Equal("Product", entitySet.Name);

                var edmComplexType = entitySet.EdmType;

                Assert.Equal(typeof(Product), edmComplexType.ClrType);
            }

            [Fact]
            public void ThereAre_6_RegisteredCollections()
            {
                Assert.Equal(6, this.entityDataModel.EntitySets.Count);
            }
        }
    }
}