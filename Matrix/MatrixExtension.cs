namespace FluentMatrix;

public static class MatrixExtension {
    public static MatrixElement ElementAt(this Matrix matrix, int row, int column) => new(matrix, row, column);

    public static double ValueAt(this Matrix matrix, int row, int column) => matrix[row, column];
    public static MatrixRow RowAt(this Matrix matrix, int rowNum) => new(matrix, rowNum);
    public static MatrixColumn ColumnAt(this Matrix matrix, int colNum) => new(matrix, colNum);

    public static MatrixRowCollection AsRowCollection(this Matrix matrix) => new(matrix);
    public static MatrixColumnCollection AsColumnCollection(this Matrix matrix) => new(matrix);

    /// <summary>
    /// Do scalar multiplication on the matrix. This operation does not create a new matrix.
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="scalar"></param>
    /// <returns></returns>
    public static Matrix ScalarMultiply(this Matrix matrix, double scalar) {
        for (int i = 0; i < matrix.Rows; i++) {
            for (int j = 0; j < matrix.Columns; j++) {
                matrix[i, j] *= scalar;
            }
        }

        return matrix;
    }

    public static Matrix ScalarMultiplyDirect(this Matrix matrix, double scalar) {
        for (int i = 0; i < matrix.Rows; i++) {
            for (int j = 0; j < matrix.Columns; j++) {
                matrix[i, j] *= scalar;
            }
        }

        return matrix;
    }

    public static Matrix ScalarMultiplyParallel(this Matrix matrix, double scalar) {
        Parallel.ForEach(
            matrix, elm => {
                elm.Value *= scalar;
            }
        );
        return matrix;
    }

    /// <summary>
    /// Transpose the matrix. This operation does not create a new matrix.
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static Matrix Transpose(this Matrix matrix) {
        // var result = new Matrix(matrix.Columns, matrix.Rows);
        for (int i = 0; i < matrix.Rows; i++) {
            for (int j = i; j < matrix.Columns; j++) {
                // swap elements
                (matrix[i, j], matrix[j, i]) = (matrix[j, i], matrix[i, j]);

                // matrix[j, i] = matrix[i, j];
            }
        }

        return matrix;
    }

    /// <summary>
    /// Round every element of the matrix to the nearest integer.
    /// This operation does not create a new matrix.
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static Matrix Round(this Matrix matrix, int digits = 0) {
        foreach (var elm in matrix) {
            elm.Value = Math.Round(elm, digits);
        }

        return matrix;
    }

    /// <summary>
    /// Multiply each element at the same row and colum of two matrices together. This operation does not create a new matrix.
    /// <remarks>
    /// This is not a standard matrix multiplication.
    /// </remarks>
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Matrix MultiplyEach(this Matrix matrix, Matrix other) {
        // matrix must be the same size as other
        if (matrix.Rows != other.Rows || matrix.Columns != other.Columns) {
            throw new ArgumentException("Matrix must be the same size as other");
        }

        foreach (var elm in matrix) {
            elm.Value *= other[elm.Row, elm.Col];
        }

        return matrix;
    }

    public static bool IsIdentity(this Matrix matrix) {
        // foreach (var item in matrix) {
        //     if (item.Col == item.Row) {
        //         if (!DoubleEquals(item, 1)) return false;
        //     } else {
        //         if (!DoubleEquals(item, 0)) return false;
        //     }
        // }

        for (int i = 0; i < matrix.Rows; i++) {
            for (int j = 0; j < matrix.Columns; j++) {
                if (!DoubleEquals(matrix[i, j], i == j ? 1 : 0)) {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool DoubleEquals(double a, double b) => Math.Abs(a - b) < 0.00001;
}
