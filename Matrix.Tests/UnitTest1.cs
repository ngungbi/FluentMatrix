using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using FluentMatrix;
using NUnit.Framework;

namespace Tests;

public class Tests {
    [SetUp]
    public void Setup() {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
    }

    [Test]
    public void Demo() {
        Matrix m1 = new MatrixBuilder(2, 2) {
            {3, 7},
            {1, -4}
        };

        double determinant = m1.Determinant();
        Console.WriteLine(determinant); // -19

        foreach (var item in m1) {
            item.Value = item.Value * 2;
        }
        Console.WriteLine();
        m1.Print();
        // 6 14
        // 2 -8

        m1.Transpose();
        Console.WriteLine();
        m1.Print();
        // 6 2
        // 14 -8
        
        m1[0,0] = 10;
        m1[0,1] = 20;
        m1[1,0] = 30;
        m1[1,1] = 40;
        Console.WriteLine();
        m1.Print();
    }

    [Test]
    public void MultiplicationTest() {
        Matrix m1 = new MatrixBuilder(2, 3) {
            {2, 3, 4},
            {1, 0, 0}
        };
        Matrix m2 = new MatrixBuilder(3, 2) {
            {0, 1000},
            {1, 100},
            {0, 10}
        };

        // var a = m1[10, 0];

        // m1.ScalarMultiply(2.0);
        //
        // var m3 = m1 * m2;
        // m3.Print();
    }


    [Test]
    public void CloneTest() {
        var matrix = MatrixCollection.MatrixA;
        var matrix2 = matrix.CloneExclude(2, 2);
        matrix.Print();
        matrix2.Print();
    }

    [Test]
    public void Test1() {
        Matrix matrix = new MatrixBuilder(3, 4) {
            {1.2, 2, 3, 4},
            {4, 5, 6, 5},
            {7, 8, 9, 6}
        };
        // var elm = matrix[1, 2];
        foreach (var item in matrix) {
            Console.WriteLine(item);
        }

        Console.WriteLine(string.Join(',', matrix.Elements.Select(x => x.Value)));
        Console.WriteLine(string.Join(',', matrix.RowAt(1)));
        Console.WriteLine(string.Join(',', matrix.ColumnAt(2)));
        Console.WriteLine();
        foreach (var item in matrix.RowCollection) {
            Console.WriteLine(string.Join(' ', item.Values));
        }

        Console.WriteLine();
        foreach (var item in matrix.ColumnCollection) {
            Console.WriteLine(string.Join(' ', item.Values));
        }

        Console.WriteLine();
        matrix.Print();
        Console.WriteLine();
        matrix.Transpose().Print();
        Console.WriteLine();

        var newMatrix = 2 * matrix; //.Clone(); // Matrix.Create(3, 4).Add(matrix.Elements);
        // matrix[2, 2] = 10;
        newMatrix.Print();
        matrix.Print();

        // x.AsMatrix();
        Assert.Pass();
    }
}
