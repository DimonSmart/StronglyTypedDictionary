namespace DimonSmart.CustomizedDictionary.Generator.Tests
{
    public class CustomizedDictionarySpecificationTests
    {
        [Theory]
        [InlineData("MyNamespace.MyDictionary<int Key, string Value>", "MyNamespace", "MyDictionary", "int", "Key", "Keys", "string", "Value", "Values")]
        [InlineData("DataStructures.EntityMap<int EntityId(EntityIds), string EntityName>", "DataStructures", "EntityMap", "int", "EntityId", "EntityIds", "string", "EntityName", "EntityNames")]
        [InlineData("Users.Profile<int UserId, string Profile>", "Users", "Profile", "int", "UserId", "UserIds", "string", "Profile", "Profiles")]
        [InlineData("Catalog.ProductMap<string SKU, decimal Price(Prices)>", "Catalog", "ProductMap", "string", "SKU", "SKUs", "decimal", "Price", "Prices")]
        [InlineData("System.Mapping<Guid ID, bool IsActive(IsActiveFlags)>", "System", "Mapping", "Guid", "ID", "IDs", "bool", "IsActive", "IsActiveFlags")]
        [InlineData("Operations.Task<int TaskId, DateTime Date(Deadlines)>", "Operations", "Task", "int", "TaskId", "TaskIds", "DateTime", "Date", "Deadlines")]
        [InlineData("Network.IpMap<string IPAddress, int Port(Ports)>", "Network", "IpMap", "string", "IPAddress", "IPAddresses", "int", "Port", "Ports")]
        [InlineData("HR.EmployeeMap<int EmployeeNumber, string Department(Departments)>", "HR", "EmployeeMap", "int", "EmployeeNumber", "EmployeeNumbers", "string", "Department", "Departments")]
        [InlineData("Finance.Account<int AccountId, double Balance>", "Finance", "Account", "int", "AccountId", "AccountIds", "double", "Balance", "Balances")]
        [InlineData("Communication.MessageMap<Guid MessageId, string Content(Contents)>", "Communication", "MessageMap", "Guid", "MessageId", "MessageIds", "string", "Content", "Contents")]
        public void CustomizedDictionarySpecification_ParseInputString_CorrectlySetsProperties(
            string input,
            string expectedNamespace,
            string expectedDictionaryName,
            string expectedKeyType,
            string expectedKeyName,
            string expectedKeyNamePlural,
            string expectedValueType,
            string expectedValueName,
            string expectedValueNamePlural)
        {
            // Act
            var spec = new CustomizedDictionarySpecification(input);

            // Assert
            Assert.Equal(expectedNamespace, spec.Namespace);
            Assert.Equal(expectedDictionaryName, spec.DictionaryName);
            Assert.Equal(expectedKeyType, spec.KeyType);
            Assert.Equal(expectedKeyName, spec.KeyName);
            Assert.Equal(expectedKeyNamePlural, spec.KeyNamePlural);
            Assert.Equal(expectedValueType, spec.ValueType);
            Assert.Equal(expectedValueName, spec.ValueName);
            Assert.Equal(expectedValueNamePlural, spec.ValueNamePlural);
        }
    }

}