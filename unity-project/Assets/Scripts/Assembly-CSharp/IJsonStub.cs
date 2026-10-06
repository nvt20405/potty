public interface IJsonStub
{
	bool Dispatch(string rmiName, string data);

	bool HasHandler(string rmiName);
}
