# StronglyTypedDictionary - Strongly Typed Dictionary Generator for .NET

## Overview

In modern software development, strongly typed identifiers (IDs) have become increasingly popular, offering an enhanced level of type safety and reducing the risk of misusing IDs of different entities. However, when working with dictionaries that map these IDs to corresponding entities or attributes, developers often face limitations due to the generic "key-value" terminology used by traditional dictionaries. This can lead to confusion, especially when handling multiple dictionaries within the same context, as each dictionary's purpose and the nature of its keys and values become obscured.

StronglyTypedDictionary aims to bridge this gap by providing a powerful code generation tool that allows developers to define dictionaries with domain-specific naming conventions for both keys and values. This approach not only improves code readability and maintainability but also aligns dictionary usage more closely with the domain model, ensuring a more intuitive development experience.

## Features

- **Custom Key and Value Naming**: Define dictionaries with meaningful names for keys and values, tailored to your domain model.
- **Seamless Integration**: Easily integrate with your .NET projects through NuGet.
- **Pluralization Support**: Automatic handling of singular and plural forms for keys and values, enhancing the natural feel of the API.
- **Strong Typing**: Enforces type safety, ensuring that keys and values are used correctly throughout your application.

## Getting Started

### Installation

To get started with StronglyTypedDictionary, add it to your project via NuGet:

```powershell
Install-Package StronglyTypedDictionary
```

### Defining a Custom Dictionary

Create a new file in your project with the extension `.dictionary`, following the naming convention `<Namespace>.<DictionaryName>.dictionary`. For example, `MyProject.ErrorNumberToMessageMapping.dictionary`.

In this file, define your custom dictionary using the format:

```
Namespace.DictionaryName<KeyType KeyName(KeyNamePlural), ValueType ValueName(ValueNamePlural)>
```

For example:

```
MyProject.StronglyTypedDictionary.ErrorNumberToMessageMapping<int errorCode(ErrorCodes), string message(Messages)>
```

This definition will be parsed by the StronglyTypedDictionary code generator to create a strongly typed dictionary class tailored to your specifications.

### Usage

Once your custom dictionary is defined, you can use it within your project as follows:

```csharp
public static void Main()
{
    var errors = new ErrorNumberToMessageMapping();
    errors.Add(404, "Not Found");
    
    if (!errors.ContainsErrorCode(404))
        Console.WriteLine("May be this is not an error at all");

    if (errors.TryGetMessage(404, out var errorMessage))
        Console.WriteLine($"404: {errorMessage}");

    var messageForUndefinedError = errors.GetMessageOrDefault(43, "Unknown Error");
}
```

## Why StronglyTypedDictionary?

- **Domain-Specific Semantics**: Leverage domain-specific terminology for greater clarity and better alignment with business logic.
- **Improved Readability**: Enhance code readability with intuitive naming, making it easier to understand and maintain.
- **Type Safety**: Benefit from the strong typing system of C#, reducing the risk of errors related to key and value misuse.

## Extended Use Cases Beyond Simple Mapping

The StronglyTypedDictionary extends the functionality of traditional dictionaries by introducing specific suffixes and associated methods that enhance code readability and domain-specific utility. Below is a table illustrating possible suffixes and the custom methods that differentiate these from a standard dictionary:

| Suffix        | Description                          | Custom Methods                     | Example Class                       | Key Method Example                    |
|---------------|--------------------------------------|------------------------------------|-------------------------------------|---------------------------------------|
| `Cache`       | Storing results for quick retrieval. | `ContainsKey`, `GetResult`         | `ResultsCache<int, Result>`         | `if (resultsCache.ContainsQuery(id))` |
| `Index`       | Accelerating access to attributes.   | `ContainsAttribute`, `GetObjectsForAttribute` | `AttributeIndex<string, List<object>>` | `if (attributeIndex.ContainsAttribute(attrName))` |
| `State`       | Storing session or state data.       | `ContainsSession`, `GetSessionData`| `SessionState<string, SessionData>` | `if (sessionState.ContainsSession(sessionId))` |
| `Pool`        | Managing a pool of resources.        | `IsResourceAvailable`, `GetResource` | `ResourcePool<int, Resource>`       | `if (resourcePool.IsResourceAvailable(resourceId))` |
| `Log`         | Tracking changes or activity.        | `GetChangesForObject`              | `ChangeLog<int, List<Change>>`      | `changeLog.GetChangesForObject(objectId)` |
| `Registry`    | Handling event registrations.        | `Register`, `GetHandlerForEvent`   | `EventHandlers<string, Action<Event>>` | `eventHandlers.GetHandlerForEvent(eventType)` |

### Example Usage: `Cache`

The `ResultsCache` class is designed to store the results of expensive operations:

```csharp
var resultsCache = new ResultsCache<int, Result>();
if (!resultsCache.ContainsQuery(queryId)) {
    var result = PerformExpensiveOperation(queryId);
    resultsCache.Add(queryId, result);
}
var cachedResult = resultsCache.GetResult(queryId);
```



## Additional Information

While StronglyTypedDictionary draws inspiration from the concept of strongly typed IDs, it focuses on improving dictionary usage by allowing custom naming for keys and values. This approach shares the ideology of enhancing code clarity and type safety, demonstrating its utility across various applications.

For more detailed documentation, usage examples, and advanced features, please refer to the [StronglyTypedDictionary Documentation](#).

Embark on a journey towards more expressive, maintainable, and intuitive code with StronglyTypedDictionary.
