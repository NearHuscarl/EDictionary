using System;

namespace EDictionary.Core.Utilities
{
	/// <summary>
	/// Enumeration for a Status, to avoid using a boolean
	/// </summary>
	public enum Status
	{
		Success,
		Warning,
		Error,
		Information,
		NULL
	}

	/// <summary>
	/// Result class without return value. To be used instead of "void"
	/// </summary>
	public class Result
	{
		public Status Status { get; set; }
		public string Message { get; set; }
		public string InnerMessage { get; set; }
		public Exception Exception { get; set; }

		public Result()
		{

		}
		public Result(Status status)
		{
			this.Status = status;
		}

		public Result(Result result)
		{
			this.Status = result.Status;
			this.Message = result.Message;
			this.InnerMessage = result.InnerMessage;
			this.Exception = result.Exception;
		}

		public Result(string message, Status status = Status.NULL, Exception exception = null)
		{
			this.Status = status;
			this.Message = message;
			this.InnerMessage = "";
			this.Exception = exception;
		}

		public Result(string message, string innerMessage = "", Status status = Status.NULL, Exception exception = null)
		{
			this.Status = status;
			this.Message = message;
			this.InnerMessage = innerMessage;
			this.Exception = exception;
		}
	}

	/// <summary>
	/// Result class which can be used to return a value
	/// </summary>
	/// <typeparam name="T">The type of the value which is returned</typeparam>
	public class Result<T> : Result
	{
		//private int result;
		//private string p;

		public T Data { get; set; }

		public Result()
		{

		}

		public Result(T data)
		{
			this.Data = data;
		}

		public Result(Result result)
		{
			this.Status = result.Status;
			this.Message = result.Message;
			this.InnerMessage = result.InnerMessage;
			this.Exception = result.Exception;

		}

		public Result(string message, string innerMessage = "", Status status = Status.NULL, Exception exception = null)
		{
			this.Status = status;
			this.Message = message;
			this.InnerMessage = innerMessage;
			this.Exception = exception;
		}

		public Result(T data, Result result)
		{
			this.Status = result.Status;
			this.Message = result.Message;
			this.InnerMessage = result.InnerMessage;
			this.Exception = result.Exception;
			this.Data = data;

		}

		public Result(T data, string message, string innerMessage = "", Status status = Status.NULL, Exception exception = null)
		{
			this.Status = status;
			this.Message = message;
			this.InnerMessage = innerMessage;
			this.Exception = exception;
			this.Data = data;

		}

		public Result(T data, string message, Status status)
		{
			this.Data = data;
			this.Message = message;
			this.Status = status;
		}
	}
}
