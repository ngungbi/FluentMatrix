using NUnit.Framework;

namespace Tests;

public class DeterminantTest {
    [SetUp]
    public void Setup() { }

    [Test]
    public void Matrix2X2Determinant() {
        var mat2 = MatrixCollection.Matrix2X2;
        var det = mat2.Determinant();
        Assert.AreEqual(-19, det, 0.00001);
    }

    [Test]
    public void Matrix3X3Determinant() {
        var mat3 = MatrixCollection.Matrix3X3;
        var det = mat3.Determinant();
        Assert.AreEqual(74, det, 0.00001);
    }

    [Test]
    public void Matrix4X4Determinant() {
        var mat4 = MatrixCollection.Matrix4X4;
        var det = mat4.Determinant();
        Assert.AreEqual(318, det, 0.00001);
    }
}
