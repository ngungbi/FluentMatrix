using System.Collections;

namespace FluentMatrix;

public interface IMatrixElements<out T> where T : IReadOnlyCollection<MatrixElement> {
    int Count { get; }
    T this[int index] { get; }
}

internal struct CollectionEnumerator<T> : IEnumerator<T> where T : struct, IReadOnlyCollection<MatrixElement> {
    private readonly IMatrixElements<T> _collection;
    private int _index = -1;

    internal CollectionEnumerator(IMatrixElements<T> collection) { _collection = collection; }


    public bool MoveNext() {
        _index++;
        return _index < _collection.Count;
    }

    public void Reset() { _index = -1; }

    public T Current => _collection[_index];

    object IEnumerator.Current => Current;
    public void Dispose() { }
}
