using System.Collections;

namespace FluentMatrix;

public interface IMatrix {
    int Rows { get; }
    int Columns { get; }
    double this[int row, int col] { get; set; }
}

public class Matrix : IMatrix, ICloneable, IReadOnlyCollection<MatrixElement> {
    public static MatrixBuilder Create(int rows, int columns) => new(rows, columns);
    private readonly double[,] _matrix;

    public Matrix(int rows, int columns) {
        // validate rows and columns
        if (rows < 1) throw new ArgumentOutOfRangeException(nameof(rows), "Matrix rows must be greater than 0");
        if (columns < 1) throw new ArgumentOutOfRangeException(nameof(columns), "Matrix columns must be greater than 0");

        Rows = rows;
        Columns = columns;
        _matrix = new double[rows, columns];
    }

    /// <summary>
    /// Gets the number of rows in the matrix.
    /// </summary>
    public int Rows { get; }

    /// <summary>
    /// Gets the number of columns in the matrix.
    /// </summary>
    public int Columns { get; }

    /// <summary>
    /// Enumerates the matrix as a collection of rows.
    /// </summary>
    public MatrixRowCollection RowCollection => new(this);

    /// <summary>
    /// Enumerates the matrix as a collection of columns.
    /// </summary>
    public MatrixColumnCollection ColumnCollection => new(this);

    /// <summary>
    /// Enumerates the matrix as a collection of cell references.
    /// </summary>
    public Elements Elements => new(this);

    /// <summary>
    /// Enumerates the matrix as a collection of cell values.
    /// </summary>
    public ValueCollection Values => new(this);

    /// <summary>
    /// Calculates a scalar multiplication and returns a new matrix.
    /// </summary>
    /// <param name="constant"></param>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static Matrix operator *(double constant, Matrix matrix) {
        var result = new MatrixBuilder(matrix.Rows, matrix.Columns);
        for (int i = 0; i < matrix.Rows; i++) {
            for (int j = 0; j < matrix.Columns; j++) {
                result[i, j] = matrix[i, j] * constant;
            }
        }

