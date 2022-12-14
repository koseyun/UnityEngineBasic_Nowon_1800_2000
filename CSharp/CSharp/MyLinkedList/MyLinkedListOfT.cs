using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinkedList
{
    // sealed 제한자
    // 상속 불가능 하도록 만드는 제한자
    public sealed class MyLinkedListNode<T>
    {
        public T Value;
        public MyLinkedListNode<T> Prev;
        public MyLinkedListNode<T> Next;

        public MyLinkedListNode(T value)
        {
            Value = value;
        }
    }

    internal class MyLinkedList<T> : IEnumerable<T>
    {
        private MyLinkedListNode<T> _first, _last, _tmp;

        public MyLinkedListNode<T> First => _first;
        public MyLinkedListNode<T> Last => _last;

        // O(1)
        public void AddFirst(T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            if (_first != null)
            {
                _tmp.Next = _first;
                _first.Prev = _tmp;
            }
            else
            {
                _last = _tmp;
            }

            _first = _tmp;
        }

        // O(1)
        public void AddLast(T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            if (_last != null)
            {
                _tmp.Prev = _last;
                _last.Next = _tmp;
            }
            else
            {
                _first = _tmp;
            }

            _last = _tmp;
        }

        // O(1)
        public MyLinkedListNode<T> AddBefore(MyLinkedListNode<T> node, T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            if (node != _first)
            {
                node.Prev.Next = _tmp;
                _tmp.Prev = node.Prev;
            }
            
            node.Prev = _tmp;            
            _tmp.Next = node;

            return _tmp;
        }

        // O(1)
        public MyLinkedListNode<T> AddAfter(MyLinkedListNode<T> node, T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            if (node != _last)
            {
                node.Next.Prev = _tmp;
                _tmp.Next = node.Next;
                //_tmp.Prev = node;
            }

            node.Next = _tmp;
            _tmp.Prev = node;

            return _tmp;
        }

        // O(N)
        public MyLinkedListNode<T> Find(T value)
        {
            _tmp = _first;

            while (_tmp != null)
            {
                /*if (_tmp.Value == value)
                    return _tmp;*/
                if (Comparer<T>.Default.Compare(_tmp.Value, value) == 0)
                    return _tmp;

                _tmp = _tmp.Next;
            }
            return null;
        }

        //O(N)
        public MyLinkedListNode<T> FindLast(T value)
        {
            _tmp = _last;

            while (_tmp != null)
            {
                /*if (_tmp.Value == value)
                    return _tmp;*/
                if (Comparer<T>.Default.Compare(_tmp.Value, value) == 0)
                    return _tmp;

                _tmp = _tmp.Prev;
            }
            return null;
        }

        // O(N)
        public bool Remove(T value)
        {
            _tmp = Find(value);

            if (_tmp != null)
            {
                if (_tmp.Prev != null)
                    _tmp.Prev.Next = _tmp.Next;
                if (_tmp.Next != null)
                    _tmp.Next.Prev = _tmp.Prev;

                _tmp = _tmp.Next = _tmp.Prev = null;
                return true;
            }
            return false;
        }

        // O(1)
        public bool Remove(MyLinkedListNode<T> node)
        {
            if (node != null)
            {
                if (node.Prev != null)
                    node.Prev.Next = node.Next;
                if (node.Next != null)
                    node.Next.Prev = node.Prev;

                node = node.Next = node.Prev = null;
                return true;
            }
            return false;
        }

        // O(N)
        public bool RemoveLast(T value)
        {
            _tmp = FindLast(value);

            if (_tmp != null)
            {
                if (_tmp.Prev != null)
                    _tmp.Prev.Next = _tmp.Next;
                if (_tmp.Next != null)
                    _tmp.Next.Prev = _tmp.Prev;

                _tmp = _tmp.Next = _tmp.Prev = null;
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T> //열거자
        {
            private readonly MyLinkedList<T> _data; //데이터 필요
            private MyLinkedListNode<T> _currentNode; //현재 가리키는 데이터 참조 또는 값
            public T Current => _currentNode.Value;

            object IEnumerator.Current => throw new NotImplementedException();

            public Enumerator(MyLinkedList<T> data)
            {
                _data = data;
                _currentNode = null;
            }

            public void Dispose()
            {
                //
            }

            public bool MoveNext()
            {
                if (_currentNode == null)
                    _currentNode = _data.First;
                else if (_currentNode == _data.Last)
                    return false;
                else
                    _currentNode = _currentNode.Next;

                return true;
            }

            public void Reset() //순회 끝나고 다시 되돌려야 함
            {
                _currentNode = null;
            }
        }
    }    
}
