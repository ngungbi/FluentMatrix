# Fluent Matrix

Simplify matrix calculation for .NET application.

## Dependencies

- .NET 6

## Key Features

- Simple enumeration of elements
- Enumeration of elements as reference
- Minimal heap memory allocation

## Example Usage

Calculate determinant of a matrix.

```c#
using FluentMatrix;

Matrix m1 = new MatrixBuilder(2,2) {
    {3, 7},
    {1, -4}
};

double determinant = m1.Determinant();
Console.WriteLine(determinant); // -19
```

Enumeration of elements in matrix.

```c#
foreach(var item in m1) {
    item.Value = item.Value * 2;
}

m1.Print();
// 6 14
// 2 -8
```
Transpose matrix.
```c#
m1.Transpose();
m1.Print();
// 6 2
// 14 -8
```
Element get and set by index.
```c#
m1[0,0] = 10;
m1[0,1] = 20;
m1[1,0] = 30;
m1[1,1] = 40;
m1.Print();
// 10 20
// 30 40
```
