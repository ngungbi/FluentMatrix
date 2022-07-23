using System;
using FluentMatrix;
using NUnit.Framework;

namespace Tests;

public class InversionTest {
    [Test]
    public void Inversion2X2Test() {
        var mat = new MatrixBuilder(2, 2) {
            {4, 7},
            {2, 6}
        };
        var det = mat.Determinant();
        Console.WriteLine(det);
        var inv = ~mat;
        inv.Print();
        var identity = inv * mat;
        identity.Round().Print();
        Assert.IsTrue(identity.IsIdentity());
    }
}
