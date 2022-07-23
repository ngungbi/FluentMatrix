using System.Collections;

namespace FluentMatrix;

public class MatrixBuilder : Matrix {
    private int _rowNum;
    public MatrixBuilder(int rows, int columns) : base(rows, columns) { }

    public MatrixBuilder Add(IEnumerable<double> values) {
        var count = 0;
        foreach (double item in values) {
            var col = count % Columns;
            var row = count / Columns;
            this[row, col] = item;
            count++;
        }

        return this;
    }

    public void Add(params double[] values) {
        if (values.Length != Columns) throw new InvalidOperationException("Number of elements must be equal to number of columns");
        if (_rowNum == Rows) throw new InvalidOperationException("Number of elements must be equal to number of rows");
        for (int i = 0; i < values.Length; i++) {
            this[_rowNum, i] = values[i];
        }

        _rowNum++;
    }

    public Matrix AsMatrix() => this;

    // public IEnumerator GetEnumerator() => 
}
