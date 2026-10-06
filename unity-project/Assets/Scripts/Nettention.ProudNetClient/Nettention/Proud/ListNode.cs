using System;

namespace Nettention.Proud
{
	internal class ListNode<T> where T : ListNode<T>
	{
		public class ListOwner
		{
			private static readonly string ConsistencyProblemText = "ListNode reports a consistency problem!";

			private T first = null;

			private T last = null;

			private int count;

			public bool IsEmpty
			{
				get
				{
					return first == null;
				}
			}

			public T First
			{
				get
				{
					return first;
				}
			}

			public T Last
			{
				get
				{
					return last;
				}
			}

			public int Count
			{
				get
				{
					return count;
				}
			}

			private void UnlinkAll()
			{
				while (!IsEmpty)
				{
					Erase(first);
				}
			}

			public void Erase(T node)
			{
				if (node.listOwner != this)
				{
					throw new Exception(ConsistencyProblemText);
				}
				T next = node.next;
				T prev = node.prev;
				if (next != null)
				{
					next.prev = prev;
				}
				if (prev != null)
				{
					prev.next = next;
				}
				if (next == null)
				{
					if (last != node)
					{
						throw new Exception(ConsistencyProblemText);
					}
					last = prev;
				}
				if (prev == null)
				{
					if (first != node)
					{
						throw new Exception(ConsistencyProblemText);
					}
					first = next;
				}
				node.prev = null;
				node.next = null;
				node.listOwner = null;
				count--;
			}

			public void PushBack(T node)
			{
				if (node.listOwner != null)
				{
					Erase(node);
				}
				if (last == null)
				{
					last = node;
					first = node;
				}
				else
				{
					last.next = node;
					node.prev = last;
					last = node;
				}
				node.listOwner = this;
				count++;
			}
		}

		private T prev = null;

		private T next = null;

		private ListOwner listOwner;

		public ListOwner Owner
		{
			get
			{
				return listOwner;
			}
		}

		public T Next
		{
			get
			{
				return next;
			}
		}

		public T Prev
		{
			get
			{
				return prev;
			}
		}

		public void UnlinkSelf()
		{
			if (listOwner != null)
			{
				listOwner.Erase((T)this);
			}
		}
	}
}
