using System.Collections;

namespace FluentMatrix;

public readonly struct MatrixRowCollection : IReadOnlyCollection<MatrixRow>, IMatrixElements<MatrixRow> {
    private readonly Matrix _matrix;
    public int Count => _matrix.Rows;
    internal MatrixRowCollection(Matrix matrix) { _matrix = matrix; }
    public MatrixRow this[int index] => new(_matrix, index);
    
    public Enumerator GetEnumerator() => new(this);
    IEnumerator<MatrixRow> IEnumerable<MatrixRow>.GetEnumerator() => GetEnumerator(); // new MatrixRowsEnumerator(this);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<MatrixRow> {
        private readonly MatrixRowCollection _collection;
        private int _index = -1;

        public Enumerator(MatrixRowCollection collection) { _collection = collection; }


        public bool MoveNext() {
            _index++;
            return _index < _collection.Count;
        }

        public void Reset() { _index = -1; }

        public MatrixRow Current => _collection[_index];

        object IEnumerator.Current => Current;
        public void Dispose() { }
    }
}

// internal struct MatrixRowsEnumerator : IEnumerator<RowIndexer> {
//     private readonly MatrixRows _rows;
//     private int _index = -1;
//
//     internal MatrixRowsEnumerator(MatrixRows rows) {
//         _rows = rows;
//     }
//
//     public bool MoveNext() {
//         _index++;
//         return _index < _rows.Count;
//     }
//
//     public void Reset() {
//         _index = -1;
//     }
//
//     public RowIndexer Current => _rows[_index];
//
//     object IEnumerator.Current => Current;
//
//     public void Dispose() { throw new NotImplementedException(); }
// }
