using System.Collections;

namespace FluentMatrix;

/// <summary>
/// Enumerate columns from left to right
/// </summary>
public readonly struct MatrixColumn :
    IReadOnlyCollection<MatrixElement>,
    IEquatable<MatrixColumn> {
    private readonly Matrix _matrix;
    private readonly int _col;

    internal MatrixColumn(Matrix matrix, int col) {
        _matrix = matrix;
        _col = col;
    }

    public int Length => _matrix.Rows;
    public int Count => _matrix.Rows;
    public int Index => _col;


    public IEnumerable<double> Values {
        get {
            for (int i = 0; i < _matrix.Rows; i++)
                yield return _matrix[i, _col];
        }
    }

    public double GetValue(int index) => _matrix[index, _col];
    public void SetValue(int index, double value) => _matrix[index, _col] = value;

    public MatrixElement this[int col] {
        get => new(_matrix, _col, col);
        set => _matrix[_col, col] = value.Value;
    }

    public Enumerator GetEnumerator() => new(this);
    IEnumerator<MatrixElement> IEnumerable<MatrixElement>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool ElementEquals(MatrixColumn other) {
        var count = Count;
        if (count != other.Count) return false;
        for (int i = 0; i < count; i++)
            if (!this[i].Equals(other[i]))
                return false;
        return true;
    }

    public bool Equals(MatrixColumn other) {
        return _matrix.Equals(other._matrix) && _col == other._col;
    }


    public override bool Equals(object? obj) {
        return obj is MatrixColumn other && Equals(other);
    }

    public override int GetHashCode() {
        return HashCode.Combine(_matrix, _col);
    }

    public static bool operator ==(MatrixColumn left, MatrixColumn right) => left.Equals(right); // left._matrix == right._matrix && left._row == right._row;

    public static bool operator !=(MatrixColumn left, MatrixColumn right) => !left.Equals(right);


    public struct Enumerator : IEnumerator<MatrixElement> {
        private readonly MatrixColumn _column;
        private int _position = -1;

        public Enumerator(MatrixColumn column) {
            _column = column;
        }

        public MatrixElement Current => _column[_position];

        public bool MoveNext() {
            _position++;
            return _position < _column.Length;
        }

        public void Reset() => _position = -1;
        object IEnumerator.Current => Current;
        public void Dispose() { }
    }
}
