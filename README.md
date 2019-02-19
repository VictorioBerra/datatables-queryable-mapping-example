# DataTables.Queryable Strongly Typed Column Mapping Layer Example

Thats a heck of a title isnt it?

All the code you need to care above it in HomeController for brevity.

- `dotnet run`
- POST to `http://localhost:5411/api/Home?param1=HelloWorld`

## Payload:

```json
{
  "draw": 1,
  "columns": [
    {
      "data": "Breed.Name",
      "name": "",
      "searchable": false,
      "orderable": true,
      "search": {
        "value": "",
        "regex": false
      }
    }
  ],
  "order": [
    {
      "column": 0,
      "dir": "asc"
    }
  ],
  "start": 0,
  "length": 30,
  "search": {
    "value": "",
    "regex": false
  }
}
```

## Notice that you get back the following:

```json
{
  "draw": 1,
  "columns": [
    {
      "data": "CatBreed.BreedName",
```

Normally you dont send this payload back to the user, this would be sent to [DataTables.Queryable](https://github.com/AlexanderKrutov/DataTables.Queryable) for automatic processing.