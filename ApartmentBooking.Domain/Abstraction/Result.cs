using System.Diagnostics.CodeAnalysis;

namespace ApartmentBooking.Domain.Abstraction;

public class Result
{
    // it is protected and internal so it can be 
        //only be accessed from this assembly and inside of this type.
    protected internal Result(bool isSuccess,Error  error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }
        IsSuccess = isSuccess;
        Error=error;
    }

    public bool IsSuccess { get; } 
    public bool IsFailure=>!IsSuccess;
    public Error Error { get;  }

    // static helper methods to create result object
    public static Result Success()=>new (true,Error.None);
    public static Result Failure(Error error) => new (false,error);
    public static Result<TValue> Success<TValue>(TValue value)=>new (value,true,Error.None);
    public static Result<TValue> Failure<TValue>(Error error)=>new (default,false,Error.None);

    // factory method 
    public static Result<TValue> Create<TValue>(TValue ? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
        

}

// generic one
public class Result<Tvalue> : Result
{
    private readonly Tvalue? _value;
    protected internal Result(Tvalue? value,bool isSuccess, Error error):base(isSuccess,error)
    {
        _value = value;
    }

    /*
     ! is the null-forgiving operator (!), which tells the compiler:
                                "Trust me, _value is not null here."
     */
    [NotNull]
    public Tvalue Value => IsSuccess ?
        _value! : throw new InvalidOperationException("the value of the failure result can not be accessed");


    /*
     🔹 Without Implicit Conversion (Explicit Call)
             Result<int> result1 = Result<int>.Create(100);  // ✅ Works, but explicit
     🔹 With Implicit Conversion (More Convenient)
            Result<int> result2 = 100;  // ✅ Automatically converted to Result<int>
     */
    public static implicit operator Result<Tvalue>(Tvalue? value) => Create(value);


}