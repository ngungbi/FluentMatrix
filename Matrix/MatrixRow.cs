using System.Collections;

namespace FluentMatrix;

public readonly struct MatrixRow :
    IReadOnlyCollection<MatrixElement>,
    IEquatable<MatrixRow> {
    private readonly Matrix _matrix;
    private readonly int _row;

    public int Count => _matrix.Columns;
    public int Index => _row;

    internal MatrixRow(Matrix matrix, int row) {
        _matrix = matrix;
        _row = row;
    }

    public MatrixElement this[int col] {
        get => new(_matrix, _row, col);
        set => _matrix[_row, col] = value.Value;
    }

    public IEnumerable<double> Values {
        get {
            for (int col = 0; col < _matrix.Columns; col++)
                yield return _matrix[_row, col];
        }
    }

    public double GetValue(int index) => _matrix[_row, index];
    public void SetValue(int index, double value) => _matrix[_row, index] = value;


    public Enumerator GetEnumerator() => new(this);
    IEnumerator<MatrixElement> IEnumerable<MatrixElement>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool ElementEquals(MatrixRow other) {
        var count = Count;
        if (count != other.Count) return false;
        for (int i = 0; i < count; i++)
            if (!this[i].Equals(other[i]))
                return false;
        return true;
    }

    public bool Equals(MatrixRow other) {
        return _matrix.Equals(other._matrix) && _row == other._row;
    }

    public override bool Equals(object? obj) {
        return obj is MatrixRow other && Equals(other);
    }

    public override int GetHashCode() {
        return HashCode.Combine(_matrix, _row);
    }

    public static bool operator ==(MatrixRow left, MatrixRow right) => left.ElementEquals(right); // left._matrix == right._matrix && left._row == right._row;
    public static bool operator !=(MatrixRow left, MatrixRow right) => !left.ElementEquals(right);

    public struct Enumerator : IEnumerator<MatrixElement> {
        private readonly MatrixRow _row;
        private int _position = -1;

        public Enumerator(MatrixRow row) {
            _row = row;
        }

        public MatrixElement Current => _row[_position];

        public bool MoveNext() {
            _position++;
            return _position < _row.Count;
        }

        public void Reset() { _position = -1; }
        object IEnumerator.Current => Current;
        public void Dispose() { }
    }
}
