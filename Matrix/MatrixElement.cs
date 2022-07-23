using System.Collections;
using System.Globalization;

namespace FluentMatrix;

public readonly struct MatrixElement : IEquatable<MatrixElement>, IComparable<double>, IComparable<MatrixElement> {
    private readonly Matrix _matrix;
    public readonly int Row;
    public readonly int Col;

    public MatrixElement(Matrix matrix, int row, int col) {
        _matrix = matrix;
        Row = row;
        Col = col;
    }

    public double Value {
        get => _matrix[Row, Col];
        set => _matrix[Row, Col] = value;
    }

    public static implicit operator double(MatrixElement element) => element.Value;

    public override string ToString() => Value.ToString(CultureInfo.DefaultThreadCurrentCulture);
    public string ToString(IFormatProvider? provider) => Value.ToString(provider);

    public void CopyTo(Matrix target) {
        target[Row, Col] = Value;
    }

    public bool Equals(MatrixElement other) => Value.Equals(other.Value);

    public int CompareTo(double other) => Value.CompareTo(other);

    public override bool Equals(object? obj) {
        return obj is MatrixElement other && Equals(other);
    }

    public override int GetHashCode() => HashCode.Combine(_matrix, Row, Col, Value);

    public int CompareTo(MatrixElement other) => Value.CompareTo(other.Value);

    public static bool operator ==(MatrixElement left, MatrixElement right) => left.Equals(right);

    public static bool operator !=(MatrixElement left, MatrixElement right) => !left.Equals(right);
}
