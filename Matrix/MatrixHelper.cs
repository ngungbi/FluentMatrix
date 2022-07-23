namespace FluentMatrix;

internal static class MatrixHelper {
    internal static double DeterminantNxN(Matrix matrix) {
        var sum = 0.0;
        var col = 1.0;
        var num = matrix.Columns;
        for (int i = 0; i < num; i++) {
            var sub = matrix.CloneExclude(0, i);
            var det = col * matrix[0, i] * sub.Determinant();
            col *= -1;
            sum += det;
        }

        return sum;
    }

    internal static double Determinant3X3(Matrix matrix) {
        var num = matrix.Columns;
        var psum = 0.0;
        for (int i = 0; i < num; i++) {
            var mul = 1.0;
            for (int j = 0; j < num; j++) {
                var col = (i + j) % num;
                var row = j % num;
                var val = matrix[row, col];
                mul *= val;
            }

            psum += mul;
        }

        var nsum = 0.0;
        for (int i = 0; i < num; i++) {
            var mul = 1.0;
            for (int j = 0; j < num; j++) {
                var col = (i + j) % num;
                var row = (num - 1) - j % num;
                var val = matrix[row, col];
                mul *= val;
            }

            nsum += mul;
        }

        return psum - nsum;
    }

    internal static Matrix InverseHelper(Matrix matrix) {
        return matrix;
    }

    private static void Swap(MatrixElement elm1, MatrixElement elm2) {
        (elm1.Value, elm2.Value) = (elm2.Value, elm1.Value);
    }

    internal static Matrix Flip2X2(Matrix matrix) {
        (matrix[0, 0], matrix[1, 1]) = (matrix[1, 1], matrix[0, 0]);
        matrix[0, 1] *= -1;
        matrix[1, 0] *= -1;

        return matrix;
    }
}