        return result;
    }

    /// <summary>
    /// Calculates an addition of two matrix and returns a new matrix. Matrix dimensions must match.
    /// </summary>
    /// <param name="left">Matrix</param>
    /// <param name="right">Matrix</param>
    /// <returns>A new matrix which is sum of both matrix.</returns>
    /// <exception cref="InvalidOperationException">When the matrix dimensions do not match.</exception>
    public static Matrix operator +(Matrix left, Matrix right) {
        if (left.Columns != right.Columns || left.Rows != right.Rows) {
            throw new InvalidOperationException("Matrix dimensions must match");
        }

        var result = new MatrixBuilder(left.Rows, left.Columns);
        for (int i = 0; i < left.Rows; i++) {
            for (int j = 0; j < left.Columns; j++) {
                result[i, j] = left[i, j] + right[i, j];
            }
        }

        return result;
    }

    /// <summary>
    /// Calculates an substraction of two matrix and returns a new matrix. Matrix dimensions must match.
    /// </summary>
    /// <param name="left">Matrix</param>
    /// <param name="right">Matrix</param>
    /// <returns>A new matrix which is the result of operation.</returns>
    /// <exception cref="InvalidOperationException">When the matrix dimensions do not match.</exception>
    public static Matrix operator -(Matrix left, Matrix right) {
        if (left.Columns != right.Columns || left.Rows != right.Rows) {
            throw new InvalidOperationException("Matrix dimensions must match");
        }

        var result = new MatrixBuilder(left.Rows, left.Columns);
        for (int i = 0; i < left.Rows; i++) {
            for (int j = 0; j < left.Columns; j++) {
                result[i, j] = left[i, j] - right[i, j];
            }
        }

        return result;
    }

    /// <summary>
    /// Calculates a scalar multiplication and returns a new matrix.
    /// </summary>
    /// <param name="constant"></param>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static Matrix operator *(Matrix matrix, double constant) => constant * matrix;

    /// <summary>
    /// Calculates a scalar division and returns a new matrix.
    /// </summary>
    /// <param name="constant"></param>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static Matrix operator /(Matrix matrix, double constant) => (1 / constant) * matrix;

    /// <summary>
    /// Calculates a cross product of two matrix and returns a new matrix.
    /// </summary>
    /// <param name="matrix1"></param>
    /// <param name="matrix2"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Matrix operator *(Matrix matrix1, Matrix matrix2) {
        if (matrix1.Columns != matrix2.Rows) {
            throw new InvalidOperationException("Matrix dimensions do not match");
        }

        var result = new Matrix(matrix1.Rows, matrix2.Columns);
        for (int i = 0; i < matrix1.Rows; i++) {
            for (int j = 0; j < matrix2.Columns; j++) {
                double sum = 0;
                for (int k = 0; k < matrix1.Columns; k++) {
                    sum += matrix1[i, k] * matrix2[k, j];
                }

                result[i, j] = sum;
            }
        }

        return result;
    }

    /// <summary>
    /// Calculates a determinant of a matrix. Matrix must be a square matrix.
    /// </summary>
    /// <remarks>
    /// For matrix 3x3 or less, the computation does not allocate any heap memory.
    /// For matrix 4x4 or more, the computation uses recursive method and
    /// create a temporary submatrix to calculate the determinant.
    /// </remarks>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">When matrix is not square.</exception>
    public double Determinant() {
        // calculate determinant of a matrix
        // if matrix is not square, throw exception
        if (Rows != Columns) {
            throw new InvalidOperationException("Matrix must be square");
        }

        return Rows switch {
            1 => _matrix[0, 0],
            2 => _matrix[0, 0] * _matrix[1, 1] - _matrix[0, 1] * _matrix[1, 0],
            3 => MatrixHelper.Determinant3X3(this),
            _ => MatrixHelper.DeterminantNxN(this)
        };
    }


    public static Matrix operator ~(Matrix matrix) {
        // var result = new MatrixBuilder(matrix.Columns, matrix.Rows);
        // inverse operation of matrix
        var multiplier = 1 / matrix.Determinant();
        return matrix.Rows switch {
            1 => multiplier * matrix,
            2 => MatrixHelper.Flip2X2(matrix.Clone()).ScalarMultiply(multiplier),
            _ => throw new NotImplementedException()
        };
    }

    public double this[int row, int col] {
        get => _matrix[row, col];
        set => _matrix[row, col] = value;
    }

    /// <summary>
    /// Creates a reference matrix (subset of a matrix) without creating a new matrix.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public Matrix this[Range row, Range col] {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public void Print() {
        foreach (var row in RowCollection) {
            Console.WriteLine(string.Join(' ', row));
        }
    }
    
    object ICloneable.Clone() => Clone();

    public Matrix Clone() {
        var newMatrix = new Matrix(Rows, Columns);
        foreach (var element in this) {
            element.CopyTo(newMatrix);
        }

        return newMatrix;
    }

    public Matrix CloneExclude(int row, int column) {
        var newMatrix = new Matrix(Rows - 1, Columns - 1);
        for (int i = 0; i < Rows; i++) {
            if (i == row) continue;
            for (int j = 0; j < Columns; j++) {
                if (j == column) continue;
                var rowIndex = i < row ? i : i - 1;
                var colIndex = j < column ? j : j - 1;
                newMatrix[rowIndex, colIndex] = _matrix[i, j];
            }
        }

        return newMatrix;
    }

    public Enumerator GetEnumerator() => new(this);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    IEnumerator<MatrixElement> IEnumerable<MatrixElement>.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<MatrixElement> {
        private readonly Matrix _matrix;
        private int _col = -1;
        private int _row = 0;
        
        public Enumerator(Matrix matrix) { _matrix = matrix; }

        public bool MoveNext() {
            _col++;
            if (_col < _matrix.Columns) return true;
            _col = 0;
            _row++;
            return _row < _matrix.Rows ? true : false;
        }

        public void Reset() {
            _col = -1;
            _row = 0;
        }

        public MatrixElement Current => new(_matrix, _row, _col);
        object IEnumerator.Current => Current;
        public void Dispose() { }
    }

    public int Count => Columns * Rows;
}
