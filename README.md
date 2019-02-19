# DataTables.Queryable Strongly Typed Column Mapping Layer Example

Thats a heck of a title isnt it?

All the code you need to care above it in HomeController for brevity.

- `dotnet run`
- POST to `http://localhost:5411/api/Home?param1=HelloWorld`

## Payload:

```
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