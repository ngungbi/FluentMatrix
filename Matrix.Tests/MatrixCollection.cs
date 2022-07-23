using FluentMatrix;

namespace Tests;

public static class MatrixCollection {
    public static readonly Matrix MatrixA = new MatrixBuilder(8, 8) {
        // create example matrix 8x8
        {1, 2, 3, 5, 6, 8, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1},
        {7, 8, 9, 1, 2, 3, 0, 1},
        {1, 2, 3, 4, 5, 6, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1},
        {7, 8, 9, 1, 2, 3, 0, 1},
        {1, 2, 3, 4, 5, 6, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1}
    };

    public static readonly Matrix Matrix2X2 = new MatrixBuilder(2, 2) {
        // create example matrix 2x2
        {3, 7},
        {1, -4}
    };

    public static readonly Matrix Matrix3X3 = new MatrixBuilder(3, 3) {
        // create example matrix 3x3
        // determinant = 74
        {5, 0, 3},
        {2, 3, 5},
        {1, -2, 3}
    };

    public static readonly Matrix Matrix4X4 = new MatrixBuilder(4, 4) {
        // create example matrix 4x4
        // determinant = 318
        {1, 0, 4, -6},
        {2, 5, 0, 3},
        {-1, 2, 3, 5},
        {2, 1, -2, 3}
    };
}
